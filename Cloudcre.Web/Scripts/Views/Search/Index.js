// namespace
window.cloudcre = window.cloudcre || {};

// routing
$.extend(window.cloudcre, {
    routing: {
        url: {},
        urls: function (a) {
            $.extend(window.cloudcre.routing.url, a);
        }
    }
});

// view model and search
$.extend(window.cloudcre, {
    locations: {},
    viewModel: (function () {
        // multiple queues for each property-type
        var _queued = {
            MultipleFamily: ko.observableArray([]),
            Office: ko.observableArray([]),
            Retail: ko.observableArray([]),
            Industrial: ko.observableArray([]),
            IndustrialCondominium: ko.observableArray([]),
            CommercialCondominium: ko.observableArray([]),
            CommercialLand: ko.observableArray([]),
            IndustrialLand: ko.observableArray([]),
            ResidentialLand: ko.observableArray([])
        },
        // select list option values
            propertyTypeOptionValues = ko.observableArray(function () {
                var values = [];
                $.each($("li", ".property-list"), function (idx, obj) {
                    values.push($(obj).text());
                });
                return values;
            } ()),
        // current property-type that has been selected
            propertyTypeSelected = ko.observable(),
        // property records from most recent search request
            propertySearchResults = ko.observable(),
        // property records that have been queued for current selected property-type
            queuedProperties = function () {
                return _queued[this.propertyTypeSelected().split(' ').join('')];
            },
        // removes property if in queue, otherwise adds property to queue
            addOrRemovePropertyFromQueue = function (sProp) {
                var properties = this.queuedProperties();
                // array of removed values
                var removed = properties.remove(function (qProp) {
                    return sProp.Id == qProp.Id;
                });

                // if no removed values
                if (!(removed.length > 0))
                    properties.push(sProp);
            },
        // is the property currently queued?
            isQueued = function (sProp) {
                // truthy or falsy
                return ko.utils.arrayFirst(this.queuedProperties()(), function (qProp) {
                    return sProp.Id == qProp.Id;
                });
            },
        // edit a property
            editProperty = function (sProp) {

                var id = sProp.Id;
                var name = sProp.Name;
                var type = sProp.PropertyTypeDescription.split(' ').join('');
                window.open(window.cloudcre.routing.url[type].edit + "/" + id + "/" + convertToSlug(name));
            },
        // remove a property from the system
            deleteProperty = function (sProp) {
                var id = sProp.Id;
                var name = sProp.Name;
                var type = sProp.PropertyTypeDescription.split(' ').join('');

                $("#delete-dialog-msg").text("Are you sure you want to delete the property, \"" + name + "\" ?");
                $("#delete-dialog").dialog("option", "buttons", {
                    Ok: function () {
                        DisableButton('Ok');
                        DisableButton('Cancel');
                        var data = { Id: id };
                        $.post(window.cloudcre.routing.url[type].remove, data || {}, function () {
                            $("#delete-dialog-msg").text("Property, \"" + name + "\", successfully removed");
                        });
                        fireDisplay();
                        var $that = $(this);
                        window.setTimeout(function () {
                            $that.dialog("close");
                        }, 3000);
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                });
                $("#delete-dialog").dialog("open");
            },
            summaryReport = function () {
                // get iframe with form for post that generates a report
                var frmWindow = getIFrameWindow();
                // reset form input values
                var fileName = $(frmWindow.document.getElementById("fileForm"));
                fileName.html("");

                // get queued property ids and create input values for form
                var type = this.propertyTypeSelected().split(' ').join(''),
                    qProps = this.queuedProperties()(),
                    inputs = '';

                fileName.attr("action", window.cloudcre.routing.url[type].summary);
                $.each(qProps, function (idx, prop) {
                    inputs += '<input type="text" name="ids" value = "' + prop.Id + '" />';
                });
                fileName.append(inputs);

                // submit form to server
                var frm = frmWindow.document.getElementById("fileForm");
                frm.submit();
            },
        // TODO: remove this function after report available for all properties
            tempCond = function () {
                var prop = this.propertyTypeSelected().split(' ').join('').toLowerCase();
                if (prop == "office" || prop == "retail" || prop == "multiplefamily")
                    return this.queuedProperties()().length > 0;

                return false;
            };

        // send search request when the selected property-type is changed
        propertyTypeSelected.subscribe(function (type) {
            fireDisplay();
        });

        // when search results are updated, the cooresponding properties 
        // in the queue may have stale data and must also updated
        propertySearchResults.subscribe(function (results) {
            var array = [];
            // save queued property ids and order to use during queue rebuilding
            var qArrayObs = _queued[window.cloudcre.viewModel.propertyTypeSelected().split(' ').join('')];
            var qArray = qArrayObs();
            for (i = 0; i < qArray.length; i++) {
                array[i] = qArray[i].Id;
            }

            // rebuild queue from fresh search results in case property data has staled
            qArrayObs([]);
            for (i = 0; i < array.length; i++) {
                $.each(results.Properties, function (idx, property) {
                    if (property.Id == array[i]) {
                        qArrayObs.push(property);
                    }
                });
            }
        });

        return {
            propertySearchResults: propertySearchResults,
            propertyTypeOptionValues: propertyTypeOptionValues,
            propertyTypeSelected: propertyTypeSelected,
            queuedProperties: queuedProperties,
            addOrRemovePropertyFromQueue: addOrRemovePropertyFromQueue,
            isQueued: isQueued,
            editProperty: editProperty,
            deleteProperty: deleteProperty,
            summaryReport: summaryReport,
            tempCond: tempCond,
            selecteMarkUp: "<i class='icon-plus icon-white'></i> Select",
            removeMarkUp: "<i class='icon-plus icon-white'></i> Remove"
        };
    })()
});

var getIFrameWindow = function() {
    var ifr = document.getElementById("fileIframe");
    if (!ifr) {
        createFrame();
    }
    var wnd = window.frames["fileIframe"];
    return wnd;
};
var createFrame = function() {
    var frame = document.createElement("iframe");
    frame.name = "fileIframe";
    frame.id = "fileIframe";

    document.body.appendChild(frame);
    generateIFrameContent();
    frame.style.width = "0px";
    frame.style.height = "0px";
    frame.style.border = "0px";
};
var generateIFrameContent = function() {
    var frameWindow = window.frames["fileIframe"];
    var content = "<form id='fileForm' method='post' enctype='application/data' action=''></form>";
    frameWindow.document.open();
    frameWindow.document.write(content);
    frameWindow.document.close();
};

// total results found
window.cloudcre.viewModel.numberOfPropertyResults = ko.dependentObservable({
    read: function () {
        if (this.propertySearchResults() != undefined) {
            var count = this.propertySearchResults().Properties.length;
            return count + (count != 1 ? " results found" : " result found");
        }
        return "";
    },
    owner: window.cloudcre.viewModel,
    //TODO: defer doesnt seem to work
    deferEvaluation: true  //do not evaluate immediately when created
});

$(function () {
    ko.applyBindings(window.cloudcre.viewModel);
});


// create location visual search widget
$(function () {
    $.post(window.cloudcre.routing.url.locations, {}, function (data) {
        window.cloudcre.locations.city = [];
        window.cloudcre.locations.county = [];
        window.cloudcre.locations.zip = [];
        $(data).each(function (idx, obj) {
            switch (obj.category) {
                case 'city':
                    window.cloudcre.locations.city.push(obj.label);
                    break;
                case 'county':
                    window.cloudcre.locations.county.push(obj.label);
                    break;
                case 'zip':
                    window.cloudcre.locations.zip.push(obj.label);
                    break;
            }
        });
    });

    window.visualSearch = VS.init({
        container: $('#search_box_container'),
        query: '',
        unquotable: [],
        callbacks: {
            search: function (query, searchCollection) {
                fireDisplay();
            },
            valueMatches: function (category, searchTerm, callback) {
                switch (category) {
                    case 'city':
                        callback(window.cloudcre.locations.city);
                        break;
                    case 'county':
                        callback(window.cloudcre.locations.county);
                        break;
                    case 'zip':
                        callback(window.cloudcre.locations.zip);
                        break;
                }
            },
            facetMatches: function (callback) {
                callback([
                { label: 'city', category: 'location' },
                { label: 'county', category: 'location' },
                { label: 'zip', category: 'location' },
              ]);
            }
        }
    });
});



var disallowUpdates = false;
var evtDragend;
var evtZoomChanged;
var evtIdle;

$(function () {
   
    // hookup all events that fire off a search request
    var to = null;

    var dates = $('#maxdate-box, #mindate-box').datepicker({
        changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        showAnim: "show",
        onSelect: function (selectedDate) {
            var option = this.id == "mindate-box" ? "minDate" : "maxDate",
					    instance = $(this).data("datepicker"),
					    date = $.datepicker.parseDate(
						    instance.settings.dateFormat ||
						    $.datepicker._defaults.dateFormat,
						    selectedDate, instance.settings);

            dates.not(this).datepicker("option", option, date);

            if ($.whitney.Property.managers.saleDateRange.IsValid()) {
                fireDisplay(to);
            }
        }
    });

    $('#maxdate-box').keyup(function () {
        if ($.whitney.Property.managers.saleDateRange.IsValid()) {
            fireDisplay(to);
            $('#maxdate-box, #mindate-box').datepicker("refresh");
        }
    });

    $('#mindate-box').keyup(function () {
        if ($.whitney.Property.managers.saleDateRange.IsValid()) {
            fireDisplay(to);
            $('#maxdate-box, #mindate-box').datepicker("refresh");
        }
    });

    $('#keywords-search-box').keyup(function () {
        fireDisplay(to);
    });

    $('#sqftmin-box').keyup(function () {
        $("#slider-range").slider("values", 0, $.whitney.Property.managers.siteSqFtSlider.SqftMin());
        fireDisplay(to);
    });

    $('#sqftmax-box').keyup(function () {
        $("#slider-range").slider("values", 1, $.whitney.Property.managers.siteSqFtSlider.SqftMax());
        fireDisplay(to);
    });


    $('#mapsearch').click(function () {
        fireDisplay(to);
    });

    $('.search-form').submit(function () {
        displayPage();
        return false;
    });
    
    // building area range slider
    $("#slider-range").slider({
        range: true,
        min: $.whitney.Property.managers.siteSqFtSlider.SliderSqftFloor(),
        max: $.whitney.Property.managers.siteSqFtSlider.SliderSqftCeiling(),
        values: [$.whitney.Property.managers.siteSqFtSlider.SqftMin(), $.whitney.Property.managers.siteSqFtSlider.SqftMax()],
        step: 100,
        slide: function (event, ui) {
            var min = $.formatNumber(ui.values[0], {
                format: "0",
                locale: "us"
            });
            var max = $.formatNumber(ui.values[1], {
                format: "0",
                locale: "us"
            });
            $.whitney.Property.managers.siteSqFtSlider.SqftMin(min);
            $.whitney.Property.managers.siteSqFtSlider.SqftMax(max);
            var to = null;
            fireDisplay(to);
        }
    });

   
    $("#dialog").dialog({
        autoOpen: false,
        height: $(window).height() * 0.8,
        width: $(window).width() * 0.8,
        resizable: false,
        draggable: false,
        modal: true,
        close: function (event, ui) {
            window.dialogViewModel = {};
            $("#dialog-form").clearForm();
            $("#dialog-form").html("");
        }
    });
    
    $("#delete-dialog").dialog({
        autoOpen: false,
        height: $(window).height() * 0.3,
        width: $(window).width() * 0.3,
        resizable: false,
        draggable: true,
        modal: true
    });

    // menu for create property
    // BUTTONS
    $('.fg-button').hover(
    		function () { $(this).removeClass('ui-state-default').addClass('ui-state-focus'); },
    		function () { $(this).removeClass('ui-state-focus').addClass('ui-state-default'); }
    	);

    // MENUS    	
    $('#flat').fgmenu({
        content: $('#flat').next().html(), // grab content from this page
        showSpeed: 400
    });

});


// dialog helpers
function DisableButton(button) {
    if ($.isArray(button)) {
        $.each(button, function (idx, item) {
            $(".ui-dialog-buttonpane button:contains('" + item + "')").attr("disabled", true).addClass("ui-state-disabled");
        });
    }
    if (Object.prototype.toString.call(button) == '[object String]') {
        $(".ui-dialog-buttonpane button:contains('" + button + "')").attr("disabled", true).addClass("ui-state-disabled");
    }
}

function EnableButton(button) {
    if ($.isArray(button)) {
        $.each(button, function (idx, item) {
            $(".ui-dialog-buttonpane button:contains('" + item + "')").attr("disabled", false).removeClass("ui-state-disabled");
        });
    }
    if (Object.prototype.toString.call(button) == '[object String]') {
        $(".ui-dialog-buttonpane button:contains('" + button + "')").attr("disabled", false).removeClass("ui-state-disabled");
    }
}

// location autocomplete combobox widget
function split(val) {
    return val.split(/,\s*/);
}
function extractLast(term) {
    return split(term).pop();
}

function ToJsDate(initialValue) {
    function regExDate(str) {

        function formatDate(d) {
            var currDate = d.getDate();
            var currMonth = d.getMonth() + 1; //months are zero based
            var currYear = d.getFullYear();
            return (currMonth + "/" + currDate + "/" + currYear);
        }

        //        var epoch = (new RegExp('/Date\\((-?[0-9]+)\\)/')).exec(str);
        //        return new Date(parseInt(epoch[1])).toDateString();

        if (!str)
            return "";

        if (str.substring(0, 6) == "/Date(") {  // MS Ajax date: /Date(19834141)/       
            str = str.match(/Date\((.*?)\)/)[1];
            return formatDate(new Date(parseInt(str)));
        }

        return str;
        //    else { // ISO Date 2007-12-31T23:59:59Z                                     
        //        var matches = str.split( /[-,:,T,Z]/);        
        //        matches[1] = (parseInt(matches[1],0)-1).toString();
        //        return formatDate(new Date(Date.UTC(matches.join(","))));
        //    }
    }

    return regExDate(initialValue);
};

// model helpers
$.extend($.whitney, {
    Property: {
        url: {},
        urls: function (a) {
            $.extend($.whitney.Property.url, a);
        },
        managers: {},
        clearResults: function (callback) {
            $("#map-side-bar").empty();
            if (callback != undefined) {
                callback();
            }
        },
        search: function (searchCriteria) {
            if (disallowUpdates == false) {
                disallowUpdates = true;
                $.ajax({
                    url: window.cloudcre.routing.url[window.cloudcre.viewModel.propertyTypeSelected().split(' ').join('')].search,
                    type: 'POST',
                    dataType: 'json',
                    data: searchCriteria,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        $.whitney.Property.clearResults();

                        // bind template with resulting property records
                        //$("#productItemTemplate").tmpl(data).appendTo($("#map-side-bar"));

                        // bind view model for queue
                        //ko.applyBindings(window.cloudcre.viewModel, $("#map-side-bar")[0]);
                        $.each(data.Properties, function (idx, obj) {
                            obj.Price = '$' + (obj.Price.toFixed(2)).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            obj.SaleDate = ToJsDate(obj.SaleDate);
                        });
                        window.cloudcre.viewModel.propertySearchResults(data);
                        //ko.applyBindings(window.cloudcre.viewModel);

                        // create map if first time
                        if (!$('#map').data('jMapping')) {
                            $('#map').jMapping({
                                category_icon_options: {
                                    'apartment': { color: '#E8413A' },
                                    'bar': { color: '#465AE0' },
                                    'other': { color: '#7CDF65' }
                                },
                                metadata_options: { type: 'html5' },
                                map_config: {
                                    navigationControlOptions: {
                                        style: google.maps.NavigationControlStyle.DEFAULT
                                    },
                                    scrollwheel: false,
                                    mapTypeId: google.maps.MapTypeId.HYBRID,
                                    zoom: 7
                                },
                                auto_fit_bounds: true
                            });

                            $(document).bind('beforeUpdate.jMapping', function (map) {
                                if (!!evtIdle)
                                    google.maps.event.removeListener(evtIdle);
                                if (!!evtDragend)
                                    google.maps.event.removeListener(evtDragend);
                                if (!!evtZoomChanged)
                                    google.maps.event.removeListener(evtZoomChanged);
                                if (!!evtDragend)
                                    google.maps.event.removeListener(evtDragend);
                                if (!!evtIdle)
                                    google.maps.event.removeListener(evtIdle);
                                evtIdle = null;
                                evtDragend = null;
                                evtZoomChanged = null;
                            });

                            $(document).bind('afterUpdate.jMapping', function (map) {
                                if ($('#mapsearch').is(':checked')) {
                                    evtDragend = google.maps.event.addListener($('#map').data('jMapping').map, 'dragend', function (evt) {
                                        fireDisplay();
                                    });

                                    evtZoomChanged = google.maps.event.addListener($('#map').data('jMapping').map, 'zoom_changed', function (evt) {
                                        fireDisplay();
                                    });
                                }
                            });
                        }
                        else {
                            $('#map').data('jMapping').settings.auto_fit_bounds = ($('#mapsearch').is(':checked') ? false : true);
                            $('#map').jMapping('update');
                        }

                        // TODO: permanently remove paging?
                        //                        $("#pageLinksTop").html($.whitney.Property.buildPageLinksFor(data.CurrentPage, data.TotalNumberOfPages, data.NumberOfTitlesFound));
                        //                        $("#pageLinksBottom").html($.whitney.Property.buildPageLinksFor(data.CurrentPage, data.TotalNumberOfPages, data.NumberOfTitlesFound));

                        disallowUpdates = false;

                    },
                    error: function () {
                        disallowUpdates = false;
                    }
                });
            }
        },
        buildPageLinksFor: function (index, totalPages, totalProperties) {
            var html = '';
            for (var i = 1; i <= totalPages; i++) {

                if (i == index)
                    html = html + "<a class='selected' href='JavaScript:displayPage(" + i + ")'>" + i + "</a>&nbsp;";
                else
                    html = html + "<a class='notselected' href='JavaScript:displayPage(" + i + ")'>" + i + "</a>&nbsp;";
            }

            return html + "of " + totalPages + " page(s), with " + totalProperties + " property record(s) in total";
        }
    }
});

// date range manager
$.extend($.whitney.Property.managers, {
    saleDateRange: {
        MaximumDateFilter: function (val) {
            if (val != undefined) {
                $('#maxdate-box').val(val);
            }
            return $('#maxdate-box').val();
        },
        MinimumDateFilter: function (val) {
            if (val != undefined) {
                $('#mindate-box').val(val);
            }
            return $('#mindate-box').val();
        },
        ParseDate: function (date) {
            var instance = $('#maxdate-box, #mindate-box').data("datepicker");
            var newdate = $.datepicker.parseDate(
					instance.settings.dateFormat ||
					$.datepicker._defaults.dateFormat,
					date, instance.settings);

            if (newdate instanceof Date)
                return newdate;
        },
        IsValid: function () {
            if (!!this.MinimumDateFilter() && !!this.MaximumDateFilter()) {
                var mindate = this.ParseDate(this.MinimumDateFilter());
                var maxdate = this.ParseDate(this.MaximumDateFilter());

                if ((mindate instanceof Date) && (maxdate instanceof Date)) {
                    if (mindate <= maxdate) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
});


// building area slider manager
$.extend($.whitney.Property.managers, {
    siteSqFtSlider: {
        option: {},
        options: function (a) {
            $.extend($.whitney.Property.managers.siteSqFtSlider.option, a);
        },
        SqftMin: function (val) {
            if (val != undefined) {
                $('#sqftmin-box').val(val);
            }
            var sqftMin = $('#sqftmin-box').val();
            return (sqftMin === '') ? null : $.parseNumber(sqftMin, { format: "#,###.00", locale: "us" });
        },
        SqftMax: function (val) {
            if (val != undefined) {
                $('#sqftmax-box').val(val);
            }
            var sqftMax = $('#sqftmax-box').val();
            return (sqftMax === '') ? null : $.parseNumber(sqftMax, { format: "#,###.00", locale: "us" });
        },
        SqftCeiling: function () {
            return this.option.sqftCeiling;
        },
        SqftFloor: function () {
            return this.option.sqftFloor;
        },
        RangeBuffer: function () {
            return this.SqftFloor();
        },
        SliderSqftCeiling: function () {
            return parseFloat(this.SqftCeiling()) + this.RangeBuffer();
        },
        SliderSqftFloor: function () {
            return 0;
        }
    }    
});


       
// Method gives keystroke timed delay before firing page refresh
function fireDisplay(to) {

    if (to != null) {
        clearTimeout(to);
        to = null;
    }
    to = setTimeout(function() {
            displayPage();
            to = null;
            setTimeout(function() {
                displayPage();
            }, 600);
        }, 200);
}

function convertToSlug(text) {
    return text
    .toLowerCase()
    .replace(/[^\w ]+/g, '')
    .replace(/ +/g, '-');
}
        
// Method called to determine the sort ordering and the current category.
// ===============================================================
function displayPage(i) {
    var index = (i != undefined) ? i : 1;
            
    if (disallowUpdates == false) {
        //var sortBy = $('#ddlSortBy').val();              
                
        var searchCriteria =
        {   Index: index,
            Query: $('#keywords-search-box').val(),
            SortBy: 1,
            PropertyTypeFilter: 1,
            SqftMaxFilter: $.whitney.Property.managers.siteSqFtSlider.SqftMax(),
            SqftMinFilter: $.whitney.Property.managers.siteSqFtSlider.SqftMin(),
            MapBoundary: null
        };

        // Add Location Query
        if (visualSearch.searchQuery.length > 0) {
            var locationCriteria =
                {
                    Location: []
                };
                
            var cities = visualSearch.searchQuery.values('city');
            if (cities.length > 0) {
                $.each(cities, function (idx, obj) {
                    locationCriteria.Location.push({ Category: "city", Query: obj });
                });
            }
            
            var counties = visualSearch.searchQuery.values('county');
            if (counties.length > 0) {
                $.each(counties, function (idx, obj) {
                    locationCriteria.Location.push({ Category: "county", Query: obj });
                });
            }
            
            var zips = visualSearch.searchQuery.values('zip');
            if (zips.length > 0) {
                $.each(zips, function(idx, obj) {
                    locationCriteria.Location.push({ Category: "zip", Query: obj });
                });
            }
            
            $.extend(true, searchCriteria, locationCriteria);
        }
        
        // Add map boundaries to query if enabled.
        if($('#mapsearch').is(':checked'))
        {
            var bounds = $('#map').data('jMapping').map.getBounds();
            var ne = bounds.getNorthEast();
            var sw = bounds.getSouthWest();                
                
            var mappingSearchCriteria =
            {
                MapBoundary: { 
                    NorthEast: { Latitude: ne.lat(), Longitude: ne.lng() },
                    SouthWest: { Latitude: sw.lat(), Longitude: sw.lng() }
                }
            };
                
            $.extend(true, searchCriteria, mappingSearchCriteria);
        }
                
        // Add date range to query if exists.
        if($.whitney.Property.managers.saleDateRange.IsValid())
        {
            var saleDateRangeCriteria =
            {                        
                MaximumDateFilter: $.whitney.Property.managers.saleDateRange.ParseDate($.whitney.Property.managers.saleDateRange.MaximumDateFilter()).toMSJSON(),
                MinimumDateFilter: $.whitney.Property.managers.saleDateRange.ParseDate($.whitney.Property.managers.saleDateRange.MinimumDateFilter()).toMSJSON()
            };
                
            $.extend(true, searchCriteria, saleDateRangeCriteria);
        }
                
        $.whitney.Property.search(JSON.stringify(searchCriteria));
    }
}
               
Date.prototype.toMSJSON = function () {
    var date = '/Date(' + this.getTime() + ')/';
    return date;
};
            
function regExDate(str) 
{
//        var epoch = (new RegExp('/Date\\((-?[0-9]+)\\)/')).exec(str);
    //        return new Date(parseInt(epoch[1])).toDateString();

    if (!str)
        return "";
           
    if (str.substring(0,6) == "/Date(") {  // MS Ajax date: /Date(19834141)/       
        str = str.match(/Date\((.*?)\)/)[1];                        
        return formatDate(new Date(parseInt(str)));
    }

    return str;
//    else { // ISO Date 2007-12-31T23:59:59Z                                     
//        var matches = str.split( /[-,:,T,Z]/);        
//        matches[1] = (parseInt(matches[1],0)-1).toString();
//        return formatDate(new Date(Date.UTC(matches.join(","))));
//    }
}
        
function formatDate(d)
{
    var currDate = d.getDate();
    var currMonth = d.getMonth() + 1; //months are zero based
    var currYear = d.getFullYear();
    return (currMonth + "/" + currDate + "/" + currYear);
}
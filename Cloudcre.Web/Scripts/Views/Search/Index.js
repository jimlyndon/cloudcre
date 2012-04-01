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

            if ($.whitney.property.managers.saleDateRange.IsValid()) {
                fireDisplay(to);
            }
        }
    });

    $('#maxdate-box').keyup(function () {
        if ($.whitney.property.managers.saleDateRange.IsValid()) {
            fireDisplay(to);
            $('#maxdate-box, #mindate-box').datepicker("refresh");
        }
    });

    $('#mindate-box').keyup(function () {
        if ($.whitney.property.managers.saleDateRange.IsValid()) {
            fireDisplay(to);
            $('#maxdate-box, #mindate-box').datepicker("refresh");
        }
    });

    $('#keywords-search-box').keyup(function () {
        fireDisplay(to);
    });

    $('#sqftmin-box').keyup(function () {
        $("#slider-range").slider("values", 0, $.whitney.property.managers.siteSqFtSlider.SqftMin());
        fireDisplay(to);
    });

    $('#sqftmax-box').keyup(function () {
        $("#slider-range").slider("values", 1, $.whitney.property.managers.siteSqFtSlider.SqftMax());
        fireDisplay(to);
    });

    $('#PropertyType').change(function () {
        fireDisplay(to);
    });

    $('#mapsearch').click(function () {
        fireDisplay(to);
    });

    $('#search-form-top').submit(function () {
        displayPage();
        return false;
    });

    $('#top-search-queue').click(function () {
        var queued =
        {
            QueuedItems: viewModel.queued()
        };
        $.cookie('ws', JSON.stringify(queued));
    });

    $('#top-search-search').click(function () {
        var queued =
        {
            QueuedItems: viewModel.queued()
        };
        $.cookie('ws', JSON.stringify(queued));
    });

    
    // building area range slider
    $("#slider-range").slider({
        range: true,
        min: $.whitney.property.managers.siteSqFtSlider.SliderSqftFloor(),
        max: $.whitney.property.managers.siteSqFtSlider.SliderSqftCeiling(),
        values: [$.whitney.property.managers.siteSqFtSlider.SqftMin(), $.whitney.property.managers.siteSqFtSlider.SqftMax()],
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
            $.whitney.property.managers.siteSqFtSlider.SqftMin(min);
            $.whitney.property.managers.siteSqFtSlider.SqftMax(max);
            var to = null;
            fireDisplay(to);
        }
    });

    ko.applyBindings(viewModel, $("#queued")[0]);
    viewModel.updateSearchResults();


//    // Encapsulates data calls to server (AJAX calls)
//    window.TemplateDataService = new function () {
//        var serviceBase = $.whitney.property.url.wizardBase, // "/ApartmentWizard/",
//            bind = function (json, templateName) {
//                var name = templateName || "StepOne";

//                $.get(serviceBase + "/" + name, function (templates) {
//                    $("#propertyWizardTemplateContainer", "body").remove();
//                    $("#personList", "body").html("");
//                    $("#personTemplate", "body").remove();

//                    var mapping = {
//                        'SaleDate': {
//                            create: function (options) {
//                                return new ko.jsonDateObservable(options.data);
//                            }
//                        }
////                        'Sale': {
////                            create: function (options) {
////                                //ko.mapping.fromJS(options.data, {}, this);
////                                return ko.mapping.fromJS(options.data, {
////                                    'Date': {
////                                        create: function (options) {
////                                            return new ko.jsonDateObservable(options.data);
////                                        }
////                                    }
////                                }, this);
////                            }
////                        }
//                    };

//                    window.dialogViewModel = ko.mapping.fromJS(json, mapping);

//                    // executed after every template refresh, after any observable change
//                    window.dialogViewModel.loaded = function () { };

//                    $("body").append(templates);

//                    ko.applyBindings(window.dialogViewModel, $("#dialog")[0]);

//                    // executed after wizard step loaded
//                    window.TemplateDataService.loaded();
//                    window.TemplateDataService.loaded = function() { };
//                });
//            },

//            getProperty = function (data, action, callback) {
//                var route = action || "StepOne" + "?cancelbutton=cancel";

//                $.ajax({
//                    url: serviceBase + "/" + route,
//                    type: 'POST',
//                    dataType: 'json',
//                    data: data || {},
//                    contentType: 'application/json; charset=utf-8',
//                    success: function (json) {
//                        if (!!callback) callback(json, action);
//                        else bind(json, action);
//                    },
//                    error: function (a, b, c) {
//                        disallowUpdates = false;
//                    }
//                });

////                $.post(serviceBase + "/" + route, data || {}, function (json) {
////                    if (!!callback) callback(json, action);
////                    else bind(json, action);
////                });
//            },

//            loaded = function () { };

//        return {
//            getProperty: getProperty,
//            bind: bind,
//            loaded: loaded
//        };
//    } ();

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

// The view model is an abstract description of the state of the UI, but without any knowledge of the UI technology (HTML)
var cookieQueued = jQuery.parseJSON($.cookie('ws')) ? jQuery.parseJSON($.cookie('ws')).QueuedItems : [];
var viewModel = {
    queued: ko.observableArray(cookieQueued),
    remove: function (item) {
        this.queued.remove(item);
        $('.select-button').each(function () {
            var $this = $(this),
                id = $this.data("id");
            if (id === item.Id) {
                $this.removeClass("ui-selected");
                $this.text("Add to queue");
            }
        });
    },
    updateSearchResults: function () {
        $("button.select-button").click(function (e, ui) {
            var $this = $(this),
                array = viewModel.queued(),
                id = $this.data("id"),
                name = $this.data("name"),
                parcelid = $this.data("parcelid");

            // unselect
            if ($this.hasClass("ui-selected")) {
                $this.removeClass("ui-selected");
                $(array).each(function (index, item) {
                    if (item.Id === id) {
                        array.splice(index, 1);
                        $this.text("Add to Queue");
                    }
                });
                viewModel.queued.remove({ "Id": id, "Name": name, "ParcelId": parcelid });
            }
            // select
            else {
                var add = true;
                $(array).each(function (index, item) {
                    if (item.Id === id) {
                        add = false;
                    }
                });
                if (add) {
                    $this.addClass("ui-selected");
                    viewModel.queued.push({ "Id": id, "Name": name, "ParcelId": parcelid });
                    $this.text("Remove from Queue");
                }
            }
        });

        // set inital state for each property based on what's in the queue
        $('.select-button').each(function () {
            var $this = $(this),
                id = $this.data("id"),
                array = viewModel.queued();
            $(array).each(function (index, item) {
                if (item.Id === id) {
                    $this.addClass("ui-selected");
                    $this.text("Remove From queue");
                }
            });
        });

        $("input.edit-button").click(function (e) {
            e.preventDefault();
            //$("#dialog").dialog("open");
            var id = $(this).data("id");
            var name = $(this).data("name");
            var type = $(this).data("type");
            window.open(window.cloudcre.routing.url[type].edit + "/" + id + "/" + convertToSlug(name));
            //window.TemplateDataService.getProperty(null, "StepOne" + "?id=" + id);
        });

        $("input.delete-button").click(function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            var name = $(this).data("name");
            var type = $(this).data("type");
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
        });
    }
};

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

// declare app/router
window.cloudcre = {};

$.extend(window.cloudcre, {
    routing: {
        url: {},
        urls: function (a) {
            $.extend(window.cloudcre.routing.url, a);
        }
    },
    locations: {},
    viewModel: {}
});

// model helpers
$.extend($.whitney, {
    property: {
        url: {},
        urls: function (a) {
            $.extend($.whitney.property.url, a);
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
                    url: window.cloudcre.routing.url[$("#PropertyType option:selected").text().split(' ').join('')].search,
                    type: 'POST',
                    dataType: 'json',
                    data: searchCriteria,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        $.whitney.property.clearResults();
                        $("#productItemTemplate").tmpl(data).appendTo($("#map-side-bar"));

                        // create map if first time
                        if (!$('#map').data('jMapping'))
                        {
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
                        
                        $("#pageLinksTop").html($.whitney.property.buildPageLinksFor(data.CurrentPage, data.TotalNumberOfPages, data.NumberOfTitlesFound));
                        $("#pageLinksBottom").html($.whitney.property.buildPageLinksFor(data.CurrentPage, data.TotalNumberOfPages, data.NumberOfTitlesFound));

                        // foreach map-location parcelid that maps to view model add .ui-selected
                        viewModel.updateSearchResults();

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
$.extend($.whitney.property.managers, {
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
$.extend($.whitney.property.managers, {
    siteSqFtSlider: {
        option: {},
        options: function (a) {
            $.extend($.whitney.property.managers.siteSqFtSlider.option, a);
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
            SqftMaxFilter: $.whitney.property.managers.siteSqFtSlider.SqftMax(),
            SqftMinFilter: $.whitney.property.managers.siteSqFtSlider.SqftMin(),
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
        if($.whitney.property.managers.saleDateRange.IsValid())
        {
            var saleDateRangeCriteria =
            {                        
                MaximumDateFilter: $.whitney.property.managers.saleDateRange.ParseDate($.whitney.property.managers.saleDateRange.MaximumDateFilter()).toMSJSON(),
                MinimumDateFilter: $.whitney.property.managers.saleDateRange.ParseDate($.whitney.property.managers.saleDateRange.MinimumDateFilter()).toMSJSON()
            };
                
            $.extend(true, searchCriteria, saleDateRangeCriteria);
        }
                
        $.whitney.property.search(JSON.stringify(searchCriteria));
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


$(function () {
    // init map and recrods on pageloaded
    displayPage();

});
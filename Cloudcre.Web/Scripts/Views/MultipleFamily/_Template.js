(function ($, window) {

    // declare app/router
    window.cloudcre = {};

    $.extend(cloudcre, {
        routing: {
            url: {},
            urls: function (a) {
                $.extend(cloudcre.routing.url, a);
            }
        },
        viewModel: {}
    });

    // custom knockout binding handlers
    ko.bindingHandlers.valueWithNullsAsEmpty = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor();
            if (value() == null) {
                value("");
            }
            ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor);
        },
        update: function (element, valueAccessor) {
            ko.bindingHandlers.value.update(element, valueAccessor);
        }
    };

    ko.bindingHandlers.valueWithInitializationFromDOMUnlessEmpty = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor();
            if (ko.isObservable(value) && element.value !== "") {
                value(element.value);
            }
            ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor);
        },
        update: function (element, valueAccessor) {
            ko.bindingHandlers.value.update(element, valueAccessor);
        }
    };

    ko.bindingHandlers.valueWithInitializationFromDOM = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                value(element.value);
            }
            ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor);
        },
        update: function (element, valueAccessor) {
            ko.bindingHandlers.value.update(element, valueAccessor);
        }
    };

    ko.JsonDateObservable = function (initialValue) {
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

        return ko.observable(regExDate(initialValue));
    };

    // gmap api v3 extensions
    window.google.maps.Map.prototype.markers = [];

    window.google.maps.Map.prototype.addMarker = function (marker) {
        this.markers[this.markers.length] = marker;
    };

    window.google.maps.Map.prototype.getMarkers = function () {
        return this.markers;
    };

    window.google.maps.Map.prototype.clearMarkers = function () {
        for (var i = 0; i < this.markers.length; i++) {
            this.markers[i].setMap(null);
        }
        this.markers = [];
    };


    // On DOM load
    $(function () {

        // initialize submission success/fail dialog modal
        $("#submit-dialog").dialog({
            autoOpen: false,
            height: $(window).height() * 0.3,
            width: $(window).width() * 0.3,
            resizable: false,
            draggable: true,
            modal: true
        });

        // view model/binding main module
        cloudcre.App = (function () {
            var module = { };
            module.createMarker = function (latitude, longitude) {
                var vm = cloudcre.viewModel;

                // set map marker
                var location = new window.google.maps.LatLng(latitude, longitude);
                var map = $("#location-map-canvas").data('map');
                if (map) {
                    map.clearMarkers();
                    var marker = new window.google.maps.Marker({
                        position: location,
                        map: map
                    });

                    map.addMarker(marker);

                    if (vm.centerOnMap) {
                        map.setCenter(location);
                    }
                    vm.centerOnMap = true;
                }
            },
            module.setAddressModelLngLat = function (location, centerOnMap) {
                var vm = cloudcre.viewModel;
                vm.centerOnMap = centerOnMap;
                vm.Latitude(location.lat());
                vm.centerOnMap = centerOnMap;
                vm.Longitude(location.lng());
            },
            module.createMap = function () {
                var vm = cloudcre.viewModel;
                if (!($("#location-map-canvas").data('map'))) {
                    if (vm.Longitude() && vm.Latitude()) {
                        var $map = $("#location-map-canvas");

                        if ($map.length > 0) {
                            var myOptions = {
                                zoom: 18,
                                center: new window.google.maps.LatLng(vm.Latitude(), vm.Longitude()),
                                mapTypeId: window.google.maps.MapTypeId.HYBRID
                            },
                                mapCanvas = new window.google.maps.Map(document.getElementById("location-map-canvas"), myOptions);

                            $map.data('map', mapCanvas);

                            window.google.maps.event.addListener(mapCanvas, 'click', function (event) {
                                module.setAddressModelLngLat(event.latLng, false);
                                $("#reset-location").show();
                            });
                        }
                        module.createMarker(vm.Latitude(), vm.Longitude());
                    }
                }

                $("#reset-location").button().click(function (e) {
                    e.preventDefault();
                    // trigger flips a flag that causes dependentObservables to fire
                    cloudcre.viewModel.trigger(cloudcre.viewModel.trigger() * -1);
                    $(this).hide();
                });
            },
            module.init = function () {
                $.ajax({
                    url: cloudcre.routing.url.model,
                    type: 'GET',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (json) {
                        module.binding(json);
                        if (typeof window.cloudcre.viewModelOnBinding == 'function') {
                            window.cloudcre.viewModelOnBinding();
                        }
                        module.bind();
                        if (typeof window.cloudcre.viewModelOnBind == 'function') {
                            window.cloudcre.viewModelOnBind();
                        }
                    }
                });
            },
            module.bind = function () {
                ko.applyBindings(cloudcre.viewModel, $("#bindingContainer")[0]);

                // hide map button initially
                $("#reset-location").hide();

                // initialize date fields
                $('.date-pick').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    showAnim: "show"
                });
            },
            module.binding = function (json) {

                function formatToTwoDecimals(value) {
                    return Math.round(value * 100) / 100;
                };

                function formatToOneDecimal(value) {
                    return Math.round(value * 10) / 10;
                };

                // knockout model custom mappings
                var mapping = {
                    'ListingDate': {
                        create: function (options) {
                            return new ko.JsonDateObservable(options.data);
                        }
                    },
                    'SaleDate': {
                        create: function (options) {
                            return new ko.JsonDateObservable(options.data);
                        }
                    },
                    'ContractDate': {
                        create: function (options) {
                            return new ko.JsonDateObservable(options.data);
                        }
                    }
                    //                        'Sale': {
                    //                            create: function (options) {
                    //                                //ko.mapping.fromJS(options.data, {}, this);
                    //                                return ko.mapping.fromJS(options.data, {
                    //                                    'Date': {
                    //                                        create: function (options) {
                    //                                            return new ko.JsonDateObservable(options.data);
                    //                                        }
                    //                                    }
                    //                                }, this);
                    //                            }
                    //                        }
                };

                // create model from json callback
                cloudcre.viewModel = ko.mapping.fromJS(json, mapping);

                var geocoder = new window.google.maps.Geocoder();
                cloudcre.viewModel.centerOnMap = true;
                cloudcre.viewModel.trigger = ko.observable(1);

                cloudcre.viewModel.query = ko.dependentObservable(function () {
                    return [this.Address.AddressLine1(), this.Address.AddressLine2(), this.Address.City(), this.Address.County(), this.Address.StateProvinceRegion(), this.Address.MetropolitanStatisticalArea(), this.Address.Zip()].join(" ");
                }, cloudcre.viewModel);

                cloudcre.viewModel.OriginalLatitude = cloudcre.viewModel.Latitude();
                cloudcre.viewModel.OriginalLongitude = cloudcre.viewModel.Longitude();
                cloudcre.viewModel.OriginalQuery = cloudcre.viewModel.query();
                cloudcre.viewModel.OriginalTrigger = cloudcre.viewModel.trigger();
                cloudcre.viewModel.geoCodeAddress = ko.dependentObservable({
                    read: function () {
                        if (this.query() != this.OriginalQuery || this.OriginalTrigger != this.trigger()) {
                            this.OriginalTrigger = this.trigger();
                            // setup settings
                            var settings = {
                                query: this.query(),
                                centerOnMap: false
                            };

                            if (!!$.trim(settings.query)) {
                                geocoder.geocode({ 'address': settings.query }, function (results, status) {
                                    if (status == window.google.maps.GeocoderStatus.OK) {
                                        module.setAddressModelLngLat(results[0].geometry.location, true);
                                        if (!($("#location-map-canvas").data('map'))) {
                                            module.createMap();
                                        }
                                    }
                                });
                            }
                        }
                    },
                    owner: cloudcre.viewModel//,
                    //deferEvaluation: true  //do not evaluate immediately when created
                });

                cloudcre.viewModel.geoCodeCoordinates = ko.dependentObservable({
                    read: function () {
                        // set marker
                        module.createMarker(this.Latitude(), this.Longitude());
                    },
                    owner: cloudcre.viewModel//,
                    //deferEvaluation: true  //do not evaluate immediately when created
                });

                cloudcre.viewModel.FloorToAreaRatio = ko.dependentObservable(function () {
                    return (this.SiteTotalSquareFoot() == 0) ? "" : formatToTwoDecimals(this.BuildingTotalSquareFoot() / this.SiteTotalSquareFoot());
                }, cloudcre.viewModel);

                cloudcre.viewModel.AverageSquareFootPerUnit = ko.dependentObservable(function () {
                    return (this.Units() == 0) ? "" : formatToTwoDecimals(this.BuildingTotalSquareFoot() / this.Units());
                }, cloudcre.viewModel);

                cloudcre.viewModel.CostPerBuildingSquareFoot = ko.dependentObservable(function () {
                    return (this.BuildingTotalSquareFoot() == 0) ? "" : formatToTwoDecimals(this.Price() / this.BuildingTotalSquareFoot());
                }, cloudcre.viewModel);

                cloudcre.viewModel.CostPerUnit = ko.dependentObservable(function () {
                    return (this.Units() == 0) ? "" : formatToTwoDecimals(this.Price() / this.Units());
                }, cloudcre.viewModel);

                cloudcre.viewModel.PotentialGrossIncomePerSqFt = ko.dependentObservable(function () {
                    return (this.BuildingTotalSquareFoot() == 0) ? "" : formatToTwoDecimals(this.PotentialGrossIncome() / this.BuildingTotalSquareFoot());
                }, cloudcre.viewModel);

                cloudcre.viewModel.PotentialGrossIncomePerBuildingUnit = ko.dependentObservable(function () {
                    return (this.Units() == 0) ? "" : formatToTwoDecimals(this.PotentialGrossIncome() / this.Units());
                }, cloudcre.viewModel);

                cloudcre.viewModel.EffectiveGrossIncomePerSqFt = ko.dependentObservable(function () {
                    return (this.BuildingTotalSquareFoot() == 0) ? "" : formatToTwoDecimals(this.EffectiveGrossIncome() / this.BuildingTotalSquareFoot());
                }, cloudcre.viewModel);

                cloudcre.viewModel.EffectiveGrossIncomePerBuildingUnit = ko.dependentObservable(function () {
                    return (this.Units() == 0) ? "" : formatToTwoDecimals(this.EffectiveGrossIncome() / this.Units());
                }, cloudcre.viewModel);

                cloudcre.viewModel.OperatingExpensePerSqFt = ko.dependentObservable(function () {
                    return (this.BuildingTotalSquareFoot() == 0) ? "" : formatToTwoDecimals(this.OperatingExpense() / this.BuildingTotalSquareFoot());
                }, cloudcre.viewModel);

                cloudcre.viewModel.OperatingExpensePerBuildingUnit = ko.dependentObservable(function () {
                    return (this.Units() == 0) ? "" : formatToTwoDecimals(this.OperatingExpense() / this.Units());
                }, cloudcre.viewModel);

                cloudcre.viewModel.NetOperatingIncomeCkb = ko.observable(false);
                cloudcre.viewModel.NetOperatingIncomeDerived = ko.dependentObservable({
                    read: function () {
                        if (this.NetOperatingIncomeCkb()) {
                            return formatToTwoDecimals(this.EffectiveGrossIncome() - this.OperatingExpense());
                        } else {
                            return cloudcre.viewModel.NetOperatingIncome();
                        }
                    },
                    write: function (value) {
                        cloudcre.viewModel.NetOperatingIncome(value);
                    },
                    owner: cloudcre.viewModel
                });

                cloudcre.viewModel.NetOperatingIncomePerSqFt = ko.dependentObservable(function () {
                    return (this.BuildingTotalSquareFoot() == 0) ? "" : formatToTwoDecimals(this.NetOperatingIncome() / this.BuildingTotalSquareFoot());
                }, cloudcre.viewModel);

                cloudcre.viewModel.NetOperatingIncomePerBuildingUnit = ko.dependentObservable(function () {
                    return (this.Units() == 0) ? "" : formatToTwoDecimals(this.NetOperatingIncome() / this.Units());
                }, cloudcre.viewModel);

                cloudcre.viewModel.OverallRate = ko.dependentObservable(function () {
                    return (this.CostPerBuildingSquareFoot() == 0) ? "" : formatToOneDecimal((this.NetOperatingIncome() / this.BuildingTotalSquareFoot()) / this.CostPerBuildingSquareFoot() * 100);
                }, cloudcre.viewModel);
            };

            return {
                createMap: module.createMap,
                init: module.init
            };

        } ());

        cloudcre.App.init();
    });

})(jQuery, window);
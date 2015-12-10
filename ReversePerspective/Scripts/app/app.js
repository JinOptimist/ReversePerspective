'use strict';

var unitIdUrl = "unit";
var currentPageUrl = "currentPage";

angular.module("app", [])
    .run([
    "$log",
    function ($log) {
        $log.log("Application started!");
    }])
    .controller("treeController", function ($scope, $http, $location, $timeout) {
        $scope.isWait = true;

        $scope.staffFromSelectedUnit = [];
        $scope.numberPerPage = 10;
        $scope.numberPages = [];

        $scope.rootItem = null;
        $scope.allData = [];
        $scope.staffToCurrentPage = [];

        $scope.searchText = "";
        $scope.searchList = [];
        $scope.searchMaxResultCount = 5;

        $scope.showFilteredStaff = false;

        $scope.currentUnitId = $location.search()[unitIdUrl];
        $scope.currentPage = $location.search()[currentPageUrl];
        $scope.currentEmployeeId = $location.search()[currentPageUrl];
        
        function updateStaffList(item) {
            item["allStaff"] = [];
            if (!item.childUnits) {
                return null;
            }
            for (var i = 0; i < item.childUnits.length; i++) {
                var currentItem = item.childUnits[i];

                if (currentItem.unitOwner) {
                    item.allStaff.push(currentItem);
                } 

                if (currentItem.childUnits) {
                    var childStaff = updateStaffList(currentItem);
                    if (childStaff) {
                        for (var j = 0; j < childStaff.length; j++) {
                            item.allStaff.push(childStaff[j]);
                        }
                    }
                }
            }

            return item.allStaff;
        }

        function updateAllData(items) {
            for (var i = 0; i < items.length; i++) {
                var currentItem = items[i];
                currentItem["cachedChildren"] = [];
                currentItem["hasChildren"] = currentItem.childUnits.length > 0;
                if (currentItem.childUnits) {
                    updateAllData(currentItem.childUnits);
                }
            }
        }

        function unitContainsId(unit, id) {
            var lookInChildren = unit.allStaff.find(
                    function (element) {
                        return element.unit.id == id;
                    }
                );

            return typeof lookInChildren !== 'undefined';
        }

        // Init here!
        $http.get("OrgChartData")
            .then(function (res) {
                $scope.allData = res.data;
                console.log("upload complete");
                updateAllData($scope.allData);

                $scope.rootItem = $scope.allData[0];
                updateStaffList($scope.rootItem);

                $scope.findAndOpenCurrentUnit();
                $scope.isWait = false;
            });

        $scope.search = function() {

            var result = [];
            var allStaff = $scope.rootItem.allStaff;

            for (var i = 0; i < allStaff.length; i++) {
                var employee = allStaff[i];
                if (employee.unit.name.indexOf($scope.searchText) > -1) {
                    result.push(allStaff[i]);
                }

                if (result.length > 10)
                    break;
            }

            $scope.searchList = result;
        }

        $scope.openSelectedUnit = function (currentUnitId) {
            $scope.currentUnitId = currentUnitId;
            $scope.searchText = "";
            $scope.findAndOpenCurrentUnit();
        }

        $scope.hideFilteredStaff = function () {
            $timeout(function () {
                $scope.showFilteredStaff = false;
            }, 100);
        }

        $scope.showChildren = function (item) {
            item.cachedChildren = item.childUnits;
        }

        $scope.hideChildren = function (item) {
            item.cachedChildren = [];
        }

        $scope.findAndOpenCurrentUnit = function () {
            if (!$scope.currentUnitId)
                return;

            var selectedItem = $scope.rootItem;
            while (true && selectedItem) {
                $scope.showChildren(selectedItem);
                if (selectedItem.unit.id == $scope.currentUnitId) {
                    break;
                }

                var lookInChildren = selectedItem.childUnits.find(
                    function(element) {
                        return element.unit.id == $scope.currentUnitId;
                    }
                );

                if (lookInChildren) {
                    selectedItem = lookInChildren;
                    break;
                }

                selectedItem = selectedItem.childUnits.find(
                    function (element) {
                        return unitContainsId(element, $scope.currentUnitId);
                    }
                );
            }

            $scope.showUnitDetail(selectedItem);
            $scope.openPage($scope.currentPage);
        }

        $scope.updateNumberPages = function () {
            console.log("$scope.staffFromSelectedUnit.length " + $scope.staffFromSelectedUnit.length);
            console.log("$scope.numberPerPage " + $scope.numberPerPage);
            var pageCount = Math.ceil($scope.staffFromSelectedUnit.length / $scope.numberPerPage);
            console.log("pageCount " + pageCount);
            var array = [];

            var page = $scope.currentPage - 0;
            var startIndex = 0;
            var endIndex = pageCount;
            if (pageCount > 10) {
                startIndex = page - 5;
                endIndex = page + 5;
                if (startIndex < 0)
                    startIndex = 0;
                if (endIndex > pageCount)
                    endIndex = pageCount;
            }

            
            //if (endIndex < 10 && pageCount > 10)

            for (var i = startIndex; i < endIndex; i++) {
                array.push(i + 1);
            }


            $scope.numberPages = array;
        };

        $scope.showUnitDetail = function (selectedItem) {
            $scope.staffFromSelectedUnit = selectedItem.allStaff.map(function (el) { return el.unitOwner; });
            $scope.updateNumberPages();

            var startPage = $scope.isWait ? $scope.currentPage : 1;
            $scope.openPage(startPage);

            var param = $location.search();
            param[unitIdUrl] = selectedItem.unit.id;
            $scope.currentUnitId = selectedItem.unit.id;
            $location.search(param);
        }

        $scope.selectEmployee = function (currentEmployeeId) {
            $scope.currentEmployeeId = currentEmployeeId;
        }

        $scope.openPage = function (currentPage) {
            console.log("currentPage " + currentPage);
            var begin = (currentPage - 1) * $scope.numberPerPage;
            var end = begin + $scope.numberPerPage;
            console.log("begin " + begin);
            console.log("end " + end);
            $scope.staffToCurrentPage = $scope.staffFromSelectedUnit.slice(begin, end);

            var param = $location.search();
            param[currentPageUrl] = currentPage;
            $scope.currentPage = currentPage;
            $location.search(param);
            $scope.updateNumberPages();
        }
    });


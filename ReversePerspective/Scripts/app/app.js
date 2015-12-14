'use strict';

angular.module("app", [])
    .run([
    "$log",
    function ($log) {
        $log.log("Application started!");
    }])
    .controller("homeController", function ($scope, $http, $location, $timeout) {
        $scope.currentOpus = null;
        $scope.opuses = [];

        
        // Init here!
        $http.get("GetOpuses")
            .then(function (res) {
                $scope.opuses = res.data;
                console.log("upload complete");
            });

        $scope.selectOpus = function (opus) {
            $scope.currentOpus = opus;
        }
    });


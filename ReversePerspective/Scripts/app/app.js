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
        $scope.heroInfo = null;
        $scope.heroInfoBlockTop = 0;

        // Init here!
        $http.get("GetOpuses")
            .then(function (res) {
                $scope.opuses = res.data;
                console.log("upload complete");
            });

        $scope.selectOpus = function (opus) {
            $scope.currentOpus = opus;
        }

        $scope.closeHeroInfo = function() {
            $scope.heroInfo = null;
        }

        $scope.selectHeroInfo = function (heroId, phraseId, heroName) {
            var top = angular.element(document.querySelector("#phrase" + phraseId)).prop('offsetTop');
            $scope.heroInfoBlockTop = top + 25;

            var url = "Home/GetHeroInfo?heroId=" + heroId + "&phraseId=" + phraseId;
            $http.get(url)
                .then(function (res) {
                    $scope.heroInfo = res.data;
                    $scope.heroInfo.heroName = heroName;
                    $scope.heroInfo.heroId = heroId;
                    $scope.heroInfo.phraseId = phraseId;
                    console.log("upload hero info complete");
                });
        }

        $scope.deleteOpus = function (opusId) {
            var url = "Home/DeleteOpus?opusId=" + opusId;
            $http.get(url);
        }

        $scope.saveHeroInfo = function () {
            var req = {
                method: "POST",
                url: 'Home/AddHeroInfo',
                headers: {
                    "Content-Type": "application/json"
                },
                data: {
                    info: $scope.heroInfo.newInfo,
                    heroId: $scope.heroInfo.heroId,
                    phraseId: $scope.heroInfo.phraseId
                }
            }

            $http(req).then(function() {
                //alert('+');
                $scope.heroInfo.newInfo = null;
                $scope.selectHeroInfo($scope.heroInfo.heroId, $scope.heroInfo.phraseId, $scope.heroInfo.heroName);
            }, function() {
                alert('bad');
            });
        }
    });


'use strict';

angular.module("app", [])
    .run([
    "$log",
    function ($log) {
        $log.log("Application started!");
    }])
    .controller("homeController", function ($scope, $http, $location, $timeout, $window) {
        $scope.isWait = true;
        $scope.isAdmin = false;
        $scope.currentOpus = null;
        $scope.opuses = [];
        $scope.heroInfo = null;
        $scope.heroInfoBlockTop = 0;

        // Init here!
        $http.get("GetOpuses")
            .then(function (res) {
                $scope.opuses = res.data;
                $scope.isWait = false;
                console.log("upload complete");
            });

        // ---------------------- Opus Region ----------------------

        $scope.selectOpus = function (opus) {
            $scope.currentOpus = opus;
        }

        $scope.deleteOpus = function (opusId) {
            var url = "Home/DeleteOpus?opusId=" + opusId;
            $http.get(url);
            //.then(function (res) {
            //    if (res === true) {
            //        var indexForDelete = -1;
            //        for (var i = 0; i < $scope.opuses.length; i++) {
            //            if ($scope.opuses[i].Id === opusId) {
            //                indexForDelete = i;
            //                break;;
            //            }
            //        }
            //        $scope.opuses[i].
            //    }
            //});
        }

        // ---------------------- HeroIngo Region ----------------------

        $scope.selectHeroInfo = function (heroId, phraseId, heroName) {
            var top = angular.element(document.querySelector("#phrase" + phraseId)).prop('offsetTop');
            top = top + 25;
            $scope.heroInfoBlockTop = top;

            $scope.heroInfo = {};
            $scope.heroInfo.heroName = heroName;
            $scope.heroInfo.isWait = true;

            var url = "Home/GetHeroInfo?heroId=" + heroId + "&phraseId=" + phraseId;
            $http.get(url)
                .then(function (res) {
                    $scope.heroInfo = res.data;
                    $scope.heroInfo.heroName = heroName;
                    $scope.heroInfo.heroId = heroId;
                    $scope.heroInfo.phraseId = phraseId;
                    $scope.heroInfo.isWait = false;
                    console.log("upload hero info complete");
                });
        }

        $scope.closeHeroInfo = function () {
            $scope.heroInfo = null;
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

            $http(req).then(function () {
                //alert('+');
                $scope.heroInfo.newInfo = null;
                $scope.selectHeroInfo($scope.heroInfo.heroId, $scope.heroInfo.phraseId, $scope.heroInfo.heroName);
            }, function () {
                alert('bad');
            });
        }

        $scope.deleteHeroInfo = function (infoId) {
            var url = "Home/DeleteHeroInfo?infoId=" + infoId;
            $http.get(url)
                .then(function (res) {
                    if (res.data) {
                        alert("Удаление прошло успешно");
                    }
                });
        }
    });


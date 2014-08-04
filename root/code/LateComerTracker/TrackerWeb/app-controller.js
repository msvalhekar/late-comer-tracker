/// <reference path="components/team/team.html" />

(function () {

    trackerApp = angular.module('lateTrackerApp', ['ngRoute']);

    var appController = function ($scope) {
        
    };

    trackerApp.controller('appController', appController);
    
    trackerApp.config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider
                .when('/teams', {
                    templateUrl: '/components/team/teams.html',
                    controller: 'teamsController'
                })
                .when('/teams/:id', {
                    templateUrl: '/components/team/team.html',
                    controller: 'teamController'
                });
        }
    ]);

}());
/// <reference path="components/team/team.html" />

(function () {

    var app = angular.module('lateTrackerApp', ['ngRoute']);

    var appController = function ($scope) {
        
    };

    app.controller('appController', appController);
    
    app.config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider.
                when('/team', {
                    templateUrl: '/components/team/team.html',
                    controller: 'TeamController'
                });
        }
    ]);

}());
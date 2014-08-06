
(function () {

    trackerApp = angular.module('lateTrackerApp', ['ngRoute']);

    var appController = function ($scope) {
        $scope.teamww = "klo";
    };

    trackerApp.controller('appController', appController);

    trackerApp.config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider
                .when('/meetings', {
                    templateUrl: '/components/meeting/meetings.html',
                    controller: 'meetingsController'
                })
                .when('/employees', {
                    templateUrl: '/components/employee/employees.html',
                    controller: 'employeesController'
                })
                .when('/teams', {
                    templateUrl: '/components/team/teams.html',
                    controller: 'teamsController'
                })
                .when('/teams/:id', {
                    templateUrl: '/components/team/team.html',
                    controller: 'teamController'
                })
               .when('/teams/edit/:id', {
                   templateUrl: '/components/team/editTeam.html',
                   controller: 'teamController'
               });
        }
    ]);

}());
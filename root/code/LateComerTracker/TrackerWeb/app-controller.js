
(function () {

    trackerApp = angular.module('lateTrackerApp', ['ngRoute', 'xeditable']);

    trackerApp.run(function (editableOptions) {
        editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
    });
    
    var appController = function ($scope) {
    };

    trackerApp.controller('appController', appController);

    trackerApp.config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider
                .when('/settings', {
                    templateUrl: '/components/configuration/configurations.html',
                    controller: 'configurationController'
                })
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

(function() {

    trackerApp = angular.module('lateTrackerApp', ['ngRoute', 'xeditable', 'ui.bootstrap']);

    trackerApp.run(function(editableOptions) {
        editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
    });

    var appController = function($scope, teamService) {

        //$scope.getTeamsForAttendance = function () {
        //teamService.getTeamsAsync(function(data) {
        //    $scope.teams = data;
        //});
        // };
        $scope.getTeams = function () {
            teamService.getTeamsAsync(function (data) {
                $scope.teams = data;
            });
        };
    };

    trackerApp.controller('appController', appController);

    trackerApp.config(['$routeProvider',
        function($routeProvider) {
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
                .when('/employees/:id', {
                    templateUrl: '/components/employee/employee.html',
                    controller: 'employeeController'
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
                })
                .when('/attendance/:id', {
                    templateUrl: '/components/attendance/attendance.html',
                    controller: 'attendanceController'
                });
        }
    ]);
}());
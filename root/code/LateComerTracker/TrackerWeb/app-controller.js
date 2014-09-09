
(function () {
    // This 
    angular.module('AjaxSpinnerService', [])
        .config(function($httpProvider) {
            $httpProvider.responseInterceptors.push('myHttpInterceptor');
            var spinnerFunction = function(data, headersGetter) {
                $('#loadingTicker').show();
                return data;
            };
            $httpProvider.defaults.transformRequest.push(spinnerFunction);
        })
        // register the interceptor as a service, intercepts ALL angular ajax http calls
        .factory('myHttpInterceptor', function ($q, $window) {
            return function (promise) {
                return promise.then(function (response) {
                    $('#loadingTicker').hide();
                    return response;

                }, function (response) {
                    $('#loadingTicker').hide();
                    return $q.reject(response);
                });
        };
    });

    trackerApp = angular.module('lateTrackerApp', ['ngRoute', 'xeditable', 'ui.bootstrap', 'AjaxSpinnerService']);

    trackerApp.run(function (editableOptions) {
        $('#loadingTicker').show();
        $('#loadingTicker').hide();
        editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
    });

    var appController = function($scope, teamService) {

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
                .when('/instructions', {
                    templateUrl: '/instructions.html'
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
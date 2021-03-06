﻿
(function() {

    function teamsController($scope, teamService) {

        var getTeamsAsync = function() {
            teamService.getTeamsAsync(function(data) {
                $scope.teams = data;
            });
        };
        getTeamsAsync();

        $scope.addTeam = function() {
            var newTeam = $scope.newTeam;
            teamService.postTeam(newTeam)
                .then(function(team) {
                    if (team) {
                        $scope.teams.push(team);
                        $scope.newTeam = null;
                    }
                });
        };

        $scope.deleteTeam = function(id) {
            teamService.deleteTeam(id).then(function() {
                getTeamsAsync();
            });
        };
    }

    trackerApp.controller("teamsController", teamsController);

    // ------- Single Team Details

    function teamController($scope, $routeParams, $location, teamService, employeeService) {
        $scope.currentEmployeeList = [];
        $scope.availableEmployeeList = [];

        teamService.getTeam($routeParams.id)
            .then(function(data) {
                $scope.team = $scope.editTeamObj = data;
                $scope.currentEmployeeList = $.grep($scope.team.Employees, function() { return true; });

                getAvailableEmployees();
            });

        $scope.updateTeam = function(team) {
            team.Employees = $.grep($scope.currentEmployeeList, function() { return true; });
            teamService.updateTeam(team)
                .then(function(data) {
                    //$scope.team = $scope.editTeamObj = data;
                    $scope.redirectToEdit(team);
                });
        };

        var updateAvailableEmployeeListWithCurrentEmp = function() {
            for (var j = $scope.currentEmployeeList.length - 1; j >= 0; j--) {
                for (var i = $scope.availableEmployeeList.length - 1; i >= 0; i--) {
                    if ($scope.availableEmployeeList[i].Id == $scope.currentEmployeeList[j].Id) {
                        $scope.availableEmployeeList[i].IsCurrentEmployee = true;
                    }
                }
            }
        };

        var getAvailableEmployees = function() {
            employeeService.getEmployeesAsync(function(data) {
                $scope.availableEmployeeList = data;
                updateAvailableEmployeeListWithCurrentEmp();
            });
        };

        $scope.removeFromCurrentEmployeeList = function(employee) {
            for (var i = $scope.currentEmployeeList.length - 1; i >= 0; i--) {
                if ($scope.currentEmployeeList[i].Id == employee.Id) {
                    $scope.currentEmployeeList.splice(i, 1);
                    for (var j = $scope.availableEmployeeList.length - 1; j >= 0; j--) {
                        if ($scope.availableEmployeeList[j].Id == employee.Id) {
                            $scope.availableEmployeeList[j].IsCurrentEmployee = false;
                            break;
                        }
                    }
                    break;
                }
            }
        };

        $scope.addToCurrentEmployeeList = function(employee) {
            employee.IsCurrentEmployee = true;
            $scope.currentEmployeeList.push(employee);
        };

        $scope.redirectToEdit = function(team) {
            $location.path('/teams/' + team.Id);
        };

        $scope.penaltyObj = {
            when: new Date()
        };
        $scope.logPenaltyDialog = function(team, employee) {
            $scope.penaltyObj.empId = employee.Id;
            $scope.penaltyObj.empName = employee.Name;
            $scope.penaltyObj.teamId = team.Id;
            $scope.penaltyObj.teamName = team.Name;
        };
    }

    trackerApp.controller("teamController", teamController);
})();

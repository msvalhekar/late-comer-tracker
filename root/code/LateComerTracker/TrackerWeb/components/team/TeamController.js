
(function () {

    // ------- All Teams
    function teamsController($scope, teamService) {

        var getTeamsAsync = function () {
            teamService.getTeamsAsync(function (data) {
                $scope.teams = data;
            });
        };
        getTeamsAsync();

        $scope.addTeam = function () {
            var newTeam = $scope.newTeam;
            teamService.postTeam(newTeam)
                .then(function (team) {
                    if (team) {
                        $scope.teams.push(team);
                        $scope.newTeam = null;
                    }
                });
        };

        $scope.editTeam = function (id) {
            alert("EditTeam function " + id);

        };

        $scope.deleteTeam = function (id) {
            teamService.deleteTeam(id).then(function () {
                getTeamsAsync();
            });
        };

        $scope.onNameChange = function () {
            var bFound = false;
            var lowerNameValue = angular.lowercase($scope.newTeam.Name);
            var teams = $scope.teams;
            for (var i = 0; i < teams.length; i++) {
                if (angular.lowercase(teams[i].Name) === lowerNameValue) {
                    bFound = true;
                    break;
                }
            }
        };
    }

    trackerApp.controller("teamsController", teamsController);

    // ------- Single Team Details
    function teamController($scope, $routeParams, teamService) {

        var promise = teamService.getTeam($routeParams.id);

        promise.then(function (data) {
            $scope.team = $scope.editTeamObj = data;
        }, function (error) {
        });


        $scope.editTeam = function (team) {

            var promise = teamService.editTeam(team).
            then(function (data) {
                $scope.team = $scope.editTeamObj = data;
            });

        };
    }

    trackerApp.controller("teamController", teamController);

})();

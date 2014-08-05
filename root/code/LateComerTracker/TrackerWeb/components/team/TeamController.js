
(function () {

    // ------- All Teams
    function teamsController($scope, trackerService) {

        var getTeamAsync =  function() {
            trackerService.getTeamsAsync(function (data) {
                $scope.teams = data;
            });
        }
        getTeamAsync();

        $scope.addTeam = function() {
            var newTeam = $scope.newTeam;
            trackerService.postTeam(newTeam)
                .then(function (team) {
                    $scope.teams.push(team);
                    $scope.newTeam = null;
            });
        };

        $scope.deleteTeam = function (id) {
            trackerService.deleteTeam(id).then(function () {
                getTeamAsync();
            });
        }

        //var promise = trackerService.getTeams();

        //promise.then(function (data) {
        //    $scope.teams = data;
        //}, function (error) {
        //});
    }
    
    trackerApp.controller("teamsController", teamsController);

    // ------- Single Team Details
    function teamController($scope, $routeParams, trackerService) {

        var promise = trackerService.getTeam($routeParams.id);

        promise.then(function (data) {
            $scope.team = data;
        }, function (error) {
        });
    }


    
    trackerApp.controller("teamController", teamController);
})();

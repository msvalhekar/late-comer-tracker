
(function () {

    // ------- All Teams
    function teamsController($scope, trackerService) {

        trackerService.getTeamsAsync(function (data) {
            $scope.teams = data;
        });

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

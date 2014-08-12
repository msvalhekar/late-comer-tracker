
(function () {

    // ------- All Teams

    function attendanceController($scope, teamService, $routeParams) {
        $scope.CurrentDateTime = new Date().toISOString().slice(0, 19).replace('T', ' ');

        teamService.getTeam($routeParams.id)
           .then(function (data) {
               $scope.teamName = data.Name;
               $scope.currentEmployeeList = $.grep(data.Employees, function () { return true; });
           });


    }

    trackerApp.controller("attendanceController", attendanceController);

})();

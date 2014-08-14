
(function () {

    // ------- All Teams

    function attendanceController($scope, teamService, $routeParams , meetingService) {
        $scope.CurrentDateTime = new Date().toISOString().slice(0, 19).replace('T', ' ');
        meetingService.getMeetingsAsync(function (data) {
            $scope.meetings = data;
            $scope.meetingType = data[0];
        });

        teamService.getTeam($routeParams.id)
           .then(function (data) {
               $scope.teamName = data.Name;
               $scope.currentEmployeeList = $.grep(data.Employees, function () { return true; });
           });


    }

    trackerApp.controller("attendanceController", attendanceController);

})();


(function() {

    function attendanceController($scope, teamService, meetingService, $routeParams, $location) {
        $scope.attendance = {
            currentDateTime : new Date().toString().substring(0, 24)
        };

        meetingService.getMeetingsAsync(function(data) {
            $scope.meetings = data;
            $scope.attendance.meeting = data[0];
        });

        teamService.getTeam($routeParams.id)
            .then(function(data) {
                $scope.attendance.teamId = data.Id;
                $scope.attendance.teamName = data.Name;
                $scope.attendance.employees = [];
                for (var i = 0; i < data.Employees.length; i++) {
                    var employee = data.Employees[i];
                    employee.cameLate = false;
                    $scope.attendance.employees.push(employee);
                }
            });

        $scope.submitAttendance = function(attendance) {
            var lateEmployeeIds = [];
            for (var i = 0; i < attendance.employees.length; i++) {
                var employee = attendance.employees[i];
                if (employee.cameLate)
                    lateEmployeeIds.push(employee.Id);
            }
            
            if (0 == lateEmployeeIds.length) {
                $scope.redirectToTeam(attendance.teamId);
                return;
            }

            teamService.markAttendance(attendance.teamId, attendance.meeting.Id, lateEmployeeIds)
                .then(function() {
                    $scope.redirectToTeam(attendance.teamId);
                });
        };

        $scope.redirectToTeam = function (teamId) {
            $location.path('/teams/' + teamId);
        };
    }

    trackerApp.controller("attendanceController", attendanceController);
})();

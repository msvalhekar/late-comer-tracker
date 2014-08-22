(function () {
    function penaltyController($scope) {
        $scope.penaltyobj = {
            EmpId: 1,
            EmpName: 1,
            TeamId: 1,
            TeamName: 1
        };
        $scope.settlePenalty = function (employee,team) {
            //$scope.penaltyobj = {
            //    EmpId: employee.Id,
            //    EmpName: employee.Name,
            //    TeamId: team.Id,
            //    TeamName:team.Name
            //};
            $scope.penaltyobj.EmpName = "MK";
        };
    };
    trackerApp.controller("penaltyController", penaltyController);
})();
(function () {
    function penaltyController($scope, teamService, $route) {

        $scope.savePenalty = function() {
            var penalty = $scope.penaltyObj;
            //alert(penalty.teamId + ' ' + penalty.teamName + ' ' + penalty.empName + ' ' + penalty.empId + ' ' + penalty.how + ' ' + penalty.when);

            teamService.logPenalty(penalty.teamId, penalty.empId, penalty.how, penalty.when)
                .then(function() {
                    $route.reload();
                });
        };
        
        $scope.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1,
        };
        $scope.maxDate = new Date();
    };
    
    trackerApp.controller("penaltyController", penaltyController);
})();
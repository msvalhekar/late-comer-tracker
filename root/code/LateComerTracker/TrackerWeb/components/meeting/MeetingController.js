
(function () {

    // ------- All Teams
    function meetingsController($scope, trackerService) {

        var promise = trackerService.getMeetings();

        promise.then(function (data) {
            $scope.meetings = data;
        }, function (error) {
        });
    }
    
    trackerApp.controller("meetingsController", meetingsController);
})();

﻿
(function () {

    function meetingsController($scope, meetingService) {

        $scope.severities = [
            { name: 'Value1', value: 1 },
            { name: 'Value2', value: 2 },
            { name: 'Value3', value: 3 },
            { name: 'Value4', value: 4 },
            { name: 'Value5', value: 5 }
        ];
        
        var getMeetingsAsync = function() {
            meetingService.getMeetingsAsync(function(data) {
                $scope.meetings = data;
            });
        };
        getMeetingsAsync();
        
        $scope.addMeeting = function () {
            var newMeeting = {
                Name: $scope.newMeeting.Name,
                Description: $scope.newMeeting.Description,
                Severity: $scope.newMeeting.SeverityObj.value
            };
            
            meetingService.postMeeting(newMeeting)
                .then(function (meeting) {
                    $scope.meetings.push(meeting);
                    $scope.newMeeting = null;
                });
        };

        $scope.deleteMeeting = function (id) {
            meetingService.deleteMeeting(id).then(function () {
                getMeetingsAsync();
            });
        };

        $scope.onNameChange = function () {
            var bFound = false;
            var lowerNameValue = angular.lowercase($scope.newMeeting.Name);
            var meetings = $scope.meetings;
            for (var i = 0; i < meetings.length; i++) {
                if (angular.lowercase(meetings[i].Name) === lowerNameValue) {
                    bFound = true;
                    break;
                }
            }
        };
    }
    
    trackerApp.controller("meetingsController", meetingsController);
})();

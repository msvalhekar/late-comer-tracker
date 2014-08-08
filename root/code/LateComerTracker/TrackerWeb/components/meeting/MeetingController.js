﻿
(function () {

    function meetingsController($scope, $filter, meetingService) {

        $scope.severities = [
            { value: 1, text: '1 pts', },
            { value: 2, text: '2 pts', },
            { value: 3, text: '3 pts', },
            { value: 4, text: '4 pts', },
            { value: 5, text: '5 pts', }
        ];

        // todo: http://vitalets.github.io/angular-xeditable/        
        var getMeetingsAsync = function() {
            meetingService.getMeetingsAsync(function(data) {
                $scope.meetings = data;
            });
        };
        getMeetingsAsync();
        
        $scope.showSeverity = function (selectedMeeting) {
            var selected = $filter('filter')($scope.severities, { value: selectedMeeting.Severity });
            return (selectedMeeting.Severity && selected.length) ? selected[0].text : 'Not set';
        };
        
        $scope.addMeeting = function () {
            var newMeeting = {
                Name: $scope.newMeeting.Name,
                Description: $scope.newMeeting.Description,
                Severity: $scope.newMeeting.SeverityObj.value
            };
            
            meetingService.postMeeting(newMeeting)
                .then(function (meeting) {
                    if (meeting) {
                        $scope.meetings.push(meeting);
                        $scope.newMeeting = null;
                    }
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

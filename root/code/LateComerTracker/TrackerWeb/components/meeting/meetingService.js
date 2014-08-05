(function() {

    var meetingService = function($http) {

        this.getMeetingsAsync = function (onSuccessCallback) {
            return $http.get("/api/meetings")
                .success(onSuccessCallback);
        };

        this.postMeeting = function (meeting) {
            return $http.post("/api/meetings/", meeting)
                .then(function (response) {
                    return response.data;
                });
        };

        this.deleteMeeting = function (meetingId) {
            return $http.delete("/api/meetings/" + meetingId).then(function () {
                return true;
            });
        };
    };

    trackerApp.service('meetingService', meetingService);
}());
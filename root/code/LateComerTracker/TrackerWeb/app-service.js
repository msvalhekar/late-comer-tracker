(function () {

    var trackerService = function ($http) {
        
        this.getMeetings = function() {
            return $http.get("/api/meetings")
                .then(function (response) {
                    return response.data;
                });
        };

        this.getTeams = function() {
            return $http.get("/api/teams")
                .then(function (response) {
                    return response.data;
                });
        };

        this.getTeam = function (teamId) {
            return $http.get("/api/teams/" + teamId)
                .then(function (response) {
                    return response.data;
                });
        };
    };

    trackerApp.service('trackerService', trackerService);
}());
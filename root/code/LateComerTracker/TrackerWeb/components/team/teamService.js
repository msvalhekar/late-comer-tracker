(function() {

    var teamService = function($http, $location) {

        this.getTeamsAsync = function(onSuccessCallback) {
            return $http.get("/api/teams")
                .success(onSuccessCallback);
        };

        this.getTeam = function(teamId) {
            return $http.get("/api/teams/" + teamId)
                .then(function(response) {
                    return response.data;
                });
        };

        this.postTeam = function(team) {
            return $http.post("/api/teams/", team)
                .then(function(response) {
                    return response.data;
                });
        };

        this.deleteTeam = function(teamId) {
            return $http.delete("/api/teams/" + teamId).then(function() {
                return true;
            });
        };

        this.updateTeam = function(team) {
            return $http.put("/api/teams/" + team.Id, team)
                .then(function(response) {
                    return response.data;
                });
        };

        this.markAttendance = function(teamId, meetingId, employeeIds) {
            var attendance = { teamId: teamId, meetingId: meetingId, employeeIds: employeeIds, source: "xyz" };
            return $http.post("api/attendance/", attendance)
                .then(function(response) {
                    return response.data;
                });
        };

        this.logPenalty = function(teamId, empId, how, when) {
            var penalty = { teamId: teamId, empId: empId, how: how, when: when, source: "xyz" };
            return $http.post("api/penalty/", penalty)
                .then(function(response) {
                    return response.data;
                });
        };
    };

    trackerApp.service('teamService', teamService);
}());
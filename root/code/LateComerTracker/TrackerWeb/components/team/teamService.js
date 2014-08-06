﻿(function() {

    var teamService = function($http) {

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
        this.editTeam = function (team) {
            return $http.put("/api/teams/"+team.Id, team)
                .then(function (response) {
                    return response.data;
                });
        };

    };

    trackerApp.service('teamService', teamService);
}());
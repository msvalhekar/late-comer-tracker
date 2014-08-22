(function() {

    var configurationsService = function($http) {

        this.getSettingsAsync = function (onSuccessCallback) {
            return $http.get("/api/configuration")
                .success(onSuccessCallback);
        };

        //this.postSetting = function (setting) {
        //    return $http.post("/api/configuration/", setting)
        //        .then(function (response) {
        //            return response.data;
        //        });
        //};

        this.updateSetting = function (setting) {
            return $http.put("/api/configuration/" + setting.Key, setting).then(function () {
                return true;
            });
        };
    };

    trackerApp.service('configurationsService', configurationsService);
}());
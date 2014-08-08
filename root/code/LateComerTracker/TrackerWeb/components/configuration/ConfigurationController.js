
(function () {

    function configurationController($scope, configurationsService) {

        var getSettingsAsync = function() {
            configurationsService.getSettingsAsync(function (data) {
                $scope.settings = data;
            });
        };
        getSettingsAsync();
        
        //$scope.addSetting = function () {
        //    configurationsService.postSetting($scope.newSetting)
        //        .then(function (setting) {
        //            if (setting) {
        //                $scope.meetings.push(setting);
        //                $scope.newSetting = null;
        //            }
        //        });
        //};

        $scope.editSetting = function (setting) {
            configurationsService.editSetting(setting).then(function () {
                getSettingsAsync();
            });
        };

        //$scope.onNameChange = function () {
        //    var bFound = false;
        //    var lowerNameValue = angular.lowercase($scope.newSetting.Key);
        //    var settings = $scope.settings;
        //    for (var i = 0; i < settings.length; i++) {
        //        if (angular.lowercase(settings[i].Name) === lowerNameValue) {
        //            bFound = true;
        //            break;
        //        }
        //    }
        //};
    }
    
    trackerApp.controller("configurationController", configurationController);
})();

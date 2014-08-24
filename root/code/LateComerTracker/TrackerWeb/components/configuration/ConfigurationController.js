
(function () {

    function configurationController($scope, configurationsService) {

        var getSettingsAsync = function() {
            configurationsService.getSettingsAsync(function (data) {
                $scope.settings = data;
            });
        };
        getSettingsAsync();
        
        $scope.updateSetting = function (setting) {
            configurationsService.updateSetting(setting).then(function () {
                getSettingsAsync();
            });
        };
    }
    
    trackerApp.controller("configurationController", configurationController);
})();

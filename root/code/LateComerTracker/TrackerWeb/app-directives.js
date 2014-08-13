
(function() {
    trackerApp = angular.module('lateTrackerApp');

    trackerApp.directive('checkUniqueName', function() {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function(scope, element, attrs, ctrl) {
                scope.$watch(attrs.ngModel, function (newValue) {
                    ctrl.$setValidity("checkUniqueName", true);
                    
                    var collection = scope.$eval(attrs.checkUniqueName) || [];
                    for (var i = 0; i < collection.length; i++) {
                        if (collection[i].Name === newValue) {
                            ctrl.$setValidity("checkUniqueName", false);
                            return undefined;
                        }
                    }
                    return newValue;
                });
            }
        };
    });
}());
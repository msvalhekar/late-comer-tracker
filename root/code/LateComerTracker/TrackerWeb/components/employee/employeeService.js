(function() {

    var employeeService = function($http) {

        this.getEmployeesAsync = function (onSuccessCallback) {
            return $http.get("/api/employees")
                .success(onSuccessCallback);
        };

        this.postEmployee = function (employee) {
            return $http.post("/api/employees/", employee)
                .then(function (response) {
                    return response.data;
                });
        };

        this.deleteEmployee = function (employeeId) {
            return $http.delete("/api/employees/" + employeeId).then(function () {
                return true;
            });
        };
    };

    trackerApp.service('employeeService', employeeService);
}());
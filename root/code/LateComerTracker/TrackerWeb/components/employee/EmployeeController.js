
(function () {

    function employeesController($scope, employeeService) {

        var getEmployeesAsync = function() {
            employeeService.getEmployeesAsync(function(data) {
                $scope.employees = data;
            });
        };
        getEmployeesAsync();
        
        $scope.addEmployee = function () {
            employeeService.postEmployee($scope.newEmployee)
                .then(function (employee) {
                    if (employee) {
                        $scope.employees.push(employee);
                        $scope.newEmployee = null;
                    }
                });
        };

        $scope.deleteEmployee = function (id) {
            employeeService.deleteEmployee(id).then(function () {
                getEmployeesAsync();
            });
        };
    }
    
    trackerApp.controller("employeesController", employeesController);
})();

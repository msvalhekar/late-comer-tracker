
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

        $scope.onNameChange = function () {
            var bFound = false;
            var lowerNameValue = angular.lowercase($scope.newEmployee.Name);
            var employees = $scope.employees;
            for (var i = 0; i < employees.length; i++) {
                if (angular.lowercase(employees[i].Name) === lowerNameValue) {
                    bFound = true;
                    break;
                }
            }
        };
    }
    
    trackerApp.controller("employeesController", employeesController);
})();

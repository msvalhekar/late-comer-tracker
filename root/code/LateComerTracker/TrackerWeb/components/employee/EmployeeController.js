
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
    
    // Single Employee

    function employeeController($scope, $routeParams, employeeService) {

        employeeService.getEmployeeAttendance($routeParams.id)
            .then(function(data) {
                if (0 < data.length) {
                    $scope.hasLateData = true;
                    $scope.attendance = data;
                } else {
                    $scope.hasLateData = false;
                }
            });

        employeeService.getEmployeePenalties($routeParams.id)
            .then(function(data) {
                if (0 < data.length) {
                    $scope.hasPenaltyData = true;
                    $scope.penalties = data;
                } else {
                    $scope.hasPenaltyData = false;
                }
            });
    }

    trackerApp.controller("employeeController", employeeController);
})();

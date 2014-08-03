function TeamController($scope) {
    $scope.testText = "Hello Buddy !!!";

    $.post("/api/teams", { Name: 'testName', Description: 'dont know' }, function (data) {
        if (data) $scope.testText = data.Name;
    });
}
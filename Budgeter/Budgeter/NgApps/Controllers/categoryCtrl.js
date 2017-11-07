self.initModel = function () {
    self.model.HouseholdId = authSvc.authentication.householdId;
    self.model.ExpenseTF = true;
    $http.get(authSvc.serviceBase + 'api/household/' + authSvc.authentication.householdId).then(function (response) {
        self.householdName = response.data;
    });
    self.getCategories();
};

self.initModel();
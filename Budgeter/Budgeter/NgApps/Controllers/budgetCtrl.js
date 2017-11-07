'use strict';
angular.module('BudgetApp').controller('budgetCtrl', ['$http', '$state', 'authSvc', function ($http, $state, authSvc) {

    var self = this;
    self.budgets = [];
    self.items = [];
    self.single = '';
    self.message = '';
    self.demo = true;
    self.model = {
        Id: '',
        Name: '',
        householdId: ''
    }
    self.itemModel = {
        BudgetId: '',
        Name: '',
        Amount: '',
        CategoryId: ''
    }

    self.getBudget = function () {
        $http({
            method: 'GET',
            url: authSvc.serviceBase + '/api/accounts',
            headers: {  // load this header info for the first request and it applies to all others in the controller
                'Authorization': authSvc.authentication.userName + '/token:' + authSvc.authentication.token,
                'Usename': authSvc.authentication.userName,
                'Household': authSvc.authentication.householdId
            }
        }).then(function (response) {
            self.budget = response.data;
        });
    };

    // get a list of budgets
    self.getBudgets = function () {
        return $http.get(authSvc.serviceBase + '/api/budget/All/' + authSvc.authentication.householdId).then(function (response) {
            self.budgets = response.data;
        });
    };

    self.createBudget = function () {
        self.model.householdId = authSvc.authentication.householdId;
        return $http.post(serviceBase + 'api/budget/Create/', self.model).then(function (response) {
            return response;
        });
    };

    self.editBudget = function () {
        return $http.post(serviceBase + 'api/budget/Edit', self.model).then(function (response) {
            return response;
        });
    };

    self.deleteBudget = function () {
        return $http.post(serviceBase + 'api/budget/Delete', self.model).then(function (response) {
            return response;
        });
    };
        
    self.getItems = function (id) {
        return $http.get(serviceBase + 'api/item/All/' + id).then(function (response) {
            self.items = response.data;
        });
    };

    self.createItem = function () {        
        return $http.post(serviceBase + 'api/item/Create/', self.itemModel).then(function (response) {
            return response;
        });
    };

    self.getBudgets();
    self.getItems();

}]);

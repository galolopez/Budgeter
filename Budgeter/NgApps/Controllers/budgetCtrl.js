'use strict';
angular.module('BudgetApp').controller('budgetCtrl', ['$http', '$state', 'authSvc', function ($http, $state, authSvc) {

    var self = this;
    self.currentBudgetId = '';
    self.currentItemId = '';
    self.currentCategoryId = '';
    self.budgets = [];
    self.items = [];
    self.categories = [];
    self.category = '';
    self.single = '';
    self.message = '';
    self.demo = true;
    self.model = {
        Id: '',
        Name: '',
        householdId: authSvc.authentication.householdId
    }
    self.itemModel = {
        Id: '',
        Name: '',
        Amount: '',
        BudgetId: '',
        CategoryId: ''
    }
    self.categoryModel = {
        Name: '',
        ExpenseTF: '',
        Id: ''
    };
    // **********************************************************************************************************************//
    //                                                        BUDGETS
    // **********************************************************************************************************************//
    self.getB = function (id) {
        $http({
            method: 'GET',
            url: authSvc.serviceBase + '/api/budget',
            headers: {  // load this header info for the first request and it applies to all others in the controller
                'Authorization': authSvc.authentication.userName + '/token:' + authSvc.authentication.token,
                'Usename': authSvc.authentication.userName,
                'Household': authSvc.authentication.householdId
            }
        }).then(function (response) {
            self.single = response.data;
        });
    };

    self.getBudget = function (id) {
        self.currentBudgetId = id;
        return $http.get(authSvc.serviceBase + '/api/budget/Single/' + id).then(function (response) {
            self.model = response.data;
        });
    };

    self.getBudgets = function () {
        return $http.get(authSvc.serviceBase + '/api/budget/All/' + authSvc.authentication.householdId).then(function (response) {
            self.budgets = response.data;
        });
    };

    self.createBudget = function () {
        return $http.post(serviceBase + 'api/budget/Create/', self.model).then(function (response) {
            return response;
        });
    };

    self.editBudget = function () {
        return $http.post(serviceBase + 'api/budget/Edit/', self.model).then(function (response) {
            return response;
        });
    };

    self.deleteBudget = function (id) {
        return $http.post(serviceBase + 'api/budget/Delete/' + id).then(function (response) {
            return response;
        });
    };
    // **********************************************************************************************************************//
    //                                                        ITEMS
    // **********************************************************************************************************************//
    self.getItem = function (id) {
        return $http.get(serviceBase + 'api/item/Single/' + id).then(function (response) {
            self.itemModel = response.data;
        });
    };

    self.getItems = function (id) {
        self.currentItemId = id;
        return $http.get(serviceBase + 'api/item/All/' + id).then(function (response) {
            self.items = response.data;
        });
    };

    self.createItem = function (id) {
        self.itemModel.BudgetId = id;
        return $http.post(serviceBase + 'api/item/Create/', self.itemModel).then(function (response) {
            return response;
        });
    };

    self.editItem = function () {
        return $http.post(serviceBase + 'api/item/Edit/', self.itemModel).then(function (response) {
            return response;
        });
    };

    self.deleteItem = function (id) {
        return $http.post(serviceBase + 'api/item/Delete/' + id).then(function (response) {
            return response;
        });
    };
    // **********************************************************************************************************************//
    //                                                        CATEGORIES
    // **********************************************************************************************************************//
    self.getCategory = function (id) {
        self.currentCategoryId = id;
        return $http.get(serviceBase + 'api/category/Single/' + id).then(function (response) {
            self.categoryModel = response.data;
        });
    };

    self.getCategories = function () {
        return $http.get(serviceBase + 'api/category/All/' + authSvc.authentication.householdId).then(function (response) {
            self.categories = response.data;
        });
    };

    self.createCategory = function () {
        return $http.post(serviceBase + 'api/category/Create', self.categoryModel).then(function (response) {
            return response;
        });
    };

    self.editCategory = function () {
        return $http.post(serviceBase + 'api/category/Edit/', self.categoryModel).then(function (response) {
            return response;
        });
    };

    self.deleteCategory = function (id) {
        return $http.post(serviceBase + 'api/category/Delete/' + id).then(function (response) {
            return response;
        });
    };

    self.getBudgets();
    self.getCategory();
    self.getCategories();
}]);

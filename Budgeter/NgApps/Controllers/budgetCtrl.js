'use strict';
angular.module('BudgetApp').controller('budgetCtrl', ['$http', '$state', 'authSvc', function ($http, $state, authSvc) {

    var vm = this;
    vm.currentBudgetId = '';
    vm.currentItemId = '';
    vm.currentCategoryId = '';
    vm.budgets = [];
    vm.items = [];
    vm.single = '';
    vm.message = '';
    vm.demo = true;

    vm.model = {
        Id: '',
        Name: '',
        householdId: authSvc.authentication.householdId
    }
    vm.itemModel = {
        Id: '',
        Name: '',
        Amount: '',
        BudgetId: '',
        CategoryId: ''
    }

    function setTab(tabId) {
        vm.activeTabId = tabId;
    };
    vm.setTab = setTab;

    // **********************************************************************************************************************//
    //                                                        BUDGETS
    // **********************************************************************************************************************//
    vm.getB = function (id) {
        $http({
            method: 'GET',
            url: authSvc.serviceBase + '/api/budget',
            headers: {  // load this header info for the first request and it applies to all others in the controller
                'Authorization': authSvc.authentication.userName + '/token:' + authSvc.authentication.token,
                'Usename': authSvc.authentication.userName,
                'Household': authSvc.authentication.householdId
            }
        }).then(function (response) {
            vm.single = response.data;
        });
    };

    vm.getBudget = function (id) {
        vm.currentBudgetId = id;
        return $http.get(authSvc.serviceBase + '/api/budget/Single/' + id).then(function (response) {
            vm.model = response.data;
        });
    };

    vm.getBudgets = function () {
        return $http.get(authSvc.serviceBase + '/api/budget/All/' + authSvc.authentication.householdId).then(function (response) {
            vm.budgets = response.data;
            vm.activeTab = vm.budgets[0];
            vm.activeTabId = vm.activeTab.Id;

            if (vm.activeTabId > 0) {
                vm.getItemsById(vm.activeTabId);
            }
        });
    };

    vm.createBudget = function () {
        return $http.post(serviceBase + 'api/budget/Create/', vm.model).then(function (response) {
            return response;
        });
    };

    vm.editBudget = function () {
        return $http.post(serviceBase + 'api/budget/Edit/', vm.model).then(function (response) {
            return response;
        });
    };

    vm.deleteBudget = function (id) {
        return $http.post(serviceBase + 'api/budget/Delete/' + id).then(function (response) {
            return response;
        });
    };
    // **********************************************************************************************************************//
    //                                                        ITEMS
    // **********************************************************************************************************************//
    vm.getItem = function (id) {
        return $http.get(serviceBase + 'api/item/Single/' + id).then(function (response) {
            vm.itemModel = response.data;
        });
    };

    function getItemsById(id) {
        return $http.post(serviceBase + 'api/item/All/' + id)
            .then(
                (response) => {
                    vm.items = response.data;
                });
    };
    vm.getItemsById = getItemsById;

    vm.createItem = function (id) {
        vm.itemModel.BudgetId = id;
        return $http.post(serviceBase + 'api/item/Create/', vm.itemModel).then(function (response) {
            return response;
        });
    };

    vm.editItem = function () {
        return $http.post(serviceBase + 'api/item/Edit/', vm.itemModel).then(function (response) {
            return response;
        });
    };

    vm.deleteItem = function (id) {
        return $http.post(serviceBase + 'api/item/Delete/' + id).then(function (response) {
            return response;
        });
    };

    vm.getBudgets();
}]);

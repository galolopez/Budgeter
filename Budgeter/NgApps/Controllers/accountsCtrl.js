'use strict';
// instances of requests / parameters that meet those requests, passed into callback function - angular's way of delivering info. 
angular.module('BudgetApp').controller('accountsCtrl', ['$http', '$state', 'authSvc',
    function ($http, $state, authSvc, $uibModal, $log, $scope) {

    var self = this;
    self.accts = [];
    self.transactions = [];
    self.categories = [];
    self.account = '';
    self.message = '';
    self.currentAccountId = '';
    self.currentTransactionId = '';
    self.demo = true;
    self.model = {
        Name: '',
        Balance: '',
        Archived: '',
        RecAmount: ''
    }
    self.transModel = {
        Id: '',
        Name: '',
        Amount: '',
        RecAmount: '',
        Date: '',
        Description: '',
        Status: '',
        AccountId: '',
        CategoryId: ''
    }
    // **********************************************************************************************************************//
    //                                                        ACCOUNTS
    // **********************************************************************************************************************//
    self.getA = function (id) {
        if (authSvc.household) {
            self.demo = false;
        }
        else {
            // display user message - unable to post
            self.message = "Use 'Create Account' to create an account in the household and start posting data."
            self.demo = true;
        }
        $http({
            method: 'GET',
            url: authSvc.serviceBase + '/api/accounts',
            headers: {  // load this header info for the first request and it applies to all others in the controller
                'Authorization': authSvc.authentication.userName + '/token:' + authSvc.authentication.token,
                'Usename': authSvc.authentication.userName,
                'Household': authSvc.authentication.householdId
            }
        }).then(function (response) {
            self.acccount = response.data;
        });
    };

    self.getAccount = function (id) {
        self.currentAccountId = id;
        return $http.get(serviceBase + 'api/accounts/Single/' + id).then(function (response) {
            self.model = response.data;
        });
    };

    self.getAccounts = function () {
        return $http.get(authSvc.serviceBase + 'api/accounts/All/' + authSvc.authentication.householdId).then(function (response) {
            self.accts = response.data;
        });
    };

    self.createAccount = function () {
        self.model.householdId = authSvc.authentication.householdId;
        return $http.post(authSvc.serviceBase + 'api/accounts/Create/', self.model).then(function (response) {
            return response;
        });
    };

    self.editAccount = function () {
        return $http.post(authSvc.serviceBase + 'api/accounts/Edit/', self.model).then(function (response) {
            return response;
        });
    };

    self.deleteAccount = function (id) {
        return $http.post(authSvc.serviceBase + 'api/accounts/Delete/' + id).then(function (response) {
            return response;
        });
    };

    // **********************************************************************************************************************//
    //                                                        TRANSACTIONS
    // **********************************************************************************************************************//
    self.getTransaction = function (id) {
        self.currentTransactionId = id;
        return $http.get(authSvc.serviceBase + 'api/transaction/Single/' + id).then(function (response) {
            self.transModel = response.data;
        });
    };

    self.getTransactions = function (id) {
        return $http.get(authSvc.serviceBase + 'api/transaction/ByAccount/' + id).then(function (response) {
            self.transactions = response.data;
        });
    };

    self.createTransaction = function (id) {
        self.transModel.AccountId = id;
        return $http.post(serviceBase + 'api/transaction/Create/', self.transModel).then(function (response) {
            return response;
        });
    };

    self.getCategories = function () {
        return $http.get(serviceBase + 'api/category/All/' + authSvc.authentication.householdId).then(function (response) {
            self.categories = response.data;
        });
    };

    self.editTransaction = function () {
        return $http.post(serviceBase + 'api/transaction/Edit/', self.transModel).then(function (response) {
            return response;
        });
    };

    self.deleteTransaction = function (id) {
        return $http.post(serviceBase + 'api/transaction/Delete/' + id).then(function (response) {
            return response;
        });
    };

    self.getAccounts();
    self.getTransactions();
    self.getCategories();
}]);

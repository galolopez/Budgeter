'use strict';
// instances of requests / parameters that meet those requests, passed into callback function - angular's way of delivering info. 
angular.module('BudgetApp').controller('accountsCtrl', ['$http', '$state', 'authSvc', function ($http, $state, authSvc, $uibModal, $log) {

    var self = this;
    self.accts = [];
    self.account = '';
    self.message = '';
    self.demo = true;
    self.model = {
        Name: '',
        Balance: ''
    }

    self.getAccount = function () {
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
            self.acccounts = response.data;
        });
    };

    // get list of accounts
    self.getAccounts = function () {
        return $http.get(authSvc.serviceBase + '/api/accounts/All/' + authSvc.authentication.householdId).then(function (response) {
            self.accts = response.data;
        });
    };

    self.createAccount = function () {
        self.model.householdId = authSvc.authentication.householdId;
        return $http.post(authSvc.serviceBase + 'api/accounts/Create', self.model).then(function (response) {
            return response;
        });
    };

    self.editAccount = function (id) {
        return $http.post(authSvc.serviceBase + 'api/accounts/Edit', self.model).then(function (response) {
            return response;
        });
    };

    self.getAccounts();
}]);

'use strict';
angular.module('BudgetApp').controller('itemCtrl', ['$http', '$state', 'authSvc', function ($http, $state, authSvc) {

    var self = this;
    self.items = [];
    self.model = {
        HouseholdId: '',
        Name: '',
        Id: ''
    }

    self.getItem = function () {
        if (authSvc.household) {
            $http({
                method: 'GET',
                url: authSvc.serviceBase + '/api/item',
                headers: {  // load this header info for the first request and it applies to all others in the controller
                    'Authorization': authSvc.authentication.userName + '/token:' + authSvc.authentication.token,
                    'Usename': authSvc.authentication.userName,
                    'Household': authSvc.authentication.householdId
                }
            }).then(function (response) {
                self.values = response.data;
            });
        }
        else {
            // display user message - unable to post
            self.message = "Use 'Create Household' to create a household and start posting data."
        }
    };

    self.getItems = function (id) {
        return $http.get(authSvc.serviceBase + '/api/item/All/' + id).then(function (response) {
            self.items = response.data;
        });
    };
}]);

'use strict';
angular.module('BudgetApp').controller('homeCtrl', ['$http', '$state', 'authSvc', function ($http, $state, authSvc) {

    var self = this;
    self.members = [];
    self.household = '';
    self.householdName = '';
    self.model = {
        HouseholdId: '',
        Name: ''
    }

    self.getHousehold = function () {
        if (authSvc.household) {
            $http({
                method: 'GET',
                url: authSvc.serviceBase + '/api/household',
                headers: {  // load this header info for the first request and it applies to all others in the controller
                    'Authorization': authSvc.authentication.userName + '/token:' + authSvc.authentication.token,
                    'Username': authSvc.authentication.userName,
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

    self.getHouseholdNameById = function () {
        return $http.get(authSvc.serviceBase + '/api/household/Name/' + authSvc.authentication.householdId).then(function (response) {
            self.householdName = response.data;
        });
    };

    self.getHouseholdMembers = function () {
        return $http.get(authSvc.serviceBase + '/api/household/Members/' + authSvc.authentication.householdId).then(function (response) {
            self.members = response.data;
        });
    };

    self.createHousehold = function () {
        $http({
            method: 'Post',
            url: authSvc.serviceBase + '/api/household/Create',
            headers: {
                'Authorization': authSvc.authentication.userName + '/token:' + authSvc.authentication.token,
                'Username': authSvc.authentication.userName,
                'Household': authSvc.authentication.householdId
        }
        }).then(function (response) {
            //authSvc.authentication.householdId = response.data;
            self.model = response.data;
        });
    };
}]);

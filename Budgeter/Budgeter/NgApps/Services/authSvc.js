'use strict';
angular.module('BudgetApp')
    .factory('authSvc', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var authServiceFactory = {};

        var _authentication = {
            isAuth: false,
            userName: ""
        };

        var _saveRegistration = function (registration) {

            _logout();

            return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
                return response;
            });
        };

        var _login = function (loginData) {
            // This login function only works for the user's username, not e-mail. 

            var data = "grant_type=password&username=" + loginData.Username + "&password=" + loginData.Password;
        
            var deferred = $q.defer();

            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                _authentication.isAuth = true;
                _authentication.userName = response.userName;
                _authentication.token = response.access_token;
                _authentication.firstName = response.firstName;
                _authentication.lastName = response.lastName;
			    _authentication.email = response.email;
                _authentication.roles = response.roles;
                _authentication.householdId = response.householdId;
                _authentication.refreshToken = response.refresh_token;

                localStorageService.set('authorizationData', _authentication);

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _logout = function () {

            localStorageService.remove('authorizationData');

            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.firstName = "";
            _authentication.lastName = "";
		    _authentication.email = "";
            _authentication.token = "";
            _authentication.roles = [];
            _authentication.householdId = "";
            _authentication.refreshToken = "";

        };

        var _fillAuthData = function () {

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuth = true;
                _authentication.userName = authData.userName;
                _authentication.firstName = authData.firstName;
                _authentication.lastName = authData.lastName;
			    _authentication.email = authData.email;
                _authentication.token = authData.token;
                _authentication.roles = authData.roles;
                _authentication.householdId = authData.householdId;
                _authentication.refreshToken = authData.refreshToken;
            }

        };

        var _refreshToken = function () {
            var deferred = $q.defer();

            var authData = localStorageService.get('authorizationData');

            if (authData) {

                var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken;

                localStorageService.remove('authorizationData');

                $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                    localStorageService.set('authorizationData', {
                        token: response.access_token, userName: response.userName, email: response.email, refreshToken: response.refresh_token,
                        firstName: response.firstName, lastName: response.lastName, roles: response.roles, householdId: response.householdId});


                    deferred.resolve(response);

                }).error(function (err, status) {
                    _logout();
                    deferred.reject(err);
                });
            }

            return deferred.promise;
        };

        authServiceFactory.register = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logout = _logout;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;
        authServiceFactory.refresh = _refreshToken;
        authServiceFactory.serviceBase = serviceBase;

        return authServiceFactory;
    }])
    .factory('homeSvc', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var homeServiceFactory = {};

        var getValues = function () {
            return $http.get(serviceBase + '/api/values').then(function (response) {
                return response.data;
            });
        };

        var getValue = function (id) {
            return $http.get(serviceBase + '/api/values/' + id).then(function (response) {
                return response.data;
            });
        };

        return homeServiceFactory;
    }]);

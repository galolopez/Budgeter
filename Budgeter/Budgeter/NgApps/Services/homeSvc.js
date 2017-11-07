//'use strict';
//angular.module('app').factory('homeSvc', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

//    var serviceBase = ngAuthSettings.apiServiceBaseUri;
//    var homeServiceFactory = {};

//    var getValues = function () {
//        return $http.get(serviceBase + '/api/values').then(function (response) {
//            return response.data;
//        });
//    };

//    var getValue = function (id) {
//        return $http.get(serviceBase + '/api/values/' + id).then(function (response) {
//            return response.data;
//        });
//    };
    
//    return homeServiceFactory;
//}]);
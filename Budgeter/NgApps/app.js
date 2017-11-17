// declare the Angular CarFinder module - module is the application

// dependency injection allows to inject specific angular services. But only inject what we need: "[]"
// angular.module('BudgetApp', ['ui.bootstrap', 'chart.js']);
var app = angular.module('BudgetApp', ['ui.bootstrap', 'ui.router', 'LocalStorageModule']);

app.config(function ($stateProvider, $urlRouterProvider) {
    //
    // For any unmatched url, redirect to /state1
    $urlRouterProvider.otherwise("/");
    //
    // Now set up the states
    $stateProvider        
        .state('accounts', {
            url: "/accounts",
            templateUrl: "/NgApps/Templates/accounts.html",
            controller: "accountsCtrl as accounts"
        })
        .state('budget', {
            url: "/budget",
            templateUrl: "/NgApps/Templates/budget.html",
            controller: "budgetCtrl as budget"
        })
        .state('categories', {
            url: "/categories",
            templateUrl: "/NgApps/Templates/categories.html",
            controller: "categoriesCtrl as categories"
        })
        .state('login', {
            url: "/login",
            templateUrl: "/NgApps/Templates/login.html",
            controller: "loginCtrl as login"
        })
        .state('home', {
            url: "/home",
            templateUrl: "/NgApps/Templates/home.html",
            controller: "homeCtrl as home"
        })
        .state('register', {
            url: "/register",
            templateUrl: "/NgApps/Templates/register.html",
            controller: "registerCtrl as register"
        })
        .state('default', {
            url: "/",
            templateUrl: "",
        });
});

var serviceBase = 'http://localhost:63396/';

app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorSvc');
});

app.run(['authSvc', function (authService) {
    authService.fillAuthData();
}]);


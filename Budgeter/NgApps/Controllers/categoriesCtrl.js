'use strict';
angular.module('BudgetApp').controller('categoriesCtrl', ['$http', '$state', 'authSvc', function ($http, $state, authSvc) {

    var vm = this;

    vm.categories = [];
    vm.category = '';

    vm.categoryModel = {
        Name: '',
        ExpenseTF: '',
        Id: ''
    };

    function activate() {
        vm.getCategories();
    };
    vm.activate = activate;

    function getCategory(id) {
        return $http.get(serviceBase + 'api/category/Single/' + id)
            .then(
                (response) => {
                    vm.categoryModel = response.data;
                });
    };
    vm.getCategory = getCategory;

    function getCategories() {
        return $http.get(serviceBase + 'api/category/All/' + authSvc.authentication.householdId)
            .then(
                (response) => {
                    vm.categories = response.data;
                });
    };
    vm.getCategories = getCategories;
    
    function createCategory() {
        return $http.post(serviceBase + 'api/category/Create', vm.categoryModel)
            .then(
                (response) => {
                    return response;
                });
     };
     vm.createCategory = createCategory;

     function editCategory() {
         return $http.post(serviceBase + 'api/category/Edit/', vm.categoryModel)
             .then(
                (response) => {
                    return response;
                });
    };
    vm.editCategory = editCategory;

    function deleteCategory(id) {
        return $http.post(serviceBase + 'api/category/Delete/' + id)
            .then(
                (response) => {
                    return response;
                });
    };
    vm.deleteCategory = deleteCategory;

    vm.activate();
}]);
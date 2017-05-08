import { MainController } from './MainController';
import { MainService } from './MainService';

import './Pages/PagesModule';
import './Common/CommonModule';

import authorizeTemplate from './Pages/Authorize/AuthorizeView.html';
import errorTemplate from './Pages/Error/ErrorView.html';
import profileTemplate from './Pages/Profile/ProfileView.html'
import editCategoryTemplate from './Pages/EditCategory/EditCategoryView.html';
import editProductTemplate from './Pages/EditProduct/EditProductView.html';
import homeTemplate from './Pages/Home/HomeView.html';

angular.module('main',
    [
        'ngRoute',
        'ngAnimate',
        'pages',
        'common'
    ])
    .config(($routeProvider: ng.route.IRouteProvider) => {
        $routeProvider.when('/authorize',
            <ng.route.IRoute>
            {
                template: authorizeTemplate,
                controller: 'authorizeController',
                controllerAs: 'ctrl'
            });

        $routeProvider.when('/error/:status/:message',
            <ng.route.IRoute>
            {
                template: errorTemplate,
                controller: 'errorController',
                controllerAs: 'ctrl'
            });

        $routeProvider.when('/profile',
            <ng.route.IRoute>
            {
                template: profileTemplate,
                controller: 'profileController',
                controllerAs: 'ctrl'
            });

        $routeProvider.when('/category/edit/:id',
            <ng.route.IRoute>
            {
                template: editCategoryTemplate,
                controller: 'editCategoryController',
                controllerAs: 'ctrl'
            });

        $routeProvider.when('/product/edit/:productId/:categoryId',
            <ng.route.IRoute>
            {
                template: editProductTemplate,
                controller: 'editProductController',
                controllerAs: 'ctrl'
            });

        $routeProvider.when('/',
            <ng.route.IRoute>
            {
                template: homeTemplate,
                controller: 'homeController',
                controllerAs: 'ctrl'
            });

        $routeProvider.when('/home',
            <ng.route.IRoute>
            {
                template: homeTemplate,
                controller: 'homeController',
                controllerAs: 'ctrl'
            });
    })
    .controller('mainController', MainController)
    .service('mainService', MainService);
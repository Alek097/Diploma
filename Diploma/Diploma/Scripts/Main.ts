import { MainController } from './MainController';
import { MainService } from './MainService';

import './Pages/PagesModule';
import './Common/CommonModule';

import authorizeTemplate from './Pages/Authorize/AuthorizeView.html';
import errorTemplate from './Pages/Error/ErrorView.html';
import profileTemplate from './Pages/Profile/ProfileView.html'

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
    })
    .controller('mainController', MainController)
    .service('mainService', MainService);
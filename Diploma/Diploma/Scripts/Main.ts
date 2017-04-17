import { MainController } from './MainController';
import { MainService } from './MainService';

import './Pages/PagesModule';
import './Common/CommonModule';

import authorizeTemplates from "./Pages/Authorize/AuthorizeView.html";

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
                template: authorizeTemplates,
                controller: 'authorizeController',
                controllerAs: 'ctrl'
            });
    })
    .controller('mainController', MainController)
    .service('mainService', MainService);
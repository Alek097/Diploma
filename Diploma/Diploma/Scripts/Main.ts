import "./Pages/PagesModule";

import authorizeTemplates from "./Pages/Authorize/AuthorizeView.html";

angular.module('main',
    [
        'ngRoute',
        'ngAnimate',
        'pages'
    ])
    .config(($routeProvider: ng.route.IRouteProvider) => {
        $routeProvider.when('/authorize',
            <ng.route.IRoute>
            {
                template: authorizeTemplates,
                controller: 'authorizeController',
                controllerAs: 'ctrl'
            });
    });
import { HomeService } from './HomeService';
import { HomeController } from './HomeController';

angular.module('home',
    [
    ])
    .service('homeService', HomeService)
    .controller('homeController', HomeController);
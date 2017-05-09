import { BasketController } from './BasketController';
import { BasketService } from './BasketService';

angular.module('basket',
    [
    ])
    .controller('basketController', BasketController)
    .service('basketService', BasketService);
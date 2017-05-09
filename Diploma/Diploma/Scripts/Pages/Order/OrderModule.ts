import { OrderService } from './OrderService';
import { OrderController } from './OrderController';

angular.module('order',
    [
    ])
    .service('orderService', OrderService)
    .controller('orderController', OrderController);
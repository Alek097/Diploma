import { EditProductService } from './EditProductService';
import { EditProductController } from './EditProductController';

angular.module('editProduct',
    [
    ])
    .service('editProductService', EditProductService)
    .controller('editProductController', EditProductController);
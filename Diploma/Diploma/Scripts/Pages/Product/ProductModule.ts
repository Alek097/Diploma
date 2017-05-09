import { ProductService } from './ProductService';
import { ProductController } from './ProductController';

angular.module('product',
    [
    ])
    .service('productService', ProductService)
    .controller('productController', ProductController);
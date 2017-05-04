import { EditCategoryService } from './EditCategoryService';
import { EditCategoryController } from './EditCategoryController';

angular.module('editCategory',
    [
    ])
    .service('editCategoryService', EditCategoryService)
    .controller('editCategoryController', EditCategoryController);
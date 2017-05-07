import { EditCategoryModalController } from './EditCategoryModalController';
import { EditCategoryModalService } from './EditCategoryModalService';

angular.module('editCategoryModal',
    [
    ])
    .controller('editCategoryModalController', EditCategoryModalController)
    .service('editCategoryModalService', EditCategoryModalService);
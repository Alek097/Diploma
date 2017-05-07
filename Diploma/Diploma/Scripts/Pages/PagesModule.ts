import './Authorize/AuthorizeModule';
import './Error/ErrorModule';
import './Profile/ProfileModule';
import './EditCategory/EditCategoryModule';
import './EditProduct/EditProductModule';

angular.module('pages',
    [
        'authorize',
        'error',
        'profile',
        'editCategory',
        'editProduct'
    ]);
import './Authorize/AuthorizeModule';
import './Error/ErrorModule';
import './Profile/ProfileModule';
import './EditCategory/EditCategoryModule';
import './EditProduct/EditProductModule';
import './Home/HomeModule';
import './Product/ProductModule';

angular.module('pages',
    [
        'authorize',
        'error',
        'profile',
        'editCategory',
        'editProduct',
        'home',
        'product'
    ]);
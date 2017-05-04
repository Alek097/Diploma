import './Authorize/AuthorizeModule';
import './Error/ErrorModule';
import './Profile/ProfileModule'
import './EditCategory/EditCategoryModule'

angular.module('pages',
    [
        'authorize',
        'error',
        'profile',
        'editCategory'
    ]);
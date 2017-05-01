import './Authorize/AuthorizeModule';
import './Error/ErrorModule';
import './Profile/ProfileModule'

angular.module('pages',
    [
        'authorize',
        'error',
        'profile',
    ]);
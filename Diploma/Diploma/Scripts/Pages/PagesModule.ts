import './Authorize/AuthorizeModule';
import './Error/ErrorModule';

angular.module('pages',
    [
        'authorize',
        'error',
    ]);
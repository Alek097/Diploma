import { AuthorizeController } from './AuthorizeController';
import { AuthorizeService } from './AuthorizeService';

angular.module('authorize',
    [

    ])
    .controller('authorizeController', AuthorizeController)
    .service('authorizeService', AuthorizeService);
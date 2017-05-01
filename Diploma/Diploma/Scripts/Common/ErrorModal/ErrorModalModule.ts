import { ErrorModalController } from './ErrorModalController';
import { ErrorModalService } from './ErrorModalService';

angular.module('errorModal',
    [
    ])
    .controller('errorModalController', ErrorModalController)
    .service('errorModalService', ErrorModalService);
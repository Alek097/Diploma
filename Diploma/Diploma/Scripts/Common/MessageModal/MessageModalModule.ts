import { MessageModalController } from './MessageModalController';
import { MessageModalService } from './MessageModalService';

angular.module('messageModal',
    [

    ])
    .controller('messageModalController', MessageModalController)
    .service('messageModalService', MessageModalService);
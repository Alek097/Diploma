import { EditAddressModalController } from './EditAddressModalController';
import { EditAddressModalService } from './EditAddressModalService';

angular.module('editAddressModal',
    [
    ])
    .controller('editAddressModalController', EditAddressModalController)
    .service('editAddressModalService', EditAddressModalService);
import { ProfileService } from './ProfileService';
import { ProfileController } from './ProfileController';
import './EditAddressModal/EditAddressModalModule';

angular.module('profile',
    [
        'editAddressModal'
    ])
    .controller('profileController', ProfileController)
    .service('profileService', ProfileService);
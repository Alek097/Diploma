import { ProfileService } from './ProfileService';
import { ProfileController } from './ProfileController';

import './EditAddressModal/EditAddressModalModule';
import './EditCategoryModal/EditCategoryModalModule';

angular.module('profile',
    [
        'editAddressModal',
        'editCategoryModal'
    ])
    .controller('profileController', ProfileController)
    .service('profileService', ProfileService);
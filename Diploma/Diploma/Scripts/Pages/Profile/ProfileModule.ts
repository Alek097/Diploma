import { ProfileService } from './ProfileService';
import { ProfileController } from './ProfileController';

angular.module('profile',
    [
    ])
    .controller('profileController', ProfileController)
    .service('profileService', ProfileService);
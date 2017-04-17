import { ModalWindowService } from './ModalWindowService';

angular.module('modalWindow',
    [
    ])
    .service('modalWindowService', ModalWindowService);

import modalTemplate from './ModalWindowTemplate.html';

$('body').append(modalTemplate);
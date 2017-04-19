import './ModalWindow/ModalWindowModule';
import './ErrorModal/ErrorModalModule';
import './WaitModal/WaitModalModule';

angular.module('common',
    [
        'modalWindow',
        'errorModal',
        'waitModal'
    ]);
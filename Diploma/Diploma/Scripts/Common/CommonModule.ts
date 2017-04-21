import './ModalWindow/ModalWindowModule';
import './ErrorModal/ErrorModalModule';
import './WaitModal/WaitModalModule';
import './MessageModal/MessageModalModule';

angular.module('common',
    [
        'modalWindow',
        'errorModal',
        'waitModal',
        'messageModal'
    ]);
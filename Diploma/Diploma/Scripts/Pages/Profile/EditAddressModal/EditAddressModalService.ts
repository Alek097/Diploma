import { ModalWindowService } from '../../../Common/ModalWindow/ModalWindowService';
import { Address } from '../../../Common/Models/Address';
import { ModalOptions } from '../../../Core/ModalOptions';

import editAddressModalTemplate from './EditAddressModalView.html';

export class EditAddressModalService {
    public static $inject: string[] = [
        'modalWindowService'
    ];
    constructor(
        private _modalWindowService: ModalWindowService
    ) {
    }

    public show(address: Address, callback: (address: Address, isOk: boolean) => void): void {
        this._modalWindowService.show(
            <ModalOptions>
            {
                controller: 'editAddressModalController',
                controllerAs: 'editAddressCtrl',
                template: editAddressModalTemplate,
                inject: {
                    address: address,
                    callback: callback,
                    closeModal: () => this._modalWindowService.close()
                }
            },
            () => {
                callback(address, false);
            });
    }

    public close(): void {
        this._modalWindowService.close();
    }
}
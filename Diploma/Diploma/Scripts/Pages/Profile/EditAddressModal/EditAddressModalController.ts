import { Address } from '../../../Common/Models/Address';
import { ProfileService } from '../ProfileService';
import { ErrorModalService } from '../../../Common/ErrorModal/ErrorModalService';

export class EditAddressModalController {
    public static $inject: string[] = [
        '$scope',
        'profileService',
        'errorModalService'
    ]

    private _isEdit: boolean = false;

    constructor(
        private _scope: any,
        private _profileService: ProfileService,
        private _errorModalService: ErrorModalService) {
        let address: Address = _scope.address;

        this._scope.newAddress = new Address();
        this._scope.isWaitEdit = false;

        if (address.id == null) {
            this._scope.isWait = true;

            this._isEdit = false;

            this._profileService.getAddressId()
                .then((response) => {
                    let address: Address = new Address();
                    address.id = response.data;

                    this._setAddress(address);

                    this._scope.isWait = false;
                },
                (responce) => {
                    this._errorModalService.show(responce.status, 'Неизвестная ошибка. Мы уже знаем о ней всё.');
                    this._scope.isWait = false;
                })
        }
        else {
            this._isEdit = true;
            this._setAddress(this._scope.address);
        }
    }

    public ok(): void {
        this._scope.isWaitEdit = true;

        if (this._isEdit) {
            this._profileService.editAddress(this._scope.newAddress)
                .then((response) => {
                    if (response.data.isSuccess) {
                        this._scope.callback(response.data.value, true);
                        this._scope.closeModal();
                    }
                    else {
                        this._errorModalService.show(response.data.status, response.data.message);
                    }

                    this._scope.isWaitEdit = false;
                },
                (response) => {
                    this._errorModalService.show(response.status, 'Неизвестная ошибка. Мы уже знаем о ней всё.');
                    this._scope.isWaitEdit = false;
                });
        }
        else
        {
            this._profileService.addAddress(this._scope.newAddress)
                .then((response) => {
                    if (response.data.isSuccess) {
                        this._scope.callback(response.data.value, true);
                        this._scope.closeModal();
                    }
                    else {
                        this._errorModalService.show(response.data.status, response.data.message);
                    }

                    this._scope.isWaitEdit = false;
                },
                (response) => {
                    this._errorModalService.show(response.status, 'Неизвестная ошибка. Мы уже знаем о ней всё.');
                    this._scope.isWaitEdit = false;
                });
        }
    }

    public close(): void {
        this._scope.callback(this._scope.address, false);

        this._scope.closeModal();
    }

    private _setAddress(address: Address): void {
        for (let i in address) {
            this._scope.newAddress[i] = address[i];
        }
    }
}
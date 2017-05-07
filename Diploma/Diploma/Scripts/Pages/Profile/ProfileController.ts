import { ProfileService } from './ProfileService';
import { User } from '../../Common/Models/User';
import { MainService } from '../../MainService';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';
import { MessageModalService } from '../../Common/MessageModal/MessageModalService';
import { Global } from '../../Core/Global';
import { EditAddressModalService } from './EditAddressModal/EditAddressModalService';
import { Address } from '../../Common/Models/Address';
import { Category } from '../../Common/Models/Category';
import { EditCategoryModalService } from './EditCategoryModal/EditCategoryModalService';

export class ProfileController {
    public static $inject: string[] =
    [
        'profileService',
        'mainService',
        'waitModalService',
        'errorModalService',
        '$scope',
        'messageModalService',
        'editAddressModalService',
        'editCategoryModalService'
    ];

    public oldUser: User;

    public newUser: User;

    public categories: Category[] = [];

    public isEditEmail: boolean = false;
    public showConfirmCard: boolean = false;
    public isWaitCategories: boolean = false;

    constructor(
        private _profileService: ProfileService,
        mainService: MainService,
        waitModalService: WaitModalService,
        private _errorModalService: ErrorModalService,
        private _scope: any,
        private _messageModalService: MessageModalService,
        private _editAddressModalService: EditAddressModalService,
        private _editCategoryModalService: EditCategoryModalService
    ) {
        waitModalService.show();

        mainService.getUser()
            .then((response) => {

                if (response.data.value.isAuthorize) {
                    this.oldUser = response.data.value;

                    this._automapUser();

                    this._setScope();

                    if (response.data.value.role !== 'User') {

                        this.isWaitCategories = true;

                        this._profileService.getCategoriesNames()
                            .then((response) => {
                                if (response.data.isSuccess) {
                                    this.categories = response.data.value;
                                }
                                else {
                                    this._errorModalService.show(response.data.status, 'Ошибка сервера. Повторите попытку позже.');
                                }

                                this.isWaitCategories = false;
                                waitModalService.close();
                            },
                            (response) => {
                                this._errorModalService.show(response.status, 'Ошибка сервера. Повторите попытку позже.');

                                this.isWaitCategories = false;
                                waitModalService.close();
                            });
                    }
                    else {
                        this._errorModalService.show(response.data.status, 'Ошибка сервера. Повторите попытку позже.');
                        waitModalService.close();
                    }
                }
                else {
                    waitModalService.close();
                    location.href = '/';
                }
            },
            (response) => {
                this._errorModalService.show(response.status, 'Ошибка сервера. Повторите попытку позже.');
                location.href = '/';

                waitModalService.close();
            });
    }

    public dismissSendEditEmailConfirmMessage(): void {
        this.isEditEmail = false;

        this._setScope();
        this._automapUser();
    }

    public acceptSendEditEmailConfirmMessage(newEmail: string): void {
        this.newUser.email = newEmail;

        this._profileService.sendConfirmMessage(newEmail)
            .then((responce) => {
                if (responce.data.isSuccess) {
                    this.showConfirmCard = true;
                }
                else {
                    this._messageModalService.show(responce.data.message);
                }
            },
            (responce) => {
                this._errorModalService.show(responce.status, 'Неизвестная ошибка. Но мы уже о ней знаем');
            });
    }

    public acceptEditEmail(newEmail: string, code: string): void {
        this._profileService.editEmail(newEmail, code)
            .then((responce) => {
                if (responce.data.isSuccess) {
                    this.oldUser.email = responce.data.value;
                    Global.changeUser(this.oldUser);
                }
                else {
                    this._errorModalService.show(responce.data.status, responce.data.message);
                }
                this.dismissEditEmail();
            },
            (responce) => {
                this._errorModalService.show(responce.status, 'Неизвестная ошибка. Но мы уже о ней знаем');
                this.dismissEditEmail();
            });
    }

    public dismissEditEmail(): void {
        this.dismissSendEditEmailConfirmMessage();

        this.showConfirmCard = false;
    }

    public addAddrress(): void {
        let address = new Address();

        this._editAddressModalService.show(address, (address: Address, isOk: boolean) => {
            if (isOk) {
                this.oldUser.addresses.push(address);
            }
        });
    }

    public editAddress(id: string): void {
        this.oldUser.addresses.forEach((value, i) => {
            if (value.id == id) {
                this._editAddressModalService.show(value, (address, isOk) => {
                    if (isOk) {
                        this.oldUser.addresses[i] = address;
                    }
                });
            }
        });
    }

    public deleteAddress(id: string): void {
        this._profileService.deleteAddress(id)
            .then((responce) => {
                for (var i = 0; i < this.oldUser.addresses.length; i++) {
                    if (this.oldUser.addresses[i].id == id) {
                        this.oldUser.addresses.splice(i, 1);
                        break;
                    }
                }
            },
            (responce) => {
                this._errorModalService.show(responce.status, 'Неизвестная ошибка. Но мы уже о ней знаем')
            });
    }

    public addCategory(): void {
        this._editCategoryModalService.show(new Category(), (category, isOk) => {
            if (isOk) {
                this.categories.push(category);
            }
        });
    }

    public deleteCategory(categoryId: string): void {
        this._profileService.deleteCategory(categoryId)
            .then((responce) => {
                for (var i = 0; i < this.categories.length; i++) {
                    if (this.categories[i].id == categoryId) {
                        this.categories.splice(i, 1);
                        break;
                    }
                }
            },
            (responce) => {
                this._errorModalService.show(responce.status, 'Неизвестная ошибка. Но мы уже о ней знаем')
            });
    }

    private _setScope(): void {
        this._scope.newEmail = this.oldUser.email;
    }

    private _automapUser(): void {
        this.newUser = new User();

        for (let i in this.oldUser) {
            this.newUser[i] = this.oldUser[i];

            if (this.newUser[i] instanceof Array || this.newUser[i] == null) {
                this.newUser[i] = null;
            }
        }
    }
}
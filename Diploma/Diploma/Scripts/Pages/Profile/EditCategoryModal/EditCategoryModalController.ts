import { Category } from '../../../Common/Models/Category';
import { ProfileService } from '../ProfileService';
import { ErrorModalService } from '../../../Common/ErrorModal/ErrorModalService';


export class EditCategoryModalController {
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
        let category: Category = this._scope.category;

        this._scope.newCategory = new Category();
        this._scope.isWaitEdit = false;

        if (category.id == null) {
            this._scope.isWait = true;

            this._isEdit = false;

            this._profileService.getId()
                .then((response) => {
                    let category: Category = new Category();
                    category.id = response.data;

                    this._setCategory(category);

                    this._scope.isWait = false;
                },
                (response) => {
                    this._errorModalService.show(response.status, 'Неизвестная ошибка. Мы уже знаем о ней всё.');
                    this._scope.isWait = false;
                });
        }
        else {
            this._isEdit = true;
            this._setCategory(this._scope.category);
        }
    }

    public ok(): void {
        this._scope.isWaitEdit = true;

        if (this._isEdit) {
            this._profileService.editCategory(this._scope.newCategory)
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
        else {
            this._profileService.addCategory(this._scope.newCategory)
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
        this._scope.callback(this._scope.category, false);

        this._scope.closeModal();
    }

    private _setCategory(category: Category): void {
        for (let i in category) {
            this._scope.newCategory[i] = category[i];
        }
    }
}
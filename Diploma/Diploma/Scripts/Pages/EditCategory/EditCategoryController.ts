import { EditCategoryService } from './EditCategoryService';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';
import { Category } from '../../Common/Models/Category';
import { EditCategoryModalService } from '../Profile/EditCategoryModal/EditCategoryModalService';
import { MainService } from '../../MainService';

export class EditCategoryController {
    public static $inject: string[] =
    [
        'editCategoryService',
        '$routeParams',
        'waitModalService',
        'errorModalService',
        'editCategoryModalService',
        'mainService',
        '$timeout'
    ]

    public category: Category = null;

    constructor(
        private _editCategoryService: EditCategoryService,
        params: ng.route.IRouteParamsService,
        private _waitModalService: WaitModalService,
        private _errorModalService: ErrorModalService,
        private _editCategoryModalService: EditCategoryModalService,
        mainService: MainService,
        private _timeout: ng.ITimeoutService) {
        let id: string = params['id'];

        this._waitModalService.show();

        mainService.getUser()
            .then(response => {
                if (response.data.isSuccess && response.data.value.role != 'User') {
                    this._editCategoryService.getCategory(id)
                        .then(response => {
                            if (response.data.isSuccess) {
                                this.category = response.data.value;
                                this._waitModalService.close();
                            }
                            else {
                                this._errorModalService.show(response.data.status, response.data.message);
                            }
                        },
                        response => {
                            this._errorModalService.show(response.status, 'Неизвестная ошибка. Но мы уже о ней знаем');
                        })
                }
                else {
                    this._errorModalService.show(response.data.status, response.data.message);
                    location.href = '/'
                }
            },
            response => {
                this._errorModalService.show(response.status, 'Неизвестная ошибка. Но мы уже о ней знаем');
                location.href = '/'
            })
    }

    public editCategory(): void {
        let category: Category = new Category();

        category.id = this.category.id;
        category.name = this.category.name;
        category.description = this.category.description;

        this._editCategoryModalService.show(category,
            (category, isOk: boolean) => {
                if (isOk) {
                    this.category.name = category.name;
                    this.category.description = category.description;
                }
            })
    }

    public deleteCategory(): void {
        this._editCategoryService.deleteCategory(this.category.id)
            .then(response => {
                if (response.data.isSuccess) {
                    location.href = '#!/profile';
                }
                else {
                    this._errorModalService.show(response.data.status, response.data.message);
                }
            },
            response => {
                this._errorModalService.show(response.status, 'Неизвестная ошибка. Но мы уже о ней знаем');
            });
    }

    public deleteProduct(id: string) {
        this._editCategoryService.deleteProduct(id)
            .then(response => {
                if (response.data.isSuccess) {
                    for (var i = 0; i < this.category.products.length; i++) {
                        if (this.category.products[i].id === id) {
                            this.category.products.splice(i, 1);

                            this._timeout();

                            break;
                        }
                    }
                }
                else {
                    this._errorModalService.show(response.data.status, response.data.message);
                }
            },
            response => {
                this._errorModalService.show(response.status, 'Неизвестная ошибка. Но мы уже о ней знаем');
            });
    }
}
import { EditCategoryService } from './EditCategoryService';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';
import { Category } from '../../Common/Models/Category';
import { EditCategoryModalService } from '../Profile/EditCategoryModal/EditCategoryModalService';

export class EditCategoryController {
    public static $inject: string[] =
    [
        'editCategoryService',
        '$routeParams',
        'waitModalService',
        'errorModalService',
        'editCategoryModalService'
    ]

    public category: Category = null;

    constructor(
        private _editCategoryService: EditCategoryService,
        params: ng.route.IRouteParamsService,
        private _waitModalService: WaitModalService,
        private _errorModalService: ErrorModalService,
        private _editCategoryModalService: EditCategoryModalService) {
        let id: string = params['id'];

        this._waitModalService.show();

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
}
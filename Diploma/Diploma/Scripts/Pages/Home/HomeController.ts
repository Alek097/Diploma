import { HomeService } from './HomeService';
import { Category } from '../../Common/Models/Category';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';

export class HomeController {
    public static $inject: string[] =
    [
        'homeService',
        'errorModalService',
        'waitModalService'
    ];

    public categories: Category[] = [];
    public category: Category = null;

    constructor(
        private _homeService: HomeService,
        private _errorModalService: ErrorModalService,
        private _waitModalService: WaitModalService) {

        this._waitModalService.show();

        this._homeService.getCategories()
            .then(response => {
                if (response.data.isSuccess) {
                    this.categories = response.data.value;

                    this._waitModalService.close();
                }
                else {
                    this._errorModalService.show(response.data.status, response.data.message);
                }
            },
            response => {
                this._errorModalService.show(response.status, 'Неизвестная ошибка.');
            })
    }

    public viewCategory(id: string): void {
        this._waitModalService.show();

        this._homeService.getCategoryById(id)
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
                this._errorModalService.show(response.status, 'Неизвестная ошибка.');
            });
    }
}
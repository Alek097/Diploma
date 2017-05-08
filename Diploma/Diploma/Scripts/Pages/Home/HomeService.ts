import { Category } from '../../Common/Models/Category';
import { ControllerResult } from '../../Core/ControllerResult';

export class HomeService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private _http: ng.IHttpService) { }

    public getCategories(): ng.IHttpPromise<ControllerResult<Category[]>> {
        return this._http.get('api/Category/GetCategories');
    }

    public getCategoryById(id: string): ng.IHttpPromise<ControllerResult<Category>> {
        return this._http.get('api/Category/GetCategoryById?id=' + id);
    }
}
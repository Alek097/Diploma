import { Category } from '../../Common/Models/Category';
import { ControllerResult } from '../../Core/ControllerResult';

export class EditCategoryService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private _http: ng.IHttpService) {

    }

    public getCategory(id: string): ng.IHttpPromise<ControllerResult<Category>> {
        return this._http.get('api/Category/GetCategoryById?id=' + id);
    }

    public deleteCategory(id: string): ng.IHttpPromise<ControllerResult<any>> {
        return this._http.get('api/Category/Delete?id=' + id);
    }
}
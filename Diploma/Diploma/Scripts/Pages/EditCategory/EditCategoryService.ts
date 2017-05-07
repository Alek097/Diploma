import { Category } from '../../Common/Models/Category';
import { ControllerResult } from '../../Core/ControllerResult';
import { Product } from '../../Common/Models/Product';

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

    public deleteProduct(id: string): ng.IHttpPromise<ControllerResult<Product>> {
        return this._http.delete('api/Category/DeleteProduct?productId=' + id);
    }
}
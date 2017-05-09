import { Product } from '../../Common/Models/Product';
import { ControllerResult } from '../../Core/ControllerResult';

export class ProductService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private _http: ng.IHttpService) {

    }

    public getProductById(id: string): ng.IHttpPromise<ControllerResult<Product>> {
        return this._http.get('api/Product/GetById?productId=' + id);
    }
}
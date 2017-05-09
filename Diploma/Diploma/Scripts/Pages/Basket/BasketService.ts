import { Product } from '../../Common/Models/Product';
import { ControllerResult } from '../../Core/ControllerResult';

export class BasketService {
    public static $inject: string[] =
    [
        '$http'
    ]

    constructor(
        private _http: ng.IHttpService) {
    }

    public addOrder(products: Product[]): ng.IHttpPromise<ControllerResult<string>> {
        return this._http.post('api/Order/Add', products);
    }
}
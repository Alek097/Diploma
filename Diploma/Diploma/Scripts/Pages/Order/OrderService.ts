import { Order } from '../../Common/Models/Order';
import { ControllerResult } from '../../Core/ControllerResult';

export class OrderService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private _http: ng.IHttpService) { }

    public getOrders(): ng.IHttpPromise<ControllerResult<Order[]>> {
        return this._http.get('api/Order/GetAll');
    }
}
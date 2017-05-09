import { OrderService } from './OrderService';
import { MainService } from '../../MainService';
import { Order } from '../../Common/Models/Order';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';

export class OrderController {
    public static $inject: string[] =
    [
        'orderService',
        'mainService',
        'waitModalService',
        'errorModalService'
    ];

    public orders: Order[] = [];

    constructor(
        private _orderService: OrderService,
        mainService: MainService,
        private _waitModalService: WaitModalService,
        private _errorModalService: ErrorModalService) {
        this._waitModalService.show();

        mainService.getUser()
            .then(response => {
                if (response.data.isSuccess && response.data.value.isAuthorize) {
                    this._orderService.getOrders()
                        .then(response => {
                            if (response.data.isSuccess) {
                                this.orders = response.data.value;
                                this._waitModalService.close();
                            }
                            else {
                                this._errorModalService.show(response.data.status, response.data.message);

                                setTimeout(() => { location.href = '/' }, 1500);
                            }
                        },
                        response => {
                            this._errorModalService.show(response.data.status, response.data.message);

                            setTimeout(() => { location.href = '/' }, 1500);
                        });
                }
                else {
                    location.href = '/';
                }
            },
            response => {
                location.href = '/';
            });
    }
}
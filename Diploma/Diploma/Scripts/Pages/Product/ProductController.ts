import { ProductService } from './ProductService';
import { Product } from '../../Common/Models/Product';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';
import { MessageModalService } from '../../Common/MessageModal/MessageModalService';
import { MainService } from '../../MainService';

export class ProductController {
    public static $inject: string[] =
    [
        'productService',
        'errorModalService',
        'waitModalService',
        '$routeParams',
        'messageModalService',
        'mainService'
    ];

    public product: Product = null;
    public isAuthorize: boolean = false;

    constructor(
        private _homeService: ProductService,
        private _errorModalService: ErrorModalService,
        private _waitModalService: WaitModalService,
        params: ng.route.IRouteParamsService,
        private _messageModalService: MessageModalService,
        mainService: MainService) {

        let id: string = params['id'];

        this._waitModalService.show();

        mainService.getUser()
            .then(response => {
                if (response.data.isSuccess && response.data.value.isAuthorize) {

                    this.isAuthorize = true;

                    this._homeService.getProductById(id)
                        .then(response => {
                            if (response.data.isSuccess) {
                                this.product = response.data.value;
                                this._waitModalService.close();
                            }
                            else {
                                this._errorModalService.show(response.data.status, response.data.message);
                            }
                        },
                        response => {
                            this._errorModalService.show(response.data.status, response.data.message);

                            setTimeout(1500, () => location.href = '/');
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

    public add(): void {
        let basket: Product[] = JSON.parse(localStorage.getItem('basket'));

        if (basket == null) {
            basket = [];
        }

        basket.push(this.product);

        localStorage.setItem('basket', JSON.stringify(basket));

        this._messageModalService.show('Товар ' + this.product.name + ' добавлен в корзину.', 'Товар добавлен.')
    }
}
import { Product } from '../../Common/Models/Product';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';
import { BasketService } from './BasketService';
import { MainService } from '../../MainService';

export class BasketController {
    public static $inject: string[] =
    [
        'errorModalService',
        'waitModalService',
        'basketService',
        'mainService'
    ];

    public products: Product[];

    constructor(
        private _errorModalService: ErrorModalService,
        private _waitModalService: WaitModalService,
        private _basketService: BasketService,
        mainService: MainService) {

        this._waitModalService.show();

        mainService.getUser()
            .then(response => {
                if (response.data.isSuccess && response.data.value.isAuthorize) {
                    this.products = JSON.parse(localStorage.getItem('basket'));
                    this._waitModalService.close();
                }
                else {
                    location.href = '/';
                }
            },
            response => {
                location.href = '/';
            });
    }

    public deleteProduct(id: string): void {
        for (var i = 0; i < this.products.length; i++) {
            if (this.products[i].id === id) {
                this.products.splice(i, 1);
                break;
            }
        }

        localStorage.setItem('basket', JSON.stringify(this.products));
    }

    public addOrder(): void {
        this._waitModalService.show();

        this._basketService.addOrder(this.products)
            .then(response => {
                if (response.data.isSuccess) {
                    this._waitModalService.close();

                    localStorage.removeItem('basket');

                    setTimeout(() => { location.href = '#!/Order/' }, 1500);
                }
                else {
                    this._errorModalService.show(response.data.status, response.data.value);
                    setTimeout(() => { location.href = '/' }, 1500);
                }
            },
            response => {
                this._errorModalService.show(response.status, 'Неизвестная ошибка, но мы уже знаем о ней всё.');
                setTimeout(() => { location.href = '/' }, 1500);
            });
    }
}
import { ControllerResult } from '../../Core/ControllerResult';
import { Product } from '../../Common/Models/Product';

export class EditProductService {
    public static $inject: string[] =
    [
        '$http'
    ]

    constructor(
        private _http: ng.IHttpService) {

    }

    public addProduct(product: Product, categoryId: string): ng.IHttpPromise<ControllerResult<any>> {
        return this._http.post('api/Product/Add?categoryId=' + categoryId, product);
    }

    public getProductById(productId: string): ng.IHttpPromise<ControllerResult<Product>> {
        return this._http.get('api/Product/GetById?productId=' + productId);
    }

    public editProduct(product: Product, categoryId: string): ng.IHttpPromise<ControllerResult<any>> {
        return this._http.post('api/Product/Edit?categoryId=' + categoryId, product);
    }

    public uploadCover(data: FormData): JQueryXHR {

        return $.ajax({
            type: "POST",
            url: '/api/Product/UploadCover',
            contentType: false,
            processData: false,
            data: data
        });
    }

    public uploadImages(data: FormData): JQueryXHR {

        return $.ajax({
            type: "POST",
            url: '/api/Product/UploadImages',
            contentType: false,
            processData: false,
            data: data
        });
    }
}
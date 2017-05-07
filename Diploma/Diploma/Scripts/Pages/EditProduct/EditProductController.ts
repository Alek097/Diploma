import { EditProductService } from './EditProductService';
import { MainService } from '../../MainService';
import { Product } from '../../Common/Models/Product';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';
import { ControllerResult } from '../../Core/ControllerResult';
import { Characteristic } from '../../Common/Models/Characteristic';
import { CharacteristicsGroup } from '../../Common/Models/CharacteristicsGroup';

export class EditProductController {

    private static nextId: number = 0;

    public static $inject: string[] =
    [
        'editProductService',
        '$routeParams',
        'mainService',
        'errorModalService',
        'waitModalService',
        '$timeout'
    ]

    public product: Product;

    private _categoryId: string;
    private _productId: string;

    private _isEdit: boolean = false;

    constructor(
        private _editProductService: EditProductService,
        params: ng.route.IRouteParamsService,
        mainService: MainService,
        private _errorModalService: ErrorModalService,
        private _waitModalService: WaitModalService,
        private _timeout: ng.ITimeoutService) {

        this._productId = params['productId'];
        this._categoryId = params['categoryId'];

        _waitModalService.show();

        mainService.getUser()
            .then(response => {
                if (response.data.isSuccess) {
                    if (this._productId === '*' && response.data.value.role != 'User') {
                        this.product = new Product();
                        _waitModalService.close();
                        this._isEdit = false;
                    }
                    else if (response.data.value.role == 'User') {
                        location.href = '/';
                        _waitModalService.close();
                        return;
                    }
                    else {
                        this._isEdit = true;

                        this._editProductService.getProductById(this._productId)
                            .then(response => {
                                if (response.data.isSuccess) {
                                    this.product = response.data.value;
                                    _waitModalService.close();
                                }
                                else {
                                    this._errorModalService.show(response.data.status, response.data.message);
                                    location.href = '/'
                                    return;
                                }
                            },
                            response => {
                                this._errorModalService.show(response.status, 'Неизвестная ошибка. Но мы уже о ней знаем');
                                location.href = '/'
                                return;
                            });
                    }
                }
                else {
                    this._errorModalService.show(response.data.status, response.data.message);
                    location.href = '/';
                    return;
                }
            },
            response => {
                this._errorModalService.show(response.status, 'Неизвестная ошибка. Но мы уже о ней знаем');
                location.href = '/';
            });
    }

    public uploadCover(): void {

        this._waitModalService.show();

        let cover: any = angular.element('#product--cover input');

        let data: FormData = new FormData();

        data.append('cover', cover[0].files[0]);

        angular.element('#product--cover input').remove();

        this._editProductService.uploadCover(data)
            .done((data: ControllerResult<string>) => {
                if (data.isSuccess) {
                    this.product.coverUrl = data.value;
                    this._waitModalService.close();
                    this._timeout();
                }
                else {
                    this._errorModalService.show(data.status, data.message);
                    angular.element('#product--cover div').append('<input type="file" multiple accept="image/*" name="cover" onchange="angular.element(this).controller().uploadCover()">');
                }
            })
            .fail(() => {
                this._errorModalService.show(500, 'Неизвестная ошибка. Но мы уже о ней знаем');
                angular.element('#product--cover div').append('<input type="file" multiple accept="image/*" name="cover" onchange="angular.element(this).controller().uploadCover()">');
                location.href = '/';
            });

    }

    public deleteCover(): void {
        angular.element('#product--cover div').append('<input type="file" multiple accept="image/*" name="cover" onchange="angular.element(this).controller().uploadCover()">');

        this.product.coverUrl = null;
    }

    public uploadImages(): void {
        this._waitModalService.show();
        let cover: any = angular.element('#product--images input');

        let data: FormData = new FormData();

        for (var i = 0; i < cover[0].files.length; i++) {
            data.append('image' + i, cover[0].files[i]);
        }

        angular.element('#product--images input').remove();

        this._editProductService.uploadImages(data)
            .done((data: ControllerResult<string[]>) => {
                if (data.isSuccess) {

                    for (let i of data.value) {
                        this.product.imagesUrl.push(i);
                    }

                    this._timeout();

                    this._waitModalService.close();
                }
                else {
                    this._errorModalService.show(data.status, data.message);
                    angular.element('#product--images div').append('<input type="file" multiple accept="image/*" name="images" onchange="angular.element(this).controller().uploadImages()">');
                }
            })
            .fail(() => {
                this._errorModalService.show(500, 'Неизвестная ошибка. Но мы уже о ней знаем');
                angular.element('#product--images div').append('<input type="file" multiple accept="image/*" name="images" onchange="angular.element(this).controller().uploadImages()">');
                location.href = '/';
            });
    }

    public deleteImage(imageUrl: string): void {
        for (var i = 0; i < this.product.imagesUrl.length; i++) {
            if (imageUrl === this.product.imagesUrl[i]) {
                this.product.imagesUrl.splice(i, 1);
                this._timeout();

                if (this.product.imagesUrl.length === 0) {
                    angular.element('#product--images div').append('<input type="file" multiple accept="image/*" name="images" onchange="angular.element(this).controller().uploadImages()">');
                }
                break;
            }
        }
    }

    public addCharacteristic(): void {
        this.product.characteristics.push(new Characteristic(EditProductController.nextId++));
    }

    public deleteCharacteristic(id: number): void {
        for (var i = 0; i < this.product.characteristics.length; i++) {
            if (this.product.characteristics[i].id === id) {
                this.product.characteristics.splice(i, 1);
                break;
            }
        }
    }

    public addCharacteristicGroup(): void {
        this.product.characteristicsGroups.push(new CharacteristicsGroup(EditProductController.nextId++));
    }

    public addCharacteristicInGroup(id: number): void {

        let characteristic: Characteristic = new Characteristic(EditProductController.nextId++);

        for (var i = 0; i < this.product.characteristicsGroups.length; i++) {
            if (this.product.characteristicsGroups[i].id == id) {
                this.product.characteristicsGroups[i].characteristics.push(characteristic);
                break;
            }
        }
    }

    public deleteCharacteristicFromGroup(idCharacteristic: number, idGroup: number): void {
        for (let group of this.product.characteristicsGroups) {
            if (group.id === idGroup) {
                for (var i = 0; i < group.characteristics.length; i++) {
                    if (group.characteristics[i].id === idCharacteristic) {
                        group.characteristics.splice(i, 1);
                        break;
                    }
                }
            }
            break;
        }
    }

    public deleteCharacteristicsGroup(id: number): void {
        for (var i = 0; i < this.product.characteristicsGroups.length; i++) {
            if (this.product.characteristicsGroups[i].id === id) {
                this.product.characteristicsGroups.splice(i, 1);
                break;
            }
        }
    }

    public save(): void {

        if (this._isEdit) {
            this._editProductService.editProduct(this.product, this._categoryId)
                .then(response => {
                    if (response.data.isSuccess) {
                        location.href = '#!/category/edit/' + this._categoryId
                    }
                    else {
                        this._errorModalService.show(response.data.status, response.data.message);

                        setTimeout(() => { location.href = '#!/category/edit/' + this._categoryId }, 1000);
                    }
                },
                response => {
                    this._errorModalService.show(response.data.status, response.data.message);

                    setTimeout(() => { location.href = '#!/category/edit/' + this._categoryId }, 1000);
                });
        }
        else {
            this._editProductService.addProduct(this.product, this._categoryId)
                .then(response => {
                    if (response.data.isSuccess) {
                        location.href = '#!/category/edit/' + this._categoryId
                    }
                    else {
                        this._errorModalService.show(response.data.status, response.data.message);

                        setTimeout(() => { location.href = '#!/category/edit/' + this._categoryId }, 1000);
                    }
                },
                response => {
                    this._errorModalService.show(response.data.status, response.data.message);

                    setTimeout(() => { location.href = '#!/category/edit/' + this._categoryId }, 1000);
                });
        }
    }
}
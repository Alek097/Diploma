import { User } from '../../Common/Models/User';
import { Address } from '../../Common/Models/Address';
import { Category } from '../../Common/Models/Category';
import { ControllerResult } from '../../Core/ControllerResult';

export class ProfileService {
    public static $inject: string[] =
    [
        '$http',
    ]

    constructor(
        private _http: ng.IHttpService
    ) {

    }

    public sendConfirmMessage(newEmail: string): ng.IHttpPromise<ControllerResult<any>> {
        return this._http.get('api/Profile/SendConfirmEditEmail?newEmail=' + newEmail);
    }

    public editEmail(newEmail: string, code: string): ng.IHttpPromise<ControllerResult<string>> {
        return this._http.get('api/Profile/EditEmail?newEmail=' + newEmail + '&code=' + code);
    }

    public addAddress(address: Address): ng.IHttpPromise<ControllerResult<Address>> {
        return this._http.post('api/Profile/AddAddress', address);
    }

    public deleteAddress(id: string): ng.IHttpPromise<ControllerResult<any>> {
        return this._http.get('api/Profile/DeleteAddress?id=' + id);
    }

    public editAddress(address: Address): ng.IHttpPromise<ControllerResult<Address>> {
        return this._http.post('api/Profile/EditAddress', address);
    }

    public getId(): ng.IHttpPromise<string> {
        return this._http.get('api/Profile/GetId');
    }

    public getCategoriesNames(): ng.IHttpPromise<ControllerResult<Category[]>> {
        return this._http.get('api/Category/GetNames');
    }

    public editCategory(category: Category): ng.IHttpPromise<ControllerResult<Category>> {
        return this._http.post('api/Category/Edit', category);
    }

    public addCategory(category: Category): ng.IHttpPromise<ControllerResult<Category>> {
        return this._http.post('api/Category/Add', category);
    }

    public deleteCategory(id: string): ng.IHttpPromise<ControllerResult<any>> {
        return this._http.get('api/Category/Delete?id=' + id);
    }
}
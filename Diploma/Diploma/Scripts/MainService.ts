import { User } from './Common/Models/User';
import { ControllerResult } from './Core/ControllerResult';

export class MainService {
    public static $inject: string[] =
    [
        '$http'
    ]

    constructor(
        private _http: ng.IHttpService
    ) { }

    public getUser(): ng.IHttpPromise<ControllerResult<User>> {
        return this._http.get('api/Authorize/GetUser');
    }

    public signOut(): ng.IHttpPromise<any> {
        return this._http.get('api/Authorize/SignOut');
    }
}
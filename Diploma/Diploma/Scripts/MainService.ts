import { User } from './Common/Models/User';

export class MainService {
    public static $inject: string[] =
    [
        '$http'
    ]

    constructor(
        private _http: ng.IHttpService
    ) { }

    public getUser(): ng.IHttpPromise<User>
    {
        return this._http.get('api/Authorize/GetUser');
    }

    public signOut(): ng.IHttpPromise<any>
    {
        return this._http.get('api/Authorize/SignOut');
    }
}
import { User } from '../../Common/Models/User';

export class ProfileService {
    public static $inject: string[] =
    [
        '$http',
    ]

    constructor(
        private _http: ng.IHttpService
    ) {

    }

    public edit(user: User): ng.IHttpPromise<boolean> {
        return this._http.post('api/Profile/Edit',
            {
                user: user
            });
    }
}
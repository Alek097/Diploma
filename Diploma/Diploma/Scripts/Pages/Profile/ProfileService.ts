import { EmailEditType } from '../../Core/EmailEditType';

export class ProfileService {
    public static $inject: string[] =
    [
        '$http',
    ]

    constructor(
        private _http: ng.IHttpService
    ) {

    }

    public editEmail(email: string, editType: EmailEditType): ng.IHttpPromise<boolean> {
        return this._http.post('api/Profile/EditEmail',
            {
                email: email,
                editType: editType
            });
    }
}
import { OAuthProvider } from '../../Common/Models/OAuthProvider';

export class AuthorizeService {
    public static $inject: string[] =
    [
        '$http'
    ]

    constructor(
        private _http: ng.IHttpService
    ) { }

    public getOAuthProviders(): ng.IHttpPromise<OAuthProvider[]>
    {
        return this._http.get('api/Authorize/GetOAuthProviders');
    }

    public getRedirectUrl(provider: string): ng.IHttpPromise<string>
    {
        return this._http.get('api/Authorize/GetRedirectUrl?provider=' + provider);
    }
}
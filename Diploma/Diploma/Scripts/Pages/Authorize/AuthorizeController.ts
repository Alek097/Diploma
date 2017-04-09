import { AuthorizeService } from './AuthorizeService';
import { OAuthProvider } from '../../Common/Models/OAuthProvider';

export class AuthorizeController {
    public static $inject: string[] =
    [
        'authorizeService'
    ]

    public oauthProviders: OAuthProvider[] = [];

    constructor(
        private _authorizeService: AuthorizeService
    ) {
        this._authorizeService.getOAuthProviders()
            .then((data) => {
                this.oauthProviders = data.data;
            })
    }

    public redirectOAuth(provider: string): void
    {
        this._authorizeService.getRedirectUrl(provider)
            .then((data) => {
                location.href = data.data;
            });
    }
}
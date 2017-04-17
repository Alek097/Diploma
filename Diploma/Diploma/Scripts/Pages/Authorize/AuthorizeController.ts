import { AuthorizeService } from './AuthorizeService';
import { OAuthProvider } from '../../Common/Models/OAuthProvider';
import { ModalWindowService } from '../../Common/ModalWindow/ModalWindowService';
import { ModalOptions } from '../../Core/ModalOptions';

export class AuthorizeController {
    public static $inject: string[] =
    [
        'authorizeService',
        'modalWindowService'
    ]

    public oauthProviders: OAuthProvider[] = [];

    constructor(
        private _authorizeService: AuthorizeService,
        private _modalWindowService: ModalWindowService
    ) {
        this._authorizeService.getOAuthProviders()
            .then((data) => {
                this.oauthProviders = data.data;
            })
    }

    public redirectOAuth(provider: string): void {
        this._authorizeService.getRedirectUrl(provider)
            .then((data) => {
                location.href = data.data;
            });
    }
}
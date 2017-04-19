import { AuthorizeService } from './AuthorizeService';
import { OAuthProvider } from '../../Common/Models/OAuthProvider';
import { ModalWindowService } from '../../Common/ModalWindow/ModalWindowService';
import { ModalOptions } from '../../Core/ModalOptions';
import { MainService } from '../../MainService';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';

export class AuthorizeController {
    public static $inject: string[] =
    [
        'authorizeService',
        'mainService',
        'errorModalService'
    ]

    public oauthProviders: OAuthProvider[] = [];

    constructor(
        private _authorizeService: AuthorizeService,
        mainService: MainService,
        errorModalService: ErrorModalService
    ) {
        mainService.getUser()
            .then((responce) => {

                if (responce.data.value.isAuthorize) {
                    location.href = '/';
                }
                else {
                    this._authorizeService.getOAuthProviders()
                        .then((data) => {
                            this.oauthProviders = data.data;
                        },
                        () => {
                            errorModalService.show(500, 'Ошибка сервера. Повторите попытку позже.');
                        });
                }
            });
    }

    public redirectOAuth(provider: string): void {
        this._authorizeService.getRedirectUrl(provider)
            .then((data) => {
                location.href = data.data;
            });
    }
}
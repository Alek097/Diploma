import { ProfileService } from './ProfileService';
import { User } from '../../Common/Models/User';
import { MainService } from '../../MainService';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';
import { MessageModalService } from '../../Common/MessageModal/MessageModalService';

export class ProfileController {
    public static $inject: string[] =
    [
        'profileService',
        'mainService',
        'waitModalService',
        'errorModalService',
        '$scope',
        'messageModalService'
    ];

    public oldUser: User;

    public newUser: User;

    public isEditEmail: boolean = false;
    public showConfirmCard: boolean = false;

    constructor(
        private _profileService: ProfileService,
        mainService: MainService,
        waitModalService: WaitModalService,
        private _errorModalService: ErrorModalService,
        private _scope: any,
        private _messageModalService: MessageModalService
    ) {
        waitModalService.show();

        mainService.getUser()
            .then((responce) => {

                if (responce.data.value.isAuthorize) {
                    this.oldUser = responce.data.value;

                    this._automapUser();

                    this._setScope();
                }
                else {
                    location.href = '/';
                }

                waitModalService.close();
            },
            () => {
                this._errorModalService.show(500, 'Ошибка сервера. Повторите попытку позже.');
                location.href = '/';

                waitModalService.close();
            });
    }

    public dismissSendEditEmailConfirmMessage(): void {
        this.isEditEmail = false;

        this._setScope();
        this._automapUser();
    }

    public acceptSendEditEmailConfirmMessage(newEmail: string): void {
        this.newUser.email = newEmail;

        this._profileService.sendConfirmMessage(newEmail)
            .then((responce) => {
                if (responce.data.isSuccess) {
                    this.showConfirmCard = true;
                }
                else {
                    this._messageModalService.show(responce.data.message);
                }
            },
            (responce) => {
                this._errorModalService.show(responce.status, 'Неизвестная ошибка. Но уже о ней знаем');
            });
    }

    public acceptEditEmail(newEmail: string, code: string): void {
        this._profileService.editEmail(newEmail, code)
            .then((responce) => {
                if (responce.data.isSuccess) {
                    this.oldUser.email = responce.data.value;
                }
                else {
                    this._errorModalService.show(responce.data.status, responce.data.message);
                }
                this.dismissEditEmail();
            },
            (responce) => {
                this._errorModalService.show(responce.status, 'Неизвестная ошибка. Но уже о ней знаем');
                this.dismissEditEmail();
            });
    }

    public dismissEditEmail(): void {
        this.dismissSendEditEmailConfirmMessage();

        this.showConfirmCard = false;
    }

    private _setScope(): void {
        this._scope.newEmail = this.oldUser.email;
    }

    private _automapUser(): void {
        this.newUser = new User();

        for (let i in this.oldUser) {
            this.newUser[i] = this.oldUser[i];

            if (this.newUser[i] instanceof Array || this.newUser[i] == null) {
                this.newUser[i] = null;
            }
        }
    }
}
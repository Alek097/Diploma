import { ProfileService } from './ProfileService';
import { User } from '../../Common/Models/User';
import { MainService } from '../../MainService';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';
import { MessageModalService } from '../../Common/MessageModal/MessageModalService';
import { Button, ButtonType } from '../../Core/Button';

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

    public isEditActiveEmail: boolean = false;
    public isEditProfile: boolean = false;

    constructor(
        private _profileService: ProfileService,
        mainService: MainService,
        waitModalService: WaitModalService,
        private _errorModalService: ErrorModalService,
        private _scope: ng.IScope,
        private _messageModalService: MessageModalService
    ) {
        waitModalService.show();

        mainService.getUser()
            .then((responce) => {

                if (responce.data.value.isAuthorize) {
                    this.oldUser = responce.data.value;

                    this._automapUser();

                    this._setScope(this._scope);
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

    public dismissEditActiveEmail(): string {
        this.isEditActiveEmail = false;

        return this.newUser.email;
    }

    public acceptEditActiveEmail(newActiveEmail: string): void {
        this.newUser.email = newActiveEmail;

        this._isEditedProfile();
        this.isEditActiveEmail = false;
    }

    public dismissEditProfile(): void {
        this.isEditProfile = false;

        this._automapUser();
        this._setScope(this._scope);
    }

    public acceptEditProfile(): void {
        this._messageModalService.show('Изменение данных профиля приведут к перезагрузке страницы.', 'Вы уверены?',
            [
                new Button(
                    'Принять',
                    () => {
                        return true;
                    },
                    ButtonType.success),
                new Button(
                    'Отклонить',
                    () => {
                        return true;
                    },
                    ButtonType.danger)
            ]);
    }

    private _isEditedProfile(): void {
        for (let i in this.oldUser) {
            if (this.oldUser[i] instanceof Array || this.oldUser[i] == null) {
                continue;
            }
            else {
                if (this.oldUser[i] != this.newUser[i]) {
                    this.isEditProfile = true;
                    return;
                }
            }
        }

        this.isEditProfile = false;
    }

    private _setScope(scope: any): void {
        scope.newEmail = this.oldUser.email;
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
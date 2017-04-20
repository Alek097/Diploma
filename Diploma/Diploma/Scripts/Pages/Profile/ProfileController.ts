import { ProfileService } from './ProfileService';
import { User } from '../../Common/Models/User';
import { MainService } from '../../MainService';
import { WaitModalService } from '../../Common/WaitModal/WaitModalService';
import { ErrorModalService } from '../../Common/ErrorModal/ErrorModalService';

export class ProfileController {
    public static $inject: string[] =
    [
        'profileService',
        'mainService',
        'waitModalService',
        'errorModalService',
        '$scope'
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
        private scope: ng.IScope
    ) {
        waitModalService.show();

        mainService.getUser()
            .then((responce) => {

                if (responce.data.value.isAuthorize) {
                    this.oldUser = responce.data.value;

                    this._automapUser();

                    this._setScope(scope);
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

        return this.newUser.activeEmail;
    }

    public acceptEditActiveEmail(newActiveEmail: string): void {
        this.newUser.activeEmail = newActiveEmail;

        this._isEditedProfile();
        this.isEditActiveEmail = false;
    }

    public dismissEditProfile() {
        this.isEditProfile = false;

        this._automapUser();
        this._setScope(this.scope);
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
        scope.newActiveEmail = this.oldUser.activeEmail;
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
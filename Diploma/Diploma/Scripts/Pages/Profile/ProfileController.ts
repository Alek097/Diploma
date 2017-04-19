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
    ];

    public user: User;

    constructor(
        private _profileService: ProfileService,
        mainService: MainService,
        waitModalService: WaitModalService,
        private _errorModalService: ErrorModalService
    ) {
        waitModalService.show();

        mainService.getUser()
            .then((responce) => {

                if (responce.data.value.isAuthorize) {
                    this.user = responce.data.value;
                }
                else {
                    location.href = '/';
                }
            },
            () => {
                this._errorModalService.show(500, 'Ошибка сервера. Повторите попытку позже.');
                location.href = '/';
            });
    }
}
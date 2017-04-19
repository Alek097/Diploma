import { MainService } from './MainService';
import { User } from './Common/Models/User';
import { Global } from './Core/Global';
import { ModalWindowService } from './Common/ModalWindow/ModalWindowService';
import { ModalOptions } from './Core/ModalOptions';
import { ErrorModalService } from './Common/ErrorModal/ErrorModalService';

export class MainController {

    public user: User = new User();

    public static $inject: string[] =
    [
        'mainService',
        'modalWindowService',
        'errorModalService'
    ]

    constructor(
        private _mainService: MainService,
        private _modalWindowService: ModalWindowService,
        errorModalService: ErrorModalService
    ) {
        this._mainService.getUser()
            .then((responce) => {
                this.user = responce.data.value;
                Global.user = this.user;

                if (!(responce.data.isSuccess)) {
                    errorModalService.show(responce.data.status, responce.data.message);
                }
            },
            () => {
                errorModalService.show(500, 'Ошибка сервера. Повторите попытку позже.');
            });
    }

    public signOut(): void {
        this._mainService.signOut()
            .then(() => {
                location.href = '/';
                location.reload();
            });
    }
}
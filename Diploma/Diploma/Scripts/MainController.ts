import { MainService } from './MainService';
import { User } from './Common/Models/User';
import { Global } from './Core/Global';
import { ModalWindowService } from './Common/ModalWindow/ModalWindowService';
import { ModalOptions } from './Core/ModalOptions';
import { ErrorModalService } from './Common/ErrorModal/ErrorModalService';
import { MessageModalService } from './Common/MessageModal/MessageModalService';

export class MainController {

    public user: User = new User();

    public static $inject: string[] =
    [
        'mainService',
        'modalWindowService',
        'errorModalService',
        'messageModalService'
    ]

    constructor(
        private _mainService: MainService,
        private _modalWindowService: ModalWindowService,
        errorModalService: ErrorModalService,
        messageModalService: MessageModalService
    ) {
        this._mainService.getUser()
            .then((responce) => {
                this.user = responce.data.value;

                Global.changeUser(this.user);

                Global.onChangeUser.push((user: User) => this.user = user);

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
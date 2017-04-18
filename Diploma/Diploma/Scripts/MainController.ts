import { MainService } from './MainService';
import { User } from './Common/Models/User';
import { Global } from './Core/Global';
import { ModalWindowService } from './Common/ModalWindow/ModalWindowService';
import { ModalOptions } from './Core/ModalOptions';

import errorModalTemplate from './Common/ErrorModal/ErrorModalView.html';

export class MainController {

    public user: User = new User();

    public static $inject: string[] =
    [
        'mainService',
        'modalWindowService'
    ]

    constructor(
        private _mainService: MainService,
        private _modalWindowService: ModalWindowService
    ) {
        this._mainService.getUser()
            .then((responce) => {
                this.user = responce.data.value;
                Global.user = this.user;

                if (!(responce.data.isSuccess)) {

                    this._modalWindowService.show(
                        <ModalOptions>
                        {
                            controller: 'errorModalController',
                            template: errorModalTemplate,
                            inject: {
                                status: responce.data.status,
                                message: responce.data.message
                            }
                        }
                    );
                }
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
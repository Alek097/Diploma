import { MainService } from './MainService';
import { User } from './Common/Models/User';
import { Global } from './Core/Global';

export class MainController {

    public user: User = new User();

    public static $inject: string[] =
    [
        'mainService'
    ]

    constructor(
        private _mainService: MainService
    ) {
        this._mainService.getUser()
            .then((data) => {
                this.user = data.data;
                Global.user = data.data;
            });
    }

    public signOut(): void {
        this._mainService.signOut()
            .then(() => {
                location.reload();
            });
    }
}
import { User } from '../Common/Models/User';

export class Global {
    public static user: User = null;

    public static onChangeUser: ((user: User) => void)[] = [];

    public static changeUser(newUser: User) {
        for (let i of Global.onChangeUser) {
            if (i != null) {
                i(newUser);
            }
        }
    }
}

Global.onChangeUser.push((user) => {
    Global.user = user;
})
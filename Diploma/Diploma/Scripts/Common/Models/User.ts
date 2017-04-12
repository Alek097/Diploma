import { Order } from './Order';
import { Address } from './Address'
import { Roles } from './Roles'

export class User {
    public email: string = null;

    public userName: string = null;

    public isBanned: boolean = false;

    public isAuthorize: boolean = false;

    public orders: Order[] = [];

    public addresses: Address[] = [];

    public role: Roles = Roles.User;
}
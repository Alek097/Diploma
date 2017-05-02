import { Order } from './Order';
import { Address } from './Address'

export class User {
    public email: string = null;

    public userName: string = null;

    public isBanned: boolean = false;

    public isAuthorize: boolean = false;

    public role: string = 'User';

    public orders: Order[] = [];

    public addresses: Address[] = [];
}
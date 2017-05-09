import { Product } from './Product';

export class Order {
    public totalPrice: number = 0;

    public createDate: string = null;

    public products: Product[] = [];
}
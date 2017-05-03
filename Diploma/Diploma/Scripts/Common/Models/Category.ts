import { Product } from './Product';

export class Category {
    public id: string = null;

    public name: string = null;

    public description: string = null;

    public products: Product[] = [];
}
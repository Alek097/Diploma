import { Characteristic } from './Characteristic';
import { CharacteristicsGroup } from './CharacteristicsGroup';

export class Product {
    public id: string = null;

    public name: string = null;

    public description: string = null;

    public coverUrl: string = null;

    public imagesUrl: string[] = [];

    public price: number = null;

    public characteristicsGroups: CharacteristicsGroup[] = [];

    public characteristics: Characteristic[] = [];
}
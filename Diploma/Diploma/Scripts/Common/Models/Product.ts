import { Characteristic } from './Characteristic';
import { CharacteristicsGroup } from './CharacteristicsGroup';

export class Product {
    public name: string = null;

    public description: string = null;

    public photoPath: string = null;

    public price: number = null;

    public CharacteristicsGroups: CharacteristicsGroup[] = [];

    public Characteristics: Characteristic[] = [];
}
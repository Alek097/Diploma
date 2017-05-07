import { Characteristic } from './Characteristic'

export class CharacteristicsGroup {
    public id: number;

    public name: string = null;

    public characteristics: Characteristic[] = [];

    constructor(id: number) {
        this.id = id;
    }
}
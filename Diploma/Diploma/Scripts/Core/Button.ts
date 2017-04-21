export class Button {

    constructor(
        public name: string,
        public callBack: () => boolean,
        public buttonType: ButtonType
    ) { }
}

export enum ButtonType {
    success = 1,
    danger,
    primary,
    secondary,
    info,
    warning,
    link
}
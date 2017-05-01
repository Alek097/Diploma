import { ModalWindowService } from '../ModalWindow/ModalWindowService';
import { ModalOptions } from '../../Core/ModalOptions';
import { Button, ButtonType } from '../../Core/Button';

import messageModalTemplate from './MessageModalView.html';

export class MessageModalService {
    public static $inject: string[] =
    [
        'modalWindowService',
        '$controller'
    ];

    constructor(
        private _modalWindowService: ModalWindowService,
    ) { }

    public show(message: string, title: string = null, buttons: Button[] = null): void {

        let buttonsWrappers: ButtonWrapper[] = [];
        if (buttons != null) {
            for (let button of buttons) {
                buttonsWrappers.push(new ButtonWrapper(this._modalWindowService, button));
            }
        }

        this._modalWindowService.show(
            <ModalOptions>
            {
                controller: 'messageModalController',
                controllerAs: 'messageCtrl',
                inject: {
                    title: title,
                    message: message,
                    buttons: buttonsWrappers
                },
                template: messageModalTemplate
            });
    }

    public close(): void {
        this._modalWindowService.close();
    }

}

class ButtonWrapper {

    public name: string;
    public buttonClass: string;
    public callBack: () => void

    constructor(
        modal: ModalWindowService,
        button: Button
    ) {
        this.name = button.name;

        switch (button.buttonType) {
            case ButtonType.danger:
                this.buttonClass = 'btn-danger'
                break;

            case ButtonType.success:
                this.buttonClass = 'btn-success';
                break;

            case ButtonType.primary:
                this.buttonClass = 'btn-primary';
                break;

            case ButtonType.secondary:
                this.buttonClass = 'btn-secondary';
                break;

            case ButtonType.info:
                this.buttonClass = 'btn-info';
                break;

            case ButtonType.warning:
                this.buttonClass = 'btn-warning';
                break;

            case ButtonType.link:
                this.buttonClass = 'btn-link';
                break;
        }

        this.callBack = () => {
            let callBackResult: boolean = button.callBack();

            if (callBackResult) {
                modal.close();
            }
        }
    }
}
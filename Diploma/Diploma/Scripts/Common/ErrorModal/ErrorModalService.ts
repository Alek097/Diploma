import { ModalWindowService } from '../ModalWindow/ModalWindowService';
import { ModalOptions } from '../../Core/ModalOptions';

import errorModalTemplate from './ErrorModalView.html';

export class ErrorModalService {
    public static $inject: string[] =
    [
        'modalWindowService'
    ];

    constructor(
        private _modalWindowService: ModalWindowService
    ) { }

    public show(status: number, message: string): void {
        this._modalWindowService.show(
            <ModalOptions>
            {
                controller: 'errorModalController',
                template: errorModalTemplate,
                inject: {
                    status: status,
                    message: message
                }
            }
        );
    }

    public close(): void {
        this._modalWindowService.close();
    }
}
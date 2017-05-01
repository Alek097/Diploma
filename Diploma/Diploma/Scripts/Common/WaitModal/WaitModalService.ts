import { ModalWindowService } from '../ModalWindow/ModalWindowService';
import { ModalOptions } from '../../Core/ModalOptions';

import waitModalView from './WaitModalView.html';

export class WaitModalService {
    public static $inject: string[] =
    [
        'modalWindowService'
    ];

    constructor(
        private _modalWindowService: ModalWindowService
    ) { }

    public show(): void {
        this._modalWindowService.show(
            <ModalOptions>
            {
                template: waitModalView
            }
        );
    }

    public close(): void {
        this._modalWindowService.close();
    }
}
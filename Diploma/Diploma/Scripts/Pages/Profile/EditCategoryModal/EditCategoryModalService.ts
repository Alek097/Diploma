import { ModalWindowService } from '../../../Common/ModalWindow/ModalWindowService';
import { Category } from '../../../Common/Models/Category';
import { ModalOptions } from '../../../Core/ModalOptions';

import editCategoryModalTemplate from './EditCategoryModalView.html';

export class EditCategoryModalService {
    public static $inject: string[] = [
        'modalWindowService'
    ];

    constructor(
        private _modalWindowService: ModalWindowService
    ) {
    }

    public show(category: Category, callback: (category: Category, isOk: false) => void): void {
        this._modalWindowService.show(
            <ModalOptions>
            {
                controller: 'editCategoryModalController',
                controllerAs: 'editCategoryModalCtrl',
                template: editCategoryModalTemplate,
                inject: {
                    category: category,
                    callback: callback,
                    closeModal: () => this._modalWindowService.close()
                }
            },
            () => {
                callback(category, false);
            });
    }

    public close(): void {
        this._modalWindowService.close();
    }
}
import { ModalOptions } from '../../Core/ModalOptions'

export class ModalWindowService {
    public static $inject: string[] =
    [

    ];

    private _modal: any;
    private _insertElement: JQuery;

    constructor() {
        this._modal = angular.element('#modalWindow');
        this._insertElement = angular.element('#modalWindow>.modal-dialog');
    }

    public show(options: ModalOptions): void {
        let template: string = null;

        if (options.controller != null) {
            template = '<div ng-controller="' + options.controller + ' ' + (options.controllerAs != null || options.controllerAs != '' ? 'as ' + options.controllerAs : '') + '">' + options.template + '</div>'
        }
        else {
            template = options.template;
        }

        let insertElement = this._insertElement;

        angular.element(document).ready(function () {
            var $div = $(template);

            $(insertElement).html(<any>$div);

            angular.element(document).injector().invoke(function ($compile) {
                var scope = angular.element($div).scope();

                for (let injectName in options.inject) {
                    scope[injectName] = options.inject[injectName];
                }

                $compile($div)(scope);
                scope.$digest();
            });

        });

        this._modal.modal('show');
    }

    public close(): void {
        setTimeout(() => {
            this._modal.modal('hide');
        }, 500);
    }
}
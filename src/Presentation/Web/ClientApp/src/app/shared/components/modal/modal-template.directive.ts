import { Directive, TemplateRef } from '@angular/core';

import { ModalStateService } from '../../services';

/**
 * Directive allowing to get a reference to the template containing the modal component,
 * and to store it into the internal modal state service. Somewhere in the view, there must be
 *
 * ```
 * <ng-template modal>
 *   <appc-modal-component></appc-modal-component>
 * </ng-template>
 * ```
 *
 * in order to register the modal template to the internal modal state
 */
@Directive({
  // eslint-disable-next-line @angular-eslint/directive-selector
  selector: 'ng-template[modal]',
})
export class ModalTemplateDirective {
  constructor(modalTemplate: TemplateRef<any>, state: ModalStateService) {
    state.template = modalTemplate;
  }
}

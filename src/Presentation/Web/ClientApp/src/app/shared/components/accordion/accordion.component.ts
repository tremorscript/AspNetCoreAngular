import { Component, OnInit, Input } from '@angular/core';
import { IAccordionItem } from '../../models';

@Component({
  selector: 'appc-accordion',
  templateUrl: './accordion.component.html',
})
export class AccordionComponent {
  @Input() items: IAccordionItem[];

  constructor() {}

}

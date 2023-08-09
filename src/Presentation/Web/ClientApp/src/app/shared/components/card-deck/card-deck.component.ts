import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ICardEvent } from '../../models';

@Component({
  selector: 'appc-card-deck',
  styleUrls: ['./card-deck.component.scss'],
  templateUrl: './card-deck.component.html',
})
export class CardDeckComponent {
  @Input() title: string;
  @Input() hasSidebar: boolean;
  @Input() items: any[];
  @Input() titleKey: string;
  @Input() descriptionKey: string;
  @Input() bodyTemplate: any;
  @Input() footerTemplate: any;
  @Input() headerClass: any;
  @Output() cardClick = new EventEmitter<ICardEvent>();

  constructor() {}

  onClickHandler($event: MouseEvent, item: any) {
    this.cardClick.next({ $event, data: item });
  }
}

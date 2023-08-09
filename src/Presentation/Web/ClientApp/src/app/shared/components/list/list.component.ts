import { Component, OnInit, Input } from '@angular/core';

import { IListItem } from '../../models';

@Component({
  selector: 'appc-list',
  templateUrl: './list.component.html',
})
export class ListComponent {
  @Input() list: IListItem[];
  constructor() {}

  trackByFn(index, item) {
    return index;
  }
}

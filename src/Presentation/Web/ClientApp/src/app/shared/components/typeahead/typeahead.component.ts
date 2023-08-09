import { Component, OnInit, Input, Output } from '@angular/core';

@Component({
  selector: 'appc-typeahead',
  templateUrl: './typeahead.component.html',
})
export class TypeaheadComponent {
  @Input() label: string;
  @Input() model: string;

  constructor() {}

  search(terms) {}
}

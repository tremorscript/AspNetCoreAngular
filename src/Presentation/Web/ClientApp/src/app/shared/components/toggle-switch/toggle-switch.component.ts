import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

// Uage
// <appc-toggle-switch
//   [checked]="false"
//   (onChange)="change($event)"
// ></appc-toggle-switch>
@Component({
  selector: 'appc-toggle-switch',
  styleUrls: ['./toggle-switch.component.scss'],
  templateUrl: './toggle-switch.component.html',
})
export class ToggleSwitchComponent {
  constructor() {}

  @Input() checked: boolean;
  @Input() label: string;
  @Input() disabled: boolean;
  @Output() toggleSwitched = new EventEmitter<boolean>();

  changeHandler(e) {
    this.toggleSwitched.next(e.target.checked);
  }
}

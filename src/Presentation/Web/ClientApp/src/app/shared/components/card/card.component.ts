import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  TemplateRef,
  ViewChild,
  AfterViewInit,
  ChangeDetectorRef,
} from '@angular/core';

@Component({
  selector: 'appc-card',
  templateUrl: './card.component.html',
})
export class CardComponent implements AfterViewInit {
  @ViewChild('footerTemplate', { static: true }) footerTemplate;
  @Input() title: string;
  @Input() body: string | TemplateRef<any>;
  @Input() headerClass: string;
  @Output() Click = new EventEmitter<any>();

  showFooter: boolean;
  constructor(private cdRef: ChangeDetectorRef) {}

  ngAfterViewInit(): void {
    this.showFooter =
      this.footerTemplate.nativeElement &&
      this.footerTemplate.nativeElement.children.length > 0;
    this.cdRef.detectChanges();
  }
}

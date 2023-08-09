import { Component, OnInit } from '@angular/core';
import { User } from 'oidc-client-ts';

import { AppService, AuthorizeService } from '@app/shared';

import { eCommerceRoutes } from '../../ecommerce/eCommerceRoutes';

@Component({
  selector: 'appc-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  isCollapsed = true;

  eCommerceRoutes = [...eCommerceRoutes];

  constructor(private authService: AuthorizeService, private appService: AppService) {}

  get cultures(): ICulture[] {
    return this.appService.appData.cultures;
  }
  get currentCulture(): ICulture {
    return this.cultures.filter((x) => x.current)[0];
  }
  toggleMenu() {
    this.isCollapsed = !this.isCollapsed;
  }
}

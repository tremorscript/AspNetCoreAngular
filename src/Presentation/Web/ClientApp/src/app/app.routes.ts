import { Routes } from '@angular/router';
import { HomeComponent } from '@app/home/home.component';
import { PrivacyComponent } from '@app/components';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full', data: { state: 'home' } },
  { path: 'ecommerce', loadChildren: () => import('./ecommerce/ecommerce.module').then((m) => m.EcommerceModule) },
  { path: 'privacy', component: PrivacyComponent },
];
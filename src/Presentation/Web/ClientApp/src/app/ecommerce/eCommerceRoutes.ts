import { Routes } from '@angular/router';
import { EcommerceComponent } from './ecommerce.component';

export const eCommerceRoutes: Routes = [
  { path: '', component: EcommerceComponent, data: { displayText: 'Home' } },
  {
    path: 'shop',
    loadChildren: () => import('./shop/shop.module').then((m) => m.ShopModule),
    data: { displayText: 'Shop' },
  },
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then((m) => m.AdminModule),
    data: { displayText: 'Admin' },
  },
];

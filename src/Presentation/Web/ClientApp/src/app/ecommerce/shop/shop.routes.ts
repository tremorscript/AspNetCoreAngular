import { Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { ShopComponent } from './shop.component';
import { CustomersComponent } from './customers/customers.component';

export const shopRoutes: Routes = [
  {
    path: '',
    component: ShopComponent,
    data: { displayText: 'Shop' },
    children: [
      { path: '', component: ProductsComponent, data: { state: 'products', displayText: 'Products' } },
      { path: 'products', component: ProductsComponent, data: { state: 'products', displayText: 'Products' } },
      { path: 'customers', component: CustomersComponent, data: { state: 'customer', displayText: 'Customer' } },
    ],
  },
];

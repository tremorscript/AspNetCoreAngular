import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { eCommerceRoutes } from './eCommerceRoutes';

@NgModule({
  imports: [RouterModule.forChild(eCommerceRoutes)],
  exports: [RouterModule],
})
export class EcommerceRoutingModule {}

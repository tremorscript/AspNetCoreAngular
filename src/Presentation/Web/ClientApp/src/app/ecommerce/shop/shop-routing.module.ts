import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { shopRoutes } from './shop.routes';

@NgModule({
  imports: [RouterModule.forChild(shopRoutes)],
  exports: [RouterModule],
})
export class ShopRoutingModule {}

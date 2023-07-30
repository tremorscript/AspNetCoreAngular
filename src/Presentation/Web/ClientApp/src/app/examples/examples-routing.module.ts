import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TestExamplesComponent } from './test-examples/test-examples.component';

const routes: Routes = [
  { path: '', component: TestExamplesComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExamplesRoutingModule { }

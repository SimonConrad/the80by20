import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { SolutionToProblemRoutingModule } from './solution-to-problem-routing.module';
import { UserProblemComponent } from './user/user-problem.component';
import { UserProductFormComponent } from './user/product-form/user-product-form.component';

import {DeborathProblemComponent} from './user/deborath-problem.component'
import { DeborathProductFormComponent } from './user/product-form/deborath-product-form.component'


@NgModule({
  declarations: [
    UserProblemComponent,
    UserProductFormComponent,
    DeborathProblemComponent,
    DeborathProductFormComponent
  ],
  imports: [
    SharedModule,
    SolutionToProblemRoutingModule,
  ]
})
export class SolutionToProblemModule { }

import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { SolutionToProblemRoutingModule } from './solution-to-problem-routing.module';
import { UserProblemComponent } from './user/user-problem.component';
//import { UserProductFormComponent } from './user/product-form/user-product-form.component';


@NgModule({
  declarations: [
    UserProblemComponent//,
    //UserProductFormComponent
  ],
  imports: [
    SharedModule,
    SolutionToProblemRoutingModule
  ]
})
export class SolutionToProblemModule { }

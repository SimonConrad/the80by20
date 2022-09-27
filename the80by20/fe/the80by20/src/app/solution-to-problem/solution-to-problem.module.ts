import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { SolutionToProblemRoutingModule } from './solution-to-problem-routing.module';
import { UserProblemComponent } from './user/user-problem.component';


@NgModule({
  declarations: [
    UserProblemComponent
  ],
  imports: [
    SharedModule,
    SolutionToProblemRoutingModule
  ]
})
export class SolutionToProblemModule { }

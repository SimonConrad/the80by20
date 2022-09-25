import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { SolutionToProblemRoutingModule } from './solution-to-problem-routing.module';
import { SolutionToProblemComponent } from './solution-to-problem.component';


@NgModule({
  declarations: [
    SolutionToProblemComponent
  ],
  imports: [
    SharedModule,
    SolutionToProblemRoutingModule
  ]
})
export class SolutionToProblemModule { }

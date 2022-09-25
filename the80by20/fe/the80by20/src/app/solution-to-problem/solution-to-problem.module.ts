import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SolutionToProblemRoutingModule } from './solution-to-problem-routing.module';
import { SolutionToProblemComponent } from './solution-to-problem.component';


@NgModule({
  declarations: [
    SolutionToProblemComponent
  ],
  imports: [
    CommonModule,
    SolutionToProblemRoutingModule
  ]
})
export class SolutionToProblemModule { }

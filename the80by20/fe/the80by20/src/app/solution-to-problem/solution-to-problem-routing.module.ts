import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SolutionToProblemComponent } from './solution-to-problem.component';

const routes: Routes = [{ path: '', component: SolutionToProblemComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SolutionToProblemRoutingModule { }

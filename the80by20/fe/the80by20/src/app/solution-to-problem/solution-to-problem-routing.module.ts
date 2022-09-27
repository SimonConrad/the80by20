import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserProblemComponent } from './user/user-problem.component';

const routes: Routes = [{ path: '', component: UserProblemComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SolutionToProblemRoutingModule { }

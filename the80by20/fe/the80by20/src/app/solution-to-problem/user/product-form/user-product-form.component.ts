import { EmptyExpr } from '@angular/compiler';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { catchError, EMPTY } from 'rxjs';
import { UserProblem } from '../model/UserProblem';
import { UserProblemService } from '../user-problem.service';


@Component({
  selector: 'app-user-product-form',
  templateUrl: './user-product-form.component.html',
  styleUrls: ['./user-product-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush // INFO !!!! only detect chnages made from input properties, and events from child components (output),
  // and observables bound in the teamplate using an async pipe
  // bound values set in local properties won't trigger chnage detection, so won't update the ui
})
export class UserProductFormComponent {
  title = 'Problem';
  errorMessage = '';

  // INFO problem observable data item stream, which emits Problem or undefined
  problem$ = this.problemService.selectedProblem$
  .pipe(
    catchError(err => {
      this.errorMessage = err; //when changeDetection: ChangeDetectionStrategy.OnPush then this reactiong to chnage from ts to temaplte won't work
      return EMPTY;
    })
  );

  constructor(private problemService: UserProblemService) { }

}

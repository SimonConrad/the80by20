import { EmptyExpr } from '@angular/compiler';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { catchError, EMPTY } from 'rxjs';
import { UserProblem } from '../model/UserProblem';
import { UserProblemService } from '../user-problem.service';


@Component({
  selector: 'app-user-product-form',
  templateUrl: './user-product-form.component.html',
  styleUrls: ['./user-product-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserProductFormComponent {
  title = 'Problem';
  errorMessage = '';

  // INFO problem observable data item stream, which emits Problem or undefined
  problem$ = this.problemService.selectedProblem$
  .pipe(
    catchError(err => {
      this.errorMessage = err;
      return EMPTY;
    })
  );

  constructor(private problemService: UserProblemService) { }

}

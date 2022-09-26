import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

import { SolutionToProblemService } from './solution-to-problem.service';
import { catchError, EMPTY } from 'rxjs';
@Component({
  selector: 'app-solution-to-problem',
  templateUrl: './solution-to-problem.component.html',
  styleUrls: ['./solution-to-problem.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush
})
export class SolutionToProblemComponent {

  userProblems: string =  "Problems";
  errorMessage: string = '';
  userSolutionsToProblems: string =  "Solutions to Problems";

  userProblems$ = this.solutionToProblemService.userProblems$.pipe(
    catchError(err => {
      this.errorMessage = err;
      //return of([]);
      return EMPTY;
    })
  );

  constructor(private solutionToProblemService: SolutionToProblemService) { }
}

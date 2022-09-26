import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

import { SolutionToProblemService } from './solution-to-problem.service';
import { catchError, EMPTY } from 'rxjs';
import { ProblemCategory } from './model/ProblemCategory';
@Component({
  selector: 'app-solution-to-problem',
  templateUrl: './solution-to-problem.component.html',
  styleUrls: ['./solution-to-problem.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush
})
export class SolutionToProblemComponent {

  userProblems: string =  "User Problems (todo separate analagous component for operator)";
  userSolutionsToProblems: string =  "User Solutions to Problems";
  errorMessage: string = '';
  categories: ProblemCategory[] = [];
  selectedCategoryId = "00000000-0000-0000-0000-000000000006"

  userProblems$ = this.solutionToProblemService.userProblemswithCategory$.pipe(
    catchError(err => {
      this.errorMessage = err;
      //return of([]);
      return EMPTY;
    })
  );

  constructor(private solutionToProblemService: SolutionToProblemService) { }

  onSelected(categoryId: string): void {
    console.log('Not yet implemented');
  }

  onAdd(): void {
    console.log('Not yet implemented');
  }
}

import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

import { SolutionToProblemService } from './solution-to-problem.service';
import { BehaviorSubject, catchError, combineLatest, EMPTY, filter, map, startWith, Subject } from 'rxjs';
import { ProblemCategory } from './model/ProblemCategory';
@Component({
  selector: 'app-solution-to-problem',
  templateUrl: './solution-to-problem.component.html',
  styleUrls: ['./solution-to-problem.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush // todo uncomment and only async pipes will notify???
})
export class SolutionToProblemComponent {

  userProblems: string = "User Problems";
  userSolutionsToProblems: string = "User Solutions to Problems";
  errorMessage: string = '';

  //private categorySelectedSubject = new Subject<string>(); // stayed in component not in separate service beacouse emmitintg is from this compnent - onSelected
  private categorySelectedSubject = new BehaviorSubject<string>(''); // stayed in component not in separate service beacouse emmitintg is from this compnent - onSelected
  categorySelectedAction$ = this.categorySelectedSubject.asObservable();

  problems$ = combineLatest([
    this.solutionToProblemService.userProblemswithCategory$,
    this.categorySelectedAction$
    // .pipe(
    //   startWith(null) // done with initial value passed to BehaviorSubject
    //   )
  ])
  .pipe(
    map(([problems, selectedCategoryId])=>
      problems.filter(item =>
        selectedCategoryId ? item.categoryId === selectedCategoryId : true)
    ),
    catchError(err => {
      this.errorMessage = err;
      //return of([]);
      return EMPTY;
    })
  );

  // userProblems$ = this.solutionToProblemService.userProblemswithCategory$.pipe(
  //   catchError(err => {
  //     this.errorMessage = err;
  //     //return of([]);
  //     return EMPTY;
  //   })
  // );

 problemCategories$ = this.solutionToProblemService.problemCategories$.pipe(
    catchError(err => {
      this.errorMessage = err;
      //return of([]);
      return EMPTY;
    })
  );

  // userProblemsSimpleFilter$ = this.solutionToProblemService.userProblemswithCategory$
  //   .pipe(
  //     map(problems =>
  //       problems.filter(item => this.selectedCategoryId ? item.categoryId === this.selectedCategoryId : true
  //       ))
  //   )

  constructor(private solutionToProblemService: SolutionToProblemService) { }

  onSelected(categoryId: string): void {

    if(categoryId == ''){
      categoryId == null;
    }
    this.categorySelectedSubject.next(categoryId);
    //this.selectedCategoryId = +categoryId; // INFO + cast string to number
  }

  onAdd(): void {
    console.log('Not yet implemented');
  }
}

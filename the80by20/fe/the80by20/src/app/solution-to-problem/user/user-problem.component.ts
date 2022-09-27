import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

import { UserProblemService } from './user-problem.service';
import { BehaviorSubject, catchError, combineLatest, EMPTY, filter, map, startWith, Subject } from 'rxjs';
import { ProblemCategory } from '../shared-model/ProblemCategory';
@Component({
  selector: 'app-user-problem',
  templateUrl: './user-problem.component.html',
  styleUrls: ['./user-problem.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush // todo uncomment and only async pipes will notify???
})
export class UserProblemComponent {

  userProblems: string = "User Problems";
  problemDetails: string = "Problem details";
  errorMessage: string = '';

  //private categorySelectedSubject = new Subject<string>(); // stayed in component not in separate service beacouse emmitintg is from this compnent - onSelected
  private categorySelectedSubject = new BehaviorSubject<string>(''); // stayed in component not in separate service beacouse emmitintg is from this compnent - onSelected
  categorySelectedAction$ = this.categorySelectedSubject.asObservable();

  constructor(private solutionToProblemService: UserProblemService) { }

  // INFO the80by20\fe\docs
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

 problemCategories$ = this.solutionToProblemService.problemCategories$.pipe(
    catchError(err => {
      this.errorMessage = err;
      //return of([]);
      return EMPTY;
    })
  );

  onSelected(categoryId: string): void {

    if(categoryId == ''){
      categoryId == null;
    }
    this.categorySelectedSubject.next(categoryId);
    //this.selectedCategoryId = +categoryId; // INFO + cast string to number
  }

  onAdd(): void {
    console.log('Not yet implemented');

    // todo add also version with subscribe to invoke http.post, add to subscription obect and ondestry unsubscribe
  }

  // userProblemsSimpleFilter$ = this.solutionToProblemService.userProblemswithCategory$
  //   .pipe(
  //     map(problems =>
  //       problems.filter(item => this.selectedCategoryId ? item.categoryId === this.selectedCategoryId : true
  //       ))
  //   )

  // userProblems$ = this.solutionToProblemService.userProblemswithCategory$.pipe(
  //   catchError(err => {
  //     this.errorMessage = err;
  //     //return of([]);
  //     return EMPTY;
  //   })
  // );
}

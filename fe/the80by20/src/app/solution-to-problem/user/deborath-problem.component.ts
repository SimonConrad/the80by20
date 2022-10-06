import { ChangeDetectionStrategy, Component } from '@angular/core';

import { DeborathProblemService } from './deborath-problem.service';
import { BehaviorSubject, catchError, combineLatest, EMPTY, map, Subject, Subscription } from 'rxjs';
@Component({
  selector: 'app-deborath-problem',
  templateUrl: './deborath-problem.component.html',
  styleUrls: ['./deborath-problem.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush // INFO !!!! only detect chnages made from input properties, and events from child components (output),
  // and observables bound in the teamplate using an async pipe
  // bound values set in local properties won't trigger chnage detection, so won't update the ui
})
// export class UserProblemComponent implements OnInit {
export class DeborathProblemComponent {

  userProblems: string = "User Problems";
  problemDetails: string = "Problem details";
  //errorMessage: string = '';

  private errorMessageSubject = new Subject<string>()
  errorMessage$ = this.errorMessageSubject.asObservable();

  //private categorySelectedSubject = new Subject<string>(); // stayed in component not in separate service beacouse emmitintg is from this compnent - onSelected
  private categorySelectedSubject = new BehaviorSubject<string>(''); // stayed in component not in separate service beacouse emmitintg is from this compnent - onSelected
  categorySelectedAction$ = this.categorySelectedSubject.asObservable();

  constructor(private solutionToProblemService: DeborathProblemService) { }

  sub: Subscription = new Subscription();
  // ngOnInit(): void {
  //   //this.solutionToProblemService.addProblem();
  // }

  // INFO the80by20\fe\docs
  problems$ = combineLatest([
    //this.solutionToProblemService.userProblemswithCategory$,
    this.solutionToProblemService.problemsWithAdd$,
    this.categorySelectedAction$
    // .pipe(
    //   startWith(null) // done with initial value passed to BehaviorSubject
    //   )
  ])
    .pipe(
      map(([problems, selectedCategoryId]) =>
        problems.filter(item =>
          selectedCategoryId ? item.categoryId === selectedCategoryId : true)
      ),
      catchError(err => {
        this.errorMessageSubject.next(err);
        //return of([]);
        return EMPTY;
      })
    );

  problemCategories$ = this.solutionToProblemService.problemCategories$.pipe(
    catchError(err => {
      this.errorMessageSubject.next(err);
      //return of([]);
      return EMPTY;
    })
  );

  selectedProblem$ = this.solutionToProblemService.selectedProblem$;

  onSelected(categoryId: string): void {

    if (categoryId == '') {
      categoryId == null;
    }
    this.categorySelectedSubject.next(categoryId);
    //this.selectedCategoryId = +categoryId; // INFO + cast string to number
  }

  onProblemSelected(id: string): void {
    this.solutionToProblemService.selectedProblemChanged(id);
  }

  // todo move to form submit button
  onAdd(): void {
    this.solutionToProblemService.addProblem();
    // todo add also version with subscribe to invoke http.post, add to subscription obect and ondestry unsubscribe
  }

  onDeleted(id: string): void {
    // this.solutionToProblemService.addProblem();

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

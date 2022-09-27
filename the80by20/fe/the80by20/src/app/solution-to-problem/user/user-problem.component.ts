import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

import { UserProblemService } from './user-problem.service';
import { BehaviorSubject, catchError, combineLatest, EMPTY, filter, map, startWith, Subject, Subscription } from 'rxjs';
import { ProblemCategory } from '../shared-model/ProblemCategory';
import { UserProblem } from './model/UserProblem';
@Component({
  selector: 'app-user-problem',
  templateUrl: './user-problem.component.html',
  styleUrls: ['./user-problem.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush // INFO !!!! only detect chnages made from input properties, and events from child components (output),
  // and observables bound in the teamplate using an async pipe
  // bound values set in local properties won't trigger chnage detection, so won't update the ui
})
// export class UserProblemComponent implements OnInit {
export class UserProblemComponent implements OnInit {

  userProblems: string = "User Problems";
  problemDetails: string = "Problem details";
  //errorMessage: string = '';

  private errorMessageSubject = new Subject<string>()
  errorMessage$ = this.errorMessageSubject.asObservable();

  //private categorySelectedSubject = new Subject<string>(); // stayed in component not in separate service beacouse emmitintg is from this compnent - onSelected
  private categorySelectedSubject = new BehaviorSubject<string>(''); // stayed in component not in separate service beacouse emmitintg is from this compnent - onSelected
  categorySelectedAction$ = this.categorySelectedSubject.asObservable();

  constructor(private solutionToProblemService: UserProblemService) { }

  initProblem$ = this.solutionToProblemService.initProblem$
  problems$ = this.solutionToProblemService.problems$;
  addProblem$ = this.solutionToProblemService.addProblem$

  sub: Subscription = new Subscription();
  ngOnInit(): void {
    this.solutionToProblemService.init()
  }

   // todo move to form submit button
   onAdd(): void {
    let  newProblem : UserProblem =
    {
      problemId: "z6a4f74e-4b0a-4487-a6ff-ca2244b4afd9",
      userId: "c1bfe7bc-053c-465b-886c-6f55af7ec4fe",
      requiredSolutionTypes: "PocInCode; PlanOfImplmentingChangeInCode",
      description: "QQQQQQ",
      categoryId: "00000000-0000-0000-0000-000000000006",
      category: "architecture",
      isConfirmed: false,
      isRejected: false,
      createdAt: "",
      color: "	#000000"
    };

    this.solutionToProblemService.startAdd(newProblem);
    // todo add also version with subscribe to invoke http.post, add to subscription obect and ondestry unsubscribe
  }

  // problemCategories$ = this.solutionToProblemService.problemCategories$.pipe(
  //   catchError(err => {
  //     this.errorMessageSubject.next(err);
  //     //return of([]);
  //     return EMPTY;
  //   })
  // );


  // selectedProblem$ = this.solutionToProblemService.selectedProblem$;

  // onSelected(categoryId: string): void {

  //   if (categoryId == '') {
  //     categoryId == null;
  //   }
  //   this.categorySelectedSubject.next(categoryId);
  //   //this.selectedCategoryId = +categoryId; // INFO + cast string to number
  // }

  // onProblemSelected(problemId: string): void {
  //   this.solutionToProblemService.selectedProblemChanged(problemId);
  // }



  // onDeleted(problemId: string): void {
  //   // this.solutionToProblemService.addProblem();
  //   this.solutionToProblemService.deleteProblem(problemId)
  // }

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

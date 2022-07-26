import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

import { UserProblemService } from './user-problem.service';
import { catchError, combineLatest, EMPTY, map, Observable, Subject } from 'rxjs';
import { ProblemCategory } from '../shared-model/ProblemCategory';
import { UserProblem } from './model/UserProblem';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-user-problem',
  templateUrl: './user-problem.component.html',
  styleUrls: ['./user-problem.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush // INFO ChangeDetectionStrategy.OnPush only detect changes made from:
  // - input properties, and events from child components (output),
  // - observables bound in the template using an async pipe;
  // bound values set in local properties won't trigger change detection, so won't update the ui
  // (for example chnage deteciont mechanism won't handle userProblems value)
})

export class UserProblemComponent implements OnInit {
  userProblemsTitle: string = "User Problems";

  //#region observable data streams
  // subject for component, so defined there not in service
  private errorMessageSubject = new Subject<string>()
  errorMessageDataStream$ = this.errorMessageSubject.asObservable(); // INFO errorMessage$ observable subscribed in the template with async pipe

  private notifySubject = new Subject<string>()
  notifySubjectDataStream$ = this.notifySubject.asObservable(); // INFO notifySubjectDataStream$ observable subscribed in the template with async pipe

  problemsDataStream$ = this.userProblemService.problemsDataStream$;

  problemCategoriesDataStream$ = this.userProblemService.problemCategories$.pipe(
    catchError(err => { // INFO component can add custom behavior done at this observable emission inside pipe - custom error showing
      this.errorMessageSubject.next(err);
      return EMPTY; // INFO or return of([]);
    })
  );

  // INFO can be used as described: fe\docs\combining -all-the-streams\combining-all-the-streams.jpg
  // vm$ = combineLatest([
  //   this.problemsDataStream$,
  //   this.problemCategoriesDataStream$
  // ]).pipe(
  //   map(([problems, categories]) =>
  //     ({problems, categories}))
  //   );

  actionInProgressDataStream$ =  this.userProblemService.actionInProgressDataStream$
  //#endregion

  //#region observable action streams
  // INFO take a look at mechanism of this actionstream:
  // we are subscribing in component's template using async pipe,
  // this actionstream is defined in service using behaviorsubject and is exposed via asObservable,
  // we can add custom handling in pipe in this component,
  // emitting items into this action stream is in service but is invoked from the component method this.solutionToProblemService.init()
  initProblemActionStream$ = this.userProblemService.initProblemActionStream$.pipe(
    catchError(err => {
      this.errorMessageSubject.next(err);
      return EMPTY;
    }));

  deleteProblemActionStream$ = this.userProblemService.deleteProblemActionStream$.pipe( //  INFO subscribed with async pipe in template
    catchError(err => {
      this.errorMessageSubject.next(err);
      return EMPTY;
    }));

  selectProblemActionStream$ = this.userProblemService.selectProblemActionStream$ // INFO subscribed with async pipe in template
  //#endregion

  constructor(private userProblemService: UserProblemService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.userProblemService.startInitializeProblems()

    this.route.queryParams.subscribe((params) => {
      if (params['loggedin'] === 'success') {
        this.notifySubject.next('You have been successfully logged in'); // TODO fix as it is not showing depite it is invoked
      }
    });

  }

  onRefresh(): void {
    this.userProblemService.startInitializeProblems()
  }

  onAdd(): void {
    this.userProblemService.startSelectAction('');
  }

  onEdit(userid: string): void {
    this.userProblemService.startSelectAction(userid);
  }

  onDelete(id: string): void {
    this.userProblemService.startDeleteAction(id);
  }

  onCategorySelected(categoryId: string): void {
    this.userProblemService.startFilterAction(categoryId);
  }

  onCategorySelected2(event: any): void {
    this.userProblemService.startFilterAction(event.value);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { BehaviorSubject, catchError, combineLatest, concatMap, switchMap, map, merge, Observable, of, scan, Subject, tap, throwError, finalize } from 'rxjs';

import { UserProblem } from './model/UserProblem'
import { ProblemCategory } from '../shared-model/ProblemCategory'

@Injectable({
  providedIn: 'root'
})
export class UserProblemService {
  private userProblemsUrl = 'api/userProblems';
  private problemCategories = 'api/problemCategories';

  constructor(private http: HttpClient) {
  }

  private _problemsSubject = new BehaviorSubject<UserProblem[]>([]);
  problemsDataStream$ = this._problemsSubject.asObservable();
  get problems() {
    return this._problemsSubject.value;
  }

  private userProblems$ = this.http.get<UserProblem[]>(this.userProblemsUrl)
    .pipe(
      tap(data => console.log('User Problems: ', JSON.stringify(data))),
      catchError(this.handleError),
      finalize(() => this.stopActionInProgress())
    );

  problemCategories$ = this.http.get<ProblemCategory[]>(this.problemCategories)
    .pipe(
      tap(data => console.log('Problem categories:', JSON.stringify(data))),
      catchError(this.handleError),
    );

  userProblemswithCategory$ = combineLatest([ // INFO combineLatest check fe/docs
    this.userProblems$,
    this.problemCategories$])
    .pipe(
      map(([problems, problemCategories]) => // INFO javascript destructuring to define a name for each array element
        problems.map(problem => ({
          ...problem, // INFO spread-operator to map and copy values to property matched by name
          color: this.markWithColor(problem),
          category: problemCategories.find(c => problem.categoryId == c.id)?.name, // INFO find
          searchKey: [problem.problemId]
        } as UserProblem)))
    );

  private markWithColor(problem: UserProblem): any {
    if (problem.isRejected) {
      return "#FF0000"; //red
    }
    if (problem.isConfirmed) {
      return "	#008000"; //green
    }
    return "#000000" //black
  }


  private _actionInProgress = new BehaviorSubject<boolean>(false);
  actionInProgressDataStream$ = this._actionInProgress.asObservable();

  startActionInProgress = () => {
    this._actionInProgress.next(true)
  }

  stopActionInProgress = () => {
    this._actionInProgress.next(false)
  }

  //#region init
  startInit = () => {
    this._initProblemSubject.next('');
  }

  private _initProblemSubject = new BehaviorSubject('');
  initProblemActionStream$ = this._initProblemSubject.asObservable()
    .pipe(
      tap(() => this.startActionInProgress()),
      switchMap(() => this.userProblemswithCategory$), // TODO switchMap????
      tap(res => this._problemsSubject.next(res))
    );
  //#endregion


  //#region add
  startAdd = (userProblem: UserProblem) => {
    this._addProblemSubject.next(userProblem);
  }

  private _addProblemSubject = new Subject<UserProblem>();
  addProblemActionStream$ = this._addProblemSubject.asObservable().pipe(
    tap(problem => this._add(problem)) // todo uncomment and call backendd
    // switchMap((problem) => this.http.post(this.userProblemsUrl, problem).pipe(
    //   tap(problems => this._add(problem))
    // ))
  );

  private _add = (problem: UserProblem) => {
    this._problemsSubject.next([
      ...this.problems,
      problem
    ])
  }
  //#endregion


  //#region delete
  startDelete = (problemId: string) => {
    this._deleteProblemSubject.next(problemId);
  }

  private _deleteProblemSubject = new Subject<string>();
  deleteProblemActionStream$ = this._deleteProblemSubject.asObservable().pipe(
    // todo call http delete and when done, call _delete
    tap(problemId => this._delete(problemId))
  )
  private _delete = (problemId: UserProblem['problemId']) => {
    this._problemsSubject.next(this.problems.filter(currProblem => currProblem.problemId !== problemId))
  }
  //#endregion


  //#region edit
  // todo
  startEdit = (userProblem: UserProblem) => {
    this._editProblemSubject.next(userProblem);
  }

  private _editProblemSubject = new Subject<UserProblem>();
  editProblemActionStream$ = this._editProblemSubject.asObservable().pipe(
    // todo call http put and when done, call _delete
    tap(userProblem => {
      this._edit(userProblem)
    })
  )

  private _edit = (problem: UserProblem) => {
    this._problemsSubject.next(this.problems.map(currProblem => currProblem.problemId === problem.problemId ? problem : currProblem))
  }
  //#endregion


  //#region filter
  startFilter = (categoryId: string) => {
    this._filterProblemSubject.next(categoryId);
  }

  private _filterProblemSubject = new Subject<string>();
  filterProblemActionStream$ = this._filterProblemSubject.asObservable().pipe(
    // todo call http get to filter data
    tap(categoryId => {
      this._filter(categoryId)
    })
  )

  private _filter = (categoryId: string) => {
    this._problemsSubject.next(this.problems.filter(currProblem => currProblem.categoryId === categoryId))
  }
  //#endregion



  // userProblemswithCategory$ = combineLatest([ // INFO combineLatest check fe/docs
  //   this.userProblems$,
  //   this.problemCategories$])
  //   .pipe(
  //     map(([problems, problemCategories]) => // INFO javascript destructuring to define a name for each array element
  //       problems.map(problem => ({
  //         ...problem, // INFO spread-operator to map and copy values to property matched by name
  //         color: this.markWithColor(problem),
  //         category: problemCategories.find(c => problem.categoryId == c.id)?.name, // INFO find
  //         searchKey: [problem.problemId]
  //       } as UserProblem)))
  //   );

  // private problemSelectedSubject = new BehaviorSubject<string | null>(null)
  // problemSelectedAction$ = this.problemSelectedSubject.asObservable();

  // // INFO Action stream // how to do similar edit and delete actions??
  // private problemInsertedSubject = new Subject<UserProblem>()
  // problemInsertedAction$ = this.problemInsertedSubject.asObservable();

  // // INFO Action stream ??
  // private problemDeletedSubject = new Subject<string>()
  // problemDeletedAction$ = this.problemDeletedSubject.asObservable();

  // // INFO Action stream ??
  // private problemUpdatedSubject = new Subject<UserProblem>()
  // problemUpdatedAction$ = this.problemUpdatedSubject.asObservable();

  // // INFO Combine action stream with data stream
  // problemsWithAdd$ = merge(
  //   this.userProblemswithCategory$,
  //   this.problemInsertedAction$
  // ).pipe(
  //   scan((acc, value) => (value instanceof Array) ? [...value] : [...acc, value], [] as UserProblem[]),
  // )

  // problemsWithDelete$ = merge(
  //   this.problemsWithAdd$,
  //   this.problemDeletedAction$)
  //   .pipe(
  //     scan((acc, value) => (value instanceof Array) ? value.filter(item => item.problemId) : [], [] as UserProblem[])
  //     //??

  //   );

  // deleteProblem = (problemId: string) => {
  //   this.problemsWithAdd$.next(
  //     this.problemCategories.valueOf.filter(das) => das
  //   )
  // }

  // // INFO alternative is to send get via http for product details
  // selectedProblem$ = combineLatest([
  //   //this.userProblemswithCategory$,
  //   this.problemsWithAdd$,
  //   this.problemSelectedAction$
  // ]).pipe(
  //   map(([problems, selectedProblemId]) =>
  //     problems.find(problem => problem.problemId == selectedProblemId)
  //   ),
  //   tap(problem => console.log('selectedProblem', problem))
  // );

  // selectedProblemChanged(selectedProblemId: string): void {
  //   this.problemSelectedSubject.next(selectedProblemId) // INFO emit id to action stream
  // }

  // addProblem(newProblem?: UserProblem) {
  //   newProblem = newProblem ||
  //   {
  //     problemId: "z6a4f74e-4b0a-4487-a6ff-ca2244b4afd9",
  //     userId: "c1bfe7bc-053c-465b-886c-6f55af7ec4fe",
  //     requiredSolutionTypes: "PocInCode; PlanOfImplmentingChangeInCode",
  //     description: "QQQQQQ",
  //     categoryId: "00000000-0000-0000-0000-000000000006",
  //     category: "architecture",
  //     isConfirmed: false,
  //     isRejected: false,
  //     createdAt: "",
  //     color: "	#000000"
  //   };
  //   //this.problemInsertedSubject.next(newProblem)

  //   //todo

  //   of(newProblem) // todo change with http post observable
  //     .pipe(
  //       tap(newProblem => {
  //         this.problemInsertedSubject.next(newProblem)
  //       })
  //     ).subscribe()
  //   //http service post then pie + concatMap / mergeMap to update source-products
  //   // this.http.post<UserProblem[]>(this.userProblemsUrl)

  // }

  // private markWithColor(problem: UserProblem): any {
  //   if (problem.isRejected) {
  //     return "#FF0000"; //red
  //   }
  //   if (problem.isConfirmed) {
  //     return "	#008000"; //green
  //   }
  //   return "#000000" //black
  // }

  private handleError(err: HttpErrorResponse): Observable<never> {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.message}`;
    }
    //console.error(err);
    return throwError(() => errorMessage);
  }

  // getProducts(): Observable<UserProblemDto[]> { // INFO instead of this exposed above userProblems$ property subscribed using async pipe
  //   return this.http.get<UserProblemDto[]>(this.userProblemsUrl)
  //     .pipe(
  //       tap(data => console.log('Products: ', JSON.stringify(data))),
  //       catchError(this.handleError)
  //     );
  // }

  // userProblems$ = this.http.get<UserProblem[]>(this.userProblemsUrl)
  //   .pipe(
  //     map(problems =>
  //       problems.map(problem => ({
  //         ...problem, // INFO spread-operator
  //         color : this.markWithColor(problem),
  //         searchKey: [problem.problemId]
  //       } as UserProblem))),
  //     tap(data => console.log('User Problems: ', JSON.stringify(data))),
  //     catchError(this.handleError)
  //   );
}

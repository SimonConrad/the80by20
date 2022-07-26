import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { BehaviorSubject, catchError, combineLatest, concatMap, map, merge, Observable, of, scan, shareReplay, Subject, tap, throwError } from 'rxjs';

import { UserProblem } from './model/UserProblem'
import { ProblemCategory } from '../shared-model/ProblemCategory'

@Injectable({
  providedIn: 'root'
})
export class DeborathProblemService {
  private userProblemsUrl = 'api/userProblems';
  private problemCategories = 'api/problemCategories';

  constructor(private http: HttpClient) { }

  userProblems$ = this.http.get<UserProblem[]>(this.userProblemsUrl)
    .pipe(
      tap(data => console.log('User Problems: ', JSON.stringify(data))),
      catchError(this.handleError)
    );

  problemCategories$ = this.http.get<ProblemCategory[]>(this.problemCategories)
    .pipe(
      tap(data => console.log('Problem catgeires: ', JSON.stringify(data))),
      //shareReplay(1), // todo naprawić keszowanie
      catchError(this.handleError)
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
          searchKey: [problem.id]
        } as UserProblem)))
    );

  private problemSelectedSubject = new BehaviorSubject<string | null>(null)
  problemSelectedAction$ = this.problemSelectedSubject.asObservable();

  // INFO Action stream // how to do similar edit and delete actions??
  private problemInsertedSubject = new Subject<UserProblem>()
  problemInsertedAction$ = this.problemInsertedSubject.asObservable();


  // INFO Combine action stream with data stream
  problemsWithAdd$ = merge(
    this.userProblemswithCategory$,
    this.problemInsertedAction$
  ).pipe(
    scan((acc, value) => (value instanceof Array) ? [...value] : [...acc, value], [] as UserProblem[]),
  )


  // INFO alternative is to send get via http for product details
  selectedProblem$ = combineLatest([
    //this.userProblemswithCategory$,
    this.problemsWithAdd$,
    this.problemSelectedAction$
  ]).pipe(
    map(([problems, selectedid]) =>
      problems.find(problem => problem.id == selectedid)
    ),
    tap(problem => console.log('selectedProblem', problem))
  );

  selectedProblemChanged(selectedid: string): void {
    this.problemSelectedSubject.next(selectedid) // INFO emit id to action stream
  }

  addProblem(newProblem?: UserProblem) {
    newProblem = newProblem ||
    {
      id: "z6a4f74e-4b0a-4487-a6ff-ca2244b4afd9",
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
    this.problemInsertedSubject.next(newProblem)
  }

  private markWithColor(problem: UserProblem): any {
    if (problem.isRejected) {
      return "#FF0000"; //red
    }
    if (problem.isConfirmed) {
      return "	#008000"; //green
    }
    return "#000000" //black
  }

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
  //         searchKey: [problem.id]
  //       } as UserProblem))),
  //     tap(data => console.log('User Problems: ', JSON.stringify(data))),
  //     catchError(this.handleError)
  //   );
}

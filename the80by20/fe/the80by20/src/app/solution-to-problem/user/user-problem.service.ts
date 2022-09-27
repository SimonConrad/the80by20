import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { BehaviorSubject, catchError, combineLatest, concatMap, switchMap, map, merge, Observable, of, scan, Subject, tap, throwError, finalize } from 'rxjs';

import { UserProblem } from './model/UserProblem'
import { ProblemCategory } from '../shared-model/ProblemCategory'
import { identifierName } from '@angular/compiler';

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
          searchKey: [problem.id]
        } as UserProblem)))
    );


  private categorySelectedSubject = new BehaviorSubject<string>('');
  categorySelectedAction$ = this.categorySelectedSubject.asObservable();

  userProblemswithCategoryFiltered$ = combineLatest([
    this.userProblemswithCategory$,
    this.categorySelectedAction$
  ])
    .pipe(
      map(([problems, selectedCategoryId]) =>
        problems.filter(item =>
          selectedCategoryId ? item.categoryId === selectedCategoryId : true)
      )
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


  //#region busy-indicator
  private _actionInProgress = new BehaviorSubject<boolean>(false);
  actionInProgressDataStream$ = this._actionInProgress.asObservable();

  startActionInProgress = () => {
    this._actionInProgress.next(true)
  }

  stopActionInProgress = () => {
    this._actionInProgress.next(false)
  }
  //#endregion

  //#region init
  startInit = () => {
    this._initProblemSubject.next('');
  }

  private _initProblemSubject = new BehaviorSubject('');
  initProblemActionStream$ = this._initProblemSubject.asObservable()
    .pipe(
      tap(() => this.startActionInProgress()),
      //switchMap(() => this.userProblemswithCategory$), // TODO switchMap????
      switchMap(() => this.userProblemswithCategoryFiltered$), // TODO switchMap????
      tap(res => this._problemsSubject.next(res))
    );
  //#endregion


  //#region delete
  startDelete = (id: string) => {
    this._deleteProblemSubject.next(id);

  return this.http.delete(`${this.userProblemsUrl}/${id}`)
  }

  private _deleteProblemSubject = new Subject<string>();
  deleteProblemActionStream$ = this._deleteProblemSubject.asObservable().pipe(
    switchMap(id =>
      {
        return this.http.delete(`${this.userProblemsUrl}/${id}`);
      }),
      tap(() => this.startInit())
    // tap(id =>{
    //   this._delete(id);
    // } )
  )
  private _delete = (id: UserProblem['id']) => {
    this._problemsSubject.next(this.problems.filter(currProblem => currProblem.id !== id))
  }
  //#endregion


  //#region update
  // todo
  startUpdate = (userProblem: UserProblem) => {
    this._updateProblemSubject.next(userProblem);
  }

  private _updateProblemSubject = new Subject<UserProblem>();
  updateProblemActionStream$ = this._updateProblemSubject.asObservable().pipe(
    // todo call http put and when done, call _delete switchMap(?
    tap(userProblem => {
      this._update(userProblem)
    })
  )

  private _update = (problem: UserProblem) => {
    this._problemsSubject.next(this.problems.map(currProblem => currProblem.id === problem.id ? problem : currProblem))
  }
  //#endregion

  //#region add
  startAdd = (userProblem: UserProblem) => {
    this._addProblemSubject.next(userProblem);
  }

  private _addProblemSubject = new Subject<UserProblem>();
  addProblemActionStream$ = this._addProblemSubject.asObservable().pipe(
    tap(problem => this._add(problem)) // todo uncomment and call backendd switchMap(
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

  //#region select
  startSelect = (id: string | undefined) => {
    this._selectProblemSubject.next(id);
  }
  private _selectProblemSubject = new Subject<string | undefined>();
  selectProblemActionStream$ = this._selectProblemSubject.asObservable().pipe(
    // todo call http-get/id switchMap(
    map(id => {
      if (id === null) {
        return undefined;
      }

      if (id === '') {
        let newProblem: UserProblem =
        {
          id: '', //todo new guid
          userId: "c1bfe7bc-053c-465b-886c-6f55af7ec4fe", // todo get from user service
          requiredSolutionTypes: "",
          description: "",
          categoryId: "",
          category: "",
          isConfirmed: false,
          isRejected: false,
          createdAt: "",
          color: "#000000"
        };

        return newProblem;
      }

      return this.problems.find(p => p.id === id);
    })
  )
  //#endregion

  //#region filter
  startFilter = (categoryId: string) => {
    this.categorySelectedSubject.next(categoryId);
  }
  //#endregion

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
}

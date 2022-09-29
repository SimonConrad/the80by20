import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { BehaviorSubject, catchError, combineLatest, switchMap, map, Observable, Subject, tap, throwError, finalize, concatMap } from 'rxjs';

import { UserProblem } from './model/UserProblem'
import { ProblemCategory } from '../shared-model/ProblemCategory'

@Injectable({
  providedIn: 'root'
})
export class UserProblemService {
  private userProblemsUrl = 'api/userProblems';
  private problemCategories = 'api/problemCategories';

  constructor(private http: HttpClient) { // INFO data should not be loaded from the constructor
  }

  private problemsSubject = new BehaviorSubject<UserProblem[]>([]);
  problemsDataStream$ = this.problemsSubject.asObservable();
  get problems() {
    return this.problemsSubject.value;
  }

  problemCategories$ = this.http.get<ProblemCategory[]>(this.problemCategories)
    .pipe(
      tap(data => console.log('Problem categories:', JSON.stringify(data))),
      //shareReplay(1), // todo keszowanie
      catchError(this.handleError)
    );

  private userProblems$ = this.http.get<UserProblem[]>(this.userProblemsUrl)
    .pipe(
      tap(data => console.log('User Problems: ', JSON.stringify(data))),
      catchError(this.handleError),
      finalize(() => this.stopActionInProgress())
    );

  private userProblemswithCategory$ = combineLatest([ // INFO combineLatest check fe/docs
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

  // emitting is invoked from component click category handler onCategorySelected (which calls this service categorySelectedSubject.next)
  // INFO categorySelectedAction$ is not subscribed but it is working, probably beacouse rxjs method combineLatest subscribes it
  private categorySelectedSubject = new BehaviorSubject<string>('');
  private categorySelectedAction$ = this.categorySelectedSubject.asObservable();

  private userProblemswithCategoryFiltered$ = combineLatest([
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
  private actionInProgress = new BehaviorSubject<boolean>(false);
  actionInProgressDataStream$ = this.actionInProgress.asObservable();

  private startActionInProgress = () => {
    this.actionInProgress.next(true)
  }

  private stopActionInProgress = () => {
    this.actionInProgress.next(false)
  }
  //#endregion

  //#region init
  startInitializeProblems = () => {
    this.initProblemSubject.next('');
  }

  private initProblemSubject = new BehaviorSubject('');
  initProblemActionStream$ = this.initProblemSubject.asObservable()
    .pipe(
      tap(() => this.startActionInProgress()),
      switchMap(() => this.userProblemswithCategoryFiltered$), ////INFO conctaMap, mergeMap, switchMap described in fe\docs\higher-order mapping operators\
      tap(res => this.problemsSubject.next(res))
    );
  //#endregion

  //#region delete
  startDeleteAction = (id: string) => {
    this.deleteProblemSubject.next(id);
  }

  private deleteProblemSubject = new Subject<string>();
  deleteProblemActionStream$ = this.deleteProblemSubject.asObservable().pipe(
    switchMap(id => { //INFO conctaMap, mergeMap, switchMap described in fe\docs\higher-order mapping operators\
      return this.http.delete(`${this.userProblemsUrl}/${id}`) // INFO usage of inMemoryWebApi
      .pipe(tap(() => {
        this.delete(id);
      })); //info https://medium.com/@snorredanielsen/rxjs-accessing-a-previous-value-further-down-the-pipe-chain-b881026701c1
    }),
    //tap(() => this.startInitializeProblems()), //INFO to make thinks simplers change above tap with this for refresh from server
    catchError(this.handleError)

  )

  private delete = (id: UserProblem['id']) => {
    this.problemsSubject.next(this.problems.filter(currProblem => currProblem.id !== id))
  }
  //#endregion

  //#region update
  // todo
  startUpdate = (userProblem: UserProblem) => {
    this.updateProblemSubject.next(userProblem);
  }

  private updateProblemSubject = new Subject<UserProblem>();
  updateProblemActionStream$ = this.updateProblemSubject.asObservable().pipe(
    switchMap((userProblem) => {  //INFO conctaMap, mergeMap, switchMap described in fe\docs\higher-order mapping operators\
      return this.http.put<UserProblem>(this.userProblemsUrl, userProblem)  //INFO usage of inMemoryWebApi
      .pipe(map(() => { return userProblem }));
    }),
    tap(userProblem => {
      this.update(userProblem)
    }),
    //tap(() => this.startInitializeProblems()),
    catchError(this.handleError)
  )

  private update = (problem: UserProblem) => {
    this.problemsSubject.next(this.problems.map(currProblem => currProblem.id === problem.id ? problem : currProblem))
  }
  //#endregion

  //#region add
  startAdd = (userProblem: UserProblem) => {
    this.addProblemSubject.next(userProblem);
  }

  private addProblemSubject = new Subject<UserProblem>();
  addProblemActionStream$ = this.addProblemSubject.asObservable().pipe(
    switchMap((userProblem) => { //INFO conctaMap, mergeMap, switchMap described in fe\docs\higher-order mapping operators\
      return this.http.post<UserProblem>(this.userProblemsUrl, userProblem)  //INFO usage of inMemoryWebApi
      .pipe(map(() => { return userProblem }));
    }),
    //tap(() => this.startInitializeProblems()),
    tap(problem => this.add(problem)),
    catchError(this.handleError),
  );

  private add = (problem: UserProblem) => {
    this.problemsSubject.next([
      ...this.problems,
      problem
    ])
  }
  //#endregion

  //#region select
  startSelectAction = (id: string | undefined) => {
    this.selectProblemSubject.next(id);
  }
  private selectProblemSubject = new Subject<string | undefined>();
  selectProblemActionStream$ = this.selectProblemSubject.asObservable().pipe(
    // todo call http-get/id
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
  startFilterAction = (categoryId: string) => {
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

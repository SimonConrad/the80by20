import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { catchError, combineLatest, map, Observable, tap, throwError } from 'rxjs';

import { UserProblem } from '../model/UserProblem'
import { ProblemCategory } from '../model/ProblemCategory'

@Injectable({
  providedIn: 'root'
})
export class UserProblemService {
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
      catchError(this.handleError)
    );

  userProblemswithCategory$ = combineLatest([ // INFO combineLatest
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

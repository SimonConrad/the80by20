import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { catchError, map, Observable, tap, throwError } from 'rxjs';

import { UserProblem } from './model/UserProblem'

@Injectable({
  providedIn: 'root'
})
export class SolutionToProblemService {
  private userProblemsUrl = 'api/userProblems';
  private userSolutionsToProblemsUrl = 'api/userSolutionsToProblems';

  userProblems$ = this.http.get<UserProblem[]>(this.userProblemsUrl)
    .pipe(
      map(problems =>
        problems.map(problem => ({
          ...problem, // INFO spread-operator
          color : this.markWithColor(problem),
          searchKey: [problem.problemId]
        } as UserProblem))),
      tap(data => console.log('User Problems: ', JSON.stringify(data))),
      catchError(this.handleError)
    );

  // getProducts(): Observable<UserProblemDto[]> { // INFO instead of this exposed above userProblems$ property subscribed using async pipe
  //   return this.http.get<UserProblemDto[]>(this.userProblemsUrl)
  //     .pipe(
  //       tap(data => console.log('Products: ', JSON.stringify(data))),
  //       catchError(this.handleError)
  //     );
  // }

  constructor(private http: HttpClient) { }

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
    console.error(err);
    return throwError(() => errorMessage);
  }
}

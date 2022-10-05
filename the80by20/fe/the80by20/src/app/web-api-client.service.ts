import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, ObservableInput, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProblemCategory } from './solution-to-problem/shared-model/ProblemCategory';
import { UserProblem } from './solution-to-problem/user/model/UserProblem';

@Injectable({
  providedIn: 'root'
})
export class WebApiClientService {

  private readonly useInMemoryWebApi: boolean = environment.useInMemoryWebApi;
  private readonly userProblemsUrl = 'api/userProblems';
  private readonly problemCategoriesUrl = 'api/problemCategories';

  private readonly baseUrl: string = environment.baseApiUrl;
  private readonly apiUrl: string = `${this.baseUrl}/api`;
  private readonly usersUrl: string = `${this.baseUrl}/security/users/`;
  private readonly problemsUrl: string = `${this.baseUrl}/solution-to-problem/problems/`;

  constructor(private http: HttpClient) { }

  //#region api
  applicationData() {
    // TODO refactor to 2 implmentations of interface-web-api and factory which decides which one to register into ioc container based on env
    // also based on env import InMemoryWebApiModule
    // https://stackoverflow.com/questions/39942118/how-to-inject-different-service-based-on-certain-build-environment-in-angular2
    if (this.useInMemoryWebApi) {
      return of('The 80 by 20');
    } else {
      return this.http.get<string>(`${this.apiUrl}`);
    }
  }
  //#endregion

  //#region security
  signIn(userData: any) {
    if (this.useInMemoryWebApi) {
      //INFO generated at https://jwt.io/
      const token = environment.devjwtToken;
      return of(token);
    } else {
      let signInPayload = {
        email: "admin@wp.pl",
        password: "secret"
      }

      // let signInPayload = {
      //   email: "user1@wp.pl",
      //   password: "secret"
      // }

      return this.http.post(`${this.usersUrl}sign-in`, signInPayload)
        .pipe(map(token => {
          const result: any = token;
          return result.accessToken
        }));
    }
  }

  register(userData: any) {
    // if(this.useInMemoryWebApi) { 

    // } else{

    // }
    // TODO
    return this.http.post(`${this.usersUrl}`, userData);
  }

  me() {
    if (this.useInMemoryWebApi) {
      return of('user');
    } else {
      return this.http.get(`${this.usersUrl}me`)
    }
  }
  //#endregion

  //#region problems
  problemCategories(): Observable<ProblemCategory[]> {
    if (this.useInMemoryWebApi) {
      return this.http.get<ProblemCategory[]>(this.problemCategoriesUrl);
    } else {
      return this.http.get<any>(`${this.problemsUrl}categories-and-solution-types`)
        .pipe(map(result => result.categories))
    }
  }

  userProblems(): Observable<UserProblem[]> {
    if (this.useInMemoryWebApi) {
      return this.http.get<UserProblem[]>(this.userProblemsUrl)
    } else {
      return this.http.get<any>(`${this.problemsUrl}`)
    }
  }

  deleteUserProblem(id: any ) {
    if (this.useInMemoryWebApi) {
      return this.http.delete(`${this.userProblemsUrl}/${id}`)
    } else {
      // todo
      return this.http.delete(`${this.userProblemsUrl}/${id}`)
    }
  }

  updateUserProblem(problem: UserProblem) {
    if (this.useInMemoryWebApi) {
      return this.http.put<UserProblem>(this.userProblemsUrl, problem)
    } else {
      //todo
      return this.http.put<UserProblem>(this.userProblemsUrl, problem)
    }
  }

  addUserProblem(problem: UserProblem) {
    if (this.useInMemoryWebApi) {
      return this.http.post<UserProblem>(this.userProblemsUrl, problem)
    } else {
      //todo
      return this.http.post<UserProblem>(this.userProblemsUrl, problem)
    }
  }
  //#endregion

}

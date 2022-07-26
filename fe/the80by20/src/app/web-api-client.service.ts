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
  private readonly appApiUrl: string = `${this.baseUrl}/api`;
  private readonly usersApiUrl: string = `${this.baseUrl}/users/users/`;
  private readonly problemsApiUrl: string = `${this.baseUrl}/solution-to-problem/problems/`;

  constructor(private http: HttpClient) { }

  //#region api
  applicationData() {
    // TODO refactor to 2 implmentations of interface-web-api and factory which decides which one to register into ioc container based on env
    // also based on env import InMemoryWebApiModule
    // https://stackoverflow.com/questions/39942118/how-to-inject-different-service-based-on-certain-build-environment-in-angular2
    if (this.useInMemoryWebApi) {
      return of('The 80 by 20 [in memory web api]');
    } else {
      return this.http.get<string>(`${this.appApiUrl}`);
    }
  }
  //#endregion

  //#region users
  signIn(userData: any) {
    if (this.useInMemoryWebApi) {
      //INFO generated at https://jwt.io/
      const token = environment.devjwtToken;
      return of(token);
    } else {
      const signInPayload = {
        email: userData.email,
        password: userData.password
      }

      return this.http.post(`${this.usersApiUrl}sign-in`, signInPayload)
        .pipe(map(token => {
          const result: any = token;
          return result.accessToken
        }));
    }
  }

  register(userData: any) {
    // TODO
    return this.http.post(`${this.usersApiUrl}`, userData);
  }

  me() {
    if (this.useInMemoryWebApi) {
      return of('user');
    } else {
      return this.http.get(`${this.usersApiUrl}me`)
    }
  }
  //#endregion

  //#region problems
  problemCategories(): Observable<ProblemCategory[]> {
    if (this.useInMemoryWebApi) {
      return this.http.get<ProblemCategory[]>(this.problemCategoriesUrl);
    } else {
      return this.http.get<any>(`${this.problemsApiUrl}categories-and-solution-types`)
        .pipe(map(result => result.categories))
    }
  }

  userProblems(): Observable<UserProblem[]> {
    if (this.useInMemoryWebApi) {
      return this.http.get<UserProblem[]>(this.userProblemsUrl)
    } else {
      return this.http.get<any>(`${this.problemsApiUrl}`)
    }
  }

  addUserProblem(problem: UserProblem) {
    if (this.useInMemoryWebApi) {
      return this.http.post<UserProblem>(this.userProblemsUrl, problem)
    } else {
      
      //todo
      const addUserProblemPayload = {
        description: problem.description,
        category: "00000000-0000-0000-0000-000000000006",
        solutionElementTypes: [
          0,1,2,3
        ]
      }
      return this.http.post<UserProblem>(this.problemsApiUrl, addUserProblemPayload)
    }
  }

  updateUserProblem(problem: UserProblem) {
    if (this.useInMemoryWebApi) {
      return this.http.put<UserProblem>(this.userProblemsUrl, problem)
    } else {
      //todo
      const updateUserProblemPayload = {
        problemId: problem.id,
        description: problem.description,
        category: "00000000-0000-0000-0000-000000000006",
        updateScope: 1
      }

      return this.http.put<UserProblem>(this.problemsApiUrl, updateUserProblemPayload)
    }
  }

  deleteUserProblem(id: any ) {
    if (this.useInMemoryWebApi) {
      return this.http.delete(`${this.userProblemsUrl}/${id}`)
    } else {
      // todo do backend (buisness rules - for example cannot delete if accpeted or sopmething....)
      return of('')// this.http.delete(`${this.userProblemsUrl}/${id}`)
    }
  }
  //#endregion
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WebApiClientService {
  private readonly baseUrl: string = environment.baseApiUrl;

  private readonly apiUrl: string = `${this.baseUrl}/api`;
  private readonly  usersUrl: string = `${this.baseUrl}/security/users/`;

  constructor(private http: HttpClient) { }

  //#region api
  applicationData(){
    //return of('The 80 by 20');
    
    return this.http.get<string>(`${this.baseUrl}/api`);
  } 
  //#endregion

  //#region security
  signIn(userData: any) {   
    
    //INFO generated at https://jwt.io/
    // const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiYWRtaW4iLCJ1c2VybmFtZSI6IlNpbW9uIiwiZXhwIjoxNTE2MjM5MDIyfQ.RxwTuNFAYlRkapdnk7FuyiZV5D3OlpVVrMvQwBpzXmI"
    // return  of(token);
         
    let signInPayload = {
      email: "user1@wp.pl",
      password: "secret"
    }
    return this.http.post(`${this.usersUrl}sign-in`, signInPayload)
    .pipe(map(token => {
      const result : any = token;
      return result.accessToken
    }));
  }

  register(userData: any) {

    // TODO
    return this.http.post(`${this.usersUrl}`, userData);
  }
  
  me(){
    return of('user');

    return this.http.get(`${this.usersUrl}me`)
   
  }
  //#endregion

}

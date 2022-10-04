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
    return of('The 80 by 20');
    
    //return this.http.get<string>(`${this.baseUrl}/api`);
  } 
  //#endregion

  //#region security
  signIn(userData: any) {
       
    //INFO generated at https://jwt.io/
    const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjYWY1MTkyYS1kMzc2LTRiZTgtYTQ4Yy01NGU0NGI3MTMxYmEiLCJ1bmlxdWVfbmFtZSI6ImNhZjUxOTJhLWQzNzYtNGJlOC1hNDhjLTU0ZTQ0YjcxMzFiYSIsIm5hbWUiOiJ1c2VyMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6InVzZXIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6InVzZXIxIiwibmJmIjoxNjY0ODYzMDUzLCJleHAiOjE2NjQ4NjY2NTMsImlzcyI6InRoZTgwYnkyMC1pc3N1ZXIiLCJhdWQiOiJ0aGU4MGJ5MjAtYXVkaWVuY2UifQ.p5DVB4BZcuzz65BAZXhzc1tJt-hJUcujkzR-VSlRgKo"
    return of(token);
         
    // let signInPayload = {
    //   email: "user1@wp.pl",
    //   password: "secret"
    // }
    // return this.http.post(`${this.usersUrl}sign-in`, signInPayload)
    // .pipe(map(token => {
    //   const result : any = token;
    //   return result.accessToken
    // }));
  }

  register(userData: any) {

    // TODO
    return this.http.post(`${this.usersUrl}`, userData);
  }
  
  me(){
    return of('user');

    //return this.http.get(`${this.usersUrl}me`)
   
  }
  //#endregion

}

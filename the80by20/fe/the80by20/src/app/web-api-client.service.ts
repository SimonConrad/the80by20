import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WebApiClientService {
  private readonly baseUrl: string = environment.baseApiUrl;
  private readonly  usersUrl: string = `${this.baseUrl}/security/users/`;

  constructor(private http: HttpClient) { }

  //applicationData$ = this.http.get<string>(`${this.baseUrl}/api`);
  applicationData$ = of('The 80 by 20');

  signIn$ = () => {
    let signInPayload = {
      email: "user1@wp.pl",
      password: "secret"
    }

    return this.http.post(`${this.usersUrl}sign-in`, signInPayload);
  }
  
  //me$ = this.http.get(`${this.usersUrl}me`);
  me$ = of('user');

}

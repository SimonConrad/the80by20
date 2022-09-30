
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import * as moment from 'moment';
import { environment } from 'src/environments/environment';

const jwt = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl: string = environment.baseApiUrl;
  private uriseg = this.baseUrl + 'api/users';
  private decodedToken: DecodedToken = new DecodedToken()

  constructor(private http: HttpClient) {
    //INFO '!' is used for: Type 'string | null' is not assignable to type 'string'. you can use the non-null assertion operator to tell typescript that you know what you are doing:
    this.decodedToken = JSON.parse(localStorage.getItem('auth_meta')!) || new DecodedToken();
  }

  public register(userData: any): Observable<any> {
    const URI = this.uriseg + '/register';
    return this.http.post(URI, userData);
  }

  public login(userData: any): Observable<any> {
    // const URI = this.uriseg + '/login';
    // return this.http.post(URI, userData).pipe(map(token => {
    //   return this.saveToken(token);
    // }));

    // INFO https://jwt.io/
    const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiYWRtaW4iLCJ1c2VybmFtZSI6IlNpbW9uIiwiZXhwIjoxNTE2MjM5MDIyfQ.RxwTuNFAYlRkapdnk7FuyiZV5D3OlpVVrMvQwBpzXmI"
    return of(token).pipe(map(token => {
        return this.saveToken(token);
      }));
  }

  private saveToken(token: any): any {
    this.decodedToken = jwt.decodeToken(token);
    localStorage.setItem('auth_tkn', token);
    localStorage.setItem('auth_meta', JSON.stringify(this.decodedToken));
    return token;
  }

  public logout(): void {
    localStorage.removeItem('auth_tkn');
    localStorage.removeItem('auth_meta');

    this.decodedToken = new DecodedToken();
  }

  public isAuthenticated(): boolean {
    console.log(this.decodedToken.exp);


    if(this.decodedToken.username != '') {
      return true;
    }

    return false;

    // TODO: return moment().isBefore(moment.unix(this.decodedToken.exp));
  }


  public getUsername(): string {
    return this.decodedToken.username;
  }

}

class DecodedToken {
  exp: number = 0;
  username: string = '';
}

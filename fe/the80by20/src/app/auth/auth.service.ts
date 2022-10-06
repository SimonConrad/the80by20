import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { WebApiClientService } from '../web-api-client.service';
import * as moment from 'moment';

const jwt = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private decodedToken: DecodedToken = new DecodedToken()

  constructor(private webApiClient: WebApiClientService) {
    //INFO '!' is used for: Type 'string | null' is not assignable to type 'string'. you can use the non-null assertion operator to tell typescript that you know what you are doing:
    this.decodedToken = JSON.parse(localStorage.getItem('auth_meta')!) || new DecodedToken();
  }

  public register(userData: any): Observable<any> {
    // TODO
    return this.webApiClient.register(userData);
  }

  public login(userData: any): Observable<any> {
    // return this.webApiClient.signIn$().pipe(map(token => {
    //   const t : any = token;
    //   return this.saveToken(t.accessToken);
    // }));

        
    return this.webApiClient.signIn(userData).pipe(map(token => {
        return this.saveToken(token);
      }));
  }

  private saveToken(token: any): any {
    let res = jwt.decodeToken(token);
    res.role = res["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]; //role
    res.username = res["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]; //username
    this.decodedToken = {...res};

    console.log(`username: ${this.decodedToken.username}`);
    console.log(`expiration:  ${this.decodedToken.exp}`);
    console.log(`expiration datetime:  ${moment.unix(this.decodedToken.exp)}`);
    console.log(`role: ${ this.decodedToken.role}`);

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
    if(environment.useInMemoryWebApi){
      if(this.decodedToken.username != '') {
        return true;
      }
    }
 
    const exp = moment.unix(this.decodedToken.exp)
    return moment().isBefore(exp);
  }

  public getUsername(): string {
    return this.decodedToken.username;
  }
}

class DecodedToken {
  exp: number = 0;
  username: string = '';
  role: string = '';
}

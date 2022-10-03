import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WebApiClientService {
  private baseUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  //applicationData$ = this.http.get<string>(`${this.baseUrl}/api`);
  applicationData$ = of('The 80 by 20');
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { WebApiClientService } from 'src/app/web-api-client.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

  constructor(public auth: AuthService, 
    private router: Router, 
    private webApiClient: WebApiClientService) { }



  appName$ = this.webApiClient.applicationData$;

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/auth/login'], {queryParams: {loggedOut: 'success'}});
  }
}

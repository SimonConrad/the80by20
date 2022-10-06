import { Component, OnInit } from '@angular/core';
import { AuthService } from './../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  formData: any = {};
  errors: any = [];

  constructor(private auth: AuthService, private router: Router) { }

  register(): void {
    // this.errors = [];
    // this.auth.register(this.formData)
    //   .subscribe(() => {
    //     this.router.navigate(['/auth/login'], { queryParams: { registered: 'success' } });
    //    },
    //     (errorResponse) => {
    //       this.errors.push(errorResponse.error.error);
    //     });

    console.log(this.formData);
    this.router.navigate(['/auth/login'], { queryParams: { registered: 'success' } });
  }
}

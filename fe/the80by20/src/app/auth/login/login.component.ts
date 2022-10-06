import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from './../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required]);

  loginForm: FormGroup = this.fb.group({
    email: this.email,
    password: this.password
  });

  errors: any = [];
  notify: string | undefined;
  hide = true;

  constructor(private auth: AuthService,
    private router: Router,
    private fb: FormBuilder,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      const key1 = 'registered';
      const key2 = 'loggedOut';
      if (params[key1] === 'success') {
        this.notify = 'You have been successfully registered. Please Log in';
      }
      if (params[key2] === 'success') {
        this.notify = 'You have been loggedout successfully';
      }
    });
  }

  getEmailErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }

  getPasswordErrorMessage() {
    if (this.password.hasError('required')) {
      return 'You must enter a value';
    }

    return this.password.hasError('password') ? 'Not a valid password' : '';
  }

  // isInvalidInput(fieldName:any): boolean {
  //   return this.loginForm.controls[fieldName].invalid &&
  //     (this.loginForm.controls[fieldName].dirty || this.loginForm.controls[fieldName].touched);
  // }

  login(): void {
    console.log(this.loginForm.value);

    this.errors = [];
    this.auth.login(this.loginForm.value)
      .subscribe((token) => {
        this.router.navigate(['/solution-to-problem'], { queryParams: { loggedin: 'success' } });
       },
        (errorResponse) => {
          this.errors.push(errorResponse.error.error);
        });
  }
}

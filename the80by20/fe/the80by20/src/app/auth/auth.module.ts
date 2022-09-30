import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; // TODO consider instead importing SharedModule ?


import { Routes, RouterModule } from '@angular/router';

import { AuthComponent } from './auth.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';

import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';

// INFO based on https://appdividend.com/2022/02/02/angular-authentication/

const routes: Routes = [
  {
    path: 'auth',
    component: AuthComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent }
    ]
  }
];

@NgModule({
  declarations: [
    AuthComponent, // TODO check if it should be declared
    RegisterComponent,
    LoginComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ],
  exports: [RouterModule],
  providers: [AuthService, AuthGuard]
})
export class AuthModule { }

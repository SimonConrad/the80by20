import { NgModule } from '@angular/core'; // TODO consider instead importing SharedModule ?
import { CommonModule } from '@angular/common';

import { AuthComponent } from './auth.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';

import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';

// INFO based on https://appdividend.com/2022/02/02/angular-authentication/

@NgModule({
  declarations: [
    AuthComponent, // TODO check if it should be declared
    RegisterComponent,
    LoginComponent
  ],
  imports: [
    CommonModule
  ],
  providers: [AuthService, AuthGuard]
})
export class AuthModule { }

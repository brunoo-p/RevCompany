import { Routes } from '@angular/router';
import { SigninComponent } from './components/auth/signin/signin.component';
import { SignupComponent } from './components/auth/signup/signup.component';

export const routes: Routes = [
  {
    path: "login",
    component: SigninComponent
  },
  {
    path: "register",
    component: SignupComponent
  },
  {
    path: "**",
    redirectTo: "login"
  }
];

import { Routes } from '@angular/router';
import { SigninComponent } from './pages/signin/signin.component';
import { SignupComponent } from './pages/signup/signup.component';
import { HomeComponent } from './pages/home/home.component';
import authGuard from './services/api/auth/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [authGuard]
  },
  {
    path: 'login',
    component: SigninComponent
  },
  {
    path: "register",
    component: SignupComponent
  },
  {
    path: "**",
    pathMatch: 'full',
    redirectTo: 'login'
  }
];

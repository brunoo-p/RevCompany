import { Component } from '@angular/core';
import { PrimaryInputComponent } from "../fields/primary-input/primary-input.component";
import { LoginLayoutComponent } from "../login-layout/login-layout.component";
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthenticationFacade } from '../facade/authentication.facade';

@Component({
  selector: 'app-signin',
  imports: [PrimaryInputComponent, LoginLayoutComponent, ReactiveFormsModule],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.css'
})
export class SigninComponent {  
  loginForm!: FormGroup;

  constructor(
    private readonly authFacade: AuthenticationFacade,
    private readonly router: Router
  ){
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])
    })
  }

  async submit() : Promise<void>{

    if(this.loginForm.valid) {
      await this.authFacade.instance().signIn(this.loginForm.value);
      this.router.navigate(['']);
    }
  }

  navigate(){
    this.router.navigate(["register"]);
  }
}

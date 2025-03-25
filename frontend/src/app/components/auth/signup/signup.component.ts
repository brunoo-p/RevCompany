import { Component } from '@angular/core';
import { LoginLayoutComponent } from "../login-layout/login-layout.component";
import { PrimaryInputComponent } from "../fields/primary-input/primary-input.component";
import { FormControl, FormGroup, ReactiveFormsModule,  Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationFacade } from '../facade/authentication.facade';


@Component({
  selector: 'app-signup',
  imports: [LoginLayoutComponent, PrimaryInputComponent, ReactiveFormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  registerForm: FormGroup;

  constructor(
    private readonly authFacade: AuthenticationFacade,
    private readonly router: Router
  ) {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])

    })
  }

  async submit(): Promise<void>{
    if(this.registerForm.valid) {
      await this.authFacade.instance().signUp(this.registerForm.value);
      this.router.navigate(['']);
    }
  }
  
  navigate(){
    this.router.navigate(["login"])
  }
}

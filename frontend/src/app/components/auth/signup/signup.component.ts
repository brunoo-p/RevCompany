import { Component } from '@angular/core';
import { LoginLayoutComponent } from "../login-layout/login-layout.component";
import { PrimaryInputComponent } from "../fields/primary-input/primary-input.component";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-signup',
  imports: [LoginLayoutComponent, PrimaryInputComponent, ReactiveFormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  registerForm: FormGroup;

  constructor(
    private readonly router: Router
  ) {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])

    })
  }

  submit(){
    console.log('submit');
  }
  
  navigate(){
    this.router.navigate(["signup"])
  }
}

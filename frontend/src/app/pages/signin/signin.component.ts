import { Component } from '@angular/core';
import { PrimaryInputComponent } from "../../components/auth/fields/primary-input/primary-input.component";
import { LoginLayoutComponent } from "../../components/auth/login-layout/login-layout.component";
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthenticationFacade } from '../../components/auth/facade/authentication.facade';
import UserAuthService from '../../services/api/auth/user/userAuth.service';

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
    private readonly router: Router,
    private readonly userAuthService: UserAuthService
  ){
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])
    })
  }

  async submit() : Promise<void>{

    if(this.loginForm.valid) {
      await this.authFacade.instance().signIn(this.loginForm.value);
      this.router.navigateByUrl("/");
    }
  }

  navigate(){
    this.router.navigateByUrl("register");
  }

  handleKeyUp(e: KeyboardEvent){
    if(e.keyCode === 13){
      this.submit();
    }
  }
}

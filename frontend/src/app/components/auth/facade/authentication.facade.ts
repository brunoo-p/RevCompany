
import { Injectable } from "@angular/core";
import { BRegisterRequest } from '../../../services/domain/auth/registerRequest';
import { BLoginRequest } from '../../../services/domain/auth/loginRequest';
import { LoginService } from '../../../services/api/login/login.service';

import { Email } from '../../../services/domain/auth/credential/email';
import { Password } from '../../../services/domain/auth/credential/password';
import { LoginType, RegisterType } from '../types';
import { ContextAuthService } from '../../../services/api/context/contextAuth.service';
import { Router } from "@angular/router";
import UserAuthService from "../../../services/api/auth/user/userAuth.service";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationFacade {

  constructor(
    private readonly loginService: LoginService,
    private readonly userAuthService: UserAuthService,
    private readonly contextAuthService: ContextAuthService,
    private readonly router: Router
  ) {}

  private async signIn(login: LoginType, keepConnected = true): Promise<void> {

    const createLogin = new BLoginRequest(
      login.email,
      login.password
    );

    const response = await this.loginService.instance().signIn(createLogin);
    this.authenticate(response);
  }

  private async signUp(register: RegisterType): Promise<void> {

    const email = new Email(register.email);
    const password = new Password(register.password);
    const createRegister = new BRegisterRequest(
      register.firstName,
      register.lastName,
      email.value,
      password.value,
    );

   const response = await this.loginService.instance().signUp(createRegister);
   this.authenticate(response);

  }

  private authenticate(authenticateUser: any, keepConnected = true) {
    this.contextAuthService.userProfile = authenticateUser;
    
    if(authenticateUser.isActive) {
      this.userAuthService.authenticateUser( this.contextAuthService.userProfile);
    }
    this.router.navigate(['']);
  }

  public instance = (): IAuthFacade => ({
    signIn: (login: LoginType, keepConnected?: boolean) => this.signIn(login, keepConnected),
    signUp: (register: RegisterType) => this.signUp(register)
  })

}
interface IAuthFacade {
  signIn: (login: LoginType, keepConnected?: boolean) => Promise<void>
  signUp: (register: RegisterType) => Promise<void>
}

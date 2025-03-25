
import { Injectable } from "@angular/core";


import { StorageManagerService } from '../../../services/domain/utils/storage/storageManager.service';
import { RegisterRequest } from '../../../services/domain/auth/registerRequest';
import { LoginRequest } from '../../../services/domain/auth/loginRequest';
import { LoginService } from '../../../services/api/login/login.service';

import { Email } from '../../../services/domain/auth/credential/email';
import { Password } from '../../../services/domain/auth/credential/password';
import { LoginType, RegisterType } from '../types';
import { ContextAuthService } from '../../../services/api/context/contextAuth.service';
import { StorageKey } from "../../../services/domain/utils/storage/storage-keys";



@Injectable({
  providedIn: 'root'
})
export class AuthenticationFacade {

  constructor(
    private readonly loginService: LoginService,
    private readonly storageManager: StorageManagerService,
    private readonly contextAuthService: ContextAuthService,
  ) {}

  private setKeepConnected(data: any) {
    this.storageManager.setItem(StorageKey.user, data);
  }

  private async signIn(login: LoginType, keepConnected = true): Promise<void> {

    const createLogin = new LoginRequest(
      login.email,
      login.password
    );

    const response = await this.loginService.instance().signIn(createLogin);

    this.contextAuthService.userProfile = response;
    console.debug(response);
    
    if(response.isActive && keepConnected) {
      this.setKeepConnected( this.contextAuthService.userProfile);
    }
  }

  private async signUp(register: RegisterType): Promise<void> {
    const createRegister = new RegisterRequest(
      register.firstName,
      register.lastName,
      new Email(register.email),
      new Password(register.password),
    );

   await this.loginService.instance().signUp(createRegister);

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

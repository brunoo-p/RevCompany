import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { ApiCallerService } from '../apiCaller.service';
import { HttpStatusCode } from '../http/httpType';
import { IAuth } from '../../contracts/IAuth';
import { LoginRequest } from '../../domain/auth/loginRequest';
import { RegisterRequest } from '../../domain/auth/registerRequest';
import { AuthenticationMapperService } from '../../domain/auth/authenticationMapperService';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private readonly _baseUrl = environment.baseUrl;
  private readonly api: HttpClient;

  constructor(
    private readonly apiCallerService: ApiCallerService,
    private readonly authenticationMapperService: AuthenticationMapperService,
    private readonly httpClient: HttpClient,
  ) {

    this.api = this.httpClient;
  }

  private async signIn(api: HttpClient, login: LoginRequest): Promise<any>{

    const url = `${this._baseUrl}/auth/signin`;
    const callApi = () => api
      .post<LoginRequest>(
        url,
        login,
        { observe: 'response' }
      );
    return await this.apiCallerService.caller(callApi, this.authenticationMapperService.map.bind(this.authenticationMapperService), HttpStatusCode.OK);

  }

  private async signUp(api: HttpClient, register: RegisterRequest): Promise<any> {

    const url = `${this._baseUrl}/auth/signup`;
    const callApi = () => api
      .post<RegisterRequest>(
        url,
        register,
        { observe: 'response' }
      );
    return await this.apiCallerService.caller(callApi, this.authenticationMapperService.map, HttpStatusCode.CREATED);

  }

  instance = () : IAuth => ({
    signIn: (login: LoginRequest) => this.signIn(this.api, login),
    signUp: (register: RegisterRequest) => this.signUp(this.api, register)
  })
}

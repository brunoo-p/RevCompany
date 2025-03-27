import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { ApiCallerService } from '../apiCaller.service';
import { HttpStatusCode } from '../http/httpType';
import { IAuth } from '../../contracts/IAuth';
import { BLoginRequest } from '../../domain/auth/loginRequest';
import { BRegisterRequest } from '../../domain/auth/registerRequest';
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

  private async signIn(api: HttpClient, login: BLoginRequest): Promise<BLoginRequest>{

    const url = `${this._baseUrl}/auth/signin`;
    const callApi = () => api
      .post<BLoginRequest>(
        url,
        login,
        { observe: 'response' }
      );
    return await this.apiCallerService.caller<BLoginRequest>(callApi, this.authenticationMapperService.map.bind(this.authenticationMapperService), HttpStatusCode.OK);

  }

  private async signUp(api: HttpClient, register: BRegisterRequest): Promise<any> {

    const url = `${this._baseUrl}/auth/signup`;
    const callApi = () => api
      .post<BRegisterRequest>(
        url,
        register,
        { observe: 'response' }
      );
    return await this.apiCallerService.caller<BRegisterRequest>(callApi, this.authenticationMapperService.map, HttpStatusCode.CREATED);

  }

  instance = () : IAuth => ({
    signIn: (login: BLoginRequest) => this.signIn(this.api, login),
    signUp: (register: BRegisterRequest) => this.signUp(this.api, register)
  })
}

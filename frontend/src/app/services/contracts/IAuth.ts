import { BLoginRequest } from "../domain/auth/loginRequest";
import { BRegisterRequest } from "../domain/auth/registerRequest";


export interface IAuth {
  signIn: (login: BLoginRequest) => Promise<any>;
  signUp: (register: BRegisterRequest) => Promise<any>;
}

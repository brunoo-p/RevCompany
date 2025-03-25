import { LoginRequest } from "../domain/auth/loginRequest";
import { RegisterRequest } from "../domain/auth/registerRequest";


export interface IAuth {
  signIn: (login: LoginRequest) => Promise<any>;
  signUp: (register: RegisterRequest) => Promise<any>;
}

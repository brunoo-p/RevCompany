import { Email } from "./credential/email";
import { Password } from "./credential/password";

export class RegisterRequest {
  public firstName: string;
  public lastName: string;
  public email: Email;
  public password: Password;

  constructor(
    firstName: string,
    lastName: string,
    email: Email,
    password: Password,
  ) {

    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.password = password;
  }

}

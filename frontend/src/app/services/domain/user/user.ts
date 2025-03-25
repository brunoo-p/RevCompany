import { Email } from "../auth/credential/email";

export class User {
  public id: string;
  public firstName: string;
  public lastName: string;
  public email: Email;
  public isActive: boolean;

  constructor(
    id: string,
    firstName: string,
    lastName: string,
    email: Email,
    isActive: boolean
  ) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.isActive = isActive;
  }
}
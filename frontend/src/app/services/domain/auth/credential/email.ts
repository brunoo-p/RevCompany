export class Email {

  public static readonly REGEXP = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

  public value: string;

  constructor(email: string) {
    this.value = email;
    Email.checkIsEmail(this.value);
  }

  private static checkIsEmail(value: string): void {
    const isEmail = Email.REGEXP.test(value);

    if (!isEmail) {

      throw new Error(`Provided value=${value} is not a valid email.`);
    }
  }
}

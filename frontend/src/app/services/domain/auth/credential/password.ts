export class Password {
  private static readonly QTD_SPECIAL_CHARS = 1;

  private static readonly QTD_UPPER_CASES = 1;

  private static readonly QTD_LOWER_CASES = 1;

  private static readonly QTD_NUMBERS = 1;

  private static readonly MIN_LENGTH = 8;

  private static readonly MAX_LENGTH = 20;

  private static readonly REGEX = '^'
    + `(?=.*[a-z]{${Password.QTD_LOWER_CASES},})`
    + `(?=.*[A-Z]{${Password.QTD_UPPER_CASES},})`
    + `(?=.*[0-9]{${Password.QTD_NUMBERS},})`
    + `(?=.*[!@#$%^&*]{${Password.QTD_SPECIAL_CHARS},})`
    + `(?=.{${Password.MIN_LENGTH},${Password.MAX_LENGTH}})`;

  public static readonly REGEXP = new RegExp(Password.REGEX);

  public value: string;

  constructor(value: string) {
    this.value = value;
    Password.checkIsPassword(this.value);
  }

  private static checkIsPassword(value: string): void {

    const isValid = Password.REGEXP.test(value);
    if (!isValid) {

      throw new Error(`Provided value=${value} is not a valid password.`);

    }

  }
}

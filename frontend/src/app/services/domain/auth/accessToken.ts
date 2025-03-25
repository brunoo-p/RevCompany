export class AccessToken {

  private static readonly REFRESH_LIMIT_MIN = 10;

  private readonly generatedDate: Date;

  validUntil: Date;

  value: string;


  constructor(
    value: string,
    generatedDate: Date,
    validUntil: Date,
    ) {

    this.value = value;
    this.generatedDate = generatedDate;
    this.validUntil = validUntil;
  
  }

  public isValid(): boolean {

    return this.validUntil > new Date();

  }

  public shouldRefresh(): boolean {

    const refreshDate = new Date();
    refreshDate.setMinutes(new Date().getMinutes() + AccessToken.REFRESH_LIMIT_MIN);

    return refreshDate.getTime() >= this.validUntil.getTime();

  }

}

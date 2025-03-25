import { Injectable } from "@angular/core";
import { User } from '../../domain/user/user';

@Injectable({
  providedIn: 'root'
})
export class ContextAuthService {
  private _user!: User;

  public get userProfile(): User {
    return this._user;
  }
  public set userProfile(value: User) {
    this._user = value;
  }
}

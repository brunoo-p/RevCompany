import { Injectable } from "@angular/core";
import { User } from "../user";

@Injectable({
  providedIn: 'root'
})
export class UserMapperService {
  fromObject(data: any): User {
    return new User(
      data.id,
      data.firstName,
      data.lastName,
      data.email,
      data.isActive || true
    )
  }
}
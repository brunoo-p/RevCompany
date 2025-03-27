import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { User } from "../../../domain/user/user";
import { StorageManagerService } from "../../../domain/utils/storage/storageManager.service";
import { StorageKey } from "../../../domain/utils/storage/storage-keys";

@Injectable({
  providedIn: 'root'
})
class UserAuthService {

  private readonly currentUser$ = new BehaviorSubject<User | null>(null);

  constructor(private readonly _storageManager: StorageManagerService) {}

  getCurrentUser$() {
    return this.currentUser$.asObservable();
  }
  
  authenticateUser(user: User) {
    this.currentUser$.next(user);
    this._storageManager.setItem(StorageKey.user, user);
  }
  logoutUser() {
    this.currentUser$.next(null);
  }
}

export default UserAuthService;

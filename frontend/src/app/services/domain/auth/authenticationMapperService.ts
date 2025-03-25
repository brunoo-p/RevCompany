import { Injectable } from '@angular/core';
import { UserMapperService } from '../user/mappers/userMapper.service';
import { User } from '../user/user';
import { StorageManagerService } from '../utils/storage/storageManager.service';
import { StorageKey } from '../utils/storage/storage-keys';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationMapperService {
  
  private readonly tokenKey = StorageKey.token;
  
  constructor(
    private readonly _userMapperService: UserMapperService,
    private readonly _storeManagerService: StorageManagerService
  ) {}

  private storeToken(token: string): void {
    this._storeManagerService.setItem(this.tokenKey, token);
  }
  
  private mapUser(data: any): User {
    return this._userMapperService.fromObject(data);
  }
  
  map(data: any): User {
    if(data?.token) {
      this.storeToken(data.token);
    }
    return this.mapUser(data);
  }
}
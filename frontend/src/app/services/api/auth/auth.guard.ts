import { ContextAuthService } from './../context/contextAuth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { StorageManagerService } from '../../domain/utils/storage/storageManager.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  private user: any;
  constructor(
    private readonly router: Router,
    private readonly contextAuthService: ContextAuthService,
    private readonly storageManager: StorageManagerService
  ) {
  }

  canActivate(): boolean {

    const token = this.storageManager.getItem('@Fs:user');
    this.user = this.contextAuthService.userProfile;

    if (this.user || token) {
      return true;
    }

    this.router.navigate(['login']);
    return false;

  }

}

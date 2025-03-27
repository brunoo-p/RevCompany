import { inject } from "@angular/core";
import { Router } from "@angular/router";
import UserAuthService from "../user/userAuth.service";
import { filter, map } from "rxjs";


const authGuard = () => {

  const _userService = inject(UserAuthService);
  const _router = inject(Router);
  console.log('guard');
  return _userService.getCurrentUser$()
    .pipe(
      filter((currentUser) => currentUser !== undefined),
      map((currentUser) => {
        if(!currentUser) {
          _router.navigateByUrl('/login');
          return false;
        }
        return true;
      })
      )
}

export default authGuard;

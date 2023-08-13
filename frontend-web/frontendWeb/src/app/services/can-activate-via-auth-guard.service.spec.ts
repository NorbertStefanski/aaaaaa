import { CanActivateViaAuthGuardService } from './can-activate-via-auth-guard.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import {User} from "../models/user.model";
import {UserService} from "./user.service";

describe('CanActivateViaAuthGuardService', () => {
  let service: CanActivateViaAuthGuardService;
  let authService: AuthService;
  let router: Router;

  beforeEach(() => {
    authService = new AuthService(new UserService());
    router = jasmine.createSpyObj('Router', ['navigate']);
    service = new CanActivateViaAuthGuardService(authService, router);
  });

  it('should return true when the user is logged in', () => {
    spyOn(authService, 'loggedIn').and.returnValue(true);
    expect(service.canActivate()).toBe(true);
    expect(router.navigate).not.toHaveBeenCalled();
  });

  it('should return false and redirect to login page when user is not logged in', () => {
    spyOn(authService, 'loggedIn').and.returnValue(false);
    expect(service.canActivate()).toBe(false);
    expect(router.navigate).toHaveBeenCalledWith(['/login']);
  });
});


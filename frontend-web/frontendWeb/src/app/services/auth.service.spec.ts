import {AuthService} from "./auth.service";
import {UserService} from "./user.service";

describe('AuthService', () => {
  let service: AuthService;
  let userService: UserService;

  beforeEach(() => {
    userService = new UserService();
    service = new AuthService(userService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call login with correct credentials', () => {
    spyOn(console, 'log');
    userService.users = [{username: 'user', password: 'pass', firstName: 'John', lastName: 'Doe'}];
    service.login('user', 'pass');
    expect(console.log).toHaveBeenCalledWith(true);
  });


  it('should call login with correct credentials', () => {
    spyOn(console, 'log');
    userService.users = [{username: 'user', password: 'pass', firstName: 'John', lastName: 'Doe'}];
    service.login('user', 'pass');
    expect(console.log).toHaveBeenCalledWith(true);
  });

  it('should call login with incorrect credentials', () => {
    spyOn(console, 'log');
    userService.users = [{username: 'user', password: 'pass', firstName: 'John', lastName: 'Doe'}];
    service.login('user', 'wrongpass');
    expect(console.log).not.toHaveBeenCalled();
  });

  it('should call logout', () => {
    service.logout();
    expect(service.loggedIn()).toEqual(false);
  });

});


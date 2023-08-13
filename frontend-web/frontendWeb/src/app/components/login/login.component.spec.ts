import {LoginComponent} from "./login.component";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {UserService} from "../../services/user.service";

describe('LoginComponent', () => {
  let component: LoginComponent;
  let authService: AuthService;
  let router: Router;

  beforeEach(() => {
    authService = new AuthService(new UserService());
    router = jasmine.createSpyObj('Router', ['navigate']);
    component = new LoginComponent(authService, router);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call the authService.login method with the correct arguments', () => {
    spyOn(authService, 'login');
    component.username = 'testuser';
    component.password = 'testpassword';
    component.onSubmit({ value: { username: 'testuser', password: 'testpassword' } });
    expect(authService.login).toHaveBeenCalledWith('testuser', 'testpassword');
  });

  it('should call the router.navigate method with the correct argument when the user is logged in', () => {
    spyOn(authService, 'loggedIn').and.returnValue(true);
    component.onSubmit({ value: { username: 'testuser', password: 'testpassword' } });
    expect(router.navigate).toHaveBeenCalledWith(['/menu']);
  });

  it('should not call the router.navigate method when the user is not logged in', () => {
    spyOn(authService, 'loggedIn').and.returnValue(false);
    component.onSubmit({ value: { username: 'testuser', password: 'testpassword' } });
    expect(router.navigate).not.toHaveBeenCalled();
  });

  it('should set the showError property to true when the user is not logged in', () => {
    spyOn(authService, 'loggedIn').and.returnValue(false);
    component.onSubmit({ value: { username: 'testuser', password: 'testpassword' } });
    expect(component.showError).toBeTrue();
  });

  it('should set the showError property to false when the user is logged in', () => {
    spyOn(authService, 'loggedIn').and.returnValue(true);
    component.onSubmit({ value: { username: 'testuser', password: 'testpassword' } });
    expect(component.showError).toBeFalse();
  });
});


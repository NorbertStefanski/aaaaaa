import { Injectable } from '@angular/core';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private userService: UserService) { 

  }

  private isLoggedIn:boolean = false;


  login(username: string, password: string){
    if(this.userService.users.some(e => e.username === username) 
    && this.userService.users.some(e => e.password === password))
    {
      this.isLoggedIn = true;
      console.log(this.loggedIn());
    }
  }

  logout() {
    this.isLoggedIn = false;
  }

  loggedIn(): boolean {
    return this.isLoggedIn;
  }
}



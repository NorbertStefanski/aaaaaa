import { Injectable } from '@angular/core';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  users: User[];

  constructor() {
    this.users = [];
    let brewer: User = new User('brewer','brew','er','brewer');
    this.addUser(brewer);
  }

  addUser(user: User): void{
    this.users.push(user);
  }
}

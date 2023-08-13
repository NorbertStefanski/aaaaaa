import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  showError: boolean = false;
  username: string = "";
  password: string = "";
  userService: any;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    
  }

  onSubmit(form: any): void{
    this.authService.login(form.value.username, form.value.password);
    if(this.authService.loggedIn()){
      this.router.navigate(['/menu']);
      this.showError = false;
    }else{
      this.showError = true;
    }
  }
}

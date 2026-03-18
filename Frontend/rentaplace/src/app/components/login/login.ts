import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html'
})
export class Login {

  loginData = {
    email: '',
    password: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {

    this.authService.login(this.loginData).subscribe((res: any) => {

      console.log('Login response:', res);

      // Handle both camelCase and PascalCase responses
      const token = res.token || res.Token;
      const user  = res.user  || res.User;

      this.authService.saveToken(token);
      this.authService.saveUser(user);

      alert('Login Successful! Role: ' + (user?.role || user?.Role));

      this.router.navigate(['/']);

    }, err => {
      alert('Login Failed. Check your email and password.');
    });

  }

}

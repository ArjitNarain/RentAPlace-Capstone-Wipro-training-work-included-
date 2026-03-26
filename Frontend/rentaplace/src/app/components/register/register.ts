import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
})
export class Register {
  user: any = {};

  constructor(private auth: AuthService) {}

  registerUser() {

  
    this.auth.register(this.user).subscribe(res => {
      
      alert("Registration successful");
    });

  }

}

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

    console.log("Button clicked");

    this.auth.register(this.user).subscribe(res => {
      console.log("Registered", res);
      alert("Registration successful");
    });

  }

}

import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LOGIN } from 'src/app/models/user.models';
import { AuthorizationService } from 'src/app/services/authorization.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;
  email!: FormControl;
  password!: FormControl;

  constructor(private auth: AuthorizationService){

  }

  ngOnInit() {
    this.createFormControls();
    this.createForm();
  }

  createFormControls(){
    this.email = new FormControl('', [Validators.email, Validators.required]);
    this.password = new FormControl('', [Validators.required]);
  }

  createForm() {
    this.loginForm = new FormGroup({
        email: this.email,
        password: this.password,
    });
  }

  login()
  {
    if(this.loginForm.valid)
    {
      const body: LOGIN = {
        email: this.email.value,
        password: this.password.value
      }
      this.auth.login(body)
        .subscribe(
          res => console.log(res)
        );
    }
  }
}

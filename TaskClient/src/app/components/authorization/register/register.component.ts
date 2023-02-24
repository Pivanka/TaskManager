import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { REGISTER } from 'src/app/models/user.models';
import { AuthorizationService } from 'src/app/services/authorization.service';
import { CustomValidators, PasswordStrengthValidator } from 'src/app/services/password.validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm!: FormGroup;
  email!: FormControl;
  userName!: FormControl;
  password!: FormControl;
  confirmPassword!: FormControl;

  constructor(private auth: AuthorizationService) { }

  ngOnInit() {
    this.createFormControls();
    this.createForm();
  }

  createFormControls(){
    this.email = new FormControl('', [Validators.email, Validators.required]);
    this.password = new FormControl('', [Validators.required, PasswordStrengthValidator]);
    this.userName = new FormControl('', [Validators.required]);
    this.confirmPassword = new FormControl('', [Validators.required]);
  }

  createForm() {
    this.registerForm = new FormGroup({
        email: this.email,
        password: this.password,
        userName: this.userName,
        confirmPassword: this.confirmPassword
    },
    [CustomValidators.MatchValidator('password', 'confirmPassword')]);
  }
  register(){
    if(this.registerForm.valid)
    {
      const body: REGISTER = {
        userName: this.userName.value,
        email: this.email.value,
        password: this.password.value,
        confirmPassword: this.password.value
      }
      this.auth.register(body)
        .subscribe(
          res => console.log(res)
        );
    }
  }

  getClass(control: FormControl)
  {
    if(control.invalid && (control.dirty || control.touched))
    {
      return 'form-control error';
    }
    else{
      return 'form-control';
    }
  }

  get passwordMatchError() {
    return (
      this.registerForm.getError('mismatch') &&
      this.registerForm.get('confirmPassword')?.touched
    );
  }

}

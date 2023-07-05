import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { AccountService } from "src/app/services/account.service";

@Component({
  selector: 'app-signup',
  styleUrls: ['signup.component.scss'],
  templateUrl: 'signup.component.html'
})
export class SignUpComponent {
  signUpForm: FormGroup;

  constructor(
    private service: AccountService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {
    this.signUpForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.minLength(3)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(5)])
    })
  }

  onSubmitForm(): void {
    const name = this.signUpForm.controls['name'].value
    const email = this.signUpForm.controls['email'].value
    const password = this.signUpForm.controls['password'].value
    this.service.signup({ email, name, password })
      .subscribe((res:any) => {
        this._snackBar.open('Conta criar com sucesso, faça o login', 'Ok')
        this.router.navigateByUrl('/signin')
      }, err => this._snackBar.open(err.error.error, 'Ok'))
  }
}

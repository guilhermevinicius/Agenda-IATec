import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { ResponseAPI } from "src/app/dtos/responses";
import { AccountService } from "src/app/services/account.service";

@Component({
  selector: 'app-signin',
  styleUrls: ['signin.component.scss'],
  templateUrl: './signin.component.html'
})
export class SignInComponent implements OnInit {
  public signInForm: FormGroup;

  constructor(
    private readonly service: AccountService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {
    this.signInForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(5)])
    })
  }


  ngOnInit(): void {
  }

  onSubmitForm(): void {
    const email = this.signInForm.controls['email'].value
    const password = this.signInForm.controls['password'].value
    this.service.signin({ email, password }).subscribe((res: any) => {
      const { accessToken } = res.body
      localStorage.setItem('token', accessToken)
      this.router.navigateByUrl('/dashboard')
    }, err => this._snackBar.open(err.error.error, 'Ok'))
  }

}

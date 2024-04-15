import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPasswordModel } from 'src/app/models/reset-password';
import { AuthService } from 'src/app/services/auth.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  hidePass: boolean = true;
  canReset: boolean = true;

  private GUID = "";

  resetPasswordForm = new FormGroup({
    newPassword: new FormControl('', [Validators.required]),
    repeatPassword: new FormControl('', [Validators.required])
  })

  constructor(private router: Router,
    private route: ActivatedRoute,
    private messageSnack: MatSnackBar,
    private authService: AuthService,
    private sharedService: SharedService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.GUID = params['guid'];
    });

    this.newPassword?.setValue("");
    this.repeatPassword?.setValue("");
  }

  redirectToLogin(): void {
    this.router.navigate(['/login/with-password']);
  }

  get newPassword() {
    return this.resetPasswordForm.get('newPassword');
  }

  get repeatPassword() {
    return this.resetPasswordForm.get('repeatPassword');
  }

  resetPassword(): void {
    if (this.sharedService.validPassword(this.newPassword?.value, this.repeatPassword?.value, this.resetPasswordForm)) {
      let resetPasswordModel: ResetPasswordModel = {
        Guid: this.GUID,
        newPassword: this.newPassword?.value
      }

      this.authService.resetPassword(resetPasswordModel).subscribe({
        next: _ => {
          this.canReset = false;
        },
        error: _ => {
          this.showSnackBar("Reset password link expired, please try again!");
          this.redirectToForgotPassword();
        }
      });
    }
  }

  redirectToForgotPassword() {
    this.router.navigate(['/login/forgot-password']);
  }

  showSnackBar(message: string): void {
    this.messageSnack.open(message, "Dismiss", {
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  forgotPasswordForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email])
    })

  constructor(private router: Router,
              private authService: AuthService,
              private messageSnack: MatSnackBar,
              private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.email?.setValue(params['email']);
    })
  };

  redirectToLogin(): void {
    this.router.navigate(['/login/with-password']);
  }

  get email() {
    return this.forgotPasswordForm.get('email');
  }

  sendResetPasswordEmail() {
    this.authService.forgotPassword(this.email?.value).subscribe({
        next: _ => {
          this.showSnackBar("Please check your email!");
          this.redirectToLogin();
        },
        error: _ => {
          this.showSnackBar("Something went wrong. Please try again!");
        }
      });
  }

  showSnackBar(message: string): void {
    this.messageSnack.open(message, "Dismiss", {
    });
  }
}

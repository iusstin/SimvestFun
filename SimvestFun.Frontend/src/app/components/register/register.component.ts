import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService } from '@abacritt/angularx-social-login';
import { Register } from 'src/app/models/register';
import { AuthService } from 'src/app/services/auth.service';
import { LoadingSpinnerService } from 'src/app/services/loading-spinner.service';
import { SharedService } from 'src/app/services/shared.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  hidePass: boolean = true;
  notMatch: boolean = false;
  registerWithPassword: boolean = false;
  socialInitialized = false;

  registerForm: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
    passwordCheck: new FormControl('', Validators.required)
  });

  response: string = "";

  constructor(private authService: AuthService,
    private router: Router,
    private messageSnack: MatSnackBar,
    private socialAuthService: SocialAuthService,
    private route: ActivatedRoute,
    private spinnerService: LoadingSpinnerService,
    private sharedService: SharedService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['pass']){
        this.registerWithPassword = true;
      }
      else
        this.registerWithPassword = false;
    }
    );

    this.spinnerService.requestStarted();
    this.socialAuthService.initState.subscribe(init => {
      this.socialInitialized = init.valueOf();
      this.spinnerService.requestEnded();
    });
  }

  get name() {
    return this.registerForm.get('name');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get passwordCheck() {
    return this.registerForm.get('passwordCheck');
  }

  registerUser(): void {

    let registerData: Register = {
      name: this.name?.value,
      email: this.email?.value,
      password: this.password?.value
    }

    if (this.sharedService.validPassword(this.password?.value, this.passwordCheck?.value, this.registerForm)) {
      this.authService.registerUser(registerData).subscribe({
        next: resp => {
          this.response = resp.message;
          this.redirectToLogin(true);
          this.showSnackBar("Registration successful!");
        },
        error: (err: Error) => {
          this.showError(err);
        }
      });
    }
  }

  registerGoogleUser(): void {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then(data => {
      this.authService.registerWithGoogle(data.idToken).subscribe({
        next: user => {
          this.authService.setConnectedUser(user);
          this.authService.setToken(user.token);
          this.router.navigate(['']);
        },
        error: (err: Error) => {
          this.showError(err);
        }
      });
    });
  }

  registerFacebookUser(): void {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID).then(data => {
      this.authService.registerWithFacebook(data.authToken).subscribe({
        next: user => {
          this.authService.setConnectedUser(user);
          this.authService.setToken(user.token);
          this.router.navigate(['']);
        },
        error: (err: Error) => {
          this.showError(err);
        }
      });
    });
  }

  navigateToRegisterWithPassword(): void {
    this.router.navigate(['/register/with-password']);
  }

  redirectToLogin(password = false): void {
    password ? this.router.navigate(['/login/withpassword']) : this.router.navigate(['/login']);
  }

  redirectToHome(): void {
    this.router.navigate(['']);
  }

  showSnackBar(message: string): void {
    this.messageSnack.open(message, "Dismiss", {
    });
  }

  showError(error: Error, message = ""): void {
    if (error.message === "") {
      this.showSnackBar('Something went wrong, please try again later!');
    }
    else {
      this.showSnackBar(error.message + " " + message);
    }
  }
}

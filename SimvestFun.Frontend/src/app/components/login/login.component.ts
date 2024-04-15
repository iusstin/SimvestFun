import { Component, OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Authentication } from 'src/app/models/authentication';
import { User } from 'src/app/models/user';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/services/auth.service';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService } from '@abacritt/angularx-social-login';
import { LoadingSpinnerService } from 'src/app/services/loading-spinner.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  hidePass = true;
  loginWithPassword = false;
  socialInitialized = false;
  
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  })

  constructor(private authService: AuthService,
              private router: Router, 
              private messageSnack: MatSnackBar,
              private socialAuthService: SocialAuthService,
              private route: ActivatedRoute,
              private spinnerService: LoadingSpinnerService
              ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if(params['pass']){
        this.loginWithPassword = true;
      }
      else
        this.loginWithPassword = false;
      }
    );
    
    this.spinnerService.requestStarted();
    this.socialAuthService.initState.subscribe(init => {
      this.socialInitialized = init.valueOf();
      this.spinnerService.requestEnded();
    });
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  logInWithPassword(): void {
    let loginData : Authentication = {
      email: this.email?.value,
      password: this.password?.value
    };
    if(this.validData()) {
      this.authService.logInUser(loginData).subscribe({
        next: (user: User) => {
          this.authService.setConnectedUser(user);
          this.authService.setToken(user.token);
          this.router.navigate(['']);
        },
        error: (err: Error) => {
          this.showError(err);
        }
      });
    }
  }

  logInWithGoogle(): void {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then(data => {
      this.authService.logInWithGoogle(data.idToken).subscribe({
        next: (user: User) => {
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

  logInWithFacebook(): void {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID).then(data => {
      this.authService.logInWithFacebok(data.authToken).subscribe({
        next: (user: User) => {
          this.authService.setConnectedUser(user);
          this.authService.setToken(user.token);
          this.router.navigate(['']);
        },
        error: (err: Error) => {
          this.showError(err);
        }
      })
    });
  }

  validData():boolean {
    if(this.loginForm.invalid) {
      this.showSnackBar("Invalid data!");
      return false;
    }
    return true;
  }

  redirectToPasswordLogin(): void {
    this.router.navigate(['/login/with-password']);
  }

  redirectToRegister(): void {
    this.router.navigate(['/register']);
  }

  redirectToHome(): void {
    this.router.navigate(['']);
  }

  redirectToForgotPassword(): void {
    this.router.navigate([`/login/forgot-password/${this.email?.value}`]);
  }

  showSnackBar(message: string) {
    this.messageSnack.open(message, "Dismiss", {
    });
  }

  showError(error: Error): void {
    if(!error || !error.message){
      this.showSnackBar('Something went wrong, please try again later!');
    }
    else {
      this.showSnackBar(error.message);
    }
  }
}

import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, throwError } from 'rxjs';
import { Authentication } from '../models/authentication';
import { ForgotPasswordModel } from '../models/forgot-password';
import { Register } from '../models/register';
import { ResetPasswordModel } from '../models/reset-password';
import { User } from '../models/user';
import { baseURL } from '../utils/constants';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authUrl = `${baseURL}/auth`;
  connectedUser: User | undefined;


  httpOptions = {
    headers: new HttpHeaders({'Content-Type':'application/json'})
  };

  constructor(private http: HttpClient,
              private router: Router) { }

  registerUser(registerData: Register): Observable<any> {
    return this.http.post(`${this.authUrl}/register`, registerData, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  registerWithGoogle(token: string): Observable<User> {
    var refactorToken = "\"" + token + "\"";

    return this.http.post<User>(`${this.authUrl}/register/google`, refactorToken, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  registerWithFacebook(authToken: string): Observable<User> {
    var refactorToken = "\"" + authToken + "\"";

    return this.http.post<User>(`${this.authUrl}/register/facebook`, refactorToken, this.httpOptions).pipe(
      catchError(this.handleError)
    )
  }

  logInUser(loginData: Authentication): Observable<User> {
    return this.http.post<User>(`${this.authUrl}/authenticate`, loginData, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  logInWithGoogle(token: string): Observable<User> {
    var refactorToken = "\"" + token + "\"";
    
    return this.http.post<User>(this.authUrl + "/authenticate/google", refactorToken, this.httpOptions).pipe(
      catchError(this.handleError)
    )
  }

  logInWithFacebok(authToken: string): Observable<User> {
    var refactorToken = "\"" + authToken + "\"";

    return this.http.post<User>(`${this.authUrl}/authenticate/facebook`, refactorToken, this.httpOptions).pipe(
      catchError(this.handleError)
    )
  }

  forgotPassword(email: string): Observable<any> {
    let forgotPassordModel: ForgotPasswordModel = {
      email: email
    }
    
    return this.http.post<any>(`${this.authUrl}/forgot-password`, forgotPassordModel, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  resetPassword(model: ResetPasswordModel): Observable<any> {
    return this.http.post<any>(`${this.authUrl}/reset-password`, model, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  isUserLoggedIn(): boolean {
    return !!localStorage.getItem("token");
  }

  setConnectedUser(user: User) {
    this.connectedUser = user;
  }

  getConnectedUser(): User | undefined {
    return this.connectedUser;
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem("token");
  }

  logOutUser() {
    localStorage.removeItem('token');
    this.connectedUser = undefined;
    this.router.navigate(['']);
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = err.error.message;
    return throwError(() => new Error(errorMessage));
  }
}

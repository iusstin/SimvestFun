import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { User } from '../models/user';
import { baseURL } from '../utils/constants';
import { UsersPaging } from '../models/users-paging';
import { Follow } from '../models/follow';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  private usersUrl = `${baseURL}/users`;
  private pagingUrl = `${this.usersUrl}/paging`;
  
  httpOptions = {
    headers: new HttpHeaders({'Content-Type':'application/json'})
  };
  
  constructor(private http: HttpClient) { }

  getUser(userId: string): Observable<User> {
    return this.http.get<User>(`${this.usersUrl}/${userId}`).pipe(
      catchError(this.handleError)
    );
  }

  getUserLeaderboard(): Observable<User[]> {
    return this.http.get<User[]>(this.usersUrl).pipe(
      catchError(this.handleError)
    );
  }

  getUsersByPageIndex(pageIndex: number, name: string): Observable<UsersPaging> {
    return this.http.get<UsersPaging>(`${this.pagingUrl}/${pageIndex}/${name}`).pipe(
      catchError(this.handleError)
    );
  }

  resetUserAccount(): Observable<User> {
    return this.http.post<User>(`${this.usersUrl}/reset-account`, null, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  getConnectedUser(): Observable<User | null> {
    return this.http.get<User>(`${this.usersUrl}/connected`).pipe(
      catchError(this.handleError)
    );
  }

  getUserPortfolioValues(id: string): Observable<User> {
    return this.http.get<User>(`${this.usersUrl}/${id}/portfolioValues`).pipe(
      catchError(this.handleError)
    );
  }

  updateUserDetails(updatedUser: User): Observable<User> {
    return this.http.put<User>(`${this.usersUrl}/update-account`, updatedUser, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  updateUserDetailsByAdmin(updatedUser: User): Observable<User> {
    return this.http.put<User>(`${this.usersUrl}/update-account-by-admin`, updatedUser, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  addDailyBonus(): Observable<User> {
    return this.http.post<User>(`${this.usersUrl}/daily-bonus`, null, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  followUser(id: string): Observable<any> {
    return this.http.post(`${this.usersUrl}/${id}/follow`, null, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  unfollowUser(id: string): Observable<any> {
    return this.http.delete(`${this.usersUrl}/${id}/unfollow`).pipe(
      catchError(this.handleError)
    );
  }

  getFollow(id: string): Observable<Follow> {
    return this.http.get<Follow>(`${this.usersUrl}/${id}/follow`).pipe(
      catchError(this.handleError)
    );
  }

  getFollowedUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.usersUrl}/followed-users`).pipe(
      catchError(this.handleError)
    );
  }

  getFollowers(id: string): Observable<User[]> {
    return this.http.get<User[]>(`${this.usersUrl}/${id}/followers`).pipe(
      catchError(this.handleError)
    );
  }

  unsubscribeUser(guid: string): Observable<User> {
    return this.http.put<User>(`${this.usersUrl}/unsubscribe/${guid}`, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = "";
    return throwError(() => new Error(errorMessage));
  }
}

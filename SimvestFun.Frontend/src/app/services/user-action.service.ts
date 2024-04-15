import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { __asyncValues } from 'tslib';
import { UserAction } from '../models/user-action';
import { baseURL } from '../utils/constants';
import { UserActionModel } from '../models/user-actions-model';

@Injectable({
  providedIn: 'root'
})
export class UserActionService {

  private actionsUrl = `${baseURL}/userActions`;
  
  httpOptions = {
    headers: new HttpHeaders({'Content-Type':'application/json'})
  };

  constructor(private http: HttpClient) { }

  getActionsByUser(userId: string, numberOfActions: number): Observable<UserActionModel> {
    return this.http.get<UserActionModel>(baseURL + `/userActions/${userId}/${numberOfActions}`).pipe(
      catchError(this.handleError)
    )
  }

  getLastAction(): Observable<UserAction> {
    return this.http.get<UserAction>(`${baseURL}/userActions/last-action`).pipe(
      catchError(this.handleError)
    )
  }

  getAllActions(): Observable<UserAction[]> {
    return this.http.get<UserAction[]>(baseURL + `/userActions`).pipe(
      catchError(this.handleError)
    );
  }

  checkLastBonusAction(): Observable<boolean> {
    return this.http.get<boolean>(this.actionsUrl + "/last-bonus").pipe(
      catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = "";
    return throwError(() => new Error(errorMessage));
  }
}

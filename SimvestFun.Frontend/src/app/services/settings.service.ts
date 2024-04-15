import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { Setting } from '../models/setting';
import { baseURL } from '../utils/constants';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  url = `${baseURL}/settings`;

  httpOptions = {
    headers: new HttpHeaders({'Content-Type':'application/json'})
  };

  constructor(private http: HttpClient) { }

  getSettingByKey(key: string) : Observable <Setting>{
    return this.http.get<Setting>(`${this.url}/${key}`).pipe(
      catchError(this.handleError)
    )
  }

  updateAnnouncement(announcement : Setting) : Observable<any> {
    return this.http.put(this.url, announcement, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = err.error.message;
    return throwError(() => new Error(errorMessage));
  }
}

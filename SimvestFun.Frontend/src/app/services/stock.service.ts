import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Stock } from '../models/stock';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http'
import { catchError } from 'rxjs';
import { baseURL } from '../utils/constants';
import { UserStock } from '../models/user-stock';
import { StockPrice } from '../models/stock-price';

@Injectable({
  providedIn: 'root'
})
export class StocksService {
  private stocksUrl = `${baseURL}/stocks`;
  private usersStocksUrl = `${baseURL}/usersStocks`;
  private stockPricesUrl = `${this.stocksUrl}/prices`;

  httpOptions = {
    headers: new HttpHeaders({'Content-Type':'application/json'})
  };

  constructor(private http: HttpClient) {}

  getStocks(): Observable<Stock[]> {
    return this.http.get<Stock[]>(this.stocksUrl).pipe(
      catchError(this.handleError)
    );
  }

  buyStock(userStock: UserStock): Observable<any> {
    const url = `${this.usersStocksUrl}/buy`;
    return this.http.post(url, userStock, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  getUserStocksByUserId(userId: string): Observable<UserStock[]> {
    return this.http.get<UserStock[]>(`${this.usersStocksUrl}/user/${userId}`).pipe(
      catchError(this.handleError)
    );
  }
  getUserStockById(id: number): Observable<UserStock> {
    return this.http.get<UserStock>(`${this.usersStocksUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }
  sellUserStock(id: number, userStock: UserStock): Observable<UserStock> {
    const url = `${this.usersStocksUrl}/sell/${id}`;
    return this.http.put<UserStock>(url, userStock, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  getStockByIdWithPrices(id: string): Observable<Stock> {
    return this.http.get<Stock>(`${this.stockPricesUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = err.error;
    return throwError(() => new Error(errorMessage));
  }
}

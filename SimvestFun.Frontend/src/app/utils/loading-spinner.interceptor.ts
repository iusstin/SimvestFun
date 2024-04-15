import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { LoadingSpinnerService } from "../services/loading-spinner.service";

@Injectable()

export class SpinnerInterceptor implements HttpInterceptor {
    constructor(private spinnerService: LoadingSpinnerService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.spinnerService.requestStarted();
        return this.handler(next, req);
    }

    handler(next: HttpHandler, request: HttpRequest<any>) {
        return next.handle(request)
            .pipe(
                tap((event: any) => {
                    if(event instanceof HttpResponse) {
                        this.spinnerService.requestEnded();
                    }
                },
                    (error: HttpErrorResponse) => {
                        this.spinnerService.requestEnded();
                        throw error;
                    }
                ));
    }
}
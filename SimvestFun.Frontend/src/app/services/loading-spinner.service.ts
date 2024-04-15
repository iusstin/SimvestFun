import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class LoadingSpinnerService{
    private spinner = new BehaviorSubject<boolean>(false);
    private count = 0;

    constructor() {}

    getSpinnerObserver(): Observable<boolean> {
        return this.spinner.asObservable();
    }

    requestStarted() {
        if(++this.count === 1){
            this.spinner.next(true);
        }
    }

    requestEnded() {
        if(this.count === 0 || --this.count === 0){
            this.spinner.next(false);
        }
    }

    resetSpinner() {
        this.count = 0; 
        this.spinner.next(false);
    }
}
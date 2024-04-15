import { ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { LoadingSpinnerService } from "src/app/services/loading-spinner.service";

@Component({
    selector: 'app-loading-spinner',
    templateUrl: './loading-spinner.component.html',
    styleUrls: ['./loading-spinner.component.css']
})

export class LoadingSpinner implements OnInit{
    showSpinner = false;

    constructor(private spinnerService: LoadingSpinnerService, private cdRef: ChangeDetectorRef) { }

    ngOnInit() {
        this.init();
    }
    init() {
        this.spinnerService.getSpinnerObserver().subscribe((status) => {
            this.showSpinner = status;
            this.cdRef.detectChanges();
        });
    }
}
import { Component, Inject, OnInit, HostListener } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { DialogConfirmOutput } from "src/app/models/dialog-confirm-output";
import { DialogInputData } from "src/app/models/dialog-input-data";
import { Stock } from "src/app/models/stock";
import { User } from "src/app/models/user";
import { UserStock } from "src/app/models/user-stock";
import { StocksService } from "src/app/services/stock.service";
import { ConfirmDialogComponent } from "../confirm-dialog/confirm-dialog.component";

@Component({
    selector: 'app-sell-dialog',
    templateUrl: './sell-dialog.component.html',
    styleUrls: ['./sell-dialog.component.css']
})
export class SellDialogComponent implements OnInit {

    stock!: Stock
    user: User | undefined;
    userStock!: UserStock;
    confirmation: DialogConfirmOutput = {response: false}
    units?: number;
    quantity = new FormControl('', Validators.required);
    
    constructor( 
        public dialogRef: MatDialogRef<SellDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogInputData,
        private dialog: MatDialog,
        private messageSnack: MatSnackBar,
        private stocksService: StocksService,
    ){}
    
    ngOnInit(): void {
        this.getUserStock();
    }

    onCancelClick(): void {
        this.dialogRef.close();
    }

    onOkClick(): void {
        if(this.quantity.valid && this.quantity.value > 0){
            if(this.quantity.value > this.userStock?.unitCount)
                this.showSnackBar("You don't have enough shares to sell, please select a lower quantity");
            else this.openConfirmDialog();
        }
        else this.showSnackBar("Something went wrong, please check your data!");
    }

    openConfirmDialog(): void {
        const confirmDialog = this.dialog.open(ConfirmDialogComponent, {
            data: this.confirmation
        });

        confirmDialog.afterClosed().subscribe(conf => {
            if(conf) {
                let userStockData = {
                    applicationUserId: this.user?.id, 
                    stockId: this.stock?.id, 
                    unitCount: this.quantity.value,
                    id: this.userStock.id,
                    buyingPricePerUnit: this.stock.currentPrice
                } as UserStock;

                this.stocksService.sellUserStock(userStockData.id, userStockData).subscribe({
                    next: resp => {
                        this.showSnackBar("Successful sale!");
                        this.dialogRef.close();
                    },
                    error: err => this.showSnackBar("Something went wrong. Please try again later.")
                });
            }
        });
    }

    showSnackBar(message: string) {
        this.messageSnack.open(message, "Dismiss", { });
    }

    getUserStock(): void {
        this.userStock = this.data.userStock;
        this.user = this.data.user;
        this.stock = this.userStock.stock;
        this.units = this.userStock.unitCount;
        this.stocksService.getUserStockById(this.userStock.id).subscribe({
            next: userStock => this.userStock = userStock,
            error: err => { this.showSnackBar("Something went wrong, try again later.")}
        });
    }
}
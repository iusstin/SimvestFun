import { DecimalPipe } from '@angular/common';
import { Component, OnInit, Inject, HostListener } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DialogConfirmOutput } from 'src/app/models/dialog-confirm-output';
import { DialogInputData } from 'src/app/models/dialog-input-data';
import { Stock } from 'src/app/models/stock';
import { User } from 'src/app/models/user';
import { UserStock } from 'src/app/models/user-stock';
import { StocksService } from 'src/app/services/stock.service';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-buy-dialog',
  templateUrl: './buy-dialog.component.html',
  styleUrls: ['./buy-dialog.component.css']
})
export class BuyDialogComponent implements OnInit {

  stock: Stock | undefined;
  user: User | undefined;
  stockPrice: number = 0;
  quantity = new FormControl('', Validators.required)
  confirmation: DialogConfirmOutput = {response: false}
  maxQuantity: number = 0;

  constructor(
    public dialogRef: MatDialogRef<BuyDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogInputData,
    private messageSnack: MatSnackBar,
    private dialog: MatDialog,
    private stocksService: StocksService,
    public formatPrice: DecimalPipe,
  ) {}

  ngOnInit(): void {
    this.stock = this.data.stock;
    this.user = this.data.user;
    this.stockPrice = this.stock.currentPrice;
    this.maxQuantity = Math.floor(this.user.cash / this.stock.currentPrice);
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  onOkClick(): void {
    let bid = this.quantity.value * this.stockPrice;
    if(this.quantity.valid) {
       if(this.user && bid > this.user?.cash) {
        this.showSnackBar("You don't have enough money. Please select a lower quantity.")
      }
      else if(this.quantity.value <= 0) {
        this.showSnackBar("Please choose a quantity greater than 0");
      }
      else {
        this.openConfirmDialog();
      }
    }
  }

  onBuyMax(): void {
    if(this.maxQuantity > 0) {
      this.quantity.patchValue(this.maxQuantity);
    }
    else
      this.showSnackBar("You don't have enough money for one unit!");
  }

  openConfirmDialog(): void {
    const confirmDialog = this.dialog.open(ConfirmDialogComponent, {
      data: this.confirmation
    });
    
    confirmDialog.afterClosed().subscribe(conf => {
      if(conf) {

        let stockData = {
          applicationUserId: this.user?.id, 
          stockId: this.stock?.id, 
          unitCount: this.quantity.value, 
          buyingPricePerUnit: this.stockPrice
        } as UserStock;

        this.stocksService.buyStock(stockData).subscribe({
          next: resp => {
            this.showSnackBar("Purchase successful!");
            this.dialogRef.close();
          },
          error: err => {
            this.showSnackBar("Something went wrong. Please try again later.")
          }
        });
      }
    });
  }

  showSnackBar(message: string) {
    this.messageSnack.open(message, "Dismiss", {
    });
  }
}

import { DatePipe, DecimalPipe } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { Stock } from 'src/app/models/stock';
import { StocksService } from 'src/app/services/stock.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { BuyDialogComponent } from '../buy-dialog/buy-dialog.component';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { tz } from 'moment-timezone';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.css']
})
export class StocksComponent implements OnInit {
  
  startTime? : Date;
  endTime? : Date;
  timeZone = '';

  stocks: Stock[] = [];
  dataSource = new MatTableDataSource<Stock>([]);
  isLoading: boolean = true;
  connectedUser: User | undefined;
  parseFloat = parseFloat;
  topStocks: Stock[] = [];

  industryColumn = {
    columnDef: 'industry',
    header: 'Industry',
    cell: (stock: Stock) => `${stock.industry}`,
    cell2: () => ``
  }

  columns = [
    {
      columnDef: 'index',
      header: '',
      cell: (stock: Stock) => `${stock.index}`,
      cell2: () => ``
    },
    {
      columnDef: 'id',
      header: 'Symbol',
      cell: (stock: Stock) => `${stock.id}`,
      cell2: () => ``
    },
    {
      columnDef: 'name',
      header: 'Name',
      cell: (stock: Stock) => `${stock.name}`,
      cell2: () => ``
    },
    {
      columnDef: 'price',
      header: 'Current Price',
      cell: (stock: Stock) => `${stock.currentPrice}`,
      cell2: (stock: Stock) => `${stock.pricePercentChange}`
    },
    {
      columnDef: 'industry',
      header: 'Industry',
      cell: (stock: Stock) => `${stock.industry}`,
      cell2: () => ``
    },
    {
      columnDef: 'buy',
      header: '',
      cell: (stock:Stock) => '',
      cell2: () => ``
    }
  ];

  displayedColumns = this.columns.map(c => c.columnDef);

  constructor(private stocksService: StocksService,
              private dialog: MatDialog,
              public authService: AuthService,
              private userService: UserService,
              private router: Router
              ) { }

  ngOnInit(): void {
    this.getStocks();
    this.onResize();
    this.initializeTime();
    this.setTimeZone();
  }

  ngAfterViewInit(): void {
    this.onResize();
  }

  initializeTime(): void {
    let now = new Date();
    let pipe = new DatePipe('en-US');
    let today = pipe.transform(now, "MMM dd yyyy" );
    this.startTime = new Date(`${today} 09:30:00 EDT`);
    this.endTime = new Date(`${today} 16:00:00 EDT`);
  }

  setTimeZone(): void {
    let guess = tz.guess(true);
    let zone = tz.zone(guess);
    if(zone)
      this.timeZone = zone.abbr(new Date().getTime());
  }

  getStocks(): void {
    this.isLoading = true;
    this.stocksService.getStocks().subscribe({
      next: stocks => {
        this.stocks = stocks;
        this.dataSource = new MatTableDataSource(stocks);
        this.isLoading = false;
        this.setTopStockChanges();
      },
      error: err => {
        console.log(err.message);
      }
    });
  }

  setTopStockChanges(): void{
    this.topStocks = this.stocks.concat();
    this.topStocks.sort(function(s1, s2) {return(Math.abs(s2.pricePercentChange) - Math.abs(s1.pricePercentChange))});
    this.topStocks = this.topStocks.slice(0,3);
  }

  openBuyDialog(row: any): void {
    this.connectedUser = this.authService.getConnectedUser();

    const buyDialog = this.dialog.open(BuyDialogComponent, {
      data: {stock: row as Stock, user: this.connectedUser}
    });
    buyDialog.afterClosed().subscribe(res => {
      this.updateUser();
    });
  }

  updateUser(): void {
    if(this.connectedUser) {
      this.userService.getUser(this.connectedUser.id).subscribe({
        next: user => {
          this.authService.setConnectedUser(user);
          this.connectedUser = user;
        }
      });
    }
  }

  showStockDetails(stockId: string): void {
    this.router.navigate([`stock/${stockId}`]);
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    if(window.innerWidth < 900 && this.columns.length === 6) {
        let buyBtn = this.columns.pop();
        this.columns.pop();
        if(buyBtn)
          this.columns.push(buyBtn);
        this.displayedColumns = this.columns.map(c => c.columnDef);
    } else {
      if(window.innerWidth >=900 && this.columns.length === 5) {
        let buyBtn = this.columns.pop();
        this.columns.push(this.industryColumn);
        if(buyBtn)
          this.columns.push(buyBtn);
        this.displayedColumns = this.columns.map(c => c.columnDef);
      }
    }
  }
}

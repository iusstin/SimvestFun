import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Stock } from 'src/app/models/stock';
import { StockPrice } from 'src/app/models/stock-price';
import { StocksService } from 'src/app/services/stock.service';
import { Title } from "@angular/platform-browser";

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent implements OnInit {

  pipe: DatePipe = new DatePipe('en-US');
  prices: number[] = [];
  dates: string[] = [];
  stockPrices: StockPrice[] = [];
  stockId: string = '';
  stock: Stock = {} as Stock;
  chartColor: string = '';

  constructor(private stocksService: StocksService, private route: ActivatedRoute, private pageTitle: Title) { }

  ngOnInit(): void {
    window.scroll(0,0);
    this.route.params.subscribe(params => {
      this.stockId = params['stockId'];
      this.pageTitle.setTitle(` ${this.stockId} - Simvest.fun`);
      this.initializeChartData();
    });
  }

  initializeChartData(): void {
    this.stocksService.getStockByIdWithPrices(this.stockId).subscribe({
      next: (stock: Stock) => {
        this.stock = stock;
        this.stockPrices = stock.stockPrices;
        if(stock.pricePercentChange < 0)
          this.chartColor = 'rgba(255, 0, 38, 0.093)';
        else
          this.chartColor = 'rgba(0, 128, 0, 0.093)';
        var cnt = 0;
        for(let sp of this.stockPrices) {
          this.prices.push(sp.price);
          var date  = this.pipe.transform(sp.timeStamp, 'MMM d');
          if(date && cnt%4==0)
            this.dates.push(date);
          else
            this.dates.push('');
          cnt++;
        }
      }
    });
  }
  

}

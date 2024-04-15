import { Component, Input, OnInit } from '@angular/core';
import { ChartConfiguration, ChartType } from 'chart.js';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {

  constructor() { }

  public chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    borderColor: 'rgb(35, 57, 68)',
    backgroundColor: 'rgb(0, 175, 329, 0.093)',
    scales: {
      x: {
        ticks: {
          maxRotation: 0,
          minRotation: 0
        }
      }
    }
  };
  public chartType: ChartType = 'line';
  public chartLegend = false;

  @Input() public chartLabels: string[] = [];
  @Input() public xValues: number[] = [];
  @Input() public chartColor: string = '';
  @Input() public height: number = 0;

  public chartData = [
    {
      data: [] as number[], 
      pointBackgroundColor: 'rgba(0, 175, 329)',
      pointBorderColor: 'white',
      pointHoverBorderColor: 'rgb(0, 175, 329)',
      fill: 'start'
    }
  ];

  ngOnInit(): void {
    this.chartData[0].data = this.xValues;
    if(this.chartOptions){
      this.chartOptions.backgroundColor = this.chartColor;
      if(this.height != 0)
        this.chartOptions.maintainAspectRatio = false;
    }
  }
}

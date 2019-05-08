import { Component, OnInit, Inject, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import {Chart} from 'chart.js';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

export interface DialogDataChart {
  title: string;
  datasetLabel: string;
  labels: string[];
  rows: number[];
  bgColor: string[];
}

@Component({
  selector: 'app-chart-dialog',
  templateUrl: './chart-dialog.component.html',
  styleUrls: ['./chart-dialog.component.scss']
})
export class ChartDialogComponent implements OnInit {

  @ViewChild('chart') chartElementRef: ElementRef;
  chart : Chart;

  constructor(public dialogRef: MatDialogRef<ChartDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogDataChart) {

  }

  ngOnInit() {

    this.addBarChart();

  }

  addBarChart() {
    let context = this.chartElementRef.nativeElement;
    this.chart = new Chart(context, {
      type: 'bar',
      data: {
       labels: this.data.labels,
       datasets: [{
           label: this.data.datasetLabel,
           data: this.data.rows,
           backgroundColor: this.data.bgColor,
           borderColor: this.data.bgColor,
           borderWidth: 1
       }]
      },
      options: {
       title:{
           text: this.data.title,
          //  display: true
       },
       responsive: true,
       scales: {
           yAxes: [{
               ticks: {
                   beginAtZero: true
               }
           }]
       }
      }
      });

  }

}

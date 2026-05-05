import { Component, Input, AfterViewInit, ElementRef, ViewChild, OnChanges, SimpleChanges } from '@angular/core';
import { Chart } from 'chart.js/auto';

@Component({
  selector: 'app-simple-chart',
  standalone: true,
  template: `<canvas #chartCanvas></canvas>`,
})
export class SimpleChartComponent implements AfterViewInit, OnChanges {
  @Input() data: number[] = [];
  @Input() labels: string[] = [];
  @Input() label: string = 'Value';

  @ViewChild('chartCanvas') canvas!: ElementRef<HTMLCanvasElement>;
  chart!: Chart;

  ngAfterViewInit() {
    this.createChart();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.chart) {
      this.chart.data.labels = this.labels;
      this.chart.data.datasets[0].data = this.data;
      this.chart.update();
    }
  }

  private createChart() {
    this.chart = new Chart(this.canvas.nativeElement, {
      type: 'line',
      data: {
        labels: this.labels,
        datasets: [
          {
            label: this.label,
            data: this.data,
            borderColor: '#3498db',
            backgroundColor: 'rgba(52, 152, 219, 0.2)',
            tension: 0.3,
          },
        ],
      },
      options: {
        responsive: true,
        animation: false,
        scales: {
          y: { beginAtZero: false },
        },
      },
    });
  }
}

import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HeaderComponent } from './components/header/header.component';
import { SensorCardComponent } from './sensor-card/sensor-card.component';
import { AnomalyListComponent } from './anomaly-list/anomaly-list.component';

import { SensorDataService } from './services/sensor-data.service';
import { RealtimeService } from '../../core/services/realtime.service';
import { ReadingTesterComponent } from './components/reading-tester/reading-tester.component';
import { SimpleChartComponent } from './components/simple-chart/simple-chart.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    HeaderComponent,
    SensorCardComponent,
    AnomalyListComponent,
    ReadingTesterComponent,
    SimpleChartComponent
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class DashboardComponent {
  private sensorData = inject(SensorDataService);
  private realtime = inject(RealtimeService);
  reading$ = this.sensorData.getCurrentReading();
  anomalies$ = this.sensorData.getAnomalies();
  connectionStatus$ = this.realtime.getConnectionStatus();

  chartValues: number[] = [];
  chartLabels: string[] = [];

  ngOnInit(): void {
    console.log('DashboardComponent initialized');
    this.sensorData.loadLatest();

    this.reading$.subscribe(r => {
      if (!r) return;

      const label = new Date(r.timestamp).toLocaleTimeString();

      this.chartValues = [...this.chartValues, r.temperature];
      this.chartLabels = [...this.chartLabels, label];

      if (this.chartValues.length > 20) {
        this.chartValues = this.chartValues.slice(-20);
        this.chartLabels = this.chartLabels.slice(-20);
    }
  });
  }
}

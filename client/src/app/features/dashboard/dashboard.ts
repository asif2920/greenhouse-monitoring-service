import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HeaderComponent } from './components/header/header.component';
import { SensorCardComponent } from './sensor-card/sensor-card.component';
import { AnomalyListComponent } from './anomaly-list/anomaly-list.component';

import { SensorDataService } from './services/sensor-data.service';
import { RealtimeService } from '../../core/services/realtime.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    HeaderComponent,
    SensorCardComponent,
    AnomalyListComponent,
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

  ngOnInit(): void {
    console.log('DashboardComponent initialized');
    this.sensorData.loadLatest();
  }
}

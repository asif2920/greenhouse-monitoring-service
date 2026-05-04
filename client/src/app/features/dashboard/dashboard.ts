import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HeaderComponent } from './components/header/header.component';
import { SensorCardComponent } from './sensor-card/sensor-card.component';
import { AnomalyListComponent } from './anomaly-list/anomaly-list.component';

import { SensorDataService } from './services/sensor-data.service';

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

  reading$ = this.sensorData.getCurrentReading();
  anomalies$ = this.sensorData.getAnomalies();

  ngOnInit(): void {
    console.log('DashboardComponent initialized');
    this.sensorData.loadLatest();
  }
}

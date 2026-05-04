import { Injectable, inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ReadingResponse } from '../models/reading-response';
import { SensorReadingService } from './sensor-reading.service';
import { AnomalyService } from './anomaly.service';
import { Anomaly } from '../models/anomaly';

@Injectable({ providedIn: 'root' })
export class SensorDataService {
  private readingApi = inject(SensorReadingService);
  private anomalyApi = inject(AnomalyService);

  private currentReading$ = new BehaviorSubject<ReadingResponse | null>(null);
  private anomalies$ = new BehaviorSubject<Anomaly[]>([]);

  loadLatest(): void {
    this.readingApi.getLatestReading().subscribe({
      next: (data) => this.currentReading$.next(data),
      error: (err) => console.error('Failed to load latest reading', err),
    });

    this.anomalyApi.getLatestAnomalies().subscribe({
      next: (data) => this.anomalies$.next(data),
      error: (err) => console.error('Failed to load anomalies', err),
    });
  }

  getCurrentReading() {
    return this.currentReading$.asObservable();
  }

  getAnomalies() {
    return this.anomalies$.asObservable();
  }
}

import { Injectable, inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ReadingResponse } from '../models/reading-response';
import { Anomaly } from '../models/anomaly';
import { SensorReadingService } from './sensor-reading.service';
import { AnomalyService } from './anomaly.service';
import { RealtimeService } from '../../../core/services/realtime.service';

@Injectable({ providedIn: 'root' })
export class SensorDataService {
  private readingApi = inject(SensorReadingService);
  private anomalyApi = inject(AnomalyService);
  private realtime = inject(RealtimeService);

  private currentReading$ = new BehaviorSubject<ReadingResponse | null>(null);
  private anomalies$ = new BehaviorSubject<Anomaly[]>([]);

  constructor() {
    this.subscribeToRealtime();
  }

  private subscribeToRealtime() {
    this.realtime.getReadingStream().subscribe((reading) => {
      if (reading) {
        this.currentReading$.next(reading);
      }
    });

    this.realtime.getAnomalyStream().subscribe((anomaly) => {
      if (!anomaly || !anomaly.sensorType) return; // ignore invalid events

      const updated = [anomaly, ...this.anomalies$.value];
      this.anomalies$.next(updated);
});

  }

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

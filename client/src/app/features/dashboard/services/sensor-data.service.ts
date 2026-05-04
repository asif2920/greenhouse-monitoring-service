import { Injectable, inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ReadingResponse } from '../models/reading-response';
import { SensorReadingService } from './sensor-reading.service';

@Injectable({ providedIn: 'root' })
export class SensorDataService {
  private api = inject(SensorReadingService);

  private currentReading$ = new BehaviorSubject<ReadingResponse | null>(null);

  loadLatest(): void {
    console.log('Calling backend...');
    this.api.getLatestReading().subscribe({
      next: (data) => {
        console.log('Backend returned:', data);
        this.currentReading$.next(data);
      },
      error: (err) => console.error('Failed to load latest reading', err),
    });
  }

  getCurrentReading() {
    return this.currentReading$.asObservable();
  }
}

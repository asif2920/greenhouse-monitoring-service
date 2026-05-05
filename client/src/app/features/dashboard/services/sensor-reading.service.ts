import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ReadingResponse } from '../models/reading-response';
import { catchError, Observable, of } from 'rxjs';
import { OfflineQueueService } from './offline-queue.service';

@Injectable({ providedIn: 'root' })
export class SensorReadingService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiBaseUrl;
  private queue = inject(OfflineQueueService);

  getLatestReading(): Observable<ReadingResponse> {
    return this.http.get<ReadingResponse>(`${this.baseUrl}/api/readings/latest`);
  }

postReading(body: any): Observable<any> {
  return this.http.post(`${this.baseUrl}/api/readings`, body).pipe(
    catchError((err) => {
      console.warn('Offline: queued reading');
      this.queue.enqueue({
        url: `${this.baseUrl}/api/readings`,
        body
      });

      return of(null);
    })
  );
}

}

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ReadingResponse } from '../models/reading-response';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class SensorReadingService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiBaseUrl;

  getLatestReading(): Observable<ReadingResponse> {
    return this.http.get<ReadingResponse>(`${this.baseUrl}/api/readings/latest`);
  }

  postReading(body: any) {
  return this.http.post(`${this.baseUrl}/api/readings`, body);
}

}

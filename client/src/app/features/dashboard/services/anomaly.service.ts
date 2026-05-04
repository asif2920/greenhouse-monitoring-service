import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { Anomaly } from '../models/anomaly';

@Injectable({ providedIn: 'root' })
export class AnomalyService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiBaseUrl;

  getLatestAnomalies(): Observable<Anomaly[]> {
    return this.http.get<Anomaly[]>(`${this.baseUrl}/anomalies`);
  }
}

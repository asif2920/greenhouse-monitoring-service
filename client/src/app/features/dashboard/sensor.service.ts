import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { SensorReading } from './sensor-reading';

@Injectable({
  providedIn: 'root'
})
export class SensorService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiBaseUrl;

  getLatestReadings(): Observable<SensorReading[]> {
    return this.http.get<SensorReading[]>(`${this.baseUrl}/sensors/latest`);
  }
}

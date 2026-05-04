import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class RealtimeService {
  private hub!: signalR.HubConnection;

  private reading$ = new BehaviorSubject<any | null>(null);
  private anomaly$ = new BehaviorSubject<any | null>(null);
  private connectionStatus$ = new BehaviorSubject<'connected' | 'reconnecting' | 'disconnected'>('disconnected');

  constructor() {
    this.hub = new signalR.HubConnectionBuilder()
      .withUrl(environment.apiBaseUrl + '/hubs/greenhouse')
      .withAutomaticReconnect()
      .build();

    this.registerHandlers();
    this.start();
  }

  private registerHandlers() {
    this.hub.on('NewReading', (reading) => {
        this.reading$.next(reading);
    });

    this.hub.on('AnomalyDetected', (anomaly) => {
        const anomalies = Array.isArray(anomaly) ? anomaly : [anomaly];

        anomalies.forEach(a => {
            const normalized = {
            id: a.id,
            sensorType: a.sensorType,
            value: a.value,
            zScore: a.zScore,
            reason: a.reason,
            detectedAt: a.detectedAt
        };

        this.anomaly$.next(normalized);
    });
});

    this.hub.onreconnecting(() => this.connectionStatus$.next('reconnecting'));
    this.hub.onreconnected(() => this.connectionStatus$.next('connected'));
    this.hub.onclose(() => this.connectionStatus$.next('disconnected'));
  }

  private async start() {
    try {
      await this.hub.start();
      this.connectionStatus$.next('connected');
    } catch (err) {
      console.error('SignalR connection failed', err);
      setTimeout(() => this.start(), 3000);
    }
  }

  getReadingStream() {
    return this.reading$.asObservable();
  }

  getAnomalyStream() {
    return this.anomaly$.asObservable();
  }

  getConnectionStatus() {
    return this.connectionStatus$.asObservable();
  }
}

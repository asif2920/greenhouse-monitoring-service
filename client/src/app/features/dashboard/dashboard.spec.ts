import { describe, it, expect, vi } from 'vitest';
import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';

import { DashboardComponent } from './dashboard';
import { SensorDataService } from './services/sensor-data.service';
import { RealtimeService } from '../../core/services/realtime.service';

describe('DashboardComponent', () => {
  it('should create', () => {
    const sensorDataMock = {
      loadLatest: vi.fn(),
      getCurrentReading: vi.fn(() => of(null)),
      getAnomalies: vi.fn(() => of([]))
    };

    const realtimeMock = {
      getConnectionStatus: vi.fn(() => of('connected'))
    };

    TestBed.configureTestingModule({
      imports: [DashboardComponent],
      providers: [
        { provide: SensorDataService, useValue: sensorDataMock },
        { provide: RealtimeService, useValue: realtimeMock }
      ]
    });

    const fixture = TestBed.createComponent(DashboardComponent);
    const component = fixture.componentInstance;

    expect(component).toBeTruthy();
  });
});

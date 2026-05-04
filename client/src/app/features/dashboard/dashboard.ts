import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SensorReadingService } from './services/sensor-reading.service';
import { ReadingResponse } from './models/reading-response';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class DashboardComponent implements OnInit {
  private sensorService = inject(SensorReadingService);

  reading: ReadingResponse | null = null;
  loading = true;
  error: string | null = null;

  ngOnInit(): void {
    this.sensorService.getLatestReading().subscribe({
      next: (data: ReadingResponse) => {
        console.log('API data received:', data); // DEBUG
        this.reading = data;
        this.loading = false;
      },
      error: (err: unknown) => {
        console.error('API error:', err); // DEBUG
        this.error = 'Failed to load latest sensor reading.';
        this.loading = false;
      }
    });
  }
}

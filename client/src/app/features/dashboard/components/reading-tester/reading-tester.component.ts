import { Component, inject } from '@angular/core';
import { SensorReadingService } from '../../services/sensor-reading.service';

@Component({
  selector: 'app-reading-tester',
  templateUrl: './reading-tester.component.html',
  styleUrls: ['./reading-tester.component.scss'],
})
export class ReadingTesterComponent {
  private service = inject(SensorReadingService);

  sendRandom() {
    const reading = {
      temperature: 20 + Math.random() * 5,
      humidity: 40 + Math.random() * 10,
      co2Ppm: 400 + Math.floor(Math.random() * 200),
      timestamp: new Date().toISOString()
    };

    this.service.postReading(reading).subscribe({
      error: (err) => console.error('Failed to send reading', err),
    });
  }

  async sendBulk(count: number = 20) {
    for (let i = 0; i < count; i++) {
      this.sendRandom();
      await new Promise(res => setTimeout(res, 300));
    }
  }
}

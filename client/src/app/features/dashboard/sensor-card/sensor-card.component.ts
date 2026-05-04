import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sensor-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sensor-card.component.html',
  styleUrl: './sensor-card.component.scss',
})
export class SensorCardComponent {
  @Input() label!: string;
  @Input() value?: number;
  @Input() unit!: string;
  @Input() statusColor: 'green' | 'yellow' | 'red' = 'green';
}

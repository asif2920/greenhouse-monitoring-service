import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Anomaly } from '../models/anomaly';

@Component({
  selector: 'app-anomaly-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './anomaly-list.component.html',
  styleUrl: './anomaly-list.component.scss',
})
export class AnomalyListComponent {
  @Input() anomalies: Anomaly[] = [];
}

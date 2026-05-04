import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  @Input() lastUpdated: string | null | undefined = null;
  @Input() connectionStatus: 'connected' | 'reconnecting' | 'disconnected' | null = null;
}

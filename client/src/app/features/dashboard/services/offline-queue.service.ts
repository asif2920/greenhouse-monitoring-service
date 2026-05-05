import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class OfflineQueueService {
  private storageKey = 'offlineQueue';

  constructor(private http: HttpClient) {}

  enqueue(request: any) {
    const queue = this.getQueue();
    queue.push(request);
    localStorage.setItem(this.storageKey, JSON.stringify(queue));
  }

  getQueue(): any[] {
    return JSON.parse(localStorage.getItem(this.storageKey) || '[]');
  }

  clearQueue() {
    localStorage.setItem(this.storageKey, JSON.stringify([]));
  }

  async flush(): Promise<void> {
    const queue = this.getQueue();
    if (queue.length === 0) return;

    for (const item of queue) {
      try {
        await this.http.post(item.url, item.body).toPromise();
      } catch {
        return;
      }
    }

    this.clearQueue();
  }
}

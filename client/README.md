# 🌱 Greenhouse Monitoring Dashboard (Frontend)

A responsive, real‑time Angular dashboard for visualizing greenhouse sensor data, anomalies, and live trends.  
Built with **Angular 17**, **Standalone Components**, **Vite**, and **Chart.js**.

---

## 🚀 Features

### 📡 Real‑Time Monitoring
- Live temperature, humidity, and CO₂ readings
- Automatic updates via SignalR
- Connection status indicator

### 📊 Interactive Chart
- Smooth animated line chart for temperature trends
- Auto‑resizes for mobile and tablet
- Immutable data updates for stable rendering

### ⚠️ Anomaly Detection
- Displays anomalies streamed from backend
- Clean, scrollable list with timestamps

### 📱 Fully Responsive UI
- 3‑column → 2‑column → 1‑column sensor card layout
- Mobile‑friendly header and chart
- Developer tester panel aligned left without breaking layout

### 🧪 Developer Tools
- Built‑in Reading Tester to simulate sensor input
- Vitest + Angular TestBed setup for component testing

---

## 🛠️ Tech Stack

- **Angular 17** (Standalone Components)
- **Vite** (dev server + build)
- **Vitest** (unit testing)
- **Chart.js** (line chart)
- **RxJS** (reactive streams)
- **SCSS** (responsive styling)
- **SignalR** (real‑time updates)

---

## 📦 Installation

```bash
cd client
npm install

# 🌱 Greenhouse Monitoring System

![Build](https://img.shields.io/badge/build-passing-brightgreen)
![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Angular](https://img.shields.io/badge/Angular-17+-dd0031)
![Docker](https://img.shields.io/badge/Docker-enabled-2496ED)
![License](https://img.shields.io/badge/license-MIT-lightgrey)

A real-time IoT greenhouse monitoring system built with .NET 8, Angular, SignalR, SQLite, and Docker.

---

# 📸 Screenshots

Add your screenshots:

* Dashboard: `docs/screenshots/dashboard.png`
* Real-time updates: `docs/screenshots/realtime.png`
* Anomalies: `docs/screenshots/anomalies.png`

---

# 🚀 Run Options

You can run this project in **two ways**:

---

# 🐳 Option 1: Run with Docker (Recommended)

```bash
docker compose up --build
```

### Services

* API → [http://localhost:5000](http://localhost:5000)
* Swagger → [http://localhost:5000/swagger](http://localhost:5000/swagger)
* Frontend is not started via Docker by default.

---

## Frontend (Angular)

```bash
cd client
npm install
npm start
```

Frontend runs at:

```
http://localhost:4200
```
---

# 💻 Option 2: Run without Docker (Local Setup)

## Backend (.NET API)

```bash
cd server/src/Greenhouse.Api
dotnet restore
dotnet build
dotnet run
```

API runs at:

```
http://localhost:5000
```

---

## Frontend (Angular)

```bash
cd client
npm install
npm start
```

Frontend runs at:

```
http://localhost:4200
```

---

# 🗄️ SQLite Database Access

## 🔹 With Docker

```bash
docker exec -it greenhouse-api sh
sqlite3 /data/greenhouse.db
```

Example queries:

```sql
.tables
SELECT * FROM SensorReadings;
SELECT * FROM Anomalies;
SELECT COUNT(*) FROM SensorReadings;
```

---

## 🔹 Without Docker

Database file is created locally:

```
server/src/Greenhouse.Api/greenhouse.db
```

Open it:

```bash
sqlite3 greenhouse.db
```

If sqlite3 is missing:

```bash
sudo apt install sqlite3   # Linux
brew install sqlite        # Mac
```

---

# 📡 Test APIs with CURL

## 1. Insert 20 Sensor Readings (Single Request)

```bash
curl -X POST http://localhost:5000/api/readings \
  -H "Content-Type: application/json" \
  -d '[
    {"sequenceNumber":1,"timestamp":"2026-05-04T19:00:00Z","temperature":20,"humidity":50,"co2Ppm":500},
    {"sequenceNumber":2,"timestamp":"2026-05-04T19:01:00Z","temperature":21,"humidity":51,"co2Ppm":505},
    {"sequenceNumber":3,"timestamp":"2026-05-04T19:02:00Z","temperature":19,"humidity":49,"co2Ppm":498},
    {"sequenceNumber":4,"timestamp":"2026-05-04T19:03:00Z","temperature":20,"humidity":50,"co2Ppm":502},
    {"sequenceNumber":5,"timestamp":"2026-05-04T19:04:00Z","temperature":21,"humidity":52,"co2Ppm":501},
    {"sequenceNumber":6,"timestamp":"2026-05-04T19:05:00Z","temperature":20,"humidity":50,"co2Ppm":500},
    {"sequenceNumber":7,"timestamp":"2026-05-04T19:06:00Z","temperature":19,"humidity":48,"co2Ppm":497},
    {"sequenceNumber":8,"timestamp":"2026-05-04T19:07:00Z","temperature":20,"humidity":50,"co2Ppm":503},
    {"sequenceNumber":9,"timestamp":"2026-05-04T19:08:00Z","temperature":21,"humidity":51,"co2Ppm":504},
    {"sequenceNumber":10,"timestamp":"2026-05-04T19:09:00Z","temperature":20,"humidity":50,"co2Ppm":500},
    {"sequenceNumber":11,"timestamp":"2026-05-04T19:10:00Z","temperature":19,"humidity":49,"co2Ppm":499},
    {"sequenceNumber":12,"timestamp":"2026-05-04T19:11:00Z","temperature":20,"humidity":50,"co2Ppm":501},
    {"sequenceNumber":13,"timestamp":"2026-05-04T19:12:00Z","temperature":21,"humidity":52,"co2Ppm":503},
    {"sequenceNumber":14,"timestamp":"2026-05-04T19:13:00Z","temperature":20,"humidity":50,"co2Ppm":500},
    {"sequenceNumber":15,"timestamp":"2026-05-04T19:14:00Z","temperature":19,"humidity":48,"co2Ppm":497},
    {"sequenceNumber":16,"timestamp":"2026-05-04T19:15:00Z","temperature":20,"humidity":50,"co2Ppm":502},
    {"sequenceNumber":17,"timestamp":"2026-05-04T19:16:00Z","temperature":21,"humidity":51,"co2Ppm":504},
    {"sequenceNumber":18,"timestamp":"2026-05-04T19:17:00Z","temperature":20,"humidity":50,"co2Ppm":500},
    {"sequenceNumber":19,"timestamp":"2026-05-04T19:18:00Z","temperature":19,"humidity":49,"co2Ppm":498},
    {"sequenceNumber":20,"timestamp":"2026-05-04T19:19:00Z","temperature":20,"humidity":50,"co2Ppm":501}
  ]'
```

---

## 2. Generate anomaly-like readings (10 samples)

```bash
curl -X POST http://localhost:5000/api/readings -H "Content-Type: application/json" -d '{"temperature": 95, "humidity": 10, "timestamp": "2026-05-04T20:01:00Z"}'
curl -X POST http://localhost:5000/api/readings -H "Content-Type: application/json" -d '{"temperature": 98, "humidity": 12, "timestamp": "2026-05-04T20:02:00Z"}'
curl -X POST http://localhost:5000/api/readings -H "Content-Type: application/json" -d '{"temperature": 100, "humidity": 8, "timestamp": "2026-05-04T20:03:00Z"}'
curl -X POST http://localhost:5000/api/readings -H "Content-Type: application/json" -d '{"temperature": 102, "humidity": 5, "timestamp": "2026-05-04T20:04:00Z"}'
curl -X POST http://localhost:5000/api/readings -H "Content-Type: application/json" -d '{"temperature": 105, "humidity": 3, "timestamp": "2026-05-04T20:05:00Z"}'
curl -X POST http://localhost:5000/api/readings -H "Content-Type: application/json" -d '{"temperature": 110, "humidity": 2, "timestamp": "2026-05-04T20:06:00Z"}'
curl -X POST http://localhost:5000/api/readings -H "Content-Type: application/json" -d '{"temperature": 112, "humidity": 1, "timestamp": "2026-05-04T20:07:00Z"}'
curl -X POST http://localhost:5000/api/readings -H "Content-Type: application/json" -d '{"temperature": 115, "humidity": 1, "timestamp": "2026-05-04T20:08:00Z"}'
curl -X POST http://localhost:5000/api/readings -H "Content-Type: application/json" -d '{"temperature": 118, "humidity": 0, "timestamp": "2026-05-04T20:09:00Z"}'
curl -X POST http://localhost:5000/api/readings -H "Content-Type: application/json" -d '{"temperature": 120, "humidity": 0, "timestamp": "2026-05-04T20:10:00Z"}'
```

👉 These should trigger anomaly detection in dashboard.

---

# 🧩 Architecture

Angular → .NET API → SignalR → SQLite

---

# ⚙️ Assumptions

* Sensor readings are stored in SQLite so the latest reading can always be retrieved.

* For anomaly detection, the backend fetches the last 20 readings from the database and process them in memory.

* Detected anomalies are persisted in SQLite so /api/anomalies can return the last 20 anomalies.

* SignalR uses a single hub endpoint (/hubs/greenhouse) for real‑time updates.

* CORS is fully open during development to allow Angular (localhost:4200) to connect.

* Frontend uses simple services + BehaviorSubjects for state; no NgRx.

* UI is a single dashboard page with no routing complexity.


---

# 🔧 Features

* Sensor ingestion API
* Real-time updates (SignalR)
* Anomaly detection system
* Docker + local support
* SQLite persistence

---

# 🧠 Useful SQLite Queries

```sql
SELECT * FROM SensorReadings ORDER BY timestamp DESC;
SELECT * FROM Anomalies ORDER BY timestamp DESC;
SELECT COUNT(*) FROM SensorReadings;
SELECT * FROM SensorReadings WHERE temperature > 80;
SELECT * FROM SensorReadings WHERE co2Ppm > 600;
```

---

# 🔮 Future Improvements

* Introduce structured logging and better error reporting on both frontend and backend.
* Enhance UI with icons, color themes, and subtle micro‑interactions.
* Better dashboard charts
* Better code reuablity & clean code
* Implement a full offline queue with retry, including automatic flush on reconnection.

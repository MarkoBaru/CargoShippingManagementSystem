# Cargo Shipping Management System

Eine moderne WPF-Anwendung für das Tracking und Management von Frachtgütern, entwickelt mit .NET 8 und basierend auf Domain-Driven Design Prinzipien.

## ?? Übersicht

Das Cargo Shipping Management System ist eine umfassende Lösung für die Verwaltung von Frachtgütern in der Schifffahrt. Die Anwendung ermöglicht das Tracking von Sendungen, die Registrierung von Handling-Events und die Verwaltung von Stammdaten wie Standorten und Reisen.

## ??? Architektur

Das Projekt folgt einer sauberen, geschichteten Architektur:

```
CargoShipping/
??? CargoShipping.Domain/          # Domain-Entitäten und Geschäftslogik
??? CargoShipping.Application/     # Anwendungslogik und Services
??? CargoShipping.Infrastructure/  # Infrastruktur und Datenzugriff
??? CargoShipping.GrpcService/     # gRPC Service
??? CargoShipping.Tests/           # Unit Tests
??? CargoShipping/                 # WPF Client-Anwendung
```

## ?? Features

### ?? Cargo Management
- **Cargo Tracking**: Suche und Verfolgung von Sendungen über Tracking-ID
- **Cargo Erstellung**: Erstellung neuer Sendungen mit Origin, Destination und Deadline
- **Status-Übersicht**: Vollständige Übersicht aller Sendungen in einer DataGrid-Ansicht

### ?? Event Management
- **Handling Events**: Registrierung von Events (RECEIVE, LOAD, UNLOAD, CLAIM, CUSTOMS)
- **Event-Historie**: Vollständige Nachverfolgung aller Events pro Sendung
- **Status-Updates**: Automatische Aktualisierung des Cargo-Status basierend auf Events

### ??? Master Data Management
- **Locations**: Verwaltung von Standorten mit UN/LOCODE
- **Voyages**: Verwaltung von Reisen und Routen
- **Real-time Updates**: Live-Aktualisierung aller Daten

## ??? Technologie-Stack

- **.NET 8**: Moderne .NET Framework Version
- **WPF**: Windows Presentation Foundation für die Benutzeroberfläche
- **C# 12**: Neueste C# Sprachfeatures
- **gRPC**: Für Service-Kommunikation
- **xUnit**: Unit Testing Framework
- **Clean Architecture**: Saubere Trennung der Schichten

## ?? Domain-Modell

Das System basiert auf einem reichhaltigen Domain-Modell:

### Kern-Entitäten
- **Cargo**: Hauptentität mit TrackingId, RouteSpecification, Itinerary
- **HandlingEvent**: Events mit Typ, Zeit, Ort und Voyage-Referenz
- **Location**: Standorte mit UN/LOCODE Standard
- **Voyage**: Reisen mit Zeitplan (Schedule)
- **Itinerary**: Reiseroute mit mehreren Legs

### Value Objects
- **TrackingId**: Eindeutige Sendungsidentifikation
- **LocationRef**: Standort-Referenz
- **RouteSpecification**: Route-Spezifikation mit Origin, Destination, Deadline

### Enumerations
- **HandlingType**: RECEIVE, LOAD, UNLOAD, CLAIM, CUSTOMS
- **TransportStatus**: NOT_RECEIVED, IN_PORT, ONBOARD_CARRIER, CLAIMED, UNKNOWN

## ?? Installation und Setup

### Voraussetzungen
- Visual Studio 2022 oder höher
- .NET 8 SDK
- Windows 10/11

### Installation
1. Repository klonen:
```bash
git clone https://github.com/MarkoBaru/CargoShippingManagementSystem.git
```

2. Solution öffnen:
```bash
cd CargoShippingManagementSystem
start CargoShipping.sln
```

3. Dependencies wiederherstellen:
```bash
dotnet restore
```

4. Projekt builden:
```bash
dotnet build
```

5. Anwendung starten:
```bash
dotnet run --project CargoShipping
```

## ?? Verwendung

### Cargo Tracking
1. **Suche**: Geben Sie eine Tracking-ID in das Suchfeld ein
2. **Details**: Klicken Sie auf ein Cargo in der Liste für Details
3. **Erstellung**: Nutzen Sie "Create New Cargo" für neue Sendungen

### Event Registrierung
1. Wählen Sie das entsprechende Tab "Handling Events"
2. Füllen Sie alle Felder aus (Tracking-ID, Event-Typ, Location, Zeit)
3. Klicken Sie "Register Event" zum Speichern

### Master Data
1. **Locations**: Fügen Sie neue Standorte mit UN/LOCODE hinzu
2. **Voyages**: Erstellen Sie neue Reisen mit Voyage-Nummer

## ?? Screenshots

### Hauptansicht - Cargo Tracking
Die Hauptansicht bietet eine intuitive Oberfläche für das Cargo-Tracking mit Suchfunktion und Detailansicht.

### All Cargo - Übersicht
Vollständige Tabelle aller Sendungen mit Status, aktueller Position und ETA.

### Handling Events
Registrierung und Übersicht aller Handling-Events mit Zeitstempel.

### Master Data Management
Verwaltung von Standorten und Reisen für die Stammdatenpflege.

## ?? Testing

Das Projekt enthält umfassende Unit Tests:

```bash
# Alle Tests ausführen
dotnet test

# Tests mit Coverage
dotnet test --collect:"XPlat Code Coverage"
```

## ?? API Documentation

### gRPC Services
Die Anwendung stellt gRPC Services für externe Integration bereit:

- **CargoService**: CRUD-Operationen für Cargo
- **LocationService**: Standort-Management
- **EventService**: Event-Registrierung

## ?? Contributing

Beiträge sind willkommen! Bitte beachten Sie:

1. Fork das Repository
2. Erstellen Sie einen Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Committen Sie Ihre Änderungen (`git commit -m 'Add some AmazingFeature'`)
4. Push zum Branch (`git push origin feature/AmazingFeature`)
5. Öffnen Sie einen Pull Request

## ?? Coding Standards

- Verwenden Sie C# Naming Conventions
- Befolgen Sie SOLID Prinzipien
- Schreiben Sie Unit Tests für neue Features
- Dokumentieren Sie öffentliche APIs

## ?? Lizenz

Dieses Projekt steht unter der MIT Lizenz - siehe [LICENSE](LICENSE) Datei für Details.

## ?? Team

- **Marko Baru** - *Initial work* - [MarkoBaru](https://github.com/MarkoBaru)

## ?? Acknowledgments

- Domain-Driven Design Patterns von Eric Evans
- Clean Architecture von Robert C. Martin
- .NET Community für Inspiration und Support

## ?? Support

Bei Fragen oder Problemen:
- Öffnen Sie ein Issue auf GitHub
- Kontaktieren Sie das Entwicklerteam

---

**Entwickelt mit ?? und .NET 8**
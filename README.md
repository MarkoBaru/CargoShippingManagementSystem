# Cargo Shipping Management System

Eine moderne WPF-Anwendung f�r das Tracking und Management von Frachtg�tern, entwickelt mit .NET 8 und basierend auf Domain-Driven Design Prinzipien.

## ?? �bersicht

Das Cargo Shipping Management System ist eine umfassende L�sung f�r die Verwaltung von Frachtg�tern in der Schifffahrt. Die Anwendung erm�glicht das Tracking von Sendungen, die Registrierung von Handling-Events und die Verwaltung von Stammdaten wie Standorten und Reisen.

## ??? Architektur

Das Projekt folgt einer sauberen, geschichteten Architektur:

```
CargoShipping/
??? CargoShipping.Domain/          # Domain-Entit�ten und Gesch�ftslogik
??? CargoShipping.Application/     # Anwendungslogik und Services
??? CargoShipping.Infrastructure/  # Infrastruktur und Datenzugriff
??? CargoShipping.GrpcService/     # gRPC Service
??? CargoShipping.Tests/           # Unit Tests
??? CargoShipping/                 # WPF Client-Anwendung
```

## ?? Features

### ?? Cargo Management
- **Cargo Tracking**: Suche und Verfolgung von Sendungen �ber Tracking-ID
- **Cargo Erstellung**: Erstellung neuer Sendungen mit Origin, Destination und Deadline
- **Status-�bersicht**: Vollst�ndige �bersicht aller Sendungen in einer DataGrid-Ansicht

### ?? Event Management
- **Handling Events**: Registrierung von Events (RECEIVE, LOAD, UNLOAD, CLAIM, CUSTOMS)
- **Event-Historie**: Vollst�ndige Nachverfolgung aller Events pro Sendung
- **Status-Updates**: Automatische Aktualisierung des Cargo-Status basierend auf Events

### ??? Master Data Management
- **Locations**: Verwaltung von Standorten mit UN/LOCODE
- **Voyages**: Verwaltung von Reisen und Routen
- **Real-time Updates**: Live-Aktualisierung aller Daten

## ??? Technologie-Stack

- **.NET 8**: Moderne .NET Framework Version
- **WPF**: Windows Presentation Foundation f�r die Benutzeroberfl�che
- **C# 12**: Neueste C# Sprachfeatures
- **gRPC**: F�r Service-Kommunikation
- **xUnit**: Unit Testing Framework
- **Clean Architecture**: Saubere Trennung der Schichten

## ?? Domain-Modell

Das System basiert auf einem reichhaltigen Domain-Modell:

### Kern-Entit�ten
- **Cargo**: Hauptentit�t mit TrackingId, RouteSpecification, Itinerary
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
- Visual Studio 2022 oder h�her
- .NET 8 SDK
- Windows 10/11

### Installation
1. Repository klonen:
```bash
git clone https://github.com/MarkoBaru/CargoShippingManagementSystem.git
```

2. Solution �ffnen:
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
2. **Details**: Klicken Sie auf ein Cargo in der Liste f�r Details
3. **Erstellung**: Nutzen Sie "Create New Cargo" f�r neue Sendungen

### Event Registrierung
1. W�hlen Sie das entsprechende Tab "Handling Events"
2. F�llen Sie alle Felder aus (Tracking-ID, Event-Typ, Location, Zeit)
3. Klicken Sie "Register Event" zum Speichern

### Master Data
1. **Locations**: F�gen Sie neue Standorte mit UN/LOCODE hinzu
2. **Voyages**: Erstellen Sie neue Reisen mit Voyage-Nummer

## ?? Screenshots

### Hauptansicht - Cargo Tracking
Die Hauptansicht bietet eine intuitive Oberfl�che f�r das Cargo-Tracking mit Suchfunktion und Detailansicht.

### All Cargo - �bersicht
Vollst�ndige Tabelle aller Sendungen mit Status, aktueller Position und ETA.

### Handling Events
Registrierung und �bersicht aller Handling-Events mit Zeitstempel.

### Master Data Management
Verwaltung von Standorten und Reisen f�r die Stammdatenpflege.

## ?? Testing

Das Projekt enth�lt umfassende Unit Tests:

```bash
# Alle Tests ausf�hren
dotnet test

# Tests mit Coverage
dotnet test --collect:"XPlat Code Coverage"
```

## ?? API Documentation

### gRPC Services
Die Anwendung stellt gRPC Services f�r externe Integration bereit:

- **CargoService**: CRUD-Operationen f�r Cargo
- **LocationService**: Standort-Management
- **EventService**: Event-Registrierung

## ?? Contributing

Beitr�ge sind willkommen! Bitte beachten Sie:

1. Fork das Repository
2. Erstellen Sie einen Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Committen Sie Ihre �nderungen (`git commit -m 'Add some AmazingFeature'`)
4. Push zum Branch (`git push origin feature/AmazingFeature`)
5. �ffnen Sie einen Pull Request

## ?? Coding Standards

- Verwenden Sie C# Naming Conventions
- Befolgen Sie SOLID Prinzipien
- Schreiben Sie Unit Tests f�r neue Features
- Dokumentieren Sie �ffentliche APIs

## ?? Lizenz

Dieses Projekt steht unter der MIT Lizenz - siehe [LICENSE](LICENSE) Datei f�r Details.

## ?? Team

- **Marko Baru** - *Initial work* - [MarkoBaru](https://github.com/MarkoBaru)

## ?? Acknowledgments

- Domain-Driven Design Patterns von Eric Evans
- Clean Architecture von Robert C. Martin
- .NET Community f�r Inspiration und Support

## ?? Support

Bei Fragen oder Problemen:
- �ffnen Sie ein Issue auf GitHub
- Kontaktieren Sie das Entwicklerteam

---

**Entwickelt mit ?? und .NET 8**
# Student Schedule Manager

## Opis Projektu
Projekt jest aplikacją webową opartą na technologii ASP.NET Core Blazor, która umożliwia zarządzanie użytkownikami, grupami, lekcjami oraz ankietami. Aplikacja wykorzystuje ASP.NET Core Identity do zarządzania użytkownikami i rolami.

## Autorzy
- Tomasz Kryś
- Sebastian Kozłowski

## Wymagania Systemowe
- .NET 8 SDK
- SQL Server
- Visual Studio 2022 (lub inny kompatybilny edytor kodu)

## Konfiguracja Projektu

### Krok 1: Klonowanie Repozytorium
Skopiuj repozytorium na swój lokalny komputer:

```bash
git clone https://github.com/TwojeRepozytorium/Projekt-programowanie.git
cd Projekt-programowanie
```

### Krok 2: Konfiguracja Bazy Danych
Upewnij się, że masz zainstalowany SQL Server. Skonfiguruj połączenie z bazą danych w pliku appsettings.json:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProjektProgramowanie;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Krok 3: Przygotowanie Bazy Danych
Uruchom migracje, aby utworzyć schemat bazy danych:
```bash
dotnet ef database update
```

### Podczas tworzenia schematu bazy danych, w `ApplicationDbContext` są automatycznie tworzeni domyślni użytkownicy oraz role. (metoda `SeedUsers`)
Loginy oraz hasła dostępne są w pliku `ApplicationDbContext`.

 

### Krok 4: Uruchomienie Aplikacji
Uruchom aplikację z poziomu GUI bądź komendą:
```bash
dotnet run
```

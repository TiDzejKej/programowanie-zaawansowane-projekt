# Dokumentacja Projektu

## Spis Treści
1. [Opis Projektu](#opis-projektu)
2. [Autorzy](#autorzy)
3. [Wymagania Systemowe](#wymagania-systemowe)
4. [Konfiguracja Projektu](#konfiguracja-projektu)
   1. [Krok 1: Klonowanie Repozytorium](#krok-1-klonowanie-repozytorium)
   2. [Krok 2: Konfiguracja Bazy Danych](#krok-2-konfiguracja-bazy-danych)
   3. [Krok 3: Przygotowanie Bazy Danych](#krok-3-przygotowanie-bazy-danych)
   4. [Krok 4: Uruchomienie Aplikacji](#krok-4-uruchomienie-aplikacji)
5. [Struktura Projektu](#struktura-projektu)
   1. [Foldery i Pliki](#foldery-i-pliki)
   2. [Kontrolery](#kontrolery)
   3. [Główne Metody Kontrolerów i Uprawnienia](#głowne-metody-kontrolerow)
   3. [Modele](#modele)
   4. [Widoki](#widoki)
6. [Opis Działania Aplikacji](#opis-działania-aplikacji)
   1. [Rejestracja i Logowanie](#rejestracja-i-logowanie)
   2. [Zarządzanie Użytkownikami](#zarządzanie-użytkownikami)
   3. [Zarządzanie Grupami](#zarządzanie-grupami)
   4. [Zarządzanie Lekcjami](#zarządzanie-lekcjami)
   5. [Zarządzanie Ankietami](#zarządzanie-ankietami)
   6. [Eksportowanie Lekcji](#eksportowanie-lekcji)]
   7. [Formularze Kontaktowe](#formularze-kontaktowe)

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

### Krok 4: Uruchomienie Aplikacji
Uruchom aplikację z poziomu GUI bądź komendą:
```bash
dotnet run
```

## Struktura Projektu

### Foldery i Pliki
- **Controllers/**: Zawiera kontrolery MVC, które obsługują logikę aplikacji i zarządzają widokami.
- **Data/**: Zawiera kontekst bazy danych (`ApplicationDbContext`) oraz metody seeda, które inicjalizują dane w bazie danych.
- **Models/**: Zawiera modele danych używane w aplikacji, które reprezentują obiekty i struktury danych.
- **Views/**: Zawiera widoki Razor Pages, które są używane do renderowania interfejsu użytkownika.
- **wwwroot/**: Zawiera statyczne pliki, takie jak CSS, JavaScript, obrazy itp., które są wykorzystywane w aplikacji.
- **Program.cs**: Główny plik uruchamiający aplikację, który konfiguruje usługi i uruchamia aplikację.
- **appsettings.json**: Plik konfiguracyjny aplikacji, w którym przechowywane są ustawienia połączenia z bazą danych oraz inne parametry konfiguracyjne.

### Kontrolery
- **HomeController**: Odpowiada za obsługę głównych stron aplikacji, takich jak strona główna i strona prywatności. Zawiera również akcje do zarządzania użytkownikami.
- **AccountController**: Odpowiada za zarządzanie kontami użytkowników, w tym rejestrację, logowanie, edycję profilu, zmianę hasła oraz usuwanie konta.
- **SurveyController**: Odpowiada za zarządzanie ankietami, w tym tworzenie, wyświetlanie, usuwanie oraz przeglądanie szczegółów ankiet.
- **ContactFormController**: Odpowiada za obsługę formularzy kontaktowych, w tym wyświetlanie formularza, wysyłanie wiadomości oraz przeglądanie listy przesłanych formularzy.
- **GroupsController**: Odpowiada za zarządzanie grupami, w tym tworzenie, edycję, usuwanie oraz wyświetlanie listy grup.
- **SchedulerController**: Odpowiada za zarządzanie harmonogramem lekcji, w tym tworzenie, edycję, usuwanie oraz wyświetlanie lekcji. Zawiera również funkcje do eksportowania lekcji do formatu iCalendar.

### Główne Metody Kontrolerów i Uprawnienia

### HomeController
- **Index**: Wyświetla stronę główną aplikacji. Dostępna dla wszystkich zalogowanych użytkowników.
- **Privacy**: Wyświetla stronę prywatności. Dostępna dla wszystkich użytkowników.

### AccountController
- **Register**: Wyświetla formularz rejestracji nowego użytkownika. Dostępna tylko dla administratorów.
- **Register (POST)**: Przetwarza dane rejestracyjne i tworzy nowe konto użytkownika. Dostępna tylko dla administratorów.
- **Login**: Wyświetla formularz logowania. Dostępna dla wszystkich użytkowników.
- **Login (POST)**: Przetwarza dane logowania i loguje użytkownika. Dostępna dla wszystkich użytkowników.
- **Logout**: Wylogowuje użytkownika. Dostępna dla zalogowanych użytkowników.
- **Edit**: Wyświetla formularz edycji profilu użytkownika. Dostępna administratorów.
- **Edit (POST)**: Przetwarza dane edycji profilu i aktualizuje konto użytkownika. Dostępna dla administratorów.
- **Delete**: Wyświetla formularz usuwania konta użytkownika. Dostępna dla administratorów.
- **Delete (POST)**: Przetwarza dane usuwania konta i usuwa konto użytkownika. Dostępna dla administratorów.

### SurveyController
- **Create**: Wyświetla formularz tworzenia nowej ankiety. Dostępna dla zalogowanych studentów.
- **Create (POST)**: Przetwarza dane ankiety i tworzy nową ankietę. Dostępna dla zalogowanych studentów.
- **Index**: Wyświetla listę ankiet. Dostępna dla zalogowanych studentów, studenci widzą tylko swoje ankiety. Administrator widzi wszystkie ankiety.
- **Detail**: Wyświetla szczegóły ankiety. Dostępna dla administratorów.
- **Delete (POST)**: Usuwa ankietę. Dostępna dla administratorów.

### ContactFormController
- **Index**: Wyświetla formularz kontaktowy. Dostępna dla wszystkich użytkowników.
- **SendEmail (POST)**: Przetwarza dane formularza kontaktowego i wysyła wiadomość. Dostępna dla wszystkich użytkowników.
- **List**: Wyświetla listę przesłanych formularzy kontaktowych. Dostępna dla administratorów.

### GroupsController
- **Index**: Wyświetla listę grup. Dostępna dla administratorów i pracowników.
- **Create**: Wyświetla formularz tworzenia nowej grupy. Dostępna dla administratorów i pracowników.
- **Create (POST)**: Przetwarza dane grupy i tworzy nową grupę. Dostępna dla administratorów i pracowników.
- **Edit**: Wyświetla formularz edycji grupy. Dostępna dla administratorów i pracowników.
- **Edit (POST)**: Przetwarza dane edycji grupy i aktualizuje grupę. Dostępna dla administratorów i pracowników.
- **Delete**: Wyświetla formularz usuwania grupy. Dostępna dla administratorów i pracowników.
- **DeleteConfirmed (POST)**: Usuwa grupę. Dostępna dla administratorów i pracowników.

### SchedulerController
- **Index**: Wyświetla harmonogram lekcji. Dostępna dla zalogowanych użytkowników.
- **GetGroups**: Zwraca listę grup. Dostępna dla administratorów, pracowników, wykładowców i studentów.
- **AddLesson (POST)**: Dodaje nową lekcję. Dostępna dla administratorów i pracowników.
- **UpdateLesson (POST)**: Aktualizuje lekcję. Dostępna dla administratorów i pracowników.
- **ValidateLesson (POST)**: Waliduje lekcję pod kątem konfliktów czasowych. Dostępna dla administratorów i pracowników.
- **DeleteLesson (POST)**: Usuwa lekcję. Dostępna dla administratorów i pracowników.


### Modele
- **ApplicationUser**: Reprezentuje użytkownika aplikacji, zawiera dane takie jak nazwa użytkownika, adres e-mail, imię, nazwisko, adres zamieszkania oraz inne informacje powiązane z użytkownikiem.
- **Group**: Reprezentuje grupę, zawiera dane takie jak nazwa grupy, identyfikator nauczyciela oraz listę studentów przypisanych do grupy.
- **Lesson**: Reprezentuje lekcję, zawiera dane takie jak tytuł lekcji, czas rozpoczęcia i zakończenia, opis oraz identyfikator grupy, do której lekcja należy.
- **Survey**: Reprezentuje ankietę, zawiera dane takie jak identyfikator lekcji, identyfikator studenta, ocena kursu oraz rekomendacja.
- **ContactForm**: Reprezentuje formularz kontaktowy, zawiera dane takie jak tytuł, treść oraz adres e-mail nadawcy.
- **GroupEditViewModel**: Reprezentuje model widoku edycji grupy, zawiera dane takie jak identyfikator grupy, nazwa grupy, identyfikator nauczyciela, lista wybranych studentów oraz listy nauczycieli i studentów do wyboru.
- **LoginViewModel**: Reprezentuje model widoku logowania, zawiera dane takie jak adres e-mail, hasło oraz opcję zapamiętania użytkownika.
- **RegisterViewModel**: Reprezentuje model widoku rejestracji, zawiera dane takie jak imię, nazwisko, adres, adres e-mail, hasło, potwierdzenie hasła oraz rolę użytkownika.
- **SurveyViewModel**: Reprezentuje model widoku ankiety, zawiera dane takie jak identyfikator lekcji, identyfikator studenta, ocena kursu oraz rekomendacja.
- **UserListViewModel**: Reprezentuje model widoku listy użytkowników, zawiera listę użytkowników oraz słownik ról użytkowników.
- **UserWithRolesViewModel**: Reprezentuje model widoku użytkownika z rolami, zawiera dane takie jak identyfikator użytkownika, imię, nazwisko, adres e-mail, adres zamieszkania, data utworzenia konta oraz listy ról użytkownika.

### Widoki
- **Home/Index.cshtml**: Widok strony głównej aplikacji.
- **Home/Privacy.cshtml**: Widok strony prywatności.
- **Account/Register.cshtml**: Widok formularza rejestracji nowego użytkownika.
- **Account/Login.cshtml**: Widok formularza logowania.
- **Account/Edit.cshtml**: Widok formularza edycji profilu użytkownika.
- **Account/Detail.cshtml**: Widok szczegółów profilu użytkownika.
- **Account/Delete.cshtml**: Widok formularza usuwania konta użytkownika.
- **Survey/Create.cshtml**: Widok formularza tworzenia nowej ankiety.
- **Survey/Index.cshtml**: Widok listy ankiet.
- **Survey/Detail.cshtml**: Widok szczegółów ankiety.
- **ContactForm/Index.cshtml**: Widok formularza kontaktowego.
- **ContactForm/List.cshtml**: Widok listy przesłanych formularzy kontaktowych.
- **Groups/Index.cshtml**: Widok listy grup.
- **Groups/Edit.cshtml**: Widok formularza edycji grupy.
- **Groups/Delete.cshtml**: Widok formularza usuwania grupy.
- **Groups/Create.cshtml**: Widok formularza tworzenia nowej grupy.
- **Scheduler/Index.cshtml**: Widok harmonogramu lekcji.

## Opis Działania Aplikacji

### Rejestracja i Logowanie

W aplikacji proces rejestracji jest dostępny tylko dla administratorów. 

- **Logowanie:**  
  Użytkownicy mogą logować się, podając swój e-mail i hasło. Jeśli dane są poprawne, użytkownik zostaje zalogowany i przekierowany na stronę główną. 

![login!](wwwroot/images/login.png)

- **Rejestracja:**  
  Aby zarejestrować nowego użytkownika, należy przejść do formularza rejestracji, dostępnego tylko dla administratora. W formularzu wymagane są dane, takie jak imię, nazwisko, adres e-mail, hasło, adres zamieszkania oraz rola użytkownika. Po wypełnieniu formularza administrator może utworzyć konto. Użytkownik zostaje automatycznie przypisany do roli, adres email jest już potwierdzony.

![register!](wwwroot/images/register.png)

- **Uwaga:** Rejestracja nowych użytkowników jest dostępna tylko dla administratorów aplikacji.


### Zarządzanie Użytkownikami

Administratorzy mają pełne uprawnienia do zarządzania użytkownikami w aplikacji. Mogą:

- **Przeglądanie listy użytkowników** – Administratorzy mogą przeglądać wszystkich użytkowników zarejestrowanych w aplikacji oraz szczegóły ich kont (imię, nazwisko, adres e-mail, adres, data rejestracji).
  
![users!](wwwroot/images/user_list.png)
- **Edytowanie danych użytkowników** – Administratorzy mogą edytować dane użytkowników, takie jak imię, nazwisko, e-mail, adres, oraz przypisywać im odpowiednie role.

![edit!](wwwroot/images/user_edit.png)
- **Przypisywanie ról** – Administratorzy mają możliwość przypisywania ról użytkownikom, co determinuje ich uprawnienia w aplikacji (np. student, wykładowca, pracownik).
  
- **Usuwanie kont użytkowników** – Administratorzy mogą usuwać konta użytkowników, co powoduje ich trwałe usunięcie z systemu.

![delete!](wwwroot/images/user_delete.png)



### Zarządzanie Grupami
Administratorzy i pracownicy mogą tworzyć, edytować i usuwać grupy. Każda grupa ma przypisanego nauczyciela oraz listę studentów. Użytkownicy mogą przeglądać listę grup oraz szczegóły poszczególnych grup.

![groups!](wwwroot/images/group_list.png)

### Zarządzanie Lekcjami
Administratorzy, pracownicy mogą tworzyć, edytować i usuwać lekcje. Każda lekcja ma przypisany tytuł, czas rozpoczęcia i zakończenia, opis oraz grupę, do której należy. Użytkownicy mogą przeglądać harmonogram lekcji oraz szczegóły poszczególnych lekcji.

![lessons!](wwwroot/images/scheduler.png)
![lessons!](wwwroot/images/scheduler_day_view.png)
![lessons!](wwwroot/images/scheduler_month_view.png)
![lessons!](wwwroot/images/scheduler_agenda.png)

![lessons!](wwwroot/images/new_lesson.png)
![lessons!](wwwroot/images/lesson_end.png)
![lessons!](wwwroot/images/lesson_end2.png)

### Eksportowanie Lekcji
Użytkownicy mogą eksportować harmonogram lekcji do formatu iCalendar, co umożliwia importowanie lekcji do kalendarzy zewnętrznych aplikacji, takich jak Google Calendar czy Microsoft Outlook.

### Zarządzanie Ankietami
Studenci mogą tworzyć ankiety po zakończonych zajęciach, oceniając kursy i dodając rekomendacje. Administratorzy mogą przeglądać listę ankiet oraz szczegóły poszczególnych ankiet. Studenci mogą przeglądać tylko swoje ankiety.

![surveys!](wwwroot/images/lesson_survey.png)
![surveys!](wwwroot/images/survey_new.png)
![surveys!](wwwroot/images/survey_list.png)


### Formularze Kontaktowe
Niezarejestrowani użytkownicy mogą wysyłać formularze kontaktowe, podając tytuł, treść oraz adres e-mail. Administratorzy mogą przeglądać listę przesłanych formularzy kontaktowych.

![contact!](wwwroot/images/contact_form.png)


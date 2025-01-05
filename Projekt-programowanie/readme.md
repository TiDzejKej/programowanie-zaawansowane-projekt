# Dokumentacja Projektu

## Spis Tre�ci
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
   3. [G��wne Metody Kontroler�w i Uprawnienia](#g�owne-metody-kontrolerow)
   3. [Modele](#modele)
   4. [Widoki](#widoki)
6. [Opis Dzia�ania Aplikacji](#opis-dzia�ania-aplikacji)
   1. [Rejestracja i Logowanie](#rejestracja-i-logowanie)
   2. [Zarz�dzanie U�ytkownikami](#zarz�dzanie-u�ytkownikami)
   3. [Zarz�dzanie Grupami](#zarz�dzanie-grupami)
   4. [Zarz�dzanie Lekcjami](#zarz�dzanie-lekcjami)
   5. [Zarz�dzanie Ankietami](#zarz�dzanie-ankietami)
   6. [Eksportowanie Lekcji](#eksportowanie-lekcji)]
   7. [Formularze Kontaktowe](#formularze-kontaktowe)

## Opis Projektu
Projekt jest aplikacj� webow� opart� na technologii ASP.NET Core Blazor, kt�ra umo�liwia zarz�dzanie u�ytkownikami, grupami, lekcjami oraz ankietami. Aplikacja wykorzystuje ASP.NET Core Identity do zarz�dzania u�ytkownikami i rolami.

## Autorzy
- Tomasz Kry�
- Sebastian Koz�owski

## Wymagania Systemowe
- .NET 8 SDK
- SQL Server
- Visual Studio 2022 (lub inny kompatybilny edytor kodu)

## Konfiguracja Projektu

### Krok 1: Klonowanie Repozytorium
Skopiuj repozytorium na sw�j lokalny komputer:

```bash
git clone https://github.com/TwojeRepozytorium/Projekt-programowanie.git
cd Projekt-programowanie
```

### Krok 2: Konfiguracja Bazy Danych
Upewnij si�, �e masz zainstalowany SQL Server. Skonfiguruj po��czenie z baz� danych w pliku appsettings.json:

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
Uruchom migracje, aby utworzy� schemat bazy danych:
```bash
dotnet ef database update
```

### Krok 4: Uruchomienie Aplikacji
Uruchom aplikacj� z poziomu GUI b�d� komend�:
```bash
dotnet run
```

## Struktura Projektu

### Foldery i Pliki
- **Controllers/**: Zawiera kontrolery MVC, kt�re obs�uguj� logik� aplikacji i zarz�dzaj� widokami.
- **Data/**: Zawiera kontekst bazy danych (`ApplicationDbContext`) oraz metody seeda, kt�re inicjalizuj� dane w bazie danych.
- **Models/**: Zawiera modele danych u�ywane w aplikacji, kt�re reprezentuj� obiekty i struktury danych.
- **Views/**: Zawiera widoki Razor Pages, kt�re s� u�ywane do renderowania interfejsu u�ytkownika.
- **wwwroot/**: Zawiera statyczne pliki, takie jak CSS, JavaScript, obrazy itp., kt�re s� wykorzystywane w aplikacji.
- **Program.cs**: G��wny plik uruchamiaj�cy aplikacj�, kt�ry konfiguruje us�ugi i uruchamia aplikacj�.
- **appsettings.json**: Plik konfiguracyjny aplikacji, w kt�rym przechowywane s� ustawienia po��czenia z baz� danych oraz inne parametry konfiguracyjne.

### Kontrolery
- **HomeController**: Odpowiada za obs�ug� g��wnych stron aplikacji, takich jak strona g��wna i strona prywatno�ci. Zawiera r�wnie� akcje do zarz�dzania u�ytkownikami.
- **AccountController**: Odpowiada za zarz�dzanie kontami u�ytkownik�w, w tym rejestracj�, logowanie, edycj� profilu, zmian� has�a oraz usuwanie konta.
- **SurveyController**: Odpowiada za zarz�dzanie ankietami, w tym tworzenie, wy�wietlanie, usuwanie oraz przegl�danie szczeg��w ankiet.
- **ContactFormController**: Odpowiada za obs�ug� formularzy kontaktowych, w tym wy�wietlanie formularza, wysy�anie wiadomo�ci oraz przegl�danie listy przes�anych formularzy.
- **GroupsController**: Odpowiada za zarz�dzanie grupami, w tym tworzenie, edycj�, usuwanie oraz wy�wietlanie listy grup.
- **SchedulerController**: Odpowiada za zarz�dzanie harmonogramem lekcji, w tym tworzenie, edycj�, usuwanie oraz wy�wietlanie lekcji. Zawiera r�wnie� funkcje do eksportowania lekcji do formatu iCalendar.

### G��wne Metody Kontroler�w i Uprawnienia

### HomeController
- **Index**: Wy�wietla stron� g��wn� aplikacji. Dost�pna dla wszystkich zalogowanych u�ytkownik�w.
- **Privacy**: Wy�wietla stron� prywatno�ci. Dost�pna dla wszystkich u�ytkownik�w.

### AccountController
- **Register**: Wy�wietla formularz rejestracji nowego u�ytkownika. Dost�pna tylko dla administrator�w.
- **Register (POST)**: Przetwarza dane rejestracyjne i tworzy nowe konto u�ytkownika. Dost�pna tylko dla administrator�w.
- **Login**: Wy�wietla formularz logowania. Dost�pna dla wszystkich u�ytkownik�w.
- **Login (POST)**: Przetwarza dane logowania i loguje u�ytkownika. Dost�pna dla wszystkich u�ytkownik�w.
- **Logout**: Wylogowuje u�ytkownika. Dost�pna dla zalogowanych u�ytkownik�w.
- **Edit**: Wy�wietla formularz edycji profilu u�ytkownika. Dost�pna administrator�w.
- **Edit (POST)**: Przetwarza dane edycji profilu i aktualizuje konto u�ytkownika. Dost�pna dla administrator�w.
- **Delete**: Wy�wietla formularz usuwania konta u�ytkownika. Dost�pna dla administrator�w.
- **Delete (POST)**: Przetwarza dane usuwania konta i usuwa konto u�ytkownika. Dost�pna dla administrator�w.

### SurveyController
- **Create**: Wy�wietla formularz tworzenia nowej ankiety. Dost�pna dla zalogowanych student�w.
- **Create (POST)**: Przetwarza dane ankiety i tworzy now� ankiet�. Dost�pna dla zalogowanych student�w.
- **Index**: Wy�wietla list� ankiet. Dost�pna dla zalogowanych student�w, studenci widz� tylko swoje ankiety. Administrator widzi wszystkie ankiety.
- **Detail**: Wy�wietla szczeg�y ankiety. Dost�pna dla administrator�w.
- **Delete (POST)**: Usuwa ankiet�. Dost�pna dla administrator�w.

### ContactFormController
- **Index**: Wy�wietla formularz kontaktowy. Dost�pna dla wszystkich u�ytkownik�w.
- **SendEmail (POST)**: Przetwarza dane formularza kontaktowego i wysy�a wiadomo��. Dost�pna dla wszystkich u�ytkownik�w.
- **List**: Wy�wietla list� przes�anych formularzy kontaktowych. Dost�pna dla administrator�w.

### GroupsController
- **Index**: Wy�wietla list� grup. Dost�pna dla administrator�w i pracownik�w.
- **Create**: Wy�wietla formularz tworzenia nowej grupy. Dost�pna dla administrator�w i pracownik�w.
- **Create (POST)**: Przetwarza dane grupy i tworzy now� grup�. Dost�pna dla administrator�w i pracownik�w.
- **Edit**: Wy�wietla formularz edycji grupy. Dost�pna dla administrator�w i pracownik�w.
- **Edit (POST)**: Przetwarza dane edycji grupy i aktualizuje grup�. Dost�pna dla administrator�w i pracownik�w.
- **Delete**: Wy�wietla formularz usuwania grupy. Dost�pna dla administrator�w i pracownik�w.
- **DeleteConfirmed (POST)**: Usuwa grup�. Dost�pna dla administrator�w i pracownik�w.

### SchedulerController
- **Index**: Wy�wietla harmonogram lekcji. Dost�pna dla zalogowanych u�ytkownik�w.
- **GetGroups**: Zwraca list� grup. Dost�pna dla administrator�w, pracownik�w, wyk�adowc�w i student�w.
- **AddLesson (POST)**: Dodaje now� lekcj�. Dost�pna dla administrator�w i pracownik�w.
- **UpdateLesson (POST)**: Aktualizuje lekcj�. Dost�pna dla administrator�w i pracownik�w.
- **ValidateLesson (POST)**: Waliduje lekcj� pod k�tem konflikt�w czasowych. Dost�pna dla administrator�w i pracownik�w.
- **DeleteLesson (POST)**: Usuwa lekcj�. Dost�pna dla administrator�w i pracownik�w.


### Modele
- **ApplicationUser**: Reprezentuje u�ytkownika aplikacji, zawiera dane takie jak nazwa u�ytkownika, adres e-mail, imi�, nazwisko, adres zamieszkania oraz inne informacje powi�zane z u�ytkownikiem.
- **Group**: Reprezentuje grup�, zawiera dane takie jak nazwa grupy, identyfikator nauczyciela oraz list� student�w przypisanych do grupy.
- **Lesson**: Reprezentuje lekcj�, zawiera dane takie jak tytu� lekcji, czas rozpocz�cia i zako�czenia, opis oraz identyfikator grupy, do kt�rej lekcja nale�y.
- **Survey**: Reprezentuje ankiet�, zawiera dane takie jak identyfikator lekcji, identyfikator studenta, ocena kursu oraz rekomendacja.
- **ContactForm**: Reprezentuje formularz kontaktowy, zawiera dane takie jak tytu�, tre�� oraz adres e-mail nadawcy.
- **GroupEditViewModel**: Reprezentuje model widoku edycji grupy, zawiera dane takie jak identyfikator grupy, nazwa grupy, identyfikator nauczyciela, lista wybranych student�w oraz listy nauczycieli i student�w do wyboru.
- **LoginViewModel**: Reprezentuje model widoku logowania, zawiera dane takie jak adres e-mail, has�o oraz opcj� zapami�tania u�ytkownika.
- **RegisterViewModel**: Reprezentuje model widoku rejestracji, zawiera dane takie jak imi�, nazwisko, adres, adres e-mail, has�o, potwierdzenie has�a oraz rol� u�ytkownika.
- **SurveyViewModel**: Reprezentuje model widoku ankiety, zawiera dane takie jak identyfikator lekcji, identyfikator studenta, ocena kursu oraz rekomendacja.
- **UserListViewModel**: Reprezentuje model widoku listy u�ytkownik�w, zawiera list� u�ytkownik�w oraz s�ownik r�l u�ytkownik�w.
- **UserWithRolesViewModel**: Reprezentuje model widoku u�ytkownika z rolami, zawiera dane takie jak identyfikator u�ytkownika, imi�, nazwisko, adres e-mail, adres zamieszkania, data utworzenia konta oraz listy r�l u�ytkownika.

### Widoki
- **Home/Index.cshtml**: Widok strony g��wnej aplikacji.
- **Home/Privacy.cshtml**: Widok strony prywatno�ci.
- **Account/Register.cshtml**: Widok formularza rejestracji nowego u�ytkownika.
- **Account/Login.cshtml**: Widok formularza logowania.
- **Account/Edit.cshtml**: Widok formularza edycji profilu u�ytkownika.
- **Account/Detail.cshtml**: Widok szczeg��w profilu u�ytkownika.
- **Account/Delete.cshtml**: Widok formularza usuwania konta u�ytkownika.
- **Survey/Create.cshtml**: Widok formularza tworzenia nowej ankiety.
- **Survey/Index.cshtml**: Widok listy ankiet.
- **Survey/Detail.cshtml**: Widok szczeg��w ankiety.
- **ContactForm/Index.cshtml**: Widok formularza kontaktowego.
- **ContactForm/List.cshtml**: Widok listy przes�anych formularzy kontaktowych.
- **Groups/Index.cshtml**: Widok listy grup.
- **Groups/Edit.cshtml**: Widok formularza edycji grupy.
- **Groups/Delete.cshtml**: Widok formularza usuwania grupy.
- **Groups/Create.cshtml**: Widok formularza tworzenia nowej grupy.
- **Scheduler/Index.cshtml**: Widok harmonogramu lekcji.

## Opis Dzia�ania Aplikacji

### Rejestracja i Logowanie

W aplikacji proces rejestracji jest dost�pny tylko dla administrator�w. 

- **Logowanie:**  
  U�ytkownicy mog� logowa� si�, podaj�c sw�j e-mail i has�o. Je�li dane s� poprawne, u�ytkownik zostaje zalogowany i przekierowany na stron� g��wn�. 

![login!](wwwroot/images/login.png)

- **Rejestracja:**  
  Aby zarejestrowa� nowego u�ytkownika, nale�y przej�� do formularza rejestracji, dost�pnego tylko dla administratora. W formularzu wymagane s� dane, takie jak imi�, nazwisko, adres e-mail, has�o, adres zamieszkania oraz rola u�ytkownika. Po wype�nieniu formularza administrator mo�e utworzy� konto. U�ytkownik zostaje automatycznie przypisany do roli, adres email jest ju� potwierdzony.

![register!](wwwroot/images/register.png)

- **Uwaga:** Rejestracja nowych u�ytkownik�w jest dost�pna tylko dla administrator�w aplikacji.


### Zarz�dzanie U�ytkownikami

Administratorzy maj� pe�ne uprawnienia do zarz�dzania u�ytkownikami w aplikacji. Mog�:

- **Przegl�danie listy u�ytkownik�w** � Administratorzy mog� przegl�da� wszystkich u�ytkownik�w zarejestrowanych w aplikacji oraz szczeg�y ich kont (imi�, nazwisko, adres e-mail, adres, data rejestracji).
  
![users!](wwwroot/images/user_list.png)
- **Edytowanie danych u�ytkownik�w** � Administratorzy mog� edytowa� dane u�ytkownik�w, takie jak imi�, nazwisko, e-mail, adres, oraz przypisywa� im odpowiednie role.

![edit!](wwwroot/images/user_edit.png)
- **Przypisywanie r�l** � Administratorzy maj� mo�liwo�� przypisywania r�l u�ytkownikom, co determinuje ich uprawnienia w aplikacji (np. student, wyk�adowca, pracownik).
  
- **Usuwanie kont u�ytkownik�w** � Administratorzy mog� usuwa� konta u�ytkownik�w, co powoduje ich trwa�e usuni�cie z systemu.

![delete!](wwwroot/images/user_delete.png)



### Zarz�dzanie Grupami
Administratorzy i pracownicy mog� tworzy�, edytowa� i usuwa� grupy. Ka�da grupa ma przypisanego nauczyciela oraz list� student�w. U�ytkownicy mog� przegl�da� list� grup oraz szczeg�y poszczeg�lnych grup.

![groups!](wwwroot/images/group_list.png)

### Zarz�dzanie Lekcjami
Administratorzy, pracownicy mog� tworzy�, edytowa� i usuwa� lekcje. Ka�da lekcja ma przypisany tytu�, czas rozpocz�cia i zako�czenia, opis oraz grup�, do kt�rej nale�y. U�ytkownicy mog� przegl�da� harmonogram lekcji oraz szczeg�y poszczeg�lnych lekcji.

![lessons!](wwwroot/images/scheduler.png)
![lessons!](wwwroot/images/scheduler_day_view.png)
![lessons!](wwwroot/images/scheduler_month_view.png)
![lessons!](wwwroot/images/scheduler_agenda.png)

![lessons!](wwwroot/images/new_lesson.png)
![lessons!](wwwroot/images/lesson_end.png)
![lessons!](wwwroot/images/lesson_end2.png)

### Eksportowanie Lekcji
U�ytkownicy mog� eksportowa� harmonogram lekcji do formatu iCalendar, co umo�liwia importowanie lekcji do kalendarzy zewn�trznych aplikacji, takich jak Google Calendar czy Microsoft Outlook.

### Zarz�dzanie Ankietami
Studenci mog� tworzy� ankiety po zako�czonych zaj�ciach, oceniaj�c kursy i dodaj�c rekomendacje. Administratorzy mog� przegl�da� list� ankiet oraz szczeg�y poszczeg�lnych ankiet. Studenci mog� przegl�da� tylko swoje ankiety.

![surveys!](wwwroot/images/lesson_survey.png)
![surveys!](wwwroot/images/survey_new.png)
![surveys!](wwwroot/images/survey_list.png)


### Formularze Kontaktowe
Niezarejestrowani u�ytkownicy mog� wysy�a� formularze kontaktowe, podaj�c tytu�, tre�� oraz adres e-mail. Administratorzy mog� przegl�da� list� przes�anych formularzy kontaktowych.

![contact!](wwwroot/images/contact_form.png)


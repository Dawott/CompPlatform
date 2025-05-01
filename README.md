# Dokumentacja dla projektu Platformy Compliance

## Charakterystyka oprogramowania

**Nazwa skrócona:** </br>
Platforma Compliance

**Nazwa pełna:** </br>
Platforma ewidencji narzędzi AI stosowanych w organizacji

**Krótki opis ze wskazaniem celów**</br>
Niniejsza platforma służy jako narzędzie wspomagające organizację w zarządzaniu ryzykiem regulacyjnym związanym z `AI Act`.

Pierwszym zadaniem systemu jest utrzymywanie portfolio komponentów oraz programów używających Sztucznej Inteligencji przez cały ich cykl życia. Każdy element będzie miał przydzieloną osobę z zespołu regulacji lub jej odpowiednika.

Kolejną funkcją byłaby ewidencja ryzyka na podstawie regulacji `AI Act` oraz wszelkich polityk wewnętrznych organizacji (jeśli istnieją). Elementem tego zadania jest kwestionariusz ryzyka wraz z automatyczną kalkulacją i klasyfikacją ryzyka. 
Dzięki temu organizacja będzie miała pełen wgląd w narzędzia stosowane wewnątrz i ich potencjalne zobowiązania regulacyjne.

## Prawa autorskie

#### Autor:
Dawid Tott

#### Warunki licencyjne
[`MIT License`](LICENSE)

## Specyfika wymagań


| ID | Nazwa       | Opis                                                                           | Priorytet | Kategoria    |
|----|-------------|--------------------------------------------------------------------------------|-----------|--------------|
| 1  | Obsługa ról | Podział na użytkownika-rejestratora, użytkownika-audytora i administratora | Wymagane  | Funkcjonalne |
|2a | Rejestrowanie narzędzi | Każdy użytkownik-rejestrator może rozpocząć procedurę rejestracji narzędzia | Wymagane | Funkcjonalne |
| 2b | Dane narzędzia | Każdy użytkownik-rejestrator powinien wypełnić rubrykę z podstawowymi danymi narzędzia | Wymagane | Funkcjonalne |
| 2c | Repozytorium | Możliwość załączenia kodu źródłowego, fragmentu kodu lub pełnej dokumentacji do rejestracji | Opcjonalne | Funkcjonalne |
| 3 | Kwestionariusz ryzyka | Każdy użytkownik-rejestrator powinien uzupełnić obowiązkowy kwestionariusz ryzyka przed wysłaniem wniosku do weryfikacji | Wymagane | Funkcjonalne |
| 4a | Zapisanie draftu | Podczas edycji kwestionariusza jakikolwiek użytkownik na jakimkolwiek etapie może zapisać aktualną pracę bez wysłania do następnego statusu | Przydatne | Funkcjonalne |
| 4b | Autozapis | Podczas edycji kwestionariusz zapisze się w wersji draft bez ingerencji użytkownika | Opcjonalne | Funkcjonalne |
| 5 | Kalkulacja ryzyka | Po uzupełnieniu kwestionariusza użytkownik powinien widzieć oszacowane ryzyko | Wymagane | Funkcjonalne |
| 5 | Kalkulacja ryzyka | Po uzupełnieniu kwestionariusza użytkownik powinien widzieć oszacowane ryzyko | Wymagane | Funkcjonalne |
| 6 | Zdefiniowany przepływ statusów | Powinien istnieć zdefiniowany w konfiguracji przepływ statusów dla konkretnych ról | Wymagane | Funkcjonalne |
| 7 | Baza danych | Wszystkie kwestionariusze powinny być zapisywane w bazie danych | Wymagane | Funkcjonalne |
| 8a | Lista pytań | Podczas uzupełniania kwestionariusza powinna ładować się lista predefiniowanych pytań wraz z możliwymi odpowiedziami | Wymagane | Funkcjonalne |
| 8b | Dynamiczna lista pytań | Lista pytań w kwestionariuszu powinna dopasowywać się do kategorii rejestrowanego narzędzia | Opcjonalne | Funkcjonalne |
| 8c | Edycja listy pytań | Użytkownicy-audytorzy powinni mieć dostępną sekcję umożliwiającą edycję pytań kwestionariusza | Wymagane | Funkcjonalne |
| 9 | Weryfikacja kwestionariuszy | Użytkownicy-audytorzy powinni mieć dostęp do wszystkich wypełnionych kwestionariuszy | Wymagane | Funkcjonalne |
| 10 | Zmiana statusu i komentarze | Użytkownicy-audytorzy powinni mieć możliwość zmian statusu wniosku, odesłanie go, komentowanie | Wymagane | Funkcjonalne |
| 11 | Podłączenie z API regulatora | Możliwość raportowania bezpośrednio do regulatora gdy taka możliwość się pojawi | Opcjonalne | Funkcjonalne |
| 12 | Powiadomienia | System powinien wysyłać powiadomienia email do użytkowników przy zmianie statusu lub przydzieleniu zadania | Przydatne | Funkcjonalne | 
| 13 | Historia zmian | Rejestrowanie historii zmian we wnioskach i kwestionariuszach, z możliwością przeglądu poprzednich wersji | Przydatne | Funkcjonalne | 
| 14 | Wyszukiwarka i filtry | Zaawansowane wyszukiwanie i filtrowanie zarejestrowanych narzędzi według różnych kryteriów | Przydatne | Funkcjonalne | 
| 15 | Panel statystyk | Dashboard z kluczowymi statystykami i wizualizacjami dla audytorów i administratorów | Opcjonalne | Funkcjonalne | 
| 16 | Eksport danych | Możliwość eksportu danych do formatów CSV, PDF lub Excel | Przydatne | Funkcjonalne |

## Architektura systemu

#### Architektura rozwoju
* .NET 8.0
* Framework EF Core
* Blazor Web Apps (Server)
* Visual Studio Code (z pluginami)

#### Architektura uruchomieniowa
* .NET 8.0
* Blazor Web Apps (Server)

## Testy
#### 1. Scenariusz: Logowanie i obsługa ról
IDST-001

**Nazwa:** Weryfikacja uprawnień dla różnych ról użytkowników

**Wymagania:** 1 - Obsługa ról

**Warunki wstępne:** System zawiera zdefiniowane konta dla administratora, użytkownika-rejestratora oraz użytkownika-audytora

**Kroki testowe** 1. Zaloguj się jako użytkownik-rejestrator<br>2. Sprawdź dostępne opcje menu<br>3. Wyloguj się<br>4. Zaloguj się jako użytkownik-audytor<br>5. Sprawdź dostępne opcje menu<br>6. Wyloguj się<br>7. Zaloguj się jako administrator<br>8. Sprawdź dostępne opcje menu

**Oczekiwany rezultat:**
- Użytkownik-rejestrator ma dostęp do: rejestracji narzędzi, przeglądania swoich wniosków, edycji swoich draftów
- Użytkownik-audytor ma dostęp do: przeglądania wszystkich wniosków, weryfikacji kwestionariuszy, edycji listy pytań
- Administrator ma dostęp do wszystkich funkcji oraz zarządzania użytkownikami

#### 2. Scenariusz: Rejestracja nowego narzędzia
IDST-002

**Nazwa:** Rejestracja nowego narzędzia i wypełnienie kwestionariusza

**Wymagania:** 2a, 2b, 3, 5, 8a

**Warunki wstępne:** Użytkownik jest zalogowany jako rejestrator

**Kroki testowe:** 1. Wybierz opcję "Rejestruj nowe narzędzie"<br>2. Wypełnij podstawowe dane narzędzia (nazwa, wersja, opis, kategoria)<br>3. Prześlij dane podstawowe<br>4. Wypełnij kwestionariusz ryzyka odpowiadając na wszystkie pytania<br>5. Zapisz i wyślij wniosek

**Oczekiwany rezultat:** 
- System przyjmuje dane narzędzia
- Kwestionariusz zawiera pytania odpowiednie dla kategorii narzędzia 
- Po wypełnieniu kwestionariusza system wyświetla oszacowane ryzyko 
- Wniosek jest zapisany w bazie i zmienia status na "Do weryfikacji"

#### 3. Scenariusz: Zapisanie draftu kwestionariusza
IDST-003

**Nazwa:** Zapisanie częściowo wypełnionego kwestionariusza jako draft

**Wymagania:** 4a, 7

**Warunki wstępne:** Użytkownik jest zalogowany jako rejestrator i rozpoczął wypełnianie kwestionariusza

**Kroki testowe:** 1. Wypełnij część pytań kwestionariusza<br>2. Kliknij przycisk "Zapisz jako draft"<br>3. Wyloguj się z systemu<br>4. Zaloguj się ponownie<br>5. Sprawdź sekcję "Moje drafty"<br>6. Otwórz zapisany draft

**Oczekiwany rezultat:** 
- System zapisuje częściowo wypełniony kwestionariusz 
- Draft jest widoczny w sekcji "Moje drafty" 
- Po otwarciu draftu wszystkie wcześniej wprowadzone dane są zachowane

#### 4. Scenariusz: Weryfikacja kwestionariusza przez audytora
IDST-004

**Nazwa:** Weryfikacja i zmiana statusu kwestionariusza przez audytora

**Wymagania:** 9, 10, 6

**Warunki wstępne:** W systemie istnieje wniosek ze statusem "Do weryfikacji"

**Kroki testowe:** 1. Zaloguj się jako użytkownik-audytor<br>2. Przejdź do listy wniosków do weryfikacji<br>3. Otwórz wybrany wniosek<br>4. Przejrzyj dane narzędzia i wypełniony kwestionariusz<br>5. Dodaj komentarz do wniosku<br>6. Zmień status wniosku na "Do poprawy"<br>7. Zapisz zmiany

**Oczekiwany rezultat:** 
- Komentarz jest zapisany w systemie
- Status wniosku zmienia się na "Do poprawy" 
- Rejestrator może zobaczyć komentarz i status swojego wniosku

#### 5. Scenariusz: Edycja listy pytań kwestionariusza
IDST-005

**Nazwa:** Edycja listy pytań w kwestionariuszu przez audytora

**Wymagania:** 8c

**Warunki wstępne:** Użytkownik jest zalogowany jako audytor

**Kroki testowe:** 1. Przejdź do sekcji "Zarządzanie pytaniami"<br>2. Wybierz kategorię pytań<br>3. Dodaj nowe pytanie określając: treść, dostępne odpowiedzi, wagi ryzyka<br>4. Zapisz zmiany<br>5. Przejdź do rejestracji testowego narzędzia (jako rejestrator)<br>6. Sprawdź czy nowe pytanie pojawia się w kwestionariuszu

**Oczekiwany rezultat:** 
- Nowe pytanie zostaje dodane do bazy
- Pytanie pojawia się w kwestionariuszu dla odpowiedniej kategorii narzędzi

#### 6. Scenariusz: Test kalkulacji ryzyka
IDST-006

**Nazwa:** Weryfikacja kalkulacji poziomu ryzyka na podstawie odpowiedzi

**Wymagania:** 5

**Warunki wstępne:** Użytkownik jest zalogowany jako rejestrator i wypełnia kwestionariusz

**Kroki testowe:** 1. Rozpocznij rejestrację nowego narzędzia<br>2. Wypełnij dane podstawowe<br>3. W kwestionariuszu wybierz odpowiedzi wskazujące na wysokie ryzyko<br>4. Zapisz i wyślij wniosek<br>5. Rozpocznij rejestrację drugiego narzędzia<br>6. W kwestionariuszu wybierz odpowiedzi wskazujące na niskie ryzyko<br>7. Zapisz i wyślij wniosek

**Oczekiwany rezultat:** 
- Pierwszy wniosek otrzymuje ocenę "Wysokie ryzyko" 
- Drugi wniosek otrzymuje ocenę "Niskie ryzyko"

#### 7. Scenariusz: Test funkcji administratora
IDST-007

**Nazwa:** Weryfikacja funkcji zarządzania użytkownikami

**Wymagania:** 1

**Warunki wstępne:** Użytkownik jest zalogowany jako administrator

**Kroki testowe:** 1. Przejdź do sekcji "Zarządzanie użytkownikami"<br>2. Utwórz nowego użytkownika z rolą "Rejestrator"<br>3. Edytuj istniejącego użytkownika zmieniając jego rolę z "Rejestrator" na "Audytor"<br>4. Wyloguj się<br>5. Zaloguj się na nowo utworzone konto<br>6. Sprawdź dostępne funkcje<br>7. Wyloguj się<br>8. Zaloguj się na konto ze zmienioną rolą<br>9. Sprawdź dostępne funkcje

**Oczekiwany rezultat:** 
- Nowy użytkownik ma dostęp tylko do funkcji rejestratorów
- Użytkownik ze zmienioną rolą ma dostęp do funkcji audytorów

#### 8. Scenariusz: Test dynamicznej listy pytań
IDST-008

**Nazwa:** Weryfikacja dynamicznego dostosowania pytań do kategorii narzędzia

**Wymagania:** 8b

**Warunki wstępne:** W systemie są zdefiniowane różne kategorie narzędzi z dedykowanymi pytaniami

**Kroki testowe:** 1. Zaloguj się jako rejestrator<br>2. Rozpocznij rejestrację narzędzia kategorii "Aplikacja mobilna"<br>3. Przejdź do kwestionariusza i zapisz listę pytań<br>4. Wróć i zmień kategorię na "Narzędzie analityczne"<br>5. Przejdź ponownie do kwestionariusza

**Oczekiwany rezultat:**
- Lista pytań zmienia się odpowiednio do wybranej kategorii narzędzia

#### 9. Scenariusz: Test funkcjonalności eksportu danych
IDST-009

**Nazwa:** Weryfikacja eksportu danych do pliku PDF

**Wymagania:** 16 

**Warunki wstępne:** W systemie istnieje zweryfikowany wniosek

**Kroki testowe:** 1. Zaloguj się jako audytor<br>2. Przejdź do listy zweryfikowanych wniosków<br>3. Wybierz wniosek<br>4. Kliknij opcję "Eksportuj do PDF"<br>5. Otwórz wygenerowany plik

**Oczekiwany rezultat:** 
- System generuje plik PDF
- Plik zawiera wszystkie dane narzędzia oraz odpowiedzi z kwestionariusza
- Plik zawiera informację o oszacowanym ryzyku

#### 10. Scenariusz: Test powiadomień
IDST-010

**Nazwa:** Weryfikacja systemu powiadomień o zmianie statusu wniosku

**Wymagania:** 12

**Warunki wstępne:**Istnieje wniosek ze statusem "Do weryfikacji"

**Kroki testowe:** 1. Zaloguj się jako audytor<br>2. Przejdź do listy wniosków do weryfikacji<br>3. Otwórz wybrany wniosek<br>4. Zmień status na "Zatwierdzony"<br>5. Sprawdź, czy system wysłał powiadomienie do rejestratora

**Oczekiwany rezultat:** 
- Status wniosku zmienia się na "Zatwierdzony"
- System wysyła powiadomienie email do rejestratora o zmianie statusu
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

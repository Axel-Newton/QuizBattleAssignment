# 📝 **Översiktlig Changelog**

## **8/1**

*   Branch: `teaching/w2d1`
*   Skapade grundläggande **konsolversionen av QuizBattle**.
*   Lade till **seedade frågor**, visning av frågor och inmatning av svar.
*   Uppdaterade **Choice**– och **Question**–domänklasserna.
*   Införde enklare **domänvalidering** (t.ex. text, svarsalternativ, korrekt svar).
*   Lagt till **TODO-listor** med planerade steg för 8/1 och 9/1.
*   Började strukturera projektet inför framtida lager (Domain, Application, UI).

***

## **9/1**

*   Branch: `teaching/w2d2`
*   Fortsatt arbete med **konsolappen**.
*   Förfinade fråga– och svarshantering.
*   Kompletterade/uppdaterade **TODO 2026‑01‑09**.
*   Stabiliserade enklare flöde med 3 frågor i konsolen.
*   Förberedde inför kommande refaktorering (separation av lager).

***

## **W3D2 — 13/1**

*   Branch: `teaching/v3d2`
*   Påbörjade **stor arkitekturgenomgång** inför övergång från konsolapp till lagerbaserad backend.
*   Dokumenterade omfattande designidéer i `docs/TODO-2026-01-13.md` (som senare togs bort i refactoring).
*   Förberedde omstrukturering av Domain, Application, Infrastructure och API.

***

## **14/1**

- Branch: `Refactoring/Architecture`

Mycket stor förändring – här är en sammanfattning:

### 🔧 Arkitektur

*   Införde full **lagerstruktur**:
    *   **Domain** (ren affärslogik)
    *   **Application** (handlers, commands, responses, ports/interfaces)
    *   **Infrastructure** (in‑memory repositories)
    *   **Console** uppdaterat till att använda DI och presentatörer
    *   **API**–lager planerat (README införts men i denna diff tas en äldre version bort)

### 📦 Nya projekt

*   `QuizBattle.Infrastructure`
*   `QuizBattle.Application` (omstrukturerat kraftigt)
*   `QuizBattle.Domain` (utökad validering, nya invariants)

### 🧠 Domain–förstärkningar

*   Stora förbättringar i `Question`, `Answer`, `QuizSession`:
    *   Starka valideringsregler
    *   Stöd för kategori/svårighet
    *   Riktigt beteende: SubmitAnswer(), Finish(), invariants, tidsregler
    *   Förhindrar dubblettsvar
    *   Regler för tidsordning

### ⚙️ Application–lager

*   Infört **Handlers**:
    *   StartQuiz
    *   AnswerQuestion
    *   FinishQuiz
*   Infört **Interfaces**:
    *   IQuestionRepository
    *   ISessionRepository
    *   IQuestionService
    *   ISessionService
*   Infört **Services** som fasad över handlers.

### 🗄️ Infrastructure–lager

*   Nya in‑memory‑implementationer av repositories.

### 🖥️ Console–klienten

*   All UI‑kod flyttad till en **Presenter** (ConsoleQuestionPresenter).
*   Console använder nu **DI** och Application‑lagret.
*   Separat visning och inmatning (inga regler kvar i UI).

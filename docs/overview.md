# 🎓 **Översiktlig genomgång av QuizBattle‑arkitekturen**

Tänk på projektet som ett **hus**:

*   **Domain** = grunden och väggarna
*   **Application** = rummen (funktionerna/use cases)
*   **Infrastructure** = el/vatten (hur data lagras/hämtas)
*   **Console** = ytterdörren där användaren kommer in
*   **API (senare)** = en andra ytterdörr för webben

Varje del har ett tydligt ansvar. Nedan förklaras de på ett enkelt sätt.

***

# 🧠 1. Domain – *“Reglerna i spelet”*

**Domain-lagret** innehåller **all affärslogik** – alltså regler som alltid måste gälla.

Här finns t.ex.:

### 👉 **Question**

*   Har `Code`, `Text`, `Choices`, `CorrectAnswerCode`, `Category`, `Difficulty`.
*   Har metoder som:
    *   `IsCorrect(choiceCode)`
    *   `HasChoice(choiceCode)`
    *   Validering (t.ex. säkerställa att rätt svar finns bland valen).

### 👉 **Answer**

*   Ett faktiskt svar som studenten givit.
*   Innehåller tidpunkt, vilket val användaren tog, om det var korrekt etc.

### 👉 **QuizSession**

*   En hel spelomgång.
*   Har:
    *   `StartedAtUtc`, `FinishedAtUtc`
    *   Hur många frågor sessionen ska ha
    *   Lista av `Answers`
    *   Metoder som:
        *   `SubmitAnswer(question, selectedChoiceCode, whenUtc)`
        *   `Finish(whenUtc)`
*   Ser till att regler följs:
    *   Du kan inte svara på samma fråga två gånger.
    *   Du kan inte svara efter att sessionen är avslutad.
    *   Du kan inte ha fler svar än antalet frågor.

### 📌 Viktigt:

Domain **känner inte till** Console, API, databas, filer etc.  
Det gör domänen *ren och återanvändbar*.

***

# ⚙️ 2. Application – *“Hjärnan som styr spelet”*

Application-lagret beskriver **use cases** – alltså vad användaren kan *göra*:

*   Starta quiz
*   Svara på frågor
*   Avsluta quiz

För varje use case finns:

✔ **Command** – inputdata  
✔ **Handler** – logiken som körs  
✔ **Response** – resultatet som skickas tillbaka

### Exempel (StartQuiz):

*   **StartQuizCommand** – “Jag vill starta ett quiz, med X frågor.”
*   **StartQuizHandler** – hämtar slumpade frågor → skapar en QuizSession → sparar den
*   **StartQuizResponse** – returnerar sessionId + frågorna

### Interfaces (ports)

Application definierar *vad den behöver*, t.ex.:

*   `IQuestionRepository`
*   `ISessionRepository`
*   `IQuestionService`
*   `ISessionService`

Men **inte hur det är implementerat**.

***

# 🗄️ 3. Infrastructure – *“Så hämtas och lagras data”*

Här finns *implementationerna* av de interfaces Application behöver.

Just nu:

### ✔ InMemoryQuestionRepository

*   Innehåller seedade frågor.
*   Klarar filtrering (kategori, svårighet).
*   Slumpar frågor.

### ✔ InMemorySessionRepository

*   Lagrar QuizSession‑objekt i minnet under körning.

Senare kan man byta ut detta mot:

*   SQL
*   MongoDB
*   API‑anrop
*   Etc.

Men Application behöver inte ändras då – tack vare interfaces.

***

# 💻 4. Console – *“Användargränssnittet”*

Console‑projektet är **bara UI**.

Det:

*   Visar frågor
*   Läser användarens val
*   Kallar Application‑lagret (services)

DI (Dependency Injection) används för att koppla ihop allt:

```csharp
services
    .AddInfrastructureRepositories()
    .AddApplicationServices()
    .AddConsolePresentation();
```

### 👉 Presenter

Console använder en **ConsoleQuestionPresenter** för utskrift/inmatning.  
All UI‑kod ligger där.

Console ska **inte** ha logik – bara visa saker och kalla Application.

***

# 🔄 5. Flödet – *“Så rör sig datan genom systemet”*

Här är hela flödet i en enda kedja:

1.  Användaren startar programmet (Console).
2.  Console anropar:
        sessionService.StartAsync(questionCount)
3.  Application (StartQuizHandler) hämtar slumpfrågor från `IQuestionRepository`.
4.  Application skapar en QuizSession och sparar den via `ISessionRepository`.
5.  Console visar frågorna för spelaren.
6.  När användaren svarar:
        sessionService.AnswerAsync(sessionId, questionCode, selectedChoice)
7.  Application registrerar svaret i domänen (SubmitAnswer).
8.  Till slut:
        sessionService.FinishAsync(sessionId)
9.  Application avslutar sessionen och returnerar resultat.

***

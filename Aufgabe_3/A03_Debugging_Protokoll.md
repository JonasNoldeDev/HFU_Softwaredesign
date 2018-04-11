# Debugging - Protokoll

>Betrachtet die Source-Code-Dateien FamilyTree.cs und Program.cs. In Familytree wird eine Datenstruktur aufgebaut, die eine Art Familienstammbaum repräsentiert (Ähnlichkeiten mit realen Personen sind rein zufällig :-)).

>Macht euch klar, dass die Datenstruktur Person rekursiv ist, denn jedes Objekt vom Typ Person referenziert zwei weitere Objekte vom Typ Person, nämlich Mom und Dad.
Das stimmt! In der Klasse `Person` werden zwei statische Variablen `Mom` und `Dad` vom Typ `Person` deklariert.

>Die Methdode BuildTree() baut einen Beispiel-Baum auf. Setzt einen Breakpoint in Zeile 19 von Program.cs, startet den Debugger und seht Euch den Inhalt von root im Debugger an.
Die Variable `root` hat den Typen `Person`. Die Person heißt "Willi Cambridge". Neben dem Vor- und Nachmnamen (`FirstName`, `LastName`) sind auch die Variablen `Mom`, `Dad` und `DateOfBirth` definiert.

>Die Methode Find() durchläuft rekursiv den Baum und prüft alle Person-Objekte darauf, ob die Bedingung in Zeile 22 gegeben ist. Die erste Person, die die Bedingung erfüllt, wird zurückgeliefert.
Genau. Und zwar die erste Person, die nicht den Nachnamen "Battenberg" hat (`person.LastName != "Battenberg"`).

>Ändert die Bedingung so, dass nicht gleich die erste Person ("Willi") zurückgegeben wird. Eventuell gibt es Abstürze. Analysiert die Abstürze mit dem Debugger, überprüft Variableninhalte und den Call-Stack.
Ich habe im FamilyTree.cs die Zeile 22 geändert in: `if (person.LastName == "Battenberg")`. Beim Programmdurchlauf wird dann eine "System.NullReferenceException" geworfen. Durch das Debuggen mit einem Breakpoint in Zeile 22 wurde klar, dass die Methode Find() sich selbst rekursiv aufruft und zwar immer mit der Person `Mom` und `Dad` der Person die der Methode als Parameter übergeben wurde (`Find(person.Mom)`). Beim durchsteppen des Programms kommt irgendwann der Punkt, an dem die Person keine Person `Mom` definiert hat, wodurch die Methode in Zeile 22 dann auf ein Attribut einer Instanz mit dem Inhalt "null" zugegriffen wird (`person.LastName`). Dieser Aufruf führt zur Exception. Ändert man die Zeile 22 zu `if (person.LastName != "Cambridge")`, wird die Exception vermieden. Das liegt daran, dass die Find()-Methode nicht bis zu dem Punkt rekursiv aufgerufen wird an dem `Mom` und `Dad` den Wert "null" haben.

>Schreibt komplexere Bedingungen, findet z.B. die erste Person, die in einer Altersspanne liegt, vergleicht dazu person.DateOfBirth.Year mit DateTime.Now.Year. Analysiert mit dem Debugger, ob Eure Bedingung richtig ist.
Die Bedingung der Find()-Methode in Zeile 22 wurde in `if (DateTime.Now.Year - person.DateOfBirth.Year >= 100)` umgeschrieben. Die Bedingung habe ich mit dem Debugger geprüft.
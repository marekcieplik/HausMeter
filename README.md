Przedstawiam pomysł na program do cetyfikacji - proszę o opinię 🙂

Tytuł projektu: HausMeter - licznik domowy. 

W zmiennej enum są wpisane typy liczników: Gas, Power, WaterCold. 

Klasa MeterInFile ma property: MeterType, MeterAdress. 

Plik, w którym są pojedyncze odczyty z licznika, ma nazwę: Haus_{MeterAdress}_{MeterType}.txt. 

Statystyki zawierają minimalne i maksymalne odczyty oraz średnią - jak w kursie, tak samo jak delegaty:

Delegat odpala się, gdy wpisano nową wartość odczytu. 

Badana jest precyzja odczytu: max .3f. 

Czas wykonania planuję na dwa tygodnie.

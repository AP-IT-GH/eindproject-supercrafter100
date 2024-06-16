# The Fruity Collector

Yenthe Van den Eynden, Stijn Voeten, Tijn Wagemakers, Eline Van Zeir

## Inleiding

Het spel "The Fruity Collector" neemt spelers mee op een avontuurlijke reis door een virtuele boerderij, waar ze worden uitgedaagd om rondvliegend fruit te vangen terwijl ze moeten bewegen tussen vers en rot fruit. Met verschillende moeilijkheidsgraden, verschillende snelheden van fruitvliegen, en goede en slechte power-ups, is het de bedoeling om zo lang mogelijk te leven en zo veel mogelijk fruit te vangen.

De tutorial gaat stapsgewijs door de opbouw van het project lopen. We gaan samen een unieke VR-game creëren in Unity, waar je niet alleen fruit vangt, maar ook te maken krijgt met getrainde fruitvliegjes die je doorheen het spel gaan saboteren.

Deze tutorial leid je door het proces van het creëren van deze VR-game met behulp van Unity en Unity ML-Agents. Je leert:

- Hoe je een VR-omgeving in Unity opzet
- Hoe je fruit en power-ups in de virtuele boerderij plaatst
- Hoe je een VR-net voor de speler implementeert
- Hoe je fruitvliegjes als ML-agents traint om naar fruit te vliegen
- Hoe je power-ups integreert die de gameplay beïnvloeden
- Hoe je de game visueel aantrekkelijk maakt met graphics
  
Aan het einde van deze tutorial ben je in staat om je eigen VR fruitvangst spel te creëren, compleet met intelligente fruitvliegjes die je spelervaring dynamisch en uitdagend maken.

## Methoden

Het project is gebaseerd op de standaard VR Room van de unity [Create with VR](https://learn.unity.com/course/create-with-vr) course. Dit gebruikt de unity versie 2022.3.5f1

Om de fruitvliegen te trainen maken we gebruik van ML-agents, deze draait in een virtuele python omgeving beheerd door Anaconda. Hier heb je python 3.9.19 voor nodig. 

Voor inverse kinematics en karakter rigging maken we gebruik van Animation Rigging 1.2.1. Deze moet je gewoon installeren via de Unity package manager. Om de bewegingen op te nemen hebben we gebruik gemaakt van Recorder 4.0.3, ook deze kan je gewoon installeren bij de Unity package manager.

### Verloop van het spel
1. Je start het spel en je ziet een instellingen bord. Hier kan je de moeilijkheidsgraad aanpassen van het spel.
   - Difficulty bepaald de hoeveelheid fruitvliegen die rond gaan vliegen. 
   - Speed bepaald hoe snel de fruitjes worden afgevuurd. 
2. Eens je dat hebt gekozen, kan het spel beginnen. Je moet dan door het poortje lopen en dan kom je op het "spelveld" terecht.
3. Je ziet allemaal fruit vliegen. Je ziet gewoon fruit, "shiny" gewoon fruit, rot fruit en "shiny" rot fruit.
4. De bedoeling is dat je met het net, dat je in je rechterhand hebt, al het goed fruit gaat vangen.
    - Vang je gewoon goed fruit, dan krijg je een punt.
    - Vang je "shiny" goed fruit, dan krijg je een powerup. Deze zie je dan verschijnen op je linker pols.
    - Vang je gewoon rot fruit, dan verlies je een leven.
    - Vang je "shiny" rot fruit, dan heb je een slechte powerup. Deze wordt meteen ingeschakeld en kan een negatieve invloed hebben op je spel.
5. De linkerpols wordt gebruikt voor het zichtbaar maken van je aantal levens en de goede powerups.
    - Je ziet het aantal levens dat je nog hebt. Het spel eindigt als je geen levens meer over hebt.
    - Je ziet ook welke goede powerups je hebt gevangen. Deze kan je activeren met de linker controller trigger knop. Hier kunnen er maar max 3 powerups hebben staan, dus als je een vierde vangt verdwijnt de eerste.
6. Het spel eindigt wanneer je geen levens meer hebt. Dan wordt er een geluid afgespeeld en gaat de poort terug open.

### Objecten 
#### Statische objecten
- Voor de omgeving is er gebruik gemaakt van een [package](https://assetstore.unity.com/packages/3d/environments/fantasy/dreamscape-nature-meadows-stylized-open-world-environment-186894) van de Unity asset store. Hiermee kan je zelf de omgeving naar eigen zin opbouwen.
- In de omgeving zie je 2 houten borden. Op eentje kan je de instellingen aanpassen en op de andere kan je de score bekijken. 
- Het vangnet voor het fruit is gemaakt in blender. Deze heb je vast in je erchterhand. Op je linkerhand zie je een display waar je het aantal levens en powerups kan zien. 

#### Dynamische objecten
- Vliegend fruit: Hiervoor hebben we een fruit [package](https://assetstore.unity.com/packages/3d/props/fruits-lowpoly-pack-lite-273980) van de Unity asset store gebruikt . Dit fruit wordt uit een ton geschoten en als het de grond raakt dan verwijnt het fruit.
- Poort: Dit hek is hetzelfde hek dat we gebruiken bij de statische objecten. Hier is een animatie opgezet zodat deze open en toe kan gaan.
- Fruitvliegen: Hiervoor hebben we een dragonfly [package](https://assetstore.unity.com/packages/3d/characters/animals/insects/dragonfly-arrow-cute-series-215297) van de Unity asset store gebruikt. Hier hadden de vliegen al animaties.
- Introductie karakter: Voor de uitleg over het spel te geven hebben we gebruik gemaakt van een [human karakter](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/free-low-poly-human-rpg-character-219979). Deze is met behulp van inverse kinematics, recorder en de vr headset geanimeerd. 


## Trainingen
### Hoe training gedaan? 

### Resultaten

## Conclusie

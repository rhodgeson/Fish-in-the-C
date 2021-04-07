# Fish-in-the-C
Scripts for an interactive aquarium made with Unity.
All scripts above were either completely or mostly written by me.

Some detailed implementation information:

System for tracking current fish and tank decor
The script Collections.cs tracks the current fish in the tank as well as the current tank decor in use. This is currently working as a place to store data and does not yet interact directly with what is actively in the game scene.

Current Fish/Creatures - Hashtable
The fish are stored in a Hashtable that maps the name of a fish/creature species to a List structure consisting of DateTime objects. The length of the List<DateTime> tells how many creatures of that specific type are in the tank. The value within each DateTime of the list tells the last time fed for each of the fish. The idea is that this would be used to respawn the necessary fish every time the game is reopened, so it isn't necessary to connect each lastTimeFed to a specific game object.
The method addFish(string fishName) in the Collections.cs script adds a new DateTime value to the end of the list mapped to the given fishName. The DateTime value would be the current time, so it would act as if the fish was fed as it entered the tank to avoid any fish entering hungry.
The method removeFish(string fishName) in the Collections.cs script looks through the List mapped to the given fishName and removes the  entry in the list with the oldest DateTime, thus making there one less fish in the tank data storage.

Tank decor
There are private integer variables shortDec, tallDec, and plant. These are meant to track the current sprite visible for the short tank decor, the tall tank decor, and the plant tank decor, respectively. The methods changeShortDec(), changeTallDec(), and changePlant() are currently empty but will be implemented to shift between the different sprites for the different types of tank decor.

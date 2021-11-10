# Fish-in-the-C
Scripts for an interactive aquarium made with Unity.
All scripts above were either completely or mostly written by me.

Some detailed implementation information:

Current Fish/Creatures - Hashtable
The fish are stored in a Hashtable that maps the name of a fish/creature species to a List structure consisting of DateTime objects. The length of the List<DateTime> tells how many creatures of that specific type are in the tank. The value within each DateTime of the list tells the last time fed for each of the fish. The idea is that this would be used to respawn the necessary fish every time the game is reopened, so it isn't necessary to connect each lastTimeFed to a specific game object.
The method addFish(string fishName) in the Collections.cs script adds a new DateTime value to the end of the list mapped to the given fishName. The DateTime value would be the current time, so it would act as if the fish was fed as it entered the tank to avoid any fish entering hungry.
The method removeFish(string fishName) in the Collections.cs script looks through the List mapped to the given fishName and removes the  entry in the list with the oldest DateTime, thus making there one less fish in the tank data storage.

Hashtable JSON conversion
Wrote method refillTable() in FishSpawner.cs that accesses the hashtable in Collections.cs that gets input from the JSON when the game opens, converts its lists from JArrays filled with JValues to Lists filled with DateTimes, and replaces the hashtable in Collections.cs. This took a bit of debugging with Leslyanne to be able to find out that this was needed. We kept seeing casting errors and finally realized it was because the JSON wasn't directly converting the hashtable back correctly.
  
Check lastTimeFed on open
FishSpawner.cs has a method called spawnFish() that gets called as the game starts after refillTable() has been called. First, it iterates through all the lists in the hashtables and adds the length of each list to a variable totalFish which keeps track of the number of fish. Then, it iterates through all the lists again, and checks all the DateTimes within them. If any are within the past 24 hours, there are less than 6 totalFish, and no other fish have been added on this call of spawnFish(), a new random fish is added to the hashtable.
New fish spawning
Finally, it iterates through the same lists in the hashtable, now updated with any new fish that were added, and spawns each fish using Instantiate(). The fish being spawned are the ones that were present the last time the user opened the game plus any new ones that were added. If any fish have a lastTimeFed that was over 3 days prior, they are removed from the hashtable and their object is not spawned.
Additionally, if a starfish already exists in the Collections.cs hashtable, a new one will not be added. This is because we plan to have the starfish be stagnant on-screen, and we believe having only one at a time will make that appear smoother.


Tank decor
There are private integer variables shortDec, tallDec, and plant. These are meant to track the current sprite visible for the short tank decor, the tall tank decor, and the plant tank decor, respectively. The methods changeShortDec(), changeTallDec(), and changePlant() are currently empty but will be implemented to shift between the different sprites for the different types of tank decor.
  
Fish decor changing mechanic
The 3 decor objects in the tank are customizable. If the user presses the Edit button at the top left of the screen, they can click on the various decor objects and sift through the different items that are available.
Each decor object was originally a button on the scene canvas, where clicking it changed the image of the button, but that conflicted with the scene lighting and layering. Instead we decided to create invisible buttons that lay on top of the sprites that they are supposed to change.
When the Edit button is pressed, Buttons.cs toggles the button component for the three decor objects. When they are enabled, if the user clicks on any of the decor items with their mouse, the corresponding method is called in Buttons.cs, which then calls a method in Collections.cs (either changePlant(), changeShortDec(), or changeTallDec()) that looks a an array of sprites designated for the object and either switches the object's sprite to the next element in the sprite array or starts back at index 0. The current sprite index is stored in public variables at the top of Collections.cs so that they can be saved when the application closes. When the application starts, the decor items are automatically set to whatever the index variables are set to, which defaults to 0 on first game play.

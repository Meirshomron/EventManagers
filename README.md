# EventManagers

## Intro

Unity doesn't offer a built-in EventManger, it does thou offer **UnityEvent** - A generic argument persistent callback that can be saved with the Scene.
You can read more about them here:
https://docs.unity3d.com/ScriptReference/Events.UnityEvent_2.html

If you want to see why you should use one then check this out:
https://bf.wtf/event-driven-unity-one/

There isn't one perfect EventManger, it dependes on the type of events your game uses.
Here I'll go through the differernt EventMangers added in this project.

The main difference is the type of data that is passed in the event, if at all.

## EventManager - No parameters
Here's a link to an EventManager that passes no parameters with its events, this is perfect if you jst want to inform other componenets thatan action accured.
https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events#5cf5960fedbc2a281acd21fa


## EventManager_1 - stringified json parameters
This EventManager is capable of passing a string with its events. 
You can pass anything as a string as long as both sides (dispatcher and listener) know the structure of the parameters.
A very practicle use for this is by serializing a scriptable object to a json string and passing it that way, on the other end we'll deserialize it back to a scriptable object.
This also insures a contract - that both sides know the format of the data and no surprises.

The code of the EventManager_1 is under the "Scripts" folder. 

Example of uses:

The scriptable object of the data passed in the event:
<img  src="Images/BladeHitData.png" width="700" >

The Blade dispatches an event with the BladeHitData for any collision with another object:
<img  src="Images/Blade.png" width="700" >

The Bomb listens to the event of a hit by the blade, if it hit him then he reacts:
<img  src="Images/Bomb.png" width="700" >
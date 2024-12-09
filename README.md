Overview of the game
- The original idea of the game was a level based movement puzzle game, where you would have to avoid 3 ghosts-like creatures trailing your exact path whilst hitting a sequence of buttons to open the door to the next level, unfortunately due to time constraints the idea was reworked this: The idea of the game is a movement-based 3D snake type game, where the longer you go the more of the map you cover, and the more difficult it is to avoid your “tail”. The player controls a speedy and nimble yellow pill in first person as they jump around a large box with various platforms and obstacles trying to avoid their tail of followers from catching them. The goal of the game is simply to see how long you can survive, as well as pulling off some sick movement. The game becomes progressively harder as the “tail” grows in length with the players survival.

Link to a Video of the Gameplay
- [![Watch the video](https://img.youtube.com/vi/<5NoosoLlfk>/maxresdefault.jpg)](https://www.youtube.com/watch?v=-5NoosoLlfk)

The main parts of the game
-	Player – Pill, moved with WASD, Space for jumps, Shift for Sprinting, Mouse for Orientation and more.
-	Camera – the camera is placed onto the player itself, always orienting itself correctly.
-	Followers – the “tail” of followers created, spawned in a predetermined manner, mimicking the players every move and closing off more parts of the map the longer the game lasts.
-	Wallrunning – The player can run on walls for a short duration, increasing in speed and jumping distance to chain together fast lines.
-	Arena, expansive box with platforms varying in size and shape allowing for experimentation with the movement system.
-	
Game features
-	Followers spawn about every second
-	The followers create a “tail” that makes it harder to survive
-	Satisfying movement system including wallrunning rewarded with extra speed
-	Open platform heavy map for player freedom

Project Parts
Scripts:
  - FollowerScript – used to give the followers the logic needed to follow exact movement of the player character.
  - MultiFollowerScript – used to instantiate the followers based on the parameters given to it such as initial lag time, lag increments and number of followers.
- PlayerMovement – the basis of the player’s movement, including walking, sprinting and jumping.
- WallRunning – The basis of all the wall running interactions, ensuring the player sticks to the wall, and that they fall off after a short duration. Also provides more speed.
- CameraHolder – used to store the transform/position of the camera.
- PlayerCam – used to move the camera based on mouse input, as well as alter FOV and zTilt of the screen when WallRunning.
- MainMenu – a simple main menu script.
- GameOver – ensuring that the player nor the followers can move throughout various scripts.

Models & Prefabs:
- Ol’ reliable pill (Capsule Game Object Unity)
- Trails VFX – For red trails giving the visual effect of the followers being chained.
https://assetstore.unity.com/packages/vfx/trails-vfx-242572 
- DOTween (HOTween v2) – Easier FOV and zTilt.
https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676

Materials:
- Basic unity materials for Pill colours.

Scenes
- Main menu and Arena scene.
  
ProBuilder
- ProBuilder was heavily used for the Arena scene design, as well as testing.

Resources used
- FIRST PERSON MOVEMENT in 10 MINUTES - Unity Tutorial - https://youtu.be/f473C43s8nE?si=DcxMWcaxrGEX75OL
- ADVANCED WALL RUNNING – Unity Tutorial (Remastered) - https://www.youtube.com/watch?v=gNt9wBOrQO4 
- WALL JUMPING & CAMERA EFFECTS – Unity Tutorial - https://www.youtube.com/watch?v=WfW0k5qENxM 
- How to make an object track another object’s exact movement - https://gamedev.stackexchange.com/questions/171698/how-to-make-an-object-track-another-objects-exact-movements 
- 5 Minute MAIN MENU Unity Tutorial – 
https://www.youtube.com/watch?v=-GWjA6dixV4  

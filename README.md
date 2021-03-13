# Twin Stick Survival - Prototype

## How to play
### Overall idea
- The goal is to collect as much scores as possible by destroying hostile enemies.
- If player is collided 10 times (can be adjusted), the game ends.
- If player goes out of the field, the game ends.

### Controls & Misc
- Use mouse and WASD to move your character
- Enemies are spawned periodically over time
- An enemy has to be hit multiple times before its destroyed.
- It's not counted as kill if an enemy is pushed out of field. 
- If player is hit by enemy, the enemy pushes player forward.

## Time usage
In overall, the task took roughly 8 h to complete. 
- ~ 2 hours went to planning code desing and architecture, and sandboxing. Also, all gameplay objects (field, player, enemies) used in the game were built here.
- ~ 4 hours was used for implementation of the actual features.
- ~ 2 hour went to fixing bugs (there weren't many), renaming stuff, adding comments, doing some testing and tweaking inspector parameters.

### NOTE
- I used some types and components from my other projects (when applicable) such as *InputComponent* and *ObjectPool<T>*s (to some degree), camera utils etc.
- I didn't include writing this README to the used time.

## TODO
- There are plenty of ideas (and improvements) that I wanted to do. Since I ran out of time, I added these ideas as comments on the code.

## Unity version
Tested with - 2020.3.0f1

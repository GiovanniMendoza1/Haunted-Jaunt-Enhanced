# Haunted-Jaunt-Enhanced

This project extends Unity's John Lemon's Haunted Jaunt tutorial with new gameplay features for CS410 Game Programming. Built in Unity 6, it adds interactive elements to the stealth-based horror game where John Lemon avoids ghosts in a haunted mansion.

Features

1. Ghost Detection Cone (Dot Product)
Description: John detects ghosts within a 45° cone in front of him using a dot product to calculate the angle between his forward direction and the ghost’s position.

How to Trigger: Face a ghost (e.g., an "Enemy" object) within 45° of John’s forward direction. Check the Unity Console for messages like "Detected Enemy at angle X°".

2. Fading Door (Linear Interpolation)
Description: A door fades from opaque to transparent when John approaches, using Mathf.Lerp to smoothly interpolate the material’s alpha value, and fades back when he leaves.

How to Trigger: Walk into the trigger zone around the "FadingDoor" object to make it fade out; exit to see it fade back to opaque. The Fading door is the door to the bathroom in front of the player when you start the game.

3. Mystical Aura (Particle Effect)
Description: A swirling, ghostly particle effect surrounds John when he enters a haunted hotspot, creating a mystical aura that stops when he exits.

How to Trigger: Walk into the trigger zone of the "HauntedHotspot" object (e.g., a cursed area); the aura particles play while inside and stop when you leave. The zone is the same bathroom where the fading door is at.

4. Creepy Whisper (Sound Effect)
Description: A creepy whisper sound plays when John is within 4 units of a ghost, adding tension to close encounters.

How to Trigger: Approach any ghost (e.g., "Enemy") to within 4 units to hear the whisper; move away to stop it.

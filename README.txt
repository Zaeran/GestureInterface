Gesture Interface program
-------------------------


This program uses a webcam to detect a colour (currently blue, and unchangeable).
The movement of said colour is stored as an integer sequence, which is mapped to a 'Gesture Tree'. Upon entering a sequence, user-defined methods will be executed based on the Gesture matched to the sequence.

The program allows users to view, create and remove any Gestures they wish. All methods to be invoked by gestures must be defined in the 'Methods' class.
The 'initialize' gesture must be performed before any other gesture can be input. This is to help prevent the accidental entering of gestures.

Limitations:
-The current version can only work with one source of colour. All sources of colour are treated as one source. ie. Three blue balls will be treated as a single blue area.
-Currently, Blue is the only detectable colour. In future, the user will be able to select the colour they wish to track.
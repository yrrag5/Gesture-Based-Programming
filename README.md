# Gesture-Based-Programming

## Members
Tim Cassidy, Hugh Brady, Garry Cummins

## Overview

This project was created using the unity engine to create a 3d sandbox environment for the myo armband to interact with using its predefined gestures(Wave Left, Wave Right, Double Tap, Fist, and Fingers Spread. For more information visit: 
https://support.getmyo.com/hc/en-us/articles/202647853-What-gestures-does-the-Myo-armband-recognize-

With the use of these gestures, the user can create and instantiate objects in the scene and can drag, stack, and delete them. 

## Unity Version Used

2018.2.8f1(64-bit)

## How to run

The excutable is provided in the repository. First clone the repository in your command line directory:

git clone https://github.com/yrrag5/Gesture-Based-Programming 

Insure unity version 2018.2.8f1 is installed on your machine. Open unity and select the GestureBasedUI folder in the clone location you specified and click play once loaded.

## How it works 

The project is split into 3 different modes(Menu, Game, Select) each with its own unique script file related to its functionality.

### Menu Mode

Menu mode is the UI of the project. The user can select from the options: Continue, Load, Save and Exit. The user can also create
cuboid, cube or cylinder object to be created in the game mode. 


#### Menu Mode Gestures

•	Wave Left, Right – Used for navigating in the menu. The user will first be highlighted on the continue button. Wave right will move the user through the button array to highlight the load button and continue moving through the buttons. Wave left will go the opposite direction and will highlight the cylinder button.

•	Fingers Spread – Will bring up a message asking the user to repeat the gesture if they wish to exit application. Haptic feedback is given to the user to denote this.

•	Double Tap - Used to click button to perform its function. Haptic feedback is given to the user to denote this.

#### Menu Mode Alternative I/O Device

The menu mode can also be traversed using the mouse, this gives the user the element of choice.

### Game Mode

Main view of the project. Bridges the menu and select modes. The user can higlight a object in this mode to enter select mode. All the current and new objects in the scene are stored in the scene state.

#### Game Mode Gestures

•	Wave Left - Parses left through the array, highlighting the selecting object.

•	Wave Right - Parses right through the array, highlighting the selecting object.

•	Fingers Spread - Exits to Menu Mode. Haptic feedback is given to the user to denote this.

•	Double Tap - Enters Select Mode, passing the highlighted GameObject. Haptic feedback is given to the user to denote this.


### Select Mode 

Can be accessed from the menu and game modes. Accesses object to perform multiple actions such as moving the objects and deleting them. 

#### Select Mode Gestures

•	Wave Left, Right - Used to change the axis of movement (X, Y, Z) of the object. It was mainly used because parsing through the axes was similar to the parsing of the game objects and buttons from the Game and Menu modes.

•	Fingers Spread - Used to exit Select mode and enter the Game mode. User will need to repeat the action in order to confirm. Haptic feedback is given to the user to denote this.

•	Double Tap – Used to delete objects from the scene state array of game objects. Repeat gesture to confirm. When the object is deleted, the Game mode will then be entered and displayed. Haptic feedback is given to the user to denote this.

•	Close Fist - Used to move the object on the selected axis. While held the user can change the pivot angle to move. Increase angle to increase axis value. Decrease angle to decrease axis value. This gesture was used because it is similar to holding an object.

### Camera Control

In both Select Mode and Create Mode the user can move the mouse to orbit the selected/highlighted object. Because the Myo will be attached to one arm, this allows the user to use their other arm to move the camera, thus not interfering with gesture capture.

### Saving and Loading

#### Saving 

Saving is done by first getting the name of all the save files in the Application data path. If they are none the new save will be called "save1.csv", if there are saves the highest numbered save is stored and incremented. For example, if the highest numbered save file is "save26.csv", the next save will be named "save27.csv".

#### Loading

Loading is done through the load sub-menu, the user selects a file from the dropdown, using the mouse, and then presses the load button. This then parses the selected save file and instantiates objects in the current scene using their stored name and x, y and z position. 

### Project Demonstration

https://www.youtube.com/watch?v=eK1AQSL-siE

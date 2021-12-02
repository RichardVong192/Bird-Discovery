# Bird-Discovery

## How To Play
To begin discovering birds once the app is installed, look at 1 - 4 of the 10 unique image targets through the app and the corresponding birds will appear. Tapping once on a bird will bring up facts on the bird, while double tapping will bring up a quiz question with 4 answer options. Incorrectly answering a question will disable that answer, and allow the player to continue attempting to find the right answer to the quiz. The answers to all quiz questions can be found within the bird’s facts section.


## Installation & Running
### PC
- Download the project folder
- In Unity Hub go to “Installs” and download Unity version 2019.4.29f1, making sure to select iOS and/or Android build support if you intend to run the app on a mobile device
- Select the “Open” or “Add” button (beside the New Project button), and import the COSC477 Bird Project folder
- Open the project
- Press the play button to begin running the app

### iOS
- These instructions assume the steps 1 – 4 in the PC section have been followed.
- Navigate to “Build Settings” in the file menu
- U nder the “Platform” option, select “iOS”
- Then, select “Build” in which you will be prompted to create a new folder which will contain the appropriate files required to run the project on an iOS device
- Then, open Xcode and open the folder that was just created
- In Xcode, select “Unity-iPhone” in the top left corner
- Select the “Signing & Capabilities” tab, and tick the checkbox for “Automatically manage signing”
- Select a Team (a personal account will suffice)
- Change the bundle identifier to “com.birdproject”
- The application may now be run on your iPhone

### Android
- These instructions assume the steps 1 – 4 in the PC section have been followed.
- Navigate to “Build Settings” in the file menu
- Under the “Platform” option, select “Android”
- Then, with the Android device connected and selected as the “Run Device”, select “Build and Run”
- Specify where the APK file should be saved
- The app should now be installed on your Android device, and capable of being run when disconnected from the PC.

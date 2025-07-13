
# Augmented Reality Restaurant Menu Application

This project showcases an AR-powered Android application designed to modernize the dining experience by allowing users to interactively explore restaurant menus in real time using augmented reality.

## Features

- AR Dish Recognition  
  Scan printed menu images to detect dishes using Vuforia SDK.

- 3D Model Visualization  
  Display realistic 3D models of each dish anchored in the real world.

- Interactive Controls  
  Rotate and scale 3D models intuitively via touch gestures.

- Dynamic Information Panel  
  View detailed ingredients, nutritional values (with Nutri-Score), and star ratings.

- Multi-language Support  
  Switch between 17 languages with dynamic text and font updates (including Arabic and Chinese).

- User-Friendly Interface  
  Smooth transitions, translucent glassmorphism UI, and horizontal scrolling ingredient lists.

- High Usability  
  Validated through a user study achieving an average System Usability Scale (SUS) score of 79.5.

## Technologies Used

- Unity 3D  
- C#  
- Vuforia SDK  
- TextMesh Pro  
- Unity Localization Package  
- Universal Blur (Glassmorphism)  
- Google Fonts Integration  
- JSON and CSV Data Handling  

## Setup and Installation

Follow the steps below to install and run the project in Unity:

1. Download the Project  
   Clone or download the AR Restaurant Menu project folder.

2. Open in Unity  
   - Launch Unity Hub.  
   - Make sure your Unity version is 6000.1.0f1 or higher.  
   - Open the downloaded project folder through Unity Hub.

3. Install Dependencies  
   - Unity will automatically import required libraries and packages.  
   - Wait for the editor to fully load and resolve dependencies (this may take several minutes depending on your system).

4. Load the Main Scene  
   - Navigate to Assets/Scenes/MainScene.  
   - Drag MainScene into the Hierarchy panel.

5. Scene Preparation  
   - Ensure Button_Canvas and MainMenu_Canvas are initially inactive before hitting Play.  
   - Allow scripts to compile completely.

6. Run the Application in Editor  
   - Click Play to test the AR workflow within Unity Editor (limited interaction as our main menu will be floating).

## Building the APK for Android

1. Go to File → Build Settings.
2. Switch the platform to Android (iOS requires Xcode and is not supported here).
3. Create a new build profile if needed.
4. Click Build.
5. Confirm any additional popups that appear — click Yes to proceed.
6. Once complete, install the APK on your Android phone for testing.

If the build fails or encounters issues, you can use the pre-built APK included in the project files (used during the demo presentation).

Use the printed menu marker provided in the folder to interact with the app.

## User Workflow

1. Scan Menu  
   Launch the app and point the camera at a printed menu image to detect dishes.

2. Select Dish  
   Tap on the detected icon to place the 3D model in the scene.

3. Interact  
   Rotate and scale the 3D model using intuitive touch gestures.

4. Explore Info  
   Open the main menu to view ingredients, nutritional info, and ratings.  
   Change languages using the translation dropdown.

5. Switch Dishes  
   Close the current menu and scan a new dish to continue.

## Evaluation

A usability study was conducted with 20 participants:

- Average SUS Score: 79.5  
- Key Feedback Highlights:  
  - The interface was highly intuitive  
  - Multilingual support was appreciated  
  - 3D interactions were engaging and informative

## Authors

- Yamandeep Soni  
- Mohit Raghavbhai Bagadiya  
- Ursula Krause

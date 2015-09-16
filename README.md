# MyoUnityAndroidPlugin
Unofficial plugin which enables you to build Unity applications for Android with Myo support.
## Directory structure and Setup
You need to add both folders to your Unity projects Assets folder.
I tested the plugin with Unity 5.1.2 and a Galaxy Note 4. Although I used Myo Android SDK 0.10.0 and Myo software version 1.0.0 to build the project. The "AndroidPlugin" itself is built as an "Android Bound Service" so it is mentioned in the Manifest file but not as the main-application. This should enable you to use a second plugin more easily.

```
\Assets
    +---MyoPlugin
    |   +---Demo
    |   |   +---Scenes
    |   |   |       MyoDemoScene.unity
    |   |   \---Scripts
    |   |           MyoPluginDemo.cs
    |   +---Prefabs
    |   |       MyoManager.prefab
    |   \---Scripts
    |           MyoBinding.cs
    |           MyoManager.cs
    \---Plugins
        \---Android
            |   AndroidManifest.xml
            |   AndroidPlugin.jar
            \---libs
                \---armeabi-v7a
                        libgesture-classifier.so
```

## Getting Started

1. Import MyoPlugin and Plugins folder into your Unity project.
2. Open "MyoPlugin/Demo/Scenes/MyoDemoScene.unity".
3. In your Build Settings switch your platform to Android.
4. In Player Settings you have to set your Bundle Identifier
5. Also set minimum API Level 18 (Android 4.3 'Jelly Bean'). Everything else in Player Settings should be optional.
6. Now you should be able to build and deploy to your Android device.

## Using the Demo

1. First you must pair your Myo device. Press the "AttachToAdjacent" button then 'bump' Myo gently to your device. Note: Currently no other attach method is implemented, see "Known Issues".
2. You should immediately gain control of the cube's rotation. Poses and Orientation will be displayed as well. You can use Myo vibration, uninitialize the plugin and initialize it again using the buttons.

## The API

When you build your own scene, you simply need to have a GameObject thats called "MyoManager" with the MyoManger script attached to it. For an out of the box solution, there is also a prefab included which you can drag into your scene. You now can start the plugin via "MyoManager.Initialize", the other methods are called in the same manner. The code below represents the methods which can be called using the MyoManger.

```C#

/// Manager class for Myo Android Plugin. Use only this public API to interface with Myo inside of Unity. 
public class MyoManager : MonoBehaviour 
{
    /// Subscribe to this event to recieve Pose event notifications
    public static event Action<MyoPose> PoseEvent;
    
    /// More standard Myo events you can subscribe to
    public static event Action AttachEvent, DetachEvent, ConnectEvent, DisconnectEvent,
								ArmSyncEvent, ArmUnsyncEvent, LockEvent, UnlockEvent;

    /// Initializes and enables Myo plugin
    public static void Initialize();

    /// Uninitialize and disables Myo plugin
    public static void Uninitialize();

    /// Automatically pairs with the first Myo device that touches the iOS device. 
    public static void AttachToAdjacent();

    /// Gets the rotation of the Myo device, converted into Unity's coordinate system (See MyoToUnity).
    public static Quaternion GetQuaternion()

    /// Vibrates the Myo device for the specified length, Short, Medium, or Long.
    public static void VibrateForLength( MyoVibrateLength length );
    
    /// Tells you if the plugin is already initialized
    public static bool GetIsInitialized(){
    
    /// Tells you if a Myo is already attached to the device
  	public static bool GetIsAttached()
}

```

## Known Issues

- Only 'attachToAdjacent' method for connecting the Myo is working right now. You have to 'bump' your Myo gently against your Android device. The usual option to choose a myo from a screen isn't implemented yet.
- Currently you can only use one Myo per device. If I can get my hands on a second one i will implement this functionality too.

## Support

This project was kind of rushed, so if you have any feedback just contact me (Florian.Strieg@Student.Reutlingen-University.DE). Although i cannot guarantee any direct support I'm looking forward to develop this plugin a bit further.

## Thanks

Last but not least I would like you to know that I took inspiration and parts of code from  https://github.com/zoiclabs/Myo-Unity-iOS-Plugin. I liked the architecture so hopefully the creator will be okay with it =)

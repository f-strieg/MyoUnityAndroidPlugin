using UnityEngine;
using System.Collections;
using System;
using MyoUnity;

public class MyoManager : MonoBehaviour {
	public static MyoManager instance;
	private static AndroidJavaObject jo;

	private static bool isInitialized = false;
	private static bool isAttached = false;

	public static event Action<MyoPose> PoseEvent;
	public static event Action AttachEvent, DetachEvent, ConnectEvent, DisconnectEvent,
								ArmSyncEvent, ArmUnsyncEvent, LockEvent, UnlockEvent;

	
	private static Quaternion quaternion;

	void Awake(){
		//Singelton from UnityPatterns.com (non existing anymore)
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			if(this != instance)
				Destroy(this.gameObject);
		}
	}

	#region PluginCalls
	public static void Initialize()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if(!isInitialized){
			jo = new AndroidJavaObject ("com.strieg.myoplugin.MyoStarter");
			using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity")) {
					jo.Call ("launchService", ajo);
				}
			}
			if (jo != null)
				isInitialized = true;
		}
		#endif
	}

	public static void Uninitialize()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (isInitialized) {
			using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity")) {
					jo.Call ("stopService", ajo);
				}
			}
			jo = null;
			isInitialized = false;
			isAttached = false;
		}
		#endif
	}

	public static void AttachToAdjacent(){
		#if UNITY_ANDROID && !UNITY_EDITOR
		if(isInitialized) jo.Call ("attachToAdjacent");
		#endif
	}

	//Vibrates the Myo device for the specified length, Short, Medium, or Long.
	public static void VibrateForLength( MyoVibrateLength length ){
		#if UNITY_ANDROID && !UNITY_EDITOR
		if(isInitialized) jo.Call ("vibrateForLength", (int)length);
		#endif
	}
	#endregion

	#region DelegateCalls
	//this are the methods which are called from java
	public void OnAttach(string message){
		if (AttachEvent != null)
			AttachEvent();
		isAttached = true;
	}

	public void OnDetach(string message){
		if (DetachEvent != null)
			DetachEvent();
		isAttached = false;
	}

	public void OnConnect(string message){
		if (ConnectEvent != null)
			ConnectEvent();
		isAttached = true;
	}

	public void OnDisconnect(string message){
		if (DisconnectEvent != null)
			DisconnectEvent();
		isAttached = false;
	}

	public void OnArmSync(string message){
		if (ArmSyncEvent != null)
			ArmSyncEvent();
	}
	
	public void OnArmUnsync(string message){
		if (ArmUnsyncEvent != null)
			ArmUnsyncEvent();
	}

	public void OnUnlock(string message){
		if (UnlockEvent != null)
			UnlockEvent();
	}
	
	public void OnLock(string message){
		if (LockEvent != null)
			LockEvent();
	}
	
	public void OnPose(string message)
	{
		MyoPose pose = (MyoPose) Enum.Parse(typeof(MyoPose), message, true);
		if (PoseEvent != null) PoseEvent( pose );
	}

	public void OnOrientationData(string message)
	{
		string[] tokens = message.Split(',');
		float x=0, y=0, z=0, w=0;
		float.TryParse( tokens[0], out x );
		float.TryParse( tokens[1], out y );
		float.TryParse( tokens[2], out z );
		float.TryParse( tokens[3], out w );
	
		quaternion = new Quaternion( y, z, -x, -w );
	}
	#endregion

	#region Public Getter
	public static Quaternion GetQuaternion(){
		return quaternion;
	}

	public static bool GetIsInitialized(){
		return isInitialized;
	}

	public static bool GetIsAttached(){
		return isAttached;
	}
	#endregion
}


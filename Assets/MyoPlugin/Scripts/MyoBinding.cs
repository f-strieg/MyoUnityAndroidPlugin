using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace MyoUnity
{
	public enum MyoPose
	{
		UNKNOWN = -1,
		REST = 0,
		FIST = 1,
		WAVE_IN = 2,
		WAVE_OUT = 3,
		FINGERS_SPREAD = 4,
		DOUBLE_TAB = 5
	};

	public enum MyoVibrateLength
	{
		SHORT,
		MEDIUM,
		LONG
	};
}
﻿// Author: Nick Klingensmith - @koujaku (Twitter)
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
#if UNITY_EDITOR_WIN
using System.Runtime.InteropServices;
#endif

public static class EditorGyro {
	#if UNITY_EDITOR_WIN
	[DllImport("GyroDLL", EntryPoint = "GyroInitialize")]
	private static extern int Initialize();
	[DllImport("GyroDLL", EntryPoint = "GyroIsInitialized")]
	private static extern int IsInitialized();
	[DllImport("GyroDLL", EntryPoint = "GyroGetRotation")]
	private static extern Quaternion GetRawRotation();

	static bool _isInitialized = false;
	/// <summary>
	/// Gets the current orientation of the hardware from the Gyroscope! This is equivalent to
	/// Unity's Input.gyro.attitude, and will in fact call Input.gyro.attitude when built to
	/// your platform of preference.
	/// </summary>
	public static Quaternion GetRotation() {
		if (!_isInitialized) _isInitialized = Initialize() >= 0;
		if (!_isInitialized) return Quaternion.identity;
		return Quaternion.AngleAxis(90, Vector3.right) * GetRawRotation();
	}
	#else
	/// <summary>
	/// Calls straight to Input.gyro.attitude on Mac, or on device.
	/// </summary>
	public static Quaternion GetRotation() {
		return Input.gyro.attitude;
	}
	#endif
}

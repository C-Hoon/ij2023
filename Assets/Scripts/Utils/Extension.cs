using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameCore.UI;

public static class Extension
{
	public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
	{
		return Util.GetOrAddComponent<T>(go);
	}

	public static bool IsValid(this GameObject go)
	{
		return go != null && go.activeSelf;
	}
	public static string GetString(this Enum value)
	{
		return value.ToString();
	}
}


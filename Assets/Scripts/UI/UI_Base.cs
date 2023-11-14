using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameCore.UI
{
	public abstract class UI_Base : MonoBehaviour
	{
		//protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

		public abstract void Init();

		private void Start()
		{
			Init();
		}

		GameObject getElement(Define.Scene_Game type)
		{
			GameObject go = new GameObject();
			return go;
		}
	}
}
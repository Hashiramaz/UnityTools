using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUtility : MonoBehaviour {

   public static string GetFormattedTime(float t)
	{
        t += 1;
		t = Mathf.Max(0, t);
		int hours = ((int)t) / 3600;
		int minutes = (((int)t) - (hours * 3600)) / 60;
		int seconds = ((int)t) % 60;
		string s = "";
		s += (hours > 0) ? hours.ToString() + ":" : "";
		s += ((minutes > 0) ? (minutes < 10) ? "0" + minutes + ":" : minutes + ":" : "");
		s += (seconds < 10) ? "0" + seconds : seconds.ToString();
		return s;

	}
}

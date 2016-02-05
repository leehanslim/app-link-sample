using UnityEngine;
using System.Collections;

public class SDK 
{
	private static readonly ISocialHelper socialHelper;

	static SDK()
	{
		socialHelper = new FBHelper();
	}

	public static ISocialHelper Social
	{
		get
		{
			return socialHelper;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System;

public class FBHelper : ISocialHelper
{
	private IEnumerable<string> readPermissions;
	private IEnumerable<string> publishPermissions;

	public FBHelper()
	{
		readPermissions = new List<string>();
		publishPermissions = new List<string>();

		FB.Init( () => {
			Debug.Log ("Facebook initialize complete.");
		});
	}

	#region ISocialHelper implementation
	public bool IsLoggedIn 
	{ 
		get 
		{ 
			return FB.IsLoggedIn; 
		} 
	}

	public void Login (Action<bool> onLoginComplete = null)
	{
		FB.LogInWithReadPermissions(readPermissions, (result) => {

			if (string.IsNullOrEmpty(result.Error))
			{
				Debug.LogWarning("Facebook login successful: " + result.RawResult);
			}
			else
			{
				Debug.LogWarning("Something went wrong logging into Facebook: " + result.Error);
			}

			if (onLoginComplete != null) 
				onLoginComplete(string.IsNullOrEmpty(result.Error));

		});
	}

	public void Share (string url, string contentTitle, string contentDescription, Action<bool> onComplete)
	{
		if (IsLoggedIn)
		{
			FB.LogInWithPublishPermissions(publishPermissions, (result) => {

				if (string.IsNullOrEmpty(result.Error))
				{
					// app is authorized to publish, so we proceed
					this.APICall(new Uri(url), contentTitle, contentDescription, onComplete);
				}
			});
		}
	}
	#endregion


	#region Helpers
	private void APICall(Uri uri, string contentTitle, string contentDescription, Action<bool> onComplete)
	{
		FB.ShareLink(uri, contentTitle, contentDescription, null, (shareResult) => {

			bool success = true;
			if (shareResult.Cancelled || !string.IsNullOrEmpty(shareResult.Error)) 
		    {
				Debug.Log("ShareLink Error: " + shareResult.Error);
				success = false;
		    } 
			else if (!string.IsNullOrEmpty(shareResult.PostId)) 
		    {
		        // Print post identifier of the shared content
				Debug.Log(shareResult.PostId);
		    } 
		    else 
		    {
		        // Share succeeded without postID
		        Debug.Log("ShareLink success!");
		    }

			if (onComplete != null)
			{
				onComplete(success);
			}
		});
	}
	#endregion // Helpers
}

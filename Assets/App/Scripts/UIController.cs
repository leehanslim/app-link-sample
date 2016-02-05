using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class UIController : MonoBehaviour {

	[SerializeField]
	private GameObject fbShareButton;

	[SerializeField]
	private GameObject fbLoginButton;

	private ISocialHelper social;

	void Awake()
	{
		social = SDK.Social;
	}

	#region UI event handlers
	public void HandleOnFacebookButtonClicked()
	{
		social.Login(HandleOnFacebookLoggedIn);
	}

	public void HandleOnFBShareButtonClicked()
	{
		social.Share("https://developers.facebook.com/", "Awesome Title", "Awesome Description", ShareCallback);
	}
	#endregion // UI event handlers


	#region Facebook event handlers
	private void HandleOnFacebookLoggedIn(bool success)
	{
		if (success)
		{
			fbLoginButton.SetActive(false);
			fbShareButton.SetActive(true);
		}
	}

	private void ShareCallback (bool success) 
	{
	    Debug.Log("Share call back. Success? " + success);
	}
	#endregion // Facebook event handlers
}

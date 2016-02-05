using System.Collections;
using System.Collections.Generic;

public interface ISocialHelper 
{
	bool IsLoggedIn { get; }

	void Login(System.Action<bool> onLoginComplete = null);

	void Share(string uri, string contentTitle, string contentDescription, System.Action<bool> onComplete); 
}

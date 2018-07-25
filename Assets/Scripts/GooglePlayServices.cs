using Firebase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayServices : MonoBehaviour {


    /// <summary>
    /// Check to see if the Google play services are up to date
    /// This is required before the SDK can be used
    /// </summary>
	void Start() {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                Debug.Log("Firebase is ready to use");
            } else {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }
}

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour {
    const string databaseUrl = "<Database Url>";

    private void Start() {
        FirebaseApp app = FirebaseApp.DefaultInstance;

        app.SetEditorDatabaseUrl(databaseUrl);

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        FirebaseDatabase.DefaultInstance.GetReference("Leaderboard").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.LogError("Was unable get leaderboard");
            } else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;

                Debug.LogError("Completed");
            }
        });

    }
}

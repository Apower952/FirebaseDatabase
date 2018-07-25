using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Auth : MonoBehaviour {

    public GameObject LoginPanel;
    public GameObject WelcomePanel;
    public GameObject LeaderboardPanel;

    public Text Status;
    public Text InputError;
    public Text LoggedUser;
    public InputField Email;
    public InputField Password;

    const string Format = "Firebase user created successfully: {0} ({1})";
    FirebaseAuth auth;
    FirebaseUser user;

    bool loadScene = false;

    void Start() {
        LoginPanel.SetActive(true);
        WelcomePanel.SetActive(false);
        LeaderboardPanel.SetActive(false);
    }

    void Awake() {
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
    }

    void Update() {
        if (loadScene) {
            loadScene = false;
            LoadNewPanels();
        }
    }


    public void UserLogin() {
        if (CheckValidation() != false) {
            FirebaseLogin();
        }
    }

    protected void FirebaseLogin() {
        auth.SignInWithEmailAndPasswordAsync(Email.text, Password.text).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("The sign-in process was canceled");
                return;
            }

            if (task.IsFaulted) {
                Debug.LogError("The sign-in process encountered a problem: " + task.Exception);
                return;
            }

            if (task.IsCompleted) {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User was signed in successfully: [0] ({1})", newUser.DisplayName, newUser.UserId);

                user = auth.CurrentUser;

                if (user != null) {
                    loadScene = true;
                }
            }


        });
    }


    protected bool CheckValidation() {
        if (!String.IsNullOrEmpty(Email.text)) {
            return true;
        } else {
            InputError.text = "Your email and password do match what we have on record";
        }

        if (!String.IsNullOrEmpty(Password.text)) {
            return true;
        } else {
            InputError.text = "Your email and password do match what we have on record";
        }
        return false;
    }

    void LoadNewPanels() {
        LoginPanel.SetActive(false);

        WelcomePanel.SetActive(true);
        LeaderboardPanel.SetActive(true);

        LoggedUser.text = user.Email;
    }
}

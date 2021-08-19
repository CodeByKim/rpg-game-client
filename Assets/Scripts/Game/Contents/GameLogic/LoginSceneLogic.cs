using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneLogic : GameLogic
{
    public void RequestConnect()
    {
        NetworkService.Instance.Connect();
    }

    public void StartGame()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        SceneManager.LoadScene("Game");
    }

    public override string GetName()
    {
        return "LoginSceneLogic";
    }
    
    private void OnSceneChanged(Scene prevScene, Scene changedScene)
    {
        if (changedScene.name == "Game")
        {
            Protocol.SEND_CREATE_MY_PLAYER();
        }
    }

    public override void OnInitialzie()
    {
    }
}

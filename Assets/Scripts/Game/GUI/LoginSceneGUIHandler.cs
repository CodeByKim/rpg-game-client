using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneGUIHandler : MonoBehaviour
{
    private LoginSceneLogic mLogic;

    private void Start()
    {
        mLogic = GameFramework.GetGameLogic<LoginSceneLogic>();
    }

    public void OnConnectButtonClick()
    {        
        mLogic.RequestConnect();
    }
}

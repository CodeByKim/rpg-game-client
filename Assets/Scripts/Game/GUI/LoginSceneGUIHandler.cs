using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneGUIHandler : MonoBehaviour
{    
    public void OnConnectButtonClick()
    {
        NetworkService.Instance.Connect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneGUIHandler : MonoBehaviour
{
    private RPGGameLogic mLogic;

    private void Start()
    {
        mLogic = GameFramework.GetGameLogic<RPGGameLogic>();
    }
}
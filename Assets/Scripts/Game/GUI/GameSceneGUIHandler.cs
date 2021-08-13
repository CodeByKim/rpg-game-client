using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneGUIHandler : MonoBehaviour
{
    public void OnTeleportButtonClick()
    {
        /*
         * 1. 먼저 서버에 보내놓고
         * 2. 나 한방에 텔레포트
         */

        RPGGameLogic.Instance.Teleport(5, 5);
    }
}

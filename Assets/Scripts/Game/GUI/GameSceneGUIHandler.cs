using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneGUIHandler : MonoBehaviour
{
    public void OnTeleportButtonClick()
    {
        /*
         * 1. ���� ������ ��������
         * 2. �� �ѹ濡 �ڷ���Ʈ
         */

        RPGGameLogic.Instance.Teleport(5, 5);
    }
}

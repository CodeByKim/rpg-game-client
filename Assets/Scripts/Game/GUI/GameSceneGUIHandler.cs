using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneGUIHandler : MonoBehaviour
{
    private RPGGameLogic mLogic;
    private PrefabController mController;

    private Dictionary<int, GameObject> mPlayerNameTexts;
    private List<int> mRemovedPlayerID;

    private void Start()
    {
        mLogic = GameFramework.GetGameLogic<RPGGameLogic>();
        mController = GameFramework.GetController<PrefabController>();

        mPlayerNameTexts = new Dictionary<int, GameObject>();
        mRemovedPlayerID = new List<int>();
    }

    private void LateUpdate()
    {
        Dictionary<int, Player> players = mLogic.GetCurrentPlayers();

        foreach(var item in players)
        {
            Player player = item.Value;

            //���̵� ��������
            int playerID = player.GetID();
           // string playerID = MakePlayerID(player.GetID());

            //�� ���̵� ���� ȭ�鿡 �����Ǿ����� Ȯ���Ѵ�.                        
            if(mPlayerNameTexts.ContainsKey(playerID))
            {
                //�����Ǿ� �ִٸ� ��ǥ ����
                Transform textUI = mPlayerNameTexts[playerID].transform;
                Vector3 pos = Camera.main.WorldToScreenPoint(player.transform.position);
                textUI.position = new Vector2(pos.x, pos.y + 100);
            }
            else
            {
                //���ٸ� ���� ���Ϳ� ���� �÷��̾��̴� �������� �����ϰ� ������Ų��.
                Transform textUI = mController.Create("PlayerNameUI").transform;
                textUI.SetParent(transform);
                string newPlayerID = MakePlayerID(player.GetID());
                textUI.GetComponent<Text>().text = newPlayerID;

                Vector3 pos = Camera.main.WorldToScreenPoint(player.transform.position);
                textUI.position = new Vector2(pos.x, pos.y + 100);

                mPlayerNameTexts.Add(playerID, textUI.gameObject);
            }            
        }

        /*
         * �̹� ������ �÷��̾��� ID�� �����ؾߵȴ�.
         */
        foreach(var item in mPlayerNameTexts)
        {
            int playerID = item.Key;

            if(!players.ContainsKey(playerID))
            {
                mRemovedPlayerID.Add(playerID);
            }
        }

        foreach(var playerID in mRemovedPlayerID)
        {
            GameObject textUI = mPlayerNameTexts[playerID];
            mPlayerNameTexts.Remove(playerID);

            Destroy(textUI);
        }

        mRemovedPlayerID.Clear();
    }

    private string MakePlayerID(int playerID)
    {
        return "Player : " + playerID;
    }
}
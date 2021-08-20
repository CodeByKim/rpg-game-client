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

            //아이디를 가져오고
            int playerID = player.GetID();
           // string playerID = MakePlayerID(player.GetID());

            //이 아이디가 현재 화면에 생성되었는지 확인한다.                        
            if(mPlayerNameTexts.ContainsKey(playerID))
            {
                //생성되어 있다면 좌표 갱신
                Transform textUI = mPlayerNameTexts[playerID].transform;
                Vector3 pos = Camera.main.WorldToScreenPoint(player.transform.position);
                textUI.position = new Vector2(pos.x, pos.y + 100);
            }
            else
            {
                //없다면 새로 섹터에 들어온 플레이어이니 프리팹을 생성하고 연동시킨다.
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
         * 이미 없어진 플레이어의 ID는 삭제해야된다.
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
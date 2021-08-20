using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneGUIHandler : MonoBehaviour
{
    private RPGGameLogic mLogic;
    private PrefabController mController;

    private Dictionary<int, PlayerIDTextUI> mPlayerNameTexts;
    private List<int> mRemovedPlayerID;

    private ObjectPool<PlayerIDTextUI> mPlayerNamePool;

    private void Start()
    {
        mLogic = GameFramework.GetGameLogic<RPGGameLogic>();
        mController = GameFramework.GetController<PrefabController>();

        mPlayerNameTexts = new Dictionary<int, PlayerIDTextUI>();
        mRemovedPlayerID = new List<int>();

        GameObject nameUIPrefab = mController.GetPrefab("PlayerNameUI");
        mPlayerNamePool = new ObjectPool<PlayerIDTextUI>(20, nameUIPrefab, transform);        
    }
    
    private void LateUpdate()
    {
        Dictionary<int, Player> players = mLogic.GetCurrentPlayers();

        foreach(var item in players)
        {
            Player player = item.Value;

            // 아이디를 가져오고
            int playerID = player.GetID();           

            // 이 아이디가 현재 화면에 생성되었는지 확인한다.                        
            if(mPlayerNameTexts.ContainsKey(playerID))
            {
                // 생성되어 있다면 좌표 갱신
                PlayerIDTextUI textUI = mPlayerNameTexts[playerID];
                textUI.UpdatePosition();
            }
            else
            {
                // 아니면 새로 할당
                PlayerIDTextUI textUI = mPlayerNamePool.Alloc();
                textUI.SetPlayer(player);
                textUI.transform.SetParent(transform);

                mPlayerNameTexts.Add(playerID, textUI);
            }            
        }

        // 이미 없어진 플레이어의 ID는 삭제해야된다.
        foreach (var item in mPlayerNameTexts)
        {
            int playerID = item.Key;

            if(!players.ContainsKey(playerID))
            {
                mRemovedPlayerID.Add(playerID);
            }
        }

        foreach(var playerID in mRemovedPlayerID)
        {
            PlayerIDTextUI textUI = mPlayerNameTexts[playerID];
            mPlayerNameTexts.Remove(playerID);

            mPlayerNamePool.Free(textUI);
        }

        mRemovedPlayerID.Clear();
    }
}
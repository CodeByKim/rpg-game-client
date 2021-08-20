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

            // ���̵� ��������
            int playerID = player.GetID();           

            // �� ���̵� ���� ȭ�鿡 �����Ǿ����� Ȯ���Ѵ�.                        
            if(mPlayerNameTexts.ContainsKey(playerID))
            {
                // �����Ǿ� �ִٸ� ��ǥ ����
                PlayerIDTextUI textUI = mPlayerNameTexts[playerID];
                textUI.UpdatePosition();
            }
            else
            {
                // �ƴϸ� ���� �Ҵ�
                PlayerIDTextUI textUI = mPlayerNamePool.Alloc();
                textUI.SetPlayer(player);
                textUI.transform.SetParent(transform);

                mPlayerNameTexts.Add(playerID, textUI);
            }            
        }

        // �̹� ������ �÷��̾��� ID�� �����ؾߵȴ�.
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
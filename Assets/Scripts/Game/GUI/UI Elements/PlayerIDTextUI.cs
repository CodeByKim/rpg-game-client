using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIDTextUI : MonoBehaviour, PoolObject
{
    private Text mText;
    private Player mPlayer;

    public void Initialize()
    {
        if(mText == null)
        {
            mText = GetComponent<Text>();
        }

        mText.text = "NONE";
        mPlayer = null;
    }

    public void SetPlayer(Player player)
    {
        mPlayer = player;
        string newPlayerID = MakePlayerID(mPlayer.GetID());        
        mText.text = newPlayerID;

        UpdatePosition();
    }

    public void UpdatePosition()
    {        
        Vector3 pos = Camera.main.WorldToScreenPoint(mPlayer.transform.position);
        transform.position = new Vector2(pos.x, pos.y + 50);
    }

    private string MakePlayerID(int playerID)
    {
        return "Player : " + playerID;
    }
}

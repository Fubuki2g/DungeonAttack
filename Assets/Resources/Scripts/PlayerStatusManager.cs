using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string pNAME; // プレイヤー名
    public int pLv; // プレイヤーレベル
    public int pHP; // プレイヤーHP
    public int pSP; // プレイヤーSP
    public int pATK; // プレイヤー攻撃力
    public int[] skillSlot; // 使用可能スキル
}

[CreateAssetMenu(menuName = "ScriptableObject/Player Status Manager", fileName = "PlayerStatusManager")]

public class PlayerStatusManager : ScriptableObject
{
    public List<PlayerData> DataList;
}

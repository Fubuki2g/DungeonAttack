using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public string eNAME; // “G–¼
    public int eLv; // “GƒŒƒxƒ‹
    public int eHP; // “GHP
    public int eATK; // “GUŒ‚—Í
    public int eEXP; // “GŒoŒ±’l
    public int skillValue1; // Še“GƒXƒLƒ‹‚ÌŒø‰Ê—Ê
    public int skillValue2;
    public int skillValue3; 
}

[CreateAssetMenu(menuName = "ScriptableObject/Enemy Status Manager", fileName = "EnemyStatusManager")]
public class EnemyStatusManager : ScriptableObject
{
    public List<EnemyData> DataList;
}
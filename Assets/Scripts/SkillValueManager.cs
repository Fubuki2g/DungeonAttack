using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillValue
{
    public string skillName; // スキル名
    public int needSkillValue; // 必要SP
    public float skillValue; // スキル量
}

[CreateAssetMenu(menuName = "ScriptableObject/SkillValueManager", fileName = "SkillValueManager")]

public class SkillValueManager : ScriptableObject
{
    public List<SkillValue> DataList;
}

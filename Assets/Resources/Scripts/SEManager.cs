using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SEData
{
    public string SEName; // SE–¼
    public AudioClip AudioClip; // SE‚ÌAudioClip
}

[CreateAssetMenu(menuName = "ScriptableObject/SEManager", fileName = "SEManager")]
public class SEManager : ScriptableObject
{
    public List<SEData> DataList;
}

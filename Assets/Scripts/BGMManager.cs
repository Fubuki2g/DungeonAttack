using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BGMData
{
    public string BGMName; // BGM‚Ì–¼‘O
    public AudioClip AudioClip; // BGM‚ÌAudioClip
}

[CreateAssetMenu(menuName = "ScriptableObject/BGMManager", fileName = "BGMManager")]
public class BGMManager : ScriptableObject
{
    public List<BGMData> DataList;
}
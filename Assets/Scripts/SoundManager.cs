using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private BGMManager bgmManager; // BGM管理用ScriptableObject
    [SerializeField] private SEManager seManager;   // SE管理用ScriptableObject

    public int[] bgmNumber; // BGM番号
    public enum enumBgmNumber
    {
        Title,
        StageEasy,
        StageNormal,
        Battle,
        StrongBattle,
        BossBattle,
        GameOver,
        GameClear,
        EventFountain,
        EventMagic,
        EventMuscle,
        Shop,
        Treasure,
        Rest,
        Num
    }
    public int[] seNumber;　// SE番号
    public enum enumSENumber
    {
        Attack,
        PowerAttack,
        PowerCharge,
        Healing,
        FireBall,
        IceLance,
        Bomb,
        Damage,
        EnemySpecialAttack,
        Scythe,
        Tornado,
        Thunder,
        Breath,
        Auro,
        Lightning,
        Select,
        Back,
        StageChange,
        Pause,
        Win,
        LvUp,
        ItemGet,
        StatusDown,
        Lose,
        Num
    }

    private AudioSource bgmSource;    // BGM再生用AudioSource
    private AudioSource seSource;     // SE再生用AudioSource

    [SerializeField] private Slider bgmVolumeSlider;  // BGM音量調整用スライダー
    [SerializeField] private Slider seVolumeSlider;   // SE音量調整用スライダー

    [SerializeField] private AudioMixerGroup bgmMixerGroup;  // BGM用のAudioMixerGroup
    [SerializeField] private AudioMixerGroup seMixerGroup;   // SE用のAudioMixerGroup

    private const string BGMVolumeKey = "BGMVolume";  // BGM音量を保存するキー
    private const string SEVolumeKey = "SEVolume";    // SE音量を保存するキー

    private void Awake()
    {
        // ScriptableObjectの読み込み
        bgmManager = Resources.Load<BGMManager>("ScriptableObject/BGMManager");
        seManager = Resources.Load<SEManager>("ScriptableObject/SEManager");

        // 各配列の初期化
        bgmNumber = new int[(int)enumBgmNumber.Num];
        seNumber = new int[(int)enumSENumber.Num];

        // BGM用とSE用でAudioSourceを分けて管理
        bgmSource = gameObject.AddComponent<AudioSource>();
        seSource = gameObject.AddComponent<AudioSource>();

        // BGMとSEのAudioMixerGroupを設定
        bgmSource.outputAudioMixerGroup = bgmMixerGroup;
        seSource.outputAudioMixerGroup = seMixerGroup;

        // BGMのループを有効化
        bgmSource.loop = true;

        // 保存されている音量設定をロード
        float savedBGMVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 1.0f);  // デフォルトは1.0
        float savedSEVolume = PlayerPrefs.GetFloat(SEVolumeKey, 1.0f);    // デフォルトは1.0

        // 音量設定を反映
        SetBGMVolume(savedBGMVolume);
        SetSEVolume(savedSEVolume);

        // スライダーが設定されている場合、初期値としてAudioSourceの音量を設定
        if (bgmVolumeSlider != null)
        {
            bgmVolumeSlider.value = bgmSource.volume;
            bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (seVolumeSlider != null)
        {
            seVolumeSlider.value = seSource.volume;
            seVolumeSlider.onValueChanged.AddListener(SetSEVolume);
        }
    }

    // BGM再生
    public void PlayBGM(int bgmElement)
    {
        if (bgmElement >= 0 && bgmElement < bgmManager.DataList.Count)
        {
            BGMData bgmData = bgmManager.DataList[bgmElement];
            bgmSource.clip = bgmData.AudioClip;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("指定されたBGMがありません: " + bgmElement);
        }
    }

    // BGM停止
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // SE再生
    public void PlaySE(int seElement)
    {
        if (seElement >= 0 && seElement < seManager.DataList.Count)
        {
            SEData sEData = seManager.DataList[seElement];
            seSource.clip = sEData.AudioClip;
            seSource.Play();
        }
        else
        {
            Debug.LogWarning("指定されたSEがありません: " + seElement);
        }
    }

    // BGMの音量を設定、保存
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // SEの音量を設定、保存
    public void SetSEVolume(float volume)
    {
        seSource.volume = volume;
        PlayerPrefs.SetFloat(SEVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // サンプルSEの再生
    public void PlaySampleSE()
    {
        PlaySE((int)enumSENumber.Win);
    }
}

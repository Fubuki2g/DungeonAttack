using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GameOverManager : MonoBehaviour
{
    public GameObject defaultButton; // デフォルトで選択するボタン

    private void Start()
    {
        defaultButton = GameObject.Find("Title");
        EventSystem.current.SetSelectedGameObject(defaultButton);

        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.GameOver);
    }

    // リトライボタン
    public void Retry()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (PlayerPrefs.GetInt("Difficulty") == (int)TitleManager.enumDifficultyID.Easy)
        {
            Initiate.Fade("MainSceneEasy", Color.black, 1.0f);
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)TitleManager.enumDifficultyID.Normal)
        {
            Initiate.Fade("MainSceneNormal", Color.black, 1.0f);
        }
    }

    // タイトルへ移動するボタン
    public void Title()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        Initiate.Fade("TitleScene", Color.black, 1.0f);
    }

}

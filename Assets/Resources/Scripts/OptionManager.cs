using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public GameObject optionCanavas; // オプションキャンバス

    public GameObject optionWindow; // オプションウィンドウ
    public GameObject volumeWindow; // 音量調節ウィンドウ

    private GameObject lastSelectButton; // オプションを開く前最後に選択していたボタン

    public GameObject optionDefaultButton; // オプションウィンドウのデフォルトボタン
    public GameObject volumeDefaultButton; // 音量調節ウィンドウのデフォルトボタン

    public bool option;

    private void Start()
    {
        // 各UIオブジェクトの読み込み
        optionCanavas = GameObject.Find("OptionCanvas");
        optionWindow = GameObject.Find("OptionWindow");
        volumeWindow = GameObject.Find("VolumeWindow");
        optionDefaultButton = GameObject.Find("Restart");
        volumeDefaultButton = GameObject.Find("Back");

        // 初期非表示オブジェクトを非表示
        optionCanavas.SetActive(false);
        volumeWindow.SetActive(false);

    }

    void Update()
    {
        // Escapeキーが押されたときにポーズ画面を開き、ゲームを一時停止
        // ポーズ画面表示時はポーズ画面を閉じてゲーム再開
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Pause);
            if (!option)
            {
                Time.timeScale = 0;
                option = true;
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    lastSelectButton = EventSystem.current.currentSelectedGameObject;
                }
                optionCanavas.SetActive(true);
                EventSystem.current.SetSelectedGameObject(optionDefaultButton);
            }
            else
            {
                option = false;
                volumeWindow.SetActive(false);
                optionWindow.SetActive(true);
                optionCanavas.SetActive(false);
                EventSystem.current.SetSelectedGameObject(lastSelectButton);
                Time.timeScale = 1;
            }

        }
    }

    // リスタートボタン
    public void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Initiate.Fade(currentScene.name, Color.black, 1.0f);
        Time.timeScale = 1;
    }

    // タイトルシーンへ移動するボタン
    public void Title()
    {
        Initiate.Fade("TitleScene", Color.black, 1.0f);
        Time.timeScale = 1;
    }

    // 音量設定ウィンドウ表示ボタン
    public void Volume()
    {
        optionWindow.SetActive(false);
        volumeWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(volumeDefaultButton);
    }

    // 音量設定ウィンドウから戻るボタン
    public void Back()
    {
        volumeWindow.SetActive(false);
        optionWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionDefaultButton);
    }

    // ポーズ画面を閉じるボタン
    public void Close()
    {
        option = false;
        optionCanavas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(lastSelectButton);
        Time.timeScale = 1;
    }
}

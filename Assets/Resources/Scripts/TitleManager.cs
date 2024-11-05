using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TitleManager : MonoBehaviour
{
    public GameObject[] canvas; // タイトルシーンで使用するキャンバス
    public enum enumCanvas
    {
        Title, // タイトルを表示するキャンバス
        Select, // モード選択のキャンバス
        Stage, // ステージ選択のキャンバス
        Character, // キャラクター選択のキャンバス
        Check, // 確認のキャンバス
        Num
    }
    public TextMeshProUGUI[] highestFloor; // 最高フロア表示用のテキスト
    public enum enumHighesetFloor
    {
        Easy,
        Normal,
        Num
    }
    public GameObject[] clearImage; // クリアの際に表示するImage
    public enum enumClearImage
    {
        Easy,
        Normal,
        Num
    }
    public GameObject[] defaultButton; // デフォルト選択ボタン
    public enum enumDefaultButton
    {
        SelectButton,
        VolumeButton,
        StageButton,
        CharacterButton,
        CheckBackButton,
        Num
    }
    public int[] characterID; // キャラクターID
    public enum enumCharacterID
    {
        Warrior,
        Magician,
        Num
    }
    public int[] DifficultyID; // 難易度ID
    public enum enumDifficultyID
    {
        Easy,
        Normal,
        Num
    }

    public GameObject SelectWindow; // 初期選択ウィンドウ
    public GameObject VolumeOptionWindow; // 音量設定ウィンドウ
    public Image StageCheckImage; // ステージ選択ウィンドウ
    public Image CharacterCheckImage; // キャラクター選択ウィンドウ

    public TextMeshProUGUI pressButtonText; // タイトル表示テキスト
    public float flishingTime = 0.5f; // 点滅間隔
    public float fadeTime = 1f; // 点滅時間

    private bool TitleOff = false; // 初期表示用変数

    void Start()
    {
        // 各配列の初期化
        canvas = new GameObject[(int)enumCanvas.Num];
        highestFloor = new TextMeshProUGUI[(int)enumHighesetFloor.Num];
        clearImage = new GameObject[(int)enumClearImage.Num];
        defaultButton = new GameObject[(int)enumDefaultButton.Num];
        characterID = new int[(int)enumCharacterID.Num];
        DifficultyID = new int[(int)enumDifficultyID.Num];

        // 各UIオブジェクトの読み込み
        canvas[(int)enumCanvas.Title] = GameObject.Find("TitleCanvas");
        canvas[(int)enumCanvas.Select] = GameObject.Find("SelectCanvas");
        canvas[(int)enumCanvas.Stage] = GameObject.Find("StageSelectCanvas");
        canvas[(int)enumCanvas.Character] = GameObject.Find("CharacterSelectCanvas");
        canvas[(int)enumCanvas.Check] = GameObject.Find("CheckCanvas");

        highestFloor[(int)enumHighesetFloor.Easy] = GameObject.Find("EasyMaxFloor").GetComponent<TextMeshProUGUI>();
        highestFloor[(int)enumHighesetFloor.Normal] = GameObject.Find("NormalMaxFloor").GetComponent<TextMeshProUGUI>();

        clearImage[(int)enumClearImage.Easy] = GameObject.Find("EasyClear");
        clearImage[(int)enumClearImage.Normal] = GameObject.Find("NormalClear");

        defaultButton[(int)enumDefaultButton.SelectButton] = GameObject.Find("StageSelectButton");
        defaultButton[(int)enumDefaultButton.VolumeButton] = GameObject.Find("VolumeBackButton");
        defaultButton[(int)enumDefaultButton.StageButton] = GameObject.Find("EasyButton");
        defaultButton[(int)enumDefaultButton.CharacterButton] = GameObject.Find("WarriorButton");
        defaultButton[(int)enumDefaultButton.CheckBackButton] = GameObject.Find("NoButton");

        highestFloor[(int)enumHighesetFloor.Easy].text = PlayerPrefs.GetInt("EasyClearFloor").ToString();
        highestFloor[(int)enumHighesetFloor.Normal].text = PlayerPrefs.GetInt("NormalClearFloor").ToString();

        SelectWindow = GameObject.Find("SelectWindow");
        VolumeOptionWindow = GameObject.Find("VolumeOptionWindow");

        StageCheckImage = GameObject.Find("StageCheckImage").GetComponent<Image>();
        CharacterCheckImage = GameObject.Find("CharacterCheckImage").GetComponent<Image>();

        pressButtonText = GameObject.Find("PressButton").GetComponent<TextMeshProUGUI>();

        // 初期非表示オブジェクトを非表示
        canvas[(int)enumCanvas.Select].SetActive(false);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(false);
        clearImage[(int)enumClearImage.Easy].SetActive(false);
        clearImage[(int)enumClearImage.Normal].SetActive(false);
        VolumeOptionWindow.SetActive(false);

        // BGMの再生
        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.Title);

        StartCoroutine(FlishingText());

    }

    void Update()
    {
        // スペースが押されたら初期選択ウィンドウを表示（1度だけ）
        if (!TitleOff && (Input.GetKeyDown(KeyCode.Space)))
        {
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
            canvas[(int)enumCanvas.Title].SetActive(false);
            canvas[(int)enumCanvas.Select].SetActive(true);
            TitleOff = true;
            EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.SelectButton]);
        }

        if (PlayerPrefs.GetInt("EasyClearFloor") == 5)
        {
            clearImage[(int)enumClearImage.Easy].SetActive(true);
        }

        if (PlayerPrefs.GetInt("NormalClearFloor") == 10)
        {
            clearImage[(int)enumClearImage.Normal].SetActive(true);
        }

    }

    // 点滅処理
    private IEnumerator FlishingText()
    {
        while (true)
        {
            // フェードアウト
            yield return StartCoroutine(FadeText(1f, 0f));
            // フェード後の待機
            yield return new WaitForSeconds(flishingTime);

            // フェードイン
            yield return StartCoroutine(FadeText(0f, 1f));
            // フェード後の待機
            yield return new WaitForSeconds(flishingTime);
        }
    }

    // フェード効果を実装するコルーチン
    private IEnumerator FadeText(float startAlpha, float endAlpha)
    {
        Color originalColor = pressButtonText.color;
        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeTime);
            originalColor.a = alpha;
            pressButtonText.color = originalColor;
            yield return null;
        }

        originalColor.a = endAlpha;
        pressButtonText.color = originalColor;
    }

    // ステージセレクトボタン
    public void StageSelect()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Select].SetActive(false);
        canvas[(int)enumCanvas.Stage].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.StageButton]);
    }

    // 音量調節ボタン
    public void VolumeOption()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        SelectWindow.SetActive(false);
        VolumeOptionWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.VolumeButton]);
    }

    // 音量調節から戻るボタン
    public void VolumeBack()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        VolumeOptionWindow.SetActive(false);
        SelectWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.SelectButton]);
    }

    // ゲーム終了ボタン
    public void GameEnd()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // ステージ選択ウィンドウから戻るボタン
    public void StageBack()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Select].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.SelectButton]);
    }

    // キャラクター選択ウィンドウから戻るボタン
    public void CharacterBack()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Stage].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.StageButton]);
    }

    // イージーステージ選択ボタン
    public void Easy()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
        PlayerPrefs.SetInt("Difficulty", (int)enumDifficultyID.Easy);
    }

    // ノーマルステージステージ選択ボタン
    public void Normal()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
        PlayerPrefs.SetInt("Difficulty", (int)enumDifficultyID.Normal);
    }

    // 戦士選択ボタン
    public void Warrior()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayerPrefs.SetInt("Character", (int)enumCharacterID.Warrior);
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CheckBackButton]);
        CharacterCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Warrior");
        if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Easy)
        {
            StageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Easy");
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Normal)
        {
            StageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Normal");
        }
    }

    // 魔法使い選択ボタン
    public void Magician()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayerPrefs.SetInt("Character", (int)enumCharacterID.Magician);
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CheckBackButton]);
        CharacterCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Magician");
        if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Easy)
        {
            StageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Easy");
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Normal)
        {
            StageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Normal");
        }
    }

    
    // 確認選択肢はい
    public void Yes()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Easy)
        {
            Initiate.Fade("MainSceneEasy", Color.black, 1.0f);
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Normal)
        {
            Initiate.Fade("MainSceneNormal", Color.black, 1.0f);
        }
    }

    // 確認選択肢いいえ
    public void No()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        canvas[(int)enumCanvas.Character].SetActive(true);
        canvas[(int)enumCanvas.Check].SetActive(false);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
    }

}

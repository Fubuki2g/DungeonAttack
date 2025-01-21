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
        PlayGuide, // 遊び方説明のキャンバス
        Stage, // ステージ選択のキャンバス
        Character, // キャラクター選択のキャンバス
        Check, // 確認のキャンバス
        Num
    }
    public TextMeshProUGUI[] highestFloor; // 最高フロア表示用のテキスト
    public GameObject[] clearImage; // クリアの際に表示するImage
    public GameObject[] defaultButton; // デフォルト選択ボタン
    public enum enumDefaultButton
    {
        SelectButton, // モード選択のデフォルトボタン
        VolumeButton, // 音量調節のデフォルトボタン
        StageButton, // ステージ選択のデフォルトボタン
        CharacterButton, // キャラクター選択のデフォルトボタン
        CheckBackButton, // 最終確認のデフォルトボタン
        Num
    }
    public int[] characterID; // キャラクターID
    public enum enumCharacterID
    {
        Warrior, // 戦士
        Magician, // 魔法使い
        Thief, // シーフ
        Num
    }
    public int[] difficultyID; // 難易度ID
    public enum enumDifficultyID
    {
        Easy, // イージー
        Normal, // ノーマル
        Hard, // ハード
        Num
    }

    public GameObject selectWindow; // 初期選択ウィンドウ
    public GameObject[] playGuideWindow; // 遊び方ウィンドウ
    public enum enumPlayGuideWindow
    {
        PlayGuide, // 初期表示ウィンドウ
        Control, // 操作説明ウィンドウ
        Base, // 基本画面説明ウィンドウ
        Battle, // 戦闘画面説明ウィンドウ
        Num
    }
    public GameObject volumeOptionWindow; // 音量設定ウィンドウ
    public Image stageCheckImage; // ステージ選択ウィンドウ
    public Image characterCheckImage; // キャラクター選択ウィンドウ
    public GameObject[] characterSelectButton; // キャラクター選択のボタン
    public enum enumCharacterSelectButton
    {
        Warrior, // 戦士
        Magician, // 魔法使い
        Thief, // シーフ
        Back, // 戻るボタン
        Num
    }
    public GameObject dscriptionWindow; // キャラクター説明ウィンドウ
    public TextMeshProUGUI dscription; // キャラクター説明テキスト
    public bool characterdscripton; // キャラクター説明を表示するかどうか

    public TextMeshProUGUI selectedStage; // 確認画面でのステージ確認テキスト
    public TextMeshProUGUI selectedCharacter; // 確認画面でのキャラクター確認テキスト

    public TextMeshProUGUI pressButtonText; // タイトル表示テキスト
    public float flishingTime = 0.5f; // 点滅間隔
    public float fadeTime = 1f; // 点滅時間

    private bool TitleOff = false; // 初期表示用変数

    void Start()
    {
        // 各配列の初期化
        canvas = new GameObject[(int)enumCanvas.Num];
        highestFloor = new TextMeshProUGUI[(int)enumDifficultyID.Num];
        clearImage = new GameObject[(int)enumDifficultyID.Num];
        defaultButton = new GameObject[(int)enumDefaultButton.Num];
        characterID = new int[(int)enumCharacterID.Num];
        difficultyID = new int[(int)enumDifficultyID.Num];
        playGuideWindow = new GameObject[(int)enumPlayGuideWindow.Num];
        characterSelectButton = new GameObject[(int)enumCharacterSelectButton.Num];

        // 各UIオブジェクトの読み込み
        canvas[(int)enumCanvas.Title] = GameObject.Find("TitleCanvas");
        canvas[(int)enumCanvas.Select] = GameObject.Find("SelectCanvas");
        canvas[(int)enumCanvas.PlayGuide] = GameObject.Find("PlayGuideCanvas");
        canvas[(int)enumCanvas.Stage] = GameObject.Find("StageSelectCanvas");
        canvas[(int)enumCanvas.Character] = GameObject.Find("CharacterSelectCanvas");
        canvas[(int)enumCanvas.Check] = GameObject.Find("CheckCanvas");

        highestFloor[(int)enumDifficultyID.Easy] = GameObject.Find("EasyMaxFloor").GetComponent<TextMeshProUGUI>();
        highestFloor[(int)enumDifficultyID.Normal] = GameObject.Find("NormalMaxFloor").GetComponent<TextMeshProUGUI>();
        highestFloor[(int)enumDifficultyID.Hard] = GameObject.Find("HardMaxFloor").GetComponent<TextMeshProUGUI>();

        clearImage[(int)enumDifficultyID.Easy] = GameObject.Find("EasyClear");
        clearImage[(int)enumDifficultyID.Normal] = GameObject.Find("NormalClear");
        clearImage[(int)enumDifficultyID.Hard] = GameObject.Find("HardClear");

        defaultButton[(int)enumDefaultButton.SelectButton] = GameObject.Find("StageSelectButton");
        defaultButton[(int)enumDefaultButton.VolumeButton] = GameObject.Find("VolumeBackButton");
        defaultButton[(int)enumDefaultButton.StageButton] = GameObject.Find("EasyButton");
        defaultButton[(int)enumDefaultButton.CharacterButton] = GameObject.Find("WarriorButton");
        defaultButton[(int)enumDefaultButton.CheckBackButton] = GameObject.Find("NoButton");

        highestFloor[(int)enumDifficultyID.Easy].text = PlayerPrefs.GetInt("EasyClearFloor").ToString();
        highestFloor[(int)enumDifficultyID.Normal].text = PlayerPrefs.GetInt("NormalClearFloor").ToString();
        highestFloor[(int)enumDifficultyID.Hard].text = PlayerPrefs.GetInt("HardClearFloor").ToString();

        selectWindow = GameObject.Find("SelectWindow");
        playGuideWindow[(int)enumPlayGuideWindow.PlayGuide] = GameObject.Find("PlayGuide");
        playGuideWindow[(int)enumPlayGuideWindow.Control] = GameObject.Find("ControlPlayGuide");
        playGuideWindow[(int)enumPlayGuideWindow.Base] = GameObject.Find("BasePlayGuide");
        playGuideWindow[(int)enumPlayGuideWindow.Battle] = GameObject.Find("BattlePlayGuide");
        volumeOptionWindow = GameObject.Find("VolumeOptionWindow");

        characterSelectButton[(int)enumCharacterSelectButton.Warrior] = GameObject.Find("WarriorButton");
        characterSelectButton[(int)enumCharacterSelectButton.Magician] = GameObject.Find("MagicianButton");
        characterSelectButton[(int)enumCharacterSelectButton.Thief] = GameObject.Find("ThiefButton");
        characterSelectButton[(int)enumCharacterSelectButton.Back] = GameObject.Find("CharacterSelectBackButton");
        dscriptionWindow = GameObject.Find("DscriptionWindow");
        dscription = GameObject.Find("Description").GetComponent<TextMeshProUGUI>();

        selectedStage = GameObject.Find("SelectedStage").GetComponent<TextMeshProUGUI>();
        selectedCharacter = GameObject.Find("SelectedCharacter").GetComponent<TextMeshProUGUI>();

        stageCheckImage = GameObject.Find("StageCheckImage").GetComponent<Image>();
        characterCheckImage = GameObject.Find("CharacterCheckImage").GetComponent<Image>();

        pressButtonText = GameObject.Find("PressButton").GetComponent<TextMeshProUGUI>();

        // 初期非表示オブジェクトを非表示
        canvas[(int)enumCanvas.Select].SetActive(false);
        canvas[(int)enumCanvas.PlayGuide].SetActive(false);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(false);
        clearImage[(int)enumDifficultyID.Easy].SetActive(false);
        clearImage[(int)enumDifficultyID.Normal].SetActive(false);
        clearImage[(int)enumDifficultyID.Hard].SetActive(false);
        playGuideWindow[(int)enumPlayGuideWindow.Control].SetActive(false);
        playGuideWindow[(int)enumPlayGuideWindow.Base].SetActive(false);
        playGuideWindow[(int)enumPlayGuideWindow.Battle].SetActive(false);
        dscriptionWindow.SetActive(false);
        volumeOptionWindow.SetActive(false);

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
            clearImage[(int)enumDifficultyID.Easy].SetActive(true);
        }

        if (PlayerPrefs.GetInt("NormalClearFloor") == 10)
        {
            clearImage[(int)enumDifficultyID.Normal].SetActive(true);
        }

        if (PlayerPrefs.GetInt("HardClearFloor") == 15)
        {
            clearImage[(int)enumDifficultyID.Hard].SetActive(true);
        }

        if (characterdscripton)
        {
            dscriptionWindow.SetActive(true);
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
            if (selectedObject != null && selectedObject == characterSelectButton[(int)enumCharacterSelectButton.Warrior].gameObject)
            {
                dscription.text = "戦士：基本のアタッカー\nとにかく攻撃！";
            }
            else if (selectedObject != null && selectedObject == characterSelectButton[(int)enumCharacterSelectButton.Magician].gameObject)
            {
                dscription.text = "魔法使い：スキルアタッカー\nSPを使って戦う！消費に注意！";
            }
            else if (selectedObject != null && selectedObject == characterSelectButton[(int)enumCharacterSelectButton.Thief].gameObject)
            {
                dscription.text = "シーフ：状態異常の使い手\n毒状態をうまく使って戦え！";
            }
            else if (selectedObject != null && selectedObject == characterSelectButton[(int)enumCharacterSelectButton.Back].gameObject)
            {
                dscription.text = "";
            }
        }
        else
        {
            dscriptionWindow.SetActive(false);
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

    // 操作説明、遊び方表示
    public void PlayGuide()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Select].SetActive(false);
        canvas[(int)enumCanvas.PlayGuide].SetActive(true);
        StartCoroutine(PlayGuideViewing());
    }

    // 遊び方の表示切替処理
    public IEnumerator PlayGuideViewing()
    {
        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        playGuideWindow[(int)enumPlayGuideWindow.PlayGuide].SetActive(false);
        playGuideWindow[(int)enumPlayGuideWindow.Control].SetActive(true);

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        playGuideWindow[(int)enumPlayGuideWindow.Control].SetActive(false);
        playGuideWindow[(int)enumPlayGuideWindow.Base].SetActive(true);

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        playGuideWindow[(int)enumPlayGuideWindow.Base].SetActive(false);
        playGuideWindow[(int)enumPlayGuideWindow.Battle].SetActive(true);

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        playGuideWindow[(int)enumPlayGuideWindow.Battle].SetActive(false);
        playGuideWindow[(int)enumPlayGuideWindow.PlayGuide].SetActive(true);
        canvas[(int)enumCanvas.PlayGuide].SetActive(false);
        canvas[(int)enumCanvas.Select].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.SelectButton]);
    }

    // 音量調節ボタン
    public void VolumeOption()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        selectWindow.SetActive(false);
        volumeOptionWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.VolumeButton]);
    }

    // 音量調節から戻るボタン
    public void VolumeBack()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        volumeOptionWindow.SetActive(false);
        selectWindow.SetActive(true);
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
        characterdscripton = true;
        PlayerPrefs.SetInt("Difficulty", (int)enumDifficultyID.Easy);
    }

    // ノーマルステージ選択ボタン
    public void Normal()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
        characterdscripton = true;
        PlayerPrefs.SetInt("Difficulty", (int)enumDifficultyID.Normal);
    }

    // ハードステージ選択ボタン
    public void Hard()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
        characterdscripton = true;
        PlayerPrefs.SetInt("Difficulty", (int)enumDifficultyID.Hard);
    }

    // 戦士選択ボタン
    public void Warrior()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayerPrefs.SetInt("Character", (int)enumCharacterID.Warrior);
        characterdscripton = false;
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CheckBackButton]);
        characterCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Warrior");
        selectedCharacter.text = "戦士";
        if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Easy)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Easy");
            selectedStage.text = "イージー";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Normal)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Normal");
            selectedStage.text = "ノーマル";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Hard)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Hard");
            selectedStage.text = "ハード";
        }
    }

    // 魔法使い選択ボタン
    public void Magician()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayerPrefs.SetInt("Character", (int)enumCharacterID.Magician);
        characterdscripton = false;
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CheckBackButton]);
        characterCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Magician");
        selectedCharacter.text = "魔法使い";
        if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Easy)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Easy");
            selectedStage.text = "イージー";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Normal)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Normal");
            selectedStage.text = "ノーマル";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Hard)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Hard");
            selectedStage.text = "ハード";
        }
    }

    // シーフ選択ボタン
    public void Thief()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayerPrefs.SetInt("Character", (int)enumCharacterID.Thief);
        characterdscripton = false;
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CheckBackButton]);
        characterCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Thief");
        selectedCharacter.text = "シーフ";
        if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Easy)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Easy");
            selectedStage.text = "イージー";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Normal)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Normal");
            selectedStage.text = "ノーマル";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Hard)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Hard");
            selectedStage.text = "ハード";
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
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Hard)
        {
            Initiate.Fade("MainSceneHard", Color.black, 1.0f);
        }
    }

    // 確認選択肢いいえ
    public void No()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        canvas[(int)enumCanvas.Character].SetActive(true);
        canvas[(int)enumCanvas.Check].SetActive(false);
        characterdscripton = true;
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
    }

    // コルーチン内で次の処理に移動する際のディレイの設定
    public IEnumerator NextProcess(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }

}

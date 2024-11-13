using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TitleManager : MonoBehaviour
{
    public GameObject[] canvas; // �^�C�g���V�[���Ŏg�p����L�����o�X
    public enum enumCanvas
    {
        Title, // �^�C�g����\������L�����o�X
        Select, // ���[�h�I���̃L�����o�X
        PlayGuide, // �V�ѕ������̃L�����o�X
        Stage, // �X�e�[�W�I���̃L�����o�X
        Character, // �L�����N�^�[�I���̃L�����o�X
        Check, // �m�F�̃L�����o�X
        Num
    }
    public TextMeshProUGUI[] highestFloor; // �ō��t���A�\���p�̃e�L�X�g
    public GameObject[] clearImage; // �N���A�̍ۂɕ\������Image
    public GameObject[] defaultButton; // �f�t�H���g�I���{�^��
    public enum enumDefaultButton
    {
        SelectButton, // ���[�h�I���̃f�t�H���g�{�^��
        VolumeButton, // ���ʒ��߂̃f�t�H���g�{�^��
        StageButton, // �X�e�[�W�I���̃f�t�H���g�{�^��
        CharacterButton, // �L�����N�^�[�I���̃f�t�H���g�{�^��
        CheckBackButton, // �ŏI�m�F�̃f�t�H���g�{�^��
        Num
    }
    public int[] characterID; // �L�����N�^�[ID
    public enum enumCharacterID
    {
        Warrior, // ��m
        Magician, // ���@�g��
        Num
    }
    public int[] DifficultyID; // ��ՓxID
    public enum enumDifficultyID
    {
        Easy, // �C�[�W�[
        Normal, // �m�[�}��
        Num
    }

    public GameObject SelectWindow; // �����I���E�B���h�E
    public GameObject[] PlayGuideWindow; // �V�ѕ��E�B���h�E
    public enum enumPlayGuideWindow
    {
        PlayGuide, // �����\���E�B���h�E
        Control, // ��������E�B���h�E
        Base, // ��{��ʐ����E�B���h�E
        Battle, // �퓬��ʐ����E�B���h�E
        Num
    }
    public GameObject VolumeOptionWindow; // ���ʐݒ�E�B���h�E
    public Image StageCheckImage; // �X�e�[�W�I���E�B���h�E
    public Image CharacterCheckImage; // �L�����N�^�[�I���E�B���h�E

    public TextMeshProUGUI pressButtonText; // �^�C�g���\���e�L�X�g
    public float flishingTime = 0.5f; // �_�ŊԊu
    public float fadeTime = 1f; // �_�Ŏ���

    private bool TitleOff = false; // �����\���p�ϐ�

    void Start()
    {
        // �e�z��̏�����
        canvas = new GameObject[(int)enumCanvas.Num];
        highestFloor = new TextMeshProUGUI[(int)enumDifficultyID.Num];
        clearImage = new GameObject[(int)enumDifficultyID.Num];
        defaultButton = new GameObject[(int)enumDefaultButton.Num];
        characterID = new int[(int)enumCharacterID.Num];
        DifficultyID = new int[(int)enumDifficultyID.Num];
        PlayGuideWindow = new GameObject[(int)enumPlayGuideWindow.Num];

        // �eUI�I�u�W�F�N�g�̓ǂݍ���
        canvas[(int)enumCanvas.Title] = GameObject.Find("TitleCanvas");
        canvas[(int)enumCanvas.Select] = GameObject.Find("SelectCanvas");
        canvas[(int)enumCanvas.PlayGuide] = GameObject.Find("PlayGuideCanvas");
        canvas[(int)enumCanvas.Stage] = GameObject.Find("StageSelectCanvas");
        canvas[(int)enumCanvas.Character] = GameObject.Find("CharacterSelectCanvas");
        canvas[(int)enumCanvas.Check] = GameObject.Find("CheckCanvas");

        highestFloor[(int)enumDifficultyID.Easy] = GameObject.Find("EasyMaxFloor").GetComponent<TextMeshProUGUI>();
        highestFloor[(int)enumDifficultyID.Normal] = GameObject.Find("NormalMaxFloor").GetComponent<TextMeshProUGUI>();

        clearImage[(int)enumDifficultyID.Easy] = GameObject.Find("EasyClear");
        clearImage[(int)enumDifficultyID.Normal] = GameObject.Find("NormalClear");

        defaultButton[(int)enumDefaultButton.SelectButton] = GameObject.Find("StageSelectButton");
        defaultButton[(int)enumDefaultButton.VolumeButton] = GameObject.Find("VolumeBackButton");
        defaultButton[(int)enumDefaultButton.StageButton] = GameObject.Find("EasyButton");
        defaultButton[(int)enumDefaultButton.CharacterButton] = GameObject.Find("WarriorButton");
        defaultButton[(int)enumDefaultButton.CheckBackButton] = GameObject.Find("NoButton");

        highestFloor[(int)enumDifficultyID.Easy].text = PlayerPrefs.GetInt("EasyClearFloor").ToString();
        highestFloor[(int)enumDifficultyID.Normal].text = PlayerPrefs.GetInt("NormalClearFloor").ToString();

        SelectWindow = GameObject.Find("SelectWindow");
        PlayGuideWindow[(int)enumPlayGuideWindow.PlayGuide] = GameObject.Find("PlayGuide");
        PlayGuideWindow[(int)enumPlayGuideWindow.Control] = GameObject.Find("ControlPlayGuide");
        PlayGuideWindow[(int)enumPlayGuideWindow.Base] = GameObject.Find("BasePlayGuide");
        PlayGuideWindow[(int)enumPlayGuideWindow.Battle] = GameObject.Find("BattlePlayGuide");
        VolumeOptionWindow = GameObject.Find("VolumeOptionWindow");

        StageCheckImage = GameObject.Find("StageCheckImage").GetComponent<Image>();
        CharacterCheckImage = GameObject.Find("CharacterCheckImage").GetComponent<Image>();

        pressButtonText = GameObject.Find("PressButton").GetComponent<TextMeshProUGUI>();

        // ������\���I�u�W�F�N�g���\��
        canvas[(int)enumCanvas.Select].SetActive(false);
        canvas[(int)enumCanvas.PlayGuide].SetActive(false);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(false);
        clearImage[(int)enumDifficultyID.Easy].SetActive(false);
        clearImage[(int)enumDifficultyID.Normal].SetActive(false);
        PlayGuideWindow[(int)enumPlayGuideWindow.Control].SetActive(false);
        PlayGuideWindow[(int)enumPlayGuideWindow.Base].SetActive(false);
        PlayGuideWindow[(int)enumPlayGuideWindow.Battle].SetActive(false);
        VolumeOptionWindow.SetActive(false);

        // BGM�̍Đ�
        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.Title);

        StartCoroutine(FlishingText());

    }

    void Update()
    {
        // �X�y�[�X�������ꂽ�珉���I���E�B���h�E��\���i1�x�����j
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

    }

    // �_�ŏ���
    private IEnumerator FlishingText()
    {
        while (true)
        {
            // �t�F�[�h�A�E�g
            yield return StartCoroutine(FadeText(1f, 0f));
            // �t�F�[�h��̑ҋ@
            yield return new WaitForSeconds(flishingTime);

            // �t�F�[�h�C��
            yield return StartCoroutine(FadeText(0f, 1f));
            // �t�F�[�h��̑ҋ@
            yield return new WaitForSeconds(flishingTime);
        }
    }

    // �t�F�[�h���ʂ���������R���[�`��
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

    // �X�e�[�W�Z���N�g�{�^��
    public void StageSelect()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Select].SetActive(false);
        canvas[(int)enumCanvas.Stage].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.StageButton]);
    }

    public void PlayGuide()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Select].SetActive(false);
        canvas[(int)enumCanvas.PlayGuide].SetActive(true);
        StartCoroutine(PlayGuideViewing());
    }

    public IEnumerator PlayGuideViewing()
    {
        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayGuideWindow[(int)enumPlayGuideWindow.PlayGuide].SetActive(false);
        PlayGuideWindow[(int)enumPlayGuideWindow.Control].SetActive(true);

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayGuideWindow[(int)enumPlayGuideWindow.Control].SetActive(false);
        PlayGuideWindow[(int)enumPlayGuideWindow.Base].SetActive(true);

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayGuideWindow[(int)enumPlayGuideWindow.Base].SetActive(false);
        PlayGuideWindow[(int)enumPlayGuideWindow.Battle].SetActive(true);

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayGuideWindow[(int)enumPlayGuideWindow.Battle].SetActive(false);
        PlayGuideWindow[(int)enumPlayGuideWindow.PlayGuide].SetActive(true);
        canvas[(int)enumCanvas.PlayGuide].SetActive(false);
        canvas[(int)enumCanvas.Select].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.SelectButton]);
    }

    // ���ʒ��߃{�^��
    public void VolumeOption()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        SelectWindow.SetActive(false);
        VolumeOptionWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.VolumeButton]);
    }

    // ���ʒ��߂���߂�{�^��
    public void VolumeBack()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        VolumeOptionWindow.SetActive(false);
        SelectWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.SelectButton]);
    }

    // �Q�[���I���{�^��
    public void GameEnd()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // �X�e�[�W�I���E�B���h�E����߂�{�^��
    public void StageBack()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Select].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.SelectButton]);
    }

    // �L�����N�^�[�I���E�B���h�E����߂�{�^��
    public void CharacterBack()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Stage].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.StageButton]);
    }

    // �C�[�W�[�X�e�[�W�I���{�^��
    public void Easy()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
        PlayerPrefs.SetInt("Difficulty", (int)enumDifficultyID.Easy);
    }

    // �m�[�}���X�e�[�W�X�e�[�W�I���{�^��
    public void Normal()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
        PlayerPrefs.SetInt("Difficulty", (int)enumDifficultyID.Normal);
    }

    // ��m�I���{�^��
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

    // ���@�g���I���{�^��
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

    
    // �m�F�I�����͂�
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

    // �m�F�I����������
    public void No()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        canvas[(int)enumCanvas.Character].SetActive(true);
        canvas[(int)enumCanvas.Check].SetActive(false);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
    }

    // �R���[�`�����Ŏ��̏����Ɉړ�����ۂ̃f�B���C�̐ݒ�
    public IEnumerator NextProcess(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }

}

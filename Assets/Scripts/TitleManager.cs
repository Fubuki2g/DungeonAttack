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
        Thief, // �V�[�t
        Num
    }
    public int[] difficultyID; // ��ՓxID
    public enum enumDifficultyID
    {
        Easy, // �C�[�W�[
        Normal, // �m�[�}��
        Hard, // �n�[�h
        Num
    }

    public GameObject selectWindow; // �����I���E�B���h�E
    public GameObject[] playGuideWindow; // �V�ѕ��E�B���h�E
    public enum enumPlayGuideWindow
    {
        PlayGuide, // �����\���E�B���h�E
        Control, // ��������E�B���h�E
        Base, // ��{��ʐ����E�B���h�E
        Battle, // �퓬��ʐ����E�B���h�E
        Num
    }
    public GameObject volumeOptionWindow; // ���ʐݒ�E�B���h�E
    public Image stageCheckImage; // �X�e�[�W�I���E�B���h�E
    public Image characterCheckImage; // �L�����N�^�[�I���E�B���h�E
    public GameObject[] characterSelectButton; // �L�����N�^�[�I���̃{�^��
    public enum enumCharacterSelectButton
    {
        Warrior, // ��m
        Magician, // ���@�g��
        Thief, // �V�[�t
        Back, // �߂�{�^��
        Num
    }
    public GameObject dscriptionWindow; // �L�����N�^�[�����E�B���h�E
    public TextMeshProUGUI dscription; // �L�����N�^�[�����e�L�X�g
    public bool characterdscripton; // �L�����N�^�[������\�����邩�ǂ���

    public TextMeshProUGUI selectedStage; // �m�F��ʂł̃X�e�[�W�m�F�e�L�X�g
    public TextMeshProUGUI selectedCharacter; // �m�F��ʂł̃L�����N�^�[�m�F�e�L�X�g

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
        difficultyID = new int[(int)enumDifficultyID.Num];
        playGuideWindow = new GameObject[(int)enumPlayGuideWindow.Num];
        characterSelectButton = new GameObject[(int)enumCharacterSelectButton.Num];

        // �eUI�I�u�W�F�N�g�̓ǂݍ���
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

        // ������\���I�u�W�F�N�g���\��
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
                dscription.text = "��m�F��{�̃A�^�b�J�[\n�Ƃɂ����U���I";
            }
            else if (selectedObject != null && selectedObject == characterSelectButton[(int)enumCharacterSelectButton.Magician].gameObject)
            {
                dscription.text = "���@�g���F�X�L���A�^�b�J�[\nSP���g���Đ키�I����ɒ��ӁI";
            }
            else if (selectedObject != null && selectedObject == characterSelectButton[(int)enumCharacterSelectButton.Thief].gameObject)
            {
                dscription.text = "�V�[�t�F��Ԉُ�̎g����\n�ŏ�Ԃ����܂��g���Đ킦�I";
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

    // ��������A�V�ѕ��\��
    public void PlayGuide()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Select].SetActive(false);
        canvas[(int)enumCanvas.PlayGuide].SetActive(true);
        StartCoroutine(PlayGuideViewing());
    }

    // �V�ѕ��̕\���ؑ֏���
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

    // ���ʒ��߃{�^��
    public void VolumeOption()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        selectWindow.SetActive(false);
        volumeOptionWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.VolumeButton]);
    }

    // ���ʒ��߂���߂�{�^��
    public void VolumeBack()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        volumeOptionWindow.SetActive(false);
        selectWindow.SetActive(true);
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
        characterdscripton = true;
        PlayerPrefs.SetInt("Difficulty", (int)enumDifficultyID.Easy);
    }

    // �m�[�}���X�e�[�W�I���{�^��
    public void Normal()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
        characterdscripton = true;
        PlayerPrefs.SetInt("Difficulty", (int)enumDifficultyID.Normal);
    }

    // �n�[�h�X�e�[�W�I���{�^��
    public void Hard()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        canvas[(int)enumCanvas.Stage].SetActive(false);
        canvas[(int)enumCanvas.Character].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CharacterButton]);
        characterdscripton = true;
        PlayerPrefs.SetInt("Difficulty", (int)enumDifficultyID.Hard);
    }

    // ��m�I���{�^��
    public void Warrior()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayerPrefs.SetInt("Character", (int)enumCharacterID.Warrior);
        characterdscripton = false;
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CheckBackButton]);
        characterCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Warrior");
        selectedCharacter.text = "��m";
        if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Easy)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Easy");
            selectedStage.text = "�C�[�W�[";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Normal)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Normal");
            selectedStage.text = "�m�[�}��";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Hard)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Hard");
            selectedStage.text = "�n�[�h";
        }
    }

    // ���@�g���I���{�^��
    public void Magician()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayerPrefs.SetInt("Character", (int)enumCharacterID.Magician);
        characterdscripton = false;
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CheckBackButton]);
        characterCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Magician");
        selectedCharacter.text = "���@�g��";
        if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Easy)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Easy");
            selectedStage.text = "�C�[�W�[";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Normal)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Normal");
            selectedStage.text = "�m�[�}��";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Hard)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Hard");
            selectedStage.text = "�n�[�h";
        }
    }

    // �V�[�t�I���{�^��
    public void Thief()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        PlayerPrefs.SetInt("Character", (int)enumCharacterID.Thief);
        characterdscripton = false;
        canvas[(int)enumCanvas.Character].SetActive(false);
        canvas[(int)enumCanvas.Check].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[(int)enumDefaultButton.CheckBackButton]);
        characterCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Thief");
        selectedCharacter.text = "�V�[�t";
        if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Easy)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Easy");
            selectedStage.text = "�C�[�W�[";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Normal)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Normal");
            selectedStage.text = "�m�[�}��";
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Hard)
        {
            stageCheckImage.sprite = Resources.Load<Sprite>("Images/PublicImages/Hard");
            selectedStage.text = "�n�[�h";
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
        else if (PlayerPrefs.GetInt("Difficulty") == (int)enumDifficultyID.Hard)
        {
            Initiate.Fade("MainSceneHard", Color.black, 1.0f);
        }
    }

    // �m�F�I����������
    public void No()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
        canvas[(int)enumCanvas.Character].SetActive(true);
        canvas[(int)enumCanvas.Check].SetActive(false);
        characterdscripton = true;
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

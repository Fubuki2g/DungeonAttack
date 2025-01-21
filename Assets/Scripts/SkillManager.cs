using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillManager : Singleton<SkillManager>
{
    public SkillValueManager skillValueManager; // �X�L���̊e�l�Ǘ��p�̃X�N���v�g

    public TextMeshProUGUI mainText; // �e�L�X�g�\��
    public GameObject[] useSkillButton; // �X�L���g�p���̃{�^��
    public enum enumSkillButton
    {
        Skill1, // �X�L���P�̃{�^��
        Skill2, // �X�L���Q�̃{�^��
        Skill3, // �X�L���R�̃{�^��
        Num // �X�L���{�^���̐�
    }
    public TextMeshProUGUI[] useSkillButtonText; // �\������X�L����
    public enum enumUseSkillButtontext
    {
        Skill1, // �X�L���P�̖��O
        Skill2, // �X�L���Q�̖��O
        Skill3, // �X�L���R�̖��O
        Num // �X�L�����\����
    }
    public bool skillDescriptionDisplay; // �X�L��������\�����邩�ǂ���
    public bool[] useSkill; // �ǂ̃X�L�����g�p���邩
    public enum enumUseSkill
    {
        PowerAttack, // �p���[�A�^�b�N
        PowerUp, // �p���[�A�b�v
        Meditation, // �ґz
        FireBall, // �t�@�C�A�{�[��
        IceLance, // �A�C�X�����X
        HealMagic, // �q�[��
        PoisonKnife, // �|�C�Y���i�C�t
        PoisonBurn, // �|�C�Y���o�[��
        PoisonDrain, // �|�C�Y���h���C��
        Num // �ǂ̃X�L�����g�����̐�
    }

    private void Start()
    {
        // ScriptableObject�̓ǂݍ���
        skillValueManager = Resources.Load<SkillValueManager>("ScriptableObject/SkillValueManager");

        // �e�z��̏�����
        useSkillButton = new GameObject[(int)enumSkillButton.Num];
        useSkillButtonText = new TextMeshProUGUI[(int)enumUseSkillButtontext.Num];
        useSkill = new bool[(int)enumUseSkill.Num];

        // �eUI�I�u�W�F�N�g�̓ǂݍ���
        mainText = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>();

        useSkillButton[(int)enumSkillButton.Skill1] = GameObject.Find("SkillButton1");
        useSkillButton[(int)enumSkillButton.Skill2] = GameObject.Find("SkillButton2");
        useSkillButton[(int)enumSkillButton.Skill3] = GameObject.Find("SkillButton3");

        useSkillButtonText[(int)enumUseSkillButtontext.Skill1] = GameObject.Find("SkillButtonText1").GetComponent<TextMeshProUGUI>();
        useSkillButtonText[(int)enumUseSkillButtontext.Skill2] = GameObject.Find("SkillButtonText2").GetComponent<TextMeshProUGUI>();
        useSkillButtonText[(int)enumUseSkillButtontext.Skill3] = GameObject.Find("SkillButtonText3").GetComponent<TextMeshProUGUI>();

        // �f�[�^���X�g����X�L������ǂݍ���
        if (PlayerPrefs.GetInt("Character") == (int)TitleManager.enumCharacterID.Warrior)
        {
            useSkillButtonText[(int)enumUseSkillButtontext.Skill1].text = skillValueManager.DataList[0].skillName;
            useSkillButtonText[(int)enumUseSkillButtontext.Skill2].text = skillValueManager.DataList[1].skillName;
            useSkillButtonText[(int)enumUseSkillButtontext.Skill3].text = skillValueManager.DataList[2].skillName;
        }
        else if (PlayerPrefs.GetInt("Character") == (int)TitleManager.enumCharacterID.Magician)
        {
            useSkillButtonText[(int)enumUseSkillButtontext.Skill1].text = skillValueManager.DataList[3].skillName;
            useSkillButtonText[(int)enumUseSkillButtontext.Skill2].text = skillValueManager.DataList[4].skillName;
            useSkillButtonText[(int)enumUseSkillButtontext.Skill3].text = skillValueManager.DataList[5].skillName;
        }
        else if (PlayerPrefs.GetInt("Character") == (int)TitleManager.enumCharacterID.Thief)
        {
            useSkillButtonText[(int)enumUseSkillButtontext.Skill1].text = skillValueManager.DataList[6].skillName;
            useSkillButtonText[(int)enumUseSkillButtontext.Skill2].text = skillValueManager.DataList[7].skillName;
            useSkillButtonText[(int)enumUseSkillButtontext.Skill3].text = skillValueManager.DataList[8].skillName;
        }

        // ������\���I�u�W�F�N�g���\��
        useSkillButton[(int)enumSkillButton.Skill1].SetActive(false);
        useSkillButton[(int)enumSkillButton.Skill2].SetActive(false);
        useSkillButton[(int)enumSkillButton.Skill3].SetActive(false);
        if (PlayerPrefs.GetInt("Character") == (int)TitleManager.enumCharacterID.Magician || PlayerPrefs.GetInt("Character") ==  (int)TitleManager.enumCharacterID.Thief)
        {
            useSkillButton[(int)enumSkillButton.Skill1].SetActive(true);
        }

        BattleManager.Instance.windows[(int)BattleManager.enumWindows.SkillWindow].SetActive(false);

    }

    private void Update()
    {
        // �X�L���I�����ɃX�L���̌��ʂ�\������
        if (skillDescriptionDisplay)
        {
            if (PlayerPrefs.GetInt("Character") == (int)TitleManager.enumCharacterID.Warrior)
            {
                GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
                if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill1].gameObject)
                {
                    mainText.text = "�p���[�A�^�b�N:SP2\n�����͂����߂đ����\n�U���͂�1.5�{�̃_���[�W";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill2].gameObject)
                {
                    mainText.text = "�p���[�`���[�W:SP3\n�͂����߂Ď��̍U�����Q�{";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill3].gameObject)
                {
                    mainText.text = "�ґz:SP10\n�S��Â߂��ґz����\nHP��50��";
                }
                else if (BattleManager.Instance.defaultButton[(int)BattleManager.enumDefaultButton.SkillBackButton])
                {
                    mainText.text = "";
                }
            }
            else if (PlayerPrefs.GetInt("Character") == (int)TitleManager.enumCharacterID.Magician)
            {
                GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
                if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill1].gameObject)
                {
                    mainText.text = "�t�@�C�A�{�[��:SP3\n���̋��𑊎�ɕ���\n15�̌Œ�_���[�W";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill2].gameObject)
                {
                    mainText.text = "�A�C�X�����X:SP7\n�X�̑��𑊎�ɕ���\n30�̌Œ�_���[�W";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill3].gameObject)
                {
                    mainText.text = "�q�[��:SP10\n�����̎�����������\nHP��30��";
                }
                else if (BattleManager.Instance.defaultButton[(int)BattleManager.enumDefaultButton.SkillBackButton])
                {
                    mainText.text = "";
                }
            }
            else if (PlayerPrefs.GetInt("Character") == (int)TitleManager.enumCharacterID.Thief)
            {
                GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
                if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill1].gameObject)
                {
                    mainText.text = "�|�C�Y���i�C�t:SP2\n�G���m���œŏ�Ԃɂ���\n5�̌Œ�_���[�W";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill2].gameObject)
                {
                    mainText.text = "�|�C�Y���o�[��:SP3\n�ŏ�Ԃ̓G�ɑ�_���[�W\n30�̌Œ�_���[�W";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill3].gameObject)
                {
                    mainText.text = "�|�C�Y���h���C��:SP10\n�G�̓Ń_���[�W�ŉ񕜂���";
                }
                else if (BattleManager.Instance.defaultButton[(int)BattleManager.enumDefaultButton.SkillBackButton])
                {
                    mainText.text = "";
                }
            }
        }

    }

    // �X�L���g�p�̏���
    public IEnumerator UseSkill()
    {
        if (useSkill[(int)enumUseSkill.PowerAttack])
        {
            if (BattleManager.Instance.playerSP < skillValueManager.DataList[0].needSkillValue)
            {
                yield return StartCoroutine(NotEnoughSP());
            }
            else
            {
                useSkill[(int)enumUseSkill.PowerAttack] = false;
                yield return StartCoroutine(PowerAttack());
            }
        }
        else if (useSkill[(int)enumUseSkill.PowerUp])
        {
            if (BattleManager.Instance.playerSP < skillValueManager.DataList[1].needSkillValue)
            {
                yield return StartCoroutine(NotEnoughSP());
            }
            else if (BattleManager.Instance.powerUp == true)
            {
                mainText.text = "���łɍU���͂��オ���Ă���I";

                yield return StartCoroutine(NextProcess(1.0f));

                yield return StartCoroutine(BattleManager.Instance.Battle());
            }
            else
            {
                useSkill[(int)enumUseSkill.PowerUp] = false;
                yield return StartCoroutine(PowerUp());
            }
        }
        else if (useSkill[(int)enumUseSkill.Meditation])
        {
            if (BattleManager.Instance.playerSP < skillValueManager.DataList[2].needSkillValue)
            {
                yield return StartCoroutine(NotEnoughSP());
            }
            else if (BattleManager.Instance.playerHP == BattleManager.Instance.playerMaxHP)
            {
                mainText.text = "HP�񕜂͕K�v�Ȃ��I";

                yield return StartCoroutine(NextProcess(1.0f));

                yield return StartCoroutine(BattleManager.Instance.Battle());
            }
            else
            {
                useSkill[(int)enumUseSkill.Meditation] = false;
                yield return StartCoroutine(Meditation());
            }
        }
        else if (useSkill[(int)enumUseSkill.FireBall])
        {
            if (BattleManager.Instance.playerSP < skillValueManager.DataList[3].needSkillValue)
            {
                yield return StartCoroutine(NotEnoughSP());
            }
            else
            {
                useSkill[(int)enumUseSkill.FireBall] = false;
                yield return StartCoroutine(FireBall());
            }
        }
        else if (useSkill[(int)enumUseSkill.IceLance])
        {
            if (BattleManager.Instance.playerSP < skillValueManager.DataList[4].needSkillValue)
            {
                yield return StartCoroutine(NotEnoughSP());
            }
            else
            {
                useSkill[(int)enumUseSkill.IceLance] = false;
                yield return StartCoroutine(IceLance());
            }
        }
        else if (useSkill[(int)enumUseSkill.HealMagic])
        {
            if (BattleManager.Instance.playerSP < skillValueManager.DataList[5].needSkillValue)
            {
                yield return StartCoroutine(NotEnoughSP());
            }
            else if (BattleManager.Instance.playerHP == BattleManager.Instance.playerMaxHP)
            {
                mainText.text = "HP�񕜂͕K�v�Ȃ��I";

                yield return StartCoroutine(NextProcess(1.0f));

                yield return StartCoroutine(BattleManager.Instance.Battle());
            }
            else
            {
                useSkill[(int)enumUseSkill.HealMagic] = false;
                yield return StartCoroutine(HealMagic());
            }
        }
        else if (useSkill[(int)enumUseSkill.PoisonKnife])
        {
            if (BattleManager.Instance.playerSP < skillValueManager.DataList[6].needSkillValue)
            {
                yield return StartCoroutine(NotEnoughSP());
            }
            else
            {
                useSkill[(int)enumUseSkill.PoisonKnife] = false;
                yield return StartCoroutine(PoisonKnife());
            }
        }
        else if (useSkill[(int)enumUseSkill.PoisonBurn])
        {
            if (BattleManager.Instance.playerSP < skillValueManager.DataList[7].needSkillValue)
            {
                yield return StartCoroutine(NotEnoughSP());
            }
            else if (!BattleManager.Instance.poison)
            {
                mainText.text = "�G�͓ŏ�Ԃł͂Ȃ�";

                yield return StartCoroutine(NextProcess(1.0f));

                yield return StartCoroutine(BattleManager.Instance.Battle());
            }
            else
            {
                useSkill[(int)enumUseSkill.PoisonBurn] = false;
                yield return StartCoroutine(PoisonBurn());
            }
        }
        else if (useSkill[(int)enumUseSkill.PoisonDrain])
        {
            if (BattleManager.Instance.playerSP < skillValueManager.DataList[8].needSkillValue)
            {
                yield return StartCoroutine(NotEnoughSP());
            }
            else
            {
                useSkill[(int)enumUseSkill.PoisonDrain] = false;
                yield return StartCoroutine(PoisonDrain());
            }
        }

    }

    // SP�s�����̏���
    public IEnumerator NotEnoughSP()
    {
        mainText.text = "SP������Ȃ��I";

        yield return StartCoroutine(NextProcess(1.0f));

        yield return StartCoroutine(BattleManager.Instance.Battle());
    }

    // �p���[�A�^�b�N
    public IEnumerator PowerAttack()
    {
        mainText.text = "�p���[�A�A�^�b�N���g�����I";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.PowerAttack);
        FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
        mainText.text = $"{(int)(BattleManager.Instance.playerATK*skillValueManager.DataList[0].skillValue)}�̃_���[�W�I";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[0].needSkillValue;
        BattleManager.Instance.enemyHP -= (int)(BattleManager.Instance.playerATK * skillValueManager.DataList[0].skillValue);
        if (BattleManager.Instance.enemyHP < 0)
        {
            BattleManager.Instance.enemyHP = 0;
        }

        if(BattleManager.Instance.powerUp)
        {
            BattleManager.Instance.powerUp = false;
            BattleManager.Instance.playerATK = BattleManager.Instance.baseAttack;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        if (BattleManager.Instance.enemyHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerWin());
        }
        else
        {
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }

    }

    // �p���[�`���[�W
    public IEnumerator PowerUp()
    {
        mainText.text = "�p���[�`���[�W���g�����I";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.PowerCharge);
        FlashManager.Instance.FlashScreen(Color.yellow, 0.3f);
        mainText.text = "�U���͂�2�{�ɂȂ����I";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[1].needSkillValue;

        BattleManager.Instance.powerUp = true;
        BattleManager.Instance.baseAttack = BattleManager.Instance.playerATK;
        BattleManager.Instance.playerATK *= 2;

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
    }

    // �ґz
    public IEnumerator Meditation()
    {
        mainText.text = "�ґz���g�����I";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
        FlashManager.Instance.FlashScreen(new Color(0.5f, 1f, 0f), 0.3f);
        mainText.text = $"HP��{skillValueManager.DataList[2].skillValue}�񕜂����I";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[2].needSkillValue;
        BattleManager.Instance.playerHP += (int)skillValueManager.DataList[2].skillValue;
        if (BattleManager.Instance.playerHP > BattleManager.Instance.playerMaxHP)
        {
            BattleManager.Instance.playerHP = BattleManager.Instance.playerMaxHP;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);

    }

    // �t�@�C�A�{�[��
    public IEnumerator FireBall()
    {
        mainText.text = "�t�@�C�A�{�[�����������I";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.FireBall);
        FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
        mainText.text = $"{skillValueManager.DataList[3].skillValue}�̃_���[�W�I";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[3].needSkillValue;
        BattleManager.Instance.enemyHP -= (int)skillValueManager.DataList[3].skillValue;
        if (BattleManager.Instance.enemyHP < 0)
        {
            BattleManager.Instance.enemyHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        if (BattleManager.Instance.enemyHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerWin());
        }
        else
        {
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }
    }

    // �A�C�X�����X
    public IEnumerator IceLance()
    {
        mainText.text = "�A�C�X�����X���������I";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.IceLance);
        FlashManager.Instance.EnemyFlash(Color.cyan, 0.3f);
        mainText.text = $"{skillValueManager.DataList[4].skillValue}�̃_���[�W�I";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[4].needSkillValue;
        BattleManager.Instance.enemyHP -= (int)skillValueManager.DataList[4].skillValue;
        if (BattleManager.Instance.enemyHP < 0)
        {
            BattleManager.Instance.enemyHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        if (BattleManager.Instance.enemyHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerWin());
        }
        else
        {
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }
    }

    // �q�[��
    public IEnumerator HealMagic()
    {
        mainText.text = "�q�[�����������I";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
        FlashManager.Instance.FlashScreen(new Color(0.5f, 1f, 0f), 0.3f);
        mainText.text = $"HP��{skillValueManager.DataList[5].skillValue}�񕜂����I";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[5].needSkillValue;
        BattleManager.Instance.playerHP += (int)skillValueManager.DataList[5].skillValue;
        if (BattleManager.Instance.playerHP > BattleManager.Instance.playerMaxHP)
        {
            BattleManager.Instance.playerHP = BattleManager.Instance.playerMaxHP;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
    }

    // �|�C�Y���i�C�t
    public IEnumerator PoisonKnife()
    {
        mainText.text = "�|�C�Y���i�C�t���g�����I";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.PowerAttack);
        FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
        mainText.text = $"{skillValueManager.DataList[6].skillValue}�̃_���[�W�I";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[6].needSkillValue;
        BattleManager.Instance.enemyHP -= (int)skillValueManager.DataList[6].skillValue;
        if (BattleManager.Instance.enemyHP < 0)
        {
            BattleManager.Instance.enemyHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        if (BattleManager.Instance.enemyHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerWin());
        }
        else
        {
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }

        if (Random.value <= 0.5f && !BattleManager.Instance.poison)
        {
            mainText.text = "�G�͓ŏ�ԂɂȂ����I";
            BattleManager.Instance.poison = true;

            // ���̏����ɐi��
            yield return StartCoroutine(NextProcess(1.0f));
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }
    }

    // �|�C�Y���o�[��
    public IEnumerator PoisonBurn()
    {
        mainText.text = "�|�C�Y���o�[�����g�����I";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.IceLance);
        FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
        mainText.text = $"{skillValueManager.DataList[7].skillValue}�̃_���[�W�I";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[7].needSkillValue;
        BattleManager.Instance.enemyHP -= (int)skillValueManager.DataList[7].skillValue;
        if (BattleManager.Instance.enemyHP < 0)
        {
            BattleManager.Instance.enemyHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);

        mainText.text = "�G�̓ł��������I";
        BattleManager.Instance.poison = false;

        yield return StartCoroutine(NextProcess(1.0f));

        if (BattleManager.Instance.enemyHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerWin());
        }
        else
        {
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }
    }

    // �|�C�Y���h���C��
    public IEnumerator PoisonDrain()
    {
        mainText.text = "�|�C�Y���h���C�����g�����I";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
        FlashManager.Instance.FlashScreen(new Color(0.5f, 1f, 0f), 0.3f);
        mainText.text = $"�G�̓Ń_���[�W��\n�񕜂���悤�ɂȂ����I";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[8].needSkillValue;
        BattleManager.Instance.poisonDrain = true;

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
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

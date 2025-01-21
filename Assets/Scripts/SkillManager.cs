using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillManager : Singleton<SkillManager>
{
    public SkillValueManager skillValueManager; // スキルの各値管理用のスクリプト

    public TextMeshProUGUI mainText; // テキスト表示
    public GameObject[] useSkillButton; // スキル使用時のボタン
    public enum enumSkillButton
    {
        Skill1, // スキル１のボタン
        Skill2, // スキル２のボタン
        Skill3, // スキル３のボタン
        Num // スキルボタンの数
    }
    public TextMeshProUGUI[] useSkillButtonText; // 表示するスキル名
    public enum enumUseSkillButtontext
    {
        Skill1, // スキル１の名前
        Skill2, // スキル２の名前
        Skill3, // スキル３の名前
        Num // スキル名表示数
    }
    public bool skillDescriptionDisplay; // スキル説明を表示するかどうか
    public bool[] useSkill; // どのスキルを使用するか
    public enum enumUseSkill
    {
        PowerAttack, // パワーアタック
        PowerUp, // パワーアップ
        Meditation, // 瞑想
        FireBall, // ファイアボール
        IceLance, // アイスランス
        HealMagic, // ヒール
        PoisonKnife, // ポイズンナイフ
        PoisonBurn, // ポイズンバーン
        PoisonDrain, // ポイズンドレイン
        Num // どのスキルを使うかの数
    }

    private void Start()
    {
        // ScriptableObjectの読み込み
        skillValueManager = Resources.Load<SkillValueManager>("ScriptableObject/SkillValueManager");

        // 各配列の初期化
        useSkillButton = new GameObject[(int)enumSkillButton.Num];
        useSkillButtonText = new TextMeshProUGUI[(int)enumUseSkillButtontext.Num];
        useSkill = new bool[(int)enumUseSkill.Num];

        // 各UIオブジェクトの読み込み
        mainText = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>();

        useSkillButton[(int)enumSkillButton.Skill1] = GameObject.Find("SkillButton1");
        useSkillButton[(int)enumSkillButton.Skill2] = GameObject.Find("SkillButton2");
        useSkillButton[(int)enumSkillButton.Skill3] = GameObject.Find("SkillButton3");

        useSkillButtonText[(int)enumUseSkillButtontext.Skill1] = GameObject.Find("SkillButtonText1").GetComponent<TextMeshProUGUI>();
        useSkillButtonText[(int)enumUseSkillButtontext.Skill2] = GameObject.Find("SkillButtonText2").GetComponent<TextMeshProUGUI>();
        useSkillButtonText[(int)enumUseSkillButtontext.Skill3] = GameObject.Find("SkillButtonText3").GetComponent<TextMeshProUGUI>();

        // データリストからスキル名を読み込み
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

        // 初期非表示オブジェクトを非表示
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
        // スキル選択時にスキルの効果を表示する
        if (skillDescriptionDisplay)
        {
            if (PlayerPrefs.GetInt("Character") == (int)TitleManager.enumCharacterID.Warrior)
            {
                GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
                if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill1].gameObject)
                {
                    mainText.text = "パワーアタック:SP2\n強い力を込めて相手に\n攻撃力の1.5倍のダメージ";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill2].gameObject)
                {
                    mainText.text = "パワーチャージ:SP3\n力をためて次の攻撃が２倍";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill3].gameObject)
                {
                    mainText.text = "瞑想:SP10\n心を静めて瞑想する\nHPを50回復";
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
                    mainText.text = "ファイアボール:SP3\n炎の球を相手に放つ\n15の固定ダメージ";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill2].gameObject)
                {
                    mainText.text = "アイスランス:SP7\n氷の槍を相手に放つ\n30の固定ダメージ";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill3].gameObject)
                {
                    mainText.text = "ヒール:SP10\n癒しの呪文を唱える\nHPを30回復";
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
                    mainText.text = "ポイズンナイフ:SP2\n敵を確率で毒状態にする\n5の固定ダメージ";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill2].gameObject)
                {
                    mainText.text = "ポイズンバーン:SP3\n毒状態の敵に大ダメージ\n30の固定ダメージ";
                }
                else if (selectedObject != null && selectedObject == useSkillButton[(int)enumSkillButton.Skill3].gameObject)
                {
                    mainText.text = "ポイズンドレイン:SP10\n敵の毒ダメージで回復する";
                }
                else if (BattleManager.Instance.defaultButton[(int)BattleManager.enumDefaultButton.SkillBackButton])
                {
                    mainText.text = "";
                }
            }
        }

    }

    // スキル使用の処理
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
                mainText.text = "すでに攻撃力が上がっている！";

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
                mainText.text = "HP回復は必要ない！";

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
                mainText.text = "HP回復は必要ない！";

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
                mainText.text = "敵は毒状態ではない";

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

    // SP不足時の処理
    public IEnumerator NotEnoughSP()
    {
        mainText.text = "SPが足りない！";

        yield return StartCoroutine(NextProcess(1.0f));

        yield return StartCoroutine(BattleManager.Instance.Battle());
    }

    // パワーアタック
    public IEnumerator PowerAttack()
    {
        mainText.text = "パワーアアタックを使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.PowerAttack);
        FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
        mainText.text = $"{(int)(BattleManager.Instance.playerATK*skillValueManager.DataList[0].skillValue)}のダメージ！";

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

    // パワーチャージ
    public IEnumerator PowerUp()
    {
        mainText.text = "パワーチャージを使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.PowerCharge);
        FlashManager.Instance.FlashScreen(Color.yellow, 0.3f);
        mainText.text = "攻撃力が2倍になった！";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[1].needSkillValue;

        BattleManager.Instance.powerUp = true;
        BattleManager.Instance.baseAttack = BattleManager.Instance.playerATK;
        BattleManager.Instance.playerATK *= 2;

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
    }

    // 瞑想
    public IEnumerator Meditation()
    {
        mainText.text = "瞑想を使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
        FlashManager.Instance.FlashScreen(new Color(0.5f, 1f, 0f), 0.3f);
        mainText.text = $"HPを{skillValueManager.DataList[2].skillValue}回復した！";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[2].needSkillValue;
        BattleManager.Instance.playerHP += (int)skillValueManager.DataList[2].skillValue;
        if (BattleManager.Instance.playerHP > BattleManager.Instance.playerMaxHP)
        {
            BattleManager.Instance.playerHP = BattleManager.Instance.playerMaxHP;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);

    }

    // ファイアボール
    public IEnumerator FireBall()
    {
        mainText.text = "ファイアボールを唱えた！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.FireBall);
        FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
        mainText.text = $"{skillValueManager.DataList[3].skillValue}のダメージ！";

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

    // アイスランス
    public IEnumerator IceLance()
    {
        mainText.text = "アイスランスを唱えた！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.IceLance);
        FlashManager.Instance.EnemyFlash(Color.cyan, 0.3f);
        mainText.text = $"{skillValueManager.DataList[4].skillValue}のダメージ！";

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

    // ヒール
    public IEnumerator HealMagic()
    {
        mainText.text = "ヒールを唱えた！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
        FlashManager.Instance.FlashScreen(new Color(0.5f, 1f, 0f), 0.3f);
        mainText.text = $"HPが{skillValueManager.DataList[5].skillValue}回復した！";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[5].needSkillValue;
        BattleManager.Instance.playerHP += (int)skillValueManager.DataList[5].skillValue;
        if (BattleManager.Instance.playerHP > BattleManager.Instance.playerMaxHP)
        {
            BattleManager.Instance.playerHP = BattleManager.Instance.playerMaxHP;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
    }

    // ポイズンナイフ
    public IEnumerator PoisonKnife()
    {
        mainText.text = "ポイズンナイフを使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.PowerAttack);
        FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
        mainText.text = $"{skillValueManager.DataList[6].skillValue}のダメージ！";

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
            mainText.text = "敵は毒状態になった！";
            BattleManager.Instance.poison = true;

            // 次の処理に進む
            yield return StartCoroutine(NextProcess(1.0f));
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }
    }

    // ポイズンバーン
    public IEnumerator PoisonBurn()
    {
        mainText.text = "ポイズンバーンを使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.IceLance);
        FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
        mainText.text = $"{skillValueManager.DataList[7].skillValue}のダメージ！";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[7].needSkillValue;
        BattleManager.Instance.enemyHP -= (int)skillValueManager.DataList[7].skillValue;
        if (BattleManager.Instance.enemyHP < 0)
        {
            BattleManager.Instance.enemyHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);

        mainText.text = "敵の毒が解けた！";
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

    // ポイズンドレイン
    public IEnumerator PoisonDrain()
    {
        mainText.text = "ポイズンドレインを使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
        FlashManager.Instance.FlashScreen(new Color(0.5f, 1f, 0f), 0.3f);
        mainText.text = $"敵の毒ダメージで\n回復するようになった！";

        BattleManager.Instance.playerSP -= skillValueManager.DataList[8].needSkillValue;
        BattleManager.Instance.poisonDrain = true;

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
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

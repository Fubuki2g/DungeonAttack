using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public ItemValueManager itemValueManager; // アイテムの各値管理用のスクリプト

    public TextMeshProUGUI mainText; // テキスト表示
    public bool haveItem; // アイテムを所持しているかどうか
    public bool[] getItem; // どのアイテムを手に入れたか
    public enum enumGetItem
    {
        HPPotion, // HPポーション
        SPPotion, // SPポーション
        ATKPotion, // 攻撃ポーション
        HealHerb, // 薬草
        DamageBomb, // 爆弾
        ATKJewel, // 攻撃の宝石
        Num // どのアイテムを手に入れたかの要素数
    }
    public int[] itemValue; // アイテム使用時の効果量
    public TextMeshProUGUI itemText; // アイテム名表示用テキスト

    private void Start()
    {
        // ScriptableObjectの読み込み
        itemValueManager = Resources.Load<ItemValueManager>("ScriptableObject/ItemValueManager");

        // メインテキストとアイテム名を表示するテキストのUIオブジェクトの読み込み
        mainText = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>();
        itemText = GameObject.Find("ItemText").GetComponent<TextMeshProUGUI>();

        // 各配列の初期化
        getItem = new bool[(int)enumGetItem.Num];
        itemText.text = itemValueManager.DataList[(int)enumGetItem.Num].itemName;
    }

    // どのアイテムを所持しているかの処理
    public IEnumerator HaveItem()
    {
        if (getItem[(int)enumGetItem.HPPotion])
        {
            if (BattleManager.Instance.playerHP == BattleManager.Instance.playerMaxHP)
            {
                mainText.text = "HP回復は必要ない！";

                yield return StartCoroutine(NextProcess(1.0f));
                yield return StartCoroutine(BattleManager.Instance.Battle());

            }

            getItem[(int)enumGetItem.HPPotion] = false;
            yield return StartCoroutine(HPPotion());
        }
        else if (getItem[(int)enumGetItem.SPPotion])
        {
            if (BattleManager.Instance.playerSP == BattleManager.Instance.playerMaxSP)
            {
                mainText.text = "SP回復は必要ない！";

                yield return StartCoroutine(NextProcess(1.0f));
                yield return StartCoroutine(BattleManager.Instance.Battle());

            }

            getItem[(int)enumGetItem.SPPotion] = false;
            yield return StartCoroutine(SPPotion());
        }
        else if (getItem[(int)enumGetItem.ATKPotion])
        {
            if (BattleManager.Instance.powerUp)
            {
                mainText.text = "すでに攻撃力が上がっている！";

                yield return StartCoroutine(NextProcess(1.0f));
                yield return StartCoroutine(BattleManager.Instance.Battle());

            }

            getItem[(int)enumGetItem.ATKPotion] = false;
            yield return StartCoroutine(ATKPotion());
        }
        else if (getItem[(int)enumGetItem.HealHerb])
        {
            if (BattleManager.Instance.playerHP == BattleManager.Instance.playerMaxHP)
            {
                mainText.text = "HP回復は必要ない！";

                yield return StartCoroutine(NextProcess(1.0f));
                yield return StartCoroutine(BattleManager.Instance.Battle());

            }

            getItem[(int)enumGetItem.HealHerb] = false;
            yield return StartCoroutine(HealHerb());
        }
        else if (getItem[(int)enumGetItem.DamageBomb])
        {
            getItem[(int)enumGetItem.DamageBomb] = false;
            yield return StartCoroutine(DamageBomb());
        }
        else if (getItem[(int)enumGetItem.ATKJewel])
        {
            if (BattleManager.Instance.powerUp)
            {
                mainText.text = "すでに攻撃力が上がっている！";

                yield return StartCoroutine(NextProcess(1.0f));
                yield return StartCoroutine(BattleManager.Instance.Battle());

            }

            getItem[(int)enumGetItem.ATKJewel] = false;
            yield return StartCoroutine(ATKJewel());
        }

    }

    // HPポーション
    public IEnumerator HPPotion()
    {
        mainText.text = $"{itemValueManager.DataList[0].itemName}を使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
        FlashManager.Instance.FlashScreen(new Color(0.5f, 1f, 0f), 0.3f);
        mainText.text = $"HPが{itemValueManager.DataList[0].itemValue}回復した！";

        BattleManager.Instance.playerHP += itemValueManager.DataList[0].itemValue;
        if (BattleManager.Instance.playerHP > BattleManager.Instance.playerMaxHP)
        {
            BattleManager.Instance.playerHP = BattleManager.Instance.playerMaxHP;
        }
        haveItem = false;

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
    }

    // SPポーション
    public IEnumerator SPPotion()
    {
        mainText.text = $"{itemValueManager.DataList[1].itemName}を使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        FlashManager.Instance.FlashScreen(Color.cyan, 0.3f);
        mainText.text = $"SPが{itemValueManager.DataList[1].itemValue}回復した！";

        BattleManager.Instance.playerSP += itemValueManager.DataList[1].itemValue;
        if (BattleManager.Instance.playerSP > BattleManager.Instance.playerMaxSP)
        {
            BattleManager.Instance.playerSP = BattleManager.Instance.playerMaxSP;
        }
        haveItem = false;

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
    }

    // 攻撃ポーション
    public IEnumerator ATKPotion()
    {
        mainText.text = $"{itemValueManager.DataList[2].itemName}を使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.PowerCharge);
        FlashManager.Instance.FlashScreen(Color.yellow, 0.3f);
        mainText.text = "攻撃力が２倍になった！";
        BattleManager.Instance.powerUp = true;
        BattleManager.Instance.baseAttack = BattleManager.Instance.playerATK;
        BattleManager.Instance.playerATK *= itemValueManager.DataList[2].itemValue;
        haveItem = false;

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
    }

    // 癒し草
    public IEnumerator HealHerb()
    {
        mainText.text = $"{itemValueManager.DataList[3].itemName}を使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
        FlashManager.Instance.FlashScreen(new Color(0.5f, 1f, 0f), 0.3f);
        mainText.text = $"HPが{itemValueManager.DataList[3].itemValue}回復した！";

        BattleManager.Instance.playerHP += itemValueManager.DataList[3].itemValue;
        if (BattleManager.Instance.playerHP > BattleManager.Instance.playerMaxHP)
        {
            BattleManager.Instance.playerHP = BattleManager.Instance.playerMaxHP;
        }
        haveItem = false;

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
    }

    // 爆弾
    public IEnumerator DamageBomb()
    {
        mainText.text = $"{itemValueManager.DataList[4].itemName}を使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Bomb);
        FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
        mainText.text = $"{itemValueManager.DataList[4].itemValue}のダメージ！";

        BattleManager.Instance.enemyHP -= itemValueManager.DataList[4].itemValue;
        if (BattleManager.Instance.enemyHP <= 0)
        {
            BattleManager.Instance.enemyHP = 0;
        }
        haveItem = false;

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

    // 攻撃ジュエル
    public IEnumerator ATKJewel()
    {
        mainText.text = $"{itemValueManager.DataList[5].itemName}を使った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.PowerCharge);
        FlashManager.Instance.FlashScreen(Color.yellow, 0.3f);
        mainText.text = "攻撃力が3倍になった！";
        BattleManager.Instance.powerUp = true;
        BattleManager.Instance.baseAttack = BattleManager.Instance.playerATK;
        BattleManager.Instance.playerATK *= itemValueManager.DataList[2].itemValue;
        haveItem = false;

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

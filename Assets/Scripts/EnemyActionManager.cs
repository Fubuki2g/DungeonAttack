using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyActionManager : Singleton<EnemyActionManager>
{
    public EnemyStatusManager enemyStatusManager; // スキルの各値管理用のスクリプト

    public TextMeshProUGUI battleText; // テキスト表示

    // 敵ごとの分岐
    public IEnumerator EnemyAction()
    {
        if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[0].eNAME)
        {
            yield return StartCoroutine(Slime());
        }
        else if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[1].eNAME)
        {
            yield return StartCoroutine(IkeBat());
        }
        else if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[2].eNAME)
        {
            yield return StartCoroutine(HatGhost());
        }
        else if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[3].eNAME)
        {
            yield return StartCoroutine(GodADeath());
        }
        else if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[4].eNAME)
        {
            yield return StartCoroutine(TornadoMan());
        }
        else if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[5].eNAME)
        {
            yield return StartCoroutine(ThunderOni());
        }
        else if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[6].eNAME)
        {
            yield return StartCoroutine(InfernoButterfly());
        }
        else if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[7].eNAME)
        {
            yield return StartCoroutine(DarkDragon());
        }
        else if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[8].eNAME)
        {
            yield return StartCoroutine(IceDragon());
        }
        else if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[9].eNAME)
        {
            yield return StartCoroutine(BabyDragon());
        }
        else if (BattleManager.Instance.enemyName == enemyStatusManager.DataList[10].eNAME)
        {
            yield return StartCoroutine(LightDragon());
        }

    }

    private void Start()
    {
        // ScriptableObjectの読み込み
        enemyStatusManager = Resources.Load<EnemyStatusManager>("ScriptableObject/EnemyStatusManager");

        // メインテキストのUIオブジェクト読み込み
        battleText = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>();
    }

    // スライムの特殊攻撃
    public IEnumerator Slime()
    {
        battleText.text = $"{BattleManager.Instance.enemyName}は\nスライムショットを放った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.EnemySpecialAttack);
        FlashManager.Instance.FlashScreen(Color.green, 0.3f);
        battleText.text = $"{enemyStatusManager.DataList[0].skillValue1}のダメージ！";

        yield return new WaitForSeconds(0.5f);

        BattleManager.Instance.playerHP -= enemyStatusManager.DataList[0].skillValue1;
        if (BattleManager.Instance.playerHP < 0)
        {
            BattleManager.Instance.playerHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

    }

    // イケバットの特殊攻撃
    public IEnumerator IkeBat()
    {
        battleText.text = $"{BattleManager.Instance.enemyName}は\nフライングアタックを放った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.EnemySpecialAttack);
        FlashManager.Instance.FlashScreen(Color.black, 0.3f);
        battleText.text = $"{enemyStatusManager.DataList[1].skillValue1}のダメージ！";

        yield return new WaitForSeconds(0.5f);

        BattleManager.Instance.playerHP -= enemyStatusManager.DataList[1].skillValue1;
        if (BattleManager.Instance.playerHP < 0)
        {
            BattleManager.Instance.playerHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

    }

    // 帽子お化けの特殊攻撃
    public IEnumerator HatGhost()
    {
        battleText.text = $"{BattleManager.Instance.enemyName}は\nファイアボールを放った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.FireBall);
        FlashManager.Instance.FlashScreen(Color.red, 0.3f);
        battleText.text = $"{enemyStatusManager.DataList[2].skillValue1}のダメージ！";

        yield return new WaitForSeconds(0.5f);

        BattleManager.Instance.playerHP -= enemyStatusManager.DataList[2].skillValue1;
        if (BattleManager.Instance.playerHP < 0)
        {
            BattleManager.Instance.playerHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

    }

    // 死神の特殊攻撃
    public IEnumerator GodADeath()
    {
        battleText.text = $"{BattleManager.Instance.enemyName}は\n鎌を振りかぶった！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Scythe);
        FlashManager.Instance.FlashScreen(Color.gray, 0.3f);
        battleText.text = $"{enemyStatusManager.DataList[3].skillValue1}のダメージ！";

        yield return new WaitForSeconds(0.5f);

        BattleManager.Instance.playerHP -= enemyStatusManager.DataList[3].skillValue1;
        if (BattleManager.Instance.playerHP < 0)
        {
            BattleManager.Instance.playerHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

    }

    // タツマキの特殊攻撃
    public IEnumerator TornadoMan()
    {
        battleText.text = $"{BattleManager.Instance.enemyName}は\n竜巻を巻き起こした！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.EnemySpecialAttack);
        FlashManager.Instance.FlashScreen(Color.white, 0.3f);
        battleText.text = $"{enemyStatusManager.DataList[4].skillValue1}のダメージ！";

        yield return new WaitForSeconds(0.5f);

        BattleManager.Instance.playerHP -= enemyStatusManager.DataList[4].skillValue1;
        if (BattleManager.Instance.playerHP < 0)
        {
            BattleManager.Instance.playerHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

    }

    // 雷太郎の特殊攻撃
    public IEnumerator ThunderOni()
    {
        battleText.text = $"{BattleManager.Instance.enemyName}は\n雷雲を呼んだ！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Thunder);
        FlashManager.Instance.FlashScreen(Color.yellow, 0.3f);
        battleText.text = $"{enemyStatusManager.DataList[5].skillValue1}のダメージ！";

        yield return new WaitForSeconds(0.5f);

        BattleManager.Instance.playerHP -= enemyStatusManager.DataList[5].skillValue1;
        if (BattleManager.Instance.playerHP < 0)
        {
            BattleManager.Instance.playerHP = 0;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

    }

    // 紅蓮蝶の特殊攻撃
    public IEnumerator InfernoButterfly()
    {
        int randomValue = Random.Range(0, 5);
        if (randomValue <= 3)
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\nファイアボールを唱えた！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.FireBall);
            FlashManager.Instance.FlashScreen(Color.red, 0.3f);
            battleText.text = $"{enemyStatusManager.DataList[6].skillValue1}のダメージ！";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.playerHP -= enemyStatusManager.DataList[6].skillValue1;
            if (BattleManager.Instance.playerHP < 0)
            {
                BattleManager.Instance.playerHP = 0;
            }
        }
        else
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\n火の粉を集めた！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
            FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
            battleText.text = $"{BattleManager.Instance.enemyName}は\nHPを{enemyStatusManager.DataList[7].skillValue2}回復した！";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.enemyHP += enemyStatusManager.DataList[7].skillValue1;
            if (BattleManager.Instance.enemyHP > BattleManager.Instance.enemyMaxHP)
            {
                BattleManager.Instance.enemyHP = BattleManager.Instance.enemyMaxHP;
            }

        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

    }

    // ダークドラゴンの特殊攻撃
    public IEnumerator DarkDragon()
    {
        int randomValue = Random.Range(0, 5);
        if (randomValue <= 2)
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\nダークブレスを放った！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Breath);
            FlashManager.Instance.FlashScreen(Color.magenta, 0.3f);
            battleText.text = $"{enemyStatusManager.DataList[7].skillValue1}のダメージ";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.playerHP -= enemyStatusManager.DataList[7].skillValue1;
            if (BattleManager.Instance.playerHP < 0)
            {
                BattleManager.Instance.playerHP = 0;
            }
        }
        else
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\nダークオーラを放った！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Auro);
            FlashManager.Instance.FlashScreen(Color.magenta, 0.3f);
            battleText.text = $"{enemyStatusManager.DataList[7].skillValue2}のダメージ！";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.playerHP -= enemyStatusManager.DataList[7].skillValue2;
            if (BattleManager.Instance.playerHP < 0)
            {
                BattleManager.Instance.playerHP = 0;
            }

        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

    }

    // アイスドラゴンの特殊攻撃
    public IEnumerator IceDragon()
    {
        int randomValue = Random.Range(0, 5);
        if (randomValue <= 2)
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\nアイスブレスを放った！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Breath);
            FlashManager.Instance.FlashScreen(Color.cyan, 0.3f);
            battleText.text = $"{enemyStatusManager.DataList[8].skillValue1}のダメージ！";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.playerHP -= enemyStatusManager.DataList[8].skillValue1;
            if (BattleManager.Instance.playerHP < 0)
            {
                BattleManager.Instance.playerHP = 0;
            }
        }
        else
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\nアイスレインを放った！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.IceLance);
            FlashManager.Instance.FlashScreen(Color.cyan, 0.3f);
            battleText.text = $"{enemyStatusManager.DataList[8].skillValue2}のダメージ！";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.playerHP -= enemyStatusManager.DataList[8].skillValue2;
            if (BattleManager.Instance.playerHP < 0)
            {
                BattleManager.Instance.playerHP = 0;
            }

        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

    }

    // ベビードラゴンの特殊攻撃
    public IEnumerator BabyDragon()
    {
        int randomValue = Random.Range(0, 3);
        if (randomValue <= 1)
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\nプチブレスを放った！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Breath);
            FlashManager.Instance.FlashScreen(Color.yellow, 0.3f);
            battleText.text = $"{enemyStatusManager.DataList[9].skillValue1}のダメージ！";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.playerHP -= enemyStatusManager.DataList[9].skillValue1;
            if (BattleManager.Instance.playerHP < 0)
            {
                BattleManager.Instance.playerHP = 0;
            }
        }
        else
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\n光を集めた！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
            FlashManager.Instance.EnemyFlash(Color.yellow, 0.3f);
            battleText.text = $"{BattleManager.Instance.enemyName}は\nHPを{enemyStatusManager.DataList[9].skillValue2}回復した！";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.enemyHP += enemyStatusManager.DataList[9].skillValue3;

        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

    }

    // ライトドラゴンの特殊攻撃
    public IEnumerator LightDragon()
    {
        int randomValue = Random.Range(0, 8);
        if (randomValue <= 4)
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\nライトニングブレスを放った！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Breath);
            FlashManager.Instance.FlashScreen(Color.yellow, 0.3f);
            battleText.text = $"{enemyStatusManager.DataList[10].skillValue1}のダメージ！";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.playerHP -= enemyStatusManager.DataList[10].skillValue1;
            if (BattleManager.Instance.playerHP < 0)
            {
                BattleManager.Instance.playerHP = 0;
            }
        }
        else if (randomValue <= 6)
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\nライトニングショットを放った！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.EnemySpecialAttack);
            FlashManager.Instance.FlashScreen(Color.yellow, 0.3f);
            battleText.text = $"{enemyStatusManager.DataList[10].skillValue2}のダメージ！";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.playerHP -= enemyStatusManager.DataList[10].skillValue2;
            if (BattleManager.Instance.playerHP < 0)
            {
                BattleManager.Instance.playerHP = 0;
            }

        }
        else
        {
            battleText.text = $"{BattleManager.Instance.enemyName}は\n光を集めた！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
            FlashManager.Instance.EnemyFlash(Color.yellow, 0.3f);
            battleText.text = $"{BattleManager.Instance.enemyName}は\nHPを{enemyStatusManager.DataList[10].skillValue3}回復した！";

            yield return new WaitForSeconds(0.5f);

            BattleManager.Instance.enemyHP += enemyStatusManager.DataList[10].skillValue3;

        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP == 0)
        {
            yield return StartCoroutine(BattleManager.Instance.PlayerLose());
        }

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

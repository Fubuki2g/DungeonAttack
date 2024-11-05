using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventManager : Singleton<EventManager>
{
    public TextMeshProUGUI mainText; // テキスト表示

    public Image floorImage; // フロア背景
    public Sprite[] floorSprite; // 背景画像
    public enum enumFloorSprite
    {
        Fountain, // 泉イベント
        Magic, // 魔法使いイベント
        Muscle, // 筋肉イベント
        Num // 背景数
    }

    private void Start()
    {
        // 配列の初期化
        floorSprite = new Sprite[(int)enumFloorSprite.Num];

        // メインテキストのUIオブジェクト読み込み
        mainText = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>();
        // 各背景画像のImage、Spriteの読み込み
        floorImage = GameObject.Find("FloorImage").GetComponent<Image>();
        floorSprite[(int)enumFloorSprite.Fountain] = Resources.Load<Sprite>("Images/FloorBacks/Fountain");
        floorSprite[(int)enumFloorSprite.Magic] = Resources.Load<Sprite>("Images/FloorBacks/Magic");
        floorSprite[(int)enumFloorSprite.Muscle] = Resources.Load<Sprite>("Images/FloorBacks/Muscle");
    }

    // 奇跡の泉イベント
    public IEnumerator Fountain()
    {
        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.EventFountain);
        floorImage.sprite = floorSprite[(int)enumFloorSprite.Fountain];

        mainText.text = "あなたは奇跡の泉を見つけた！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        mainText.text = "あなたは吸い込まれるように\n泉の水を飲んだ！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
        {
            mainText.text = "体力がみるみるうちに回復した！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
            FlashManager.Instance.FlashScreen(new Color(0.5f, 1f, 0f), 0.3f);
            mainText.text = "HPが50回復した！";
            BattleManager.Instance.playerHP += 50;
            if (BattleManager.Instance.playerMaxHP < BattleManager.Instance.playerHP)
            {
                BattleManager.Instance.playerHP = BattleManager.Instance.playerMaxHP;
            }

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);

        }
        else
        {
            mainText.text = "体が重くなるのを感じた";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.StatusDown);
            FlashManager.Instance.FlashScreen(Color.red, 0.3f);
            mainText.text = "攻撃力が１下がった";
            BattleManager.Instance.playerATK -= 1;

            if (BattleManager.Instance.playerATK < 1)
            {
                BattleManager.Instance.playerATK = 1;
            }

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }

        mainText.text = "あなたは泉を後にした";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        SoundManager.Instance.StopBGM();

    }

    // 魔法使いと出会うイベント
    public IEnumerator Magic()
    {
        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.EventMagic);
        floorImage.sprite = floorSprite[(int)enumFloorSprite.Magic];

        mainText.text = "あなたは魔法使いと出会った！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        mainText.text = "あなた！\nちょうどいいところにいた！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        mainText.text = "強化魔法の練習中なの\n練習させて！\nえい！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
        {
            mainText.text = "魔力があふれてくるのを感じた";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
            FlashManager.Instance.FlashScreen(Color.cyan, 0.3f);
            mainText.text = "最大SPが10増えた！";
            BattleManager.Instance.playerMaxSP += 10;
            BattleManager.Instance.playerSP += 10;

            yield return StartCoroutine(NextProcess(1.0f));
        }
        else
        {
            mainText.text = "魔力が吸われるのように感じた";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.StatusDown);
            FlashManager.Instance.FlashScreen(Color.red, 0.3f);
            mainText.text = "最大SPが5減った！";
            BattleManager.Instance.playerMaxSP -= 5;
            BattleManager.Instance.playerSP -= 5;

            if (BattleManager.Instance.playerMaxSP < 0)
            {
                BattleManager.Instance.playerMaxSP = 0;
                BattleManager.Instance.playerSP = 0;
            }
            else if (BattleManager.Instance.playerSP < 0)
            {
                BattleManager.Instance.playerSP = 0;
            }

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }

        mainText.text = "ご協力ありがとう！\nお礼は今度でいいよ！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        mainText.text = "魔法使いは霧のように消えた";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        SoundManager.Instance.StopBGM();
    }

    // 筋肉と出会うイベント
    public IEnumerator Muscle()
    {
        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.EventMuscle);
        floorImage.sprite = floorSprite[(int)enumFloorSprite.Muscle];

        mainText.text = "突然筋肉に囲まれた！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        mainText.text = "一緒に筋肉を鍛えよう！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        mainText.text = "あなたはトレーニングに参加した！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
        {
            mainText.text = "筋肉が研ぎ澄まされた！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.PowerCharge);
            FlashManager.Instance.FlashScreen(Color.yellow, 0.3f);
            mainText.text = "攻撃力が3上がった！";
            BattleManager.Instance.playerATK += 3;

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }
        else
        {
            mainText.text = "筋肉痛になった！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.StatusDown);
            FlashManager.Instance.FlashScreen(Color.red, 0.3f);
            mainText.text = "攻撃力が1下がった！";
            BattleManager.Instance.playerATK -= 1;

            if (BattleManager.Instance.playerATK < 1)
            {
                BattleManager.Instance.playerATK = 1;
            }

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }

        mainText.text = "筋肉は去っていった";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        SoundManager.Instance.StopBGM();
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

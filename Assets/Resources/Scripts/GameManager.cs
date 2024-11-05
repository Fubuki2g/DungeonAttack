using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public TextMeshProUGUI mainText; // テキスト表示
    public TextMeshProUGUI floorNumberText; // フロア数表示

    public float floorNumber = 0; // フロア数
    public int maxFloorNumber; // 最大フロア数

    public Image floorImage; // フロア背景
    public Sprite floorSprite; // フロア背景画像
    public Image floorIconImage; // フロアアイコン
    public Sprite[] floorIconSprite; // フロアアイコン画像
    public enum enumFloorIconSprite
    {
        BattleFloor, // 戦闘フロアアイコン
        StrongFloor, // 強敵フロアアイコン
        BossFloor, // ボスフロアアイコン
        ShopFloor, // ショップフロアアイコン
        EventFloor, // イベントフロアアイコン
        TreasureFloor, // 宝箱フロアアイコン
        RestFloor, // 休憩フロアアイコン
        Num // フロアアイコン画像数
    }

    void Start()
    {
        // 配列の初期化
        floorIconSprite = new Sprite[(int)enumFloorIconSprite.Num];

        // UIオブジェクトの読み込み
        mainText = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>();
        floorNumberText = GameObject.Find("FloorNumber").GetComponent<TextMeshProUGUI>();
        floorImage = GameObject.Find("FloorImage").GetComponent<Image>();
        floorSprite = Resources.Load<Sprite>("Images/FloorBacks/DefaultBack");
        floorIconImage = GameObject.Find("FloorIcon").GetComponent<Image>();

        // フロアアイコンのSpriteの読み込み
        floorIconSprite[(int)enumFloorIconSprite.BattleFloor] = Resources.Load<Sprite>("Images/FloorIcons/BattleFloor");
        floorIconSprite[(int)enumFloorIconSprite.StrongFloor] = Resources.Load<Sprite>("Images/FloorIcons/StrongFloor");
        floorIconSprite[(int)enumFloorIconSprite.BossFloor] = Resources.Load<Sprite>("Images/FloorIcons/BossFloor");
        floorIconSprite[(int)enumFloorIconSprite.ShopFloor] = Resources.Load<Sprite>("Images/FloorIcons/ShopFloor");
        floorIconSprite[(int)enumFloorIconSprite.EventFloor] = Resources.Load<Sprite>("Images/FloorIcons/EventFloor");
        floorIconSprite[(int)enumFloorIconSprite.TreasureFloor] = Resources.Load<Sprite>("Images/FloorIcons/TreasureFloor");
        floorIconSprite[(int)enumFloorIconSprite.RestFloor] = Resources.Load<Sprite>("Images/FloorIcons/RestFloor");

        // デフォルトのフロア数とフロア背景の表示
        floorNumberText.text = floorNumber.ToString();
        floorImage.sprite = floorSprite;

        // ゲーム開始のコルーチン
        StartCoroutine(StartAdventure());
    }

    // ゲーム開始時のコルーチン
    public IEnumerator StartAdventure()
    {
        mainText.text = "探索スタート！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (PlayerPrefs.GetInt("Difficulty") == (int)TitleManager.enumDifficultyID.Easy)
        {
            SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.StageEasy);
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)TitleManager.enumDifficultyID.Normal)
        {
            SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.StageNormal);
        }
        yield return StartCoroutine(NextFloor());

    }

    // 次のフロアに進むときのコルーチン
    public IEnumerator NextFloor()
    {
        mainText.text = "次のフロアへ";

        yield return StartCoroutine(NextProcess(0.5f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.StageChange);
        floorNumberText.text = (floorNumber += 1).ToString();

        yield return new WaitForSeconds(1.0f);

        // 最終フロアはボスフロア
        if (floorNumber == maxFloorNumber)
        {
            yield return StartCoroutine(BattleManager.Instance.BossStart());
        }
        // 奇数フロアは戦闘フロア
        else if (floorNumber % 2 != 0)
        {
            floorIconImage.sprite = floorIconSprite[(int)enumFloorIconSprite.BattleFloor];
            yield return StartCoroutine(BattleManager.Instance.BattleStart());
        }
        // 偶数フロアかつ全体の半分以上進んでいるか、イージーステージではない場合は以下からランダム
        else if (floorNumber < maxFloorNumber&& floorNumber > maxFloorNumber/2 && floorNumber % 2 == 0 || PlayerPrefs.GetInt("Difficulty") != (int)TitleManager.enumDifficultyID.Easy)
        {
            IEnumerator[] coroutines = new IEnumerator[]
            {
                ShopFloor(),
                EventFloor(),
                TreasureFloor(),
                RestFloor(),
                BattleManager.Instance.StrongStart()
            };

            int random = UnityEngine.Random.Range(0, coroutines.Length);
            yield return StartCoroutine(coroutines[random]);
        }
        // イージーステージの場合は以下からランダム
        else
        {
            IEnumerator[] coroutines = new IEnumerator[]
            {
                ShopFloor(),
                EventFloor(),
                TreasureFloor(),
                RestFloor(),
            };

            int random = UnityEngine.Random.Range(0, coroutines.Length);
            yield return StartCoroutine(coroutines[random]);
        }

    }

    // ショップフロアの処理
    public IEnumerator ShopFloor()
    {
        floorIconImage.sprite = floorIconSprite[(int)enumFloorIconSprite.ShopFloor];
        mainText.text = "ショップフロアだ！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.Shop);
        IEnumerator[] shopcoroutines = new IEnumerator[]
            {
                ShopManager.Instance.HPShop(),
                ShopManager.Instance.SPShop(),
                ShopManager.Instance.ATKShop()
            };

        int random = UnityEngine.Random.Range(0, shopcoroutines.Length);
        yield return StartCoroutine(shopcoroutines[random]);

        yield return StartCoroutine(NextFloor());

    }

    // イベントフロアの処理
    public IEnumerator EventFloor()
    {
        floorIconImage.sprite = floorIconSprite[(int)enumFloorIconSprite.EventFloor];
        mainText.text = "イベントフロアだ！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        IEnumerator[] eventcoroutines = new IEnumerator[]
            {
                EventManager.Instance.Fountain(),
                EventManager.Instance.Magic(),
                EventManager.Instance.Muscle()
            };

        int random = UnityEngine.Random.Range(0, eventcoroutines.Length);
        yield return StartCoroutine(eventcoroutines[random]);

        yield return StartCoroutine(NextFloor());

    }

    // 宝箱フロアの処理
    public IEnumerator TreasureFloor()
    {
        floorIconImage.sprite = floorIconSprite[(int)enumFloorIconSprite.TreasureFloor];
        mainText.text = "お宝フロアだ！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.Treasure);
        yield return StartCoroutine(TreasureManager.Instance.RandomItem());

        yield return StartCoroutine(NextFloor());

    }

    // 休憩フロアの処理
    public IEnumerator RestFloor()
    {
        floorIconImage.sprite = floorIconSprite[(int)enumFloorIconSprite.RestFloor];
        mainText.text = "休憩フロアだ！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.Rest);
        yield return StartCoroutine(RestManager.Instance.Rest());

        yield return StartCoroutine(NextFloor());

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

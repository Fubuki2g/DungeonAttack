using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BattleManager: Singleton<BattleManager>
{
    public EnemyStatusManager enemyStatusManager; // 敵ステータス用スクリプト
    public PlayerStatusManager playerStatusManager; // プレイヤー初期ステータス用スクリプト

    public string playerName;  // プレイヤー名
    public int playerLv; // プレイヤーレベル
    public int playerHP; // プレイヤーHP
    public int playerMaxHP; // プレイヤー最大HP
    public int playerSP; // プレイヤーSP
    public int playerMaxSP; // プレイヤー最大SP
    public int playerATK; // プレイヤー攻撃力
    public int playerEXP; // プレイヤー経験値
    public int playerNextLvEXP; // レベルアップまでの経験値
    public int[] skillSlot; // キャラクターごとの使用可能スキル
    public enum enumSkillSlot
    {
        Skill1,
        Skill2,
        Skill3,
        Num
    }

    public string enemyName; // 敵名
    public int enemyLv; // 敵レベル
    public int enemyHP; // 敵HP
    public int enemyMaxHP; // 敵最大HP
    public int enemyATK; // 敵攻撃力
    public int enemyEXP; // 敵経験値

    public int baseAttack; // 攻撃力上昇時の元の攻撃力
    public bool powerUp; // 攻撃力上昇状態を表す

    public bool bossBattle; // 現在がボスバトルかどうか

    public Image floorBackImage; // フロアの背景を当てはめるImageオブジェクト
    public Sprite floorBackSprite; // フロア画像
    public Image displayEnemyImage; // 敵を当てはめるImageオブジェクト
    public Sprite[] enemySprite; // 敵の画像
    public enum enumEnemySprite
    {
        Slime,
        IkeBat,
        HatGhost,
        GodADeath,
        Tornado,
        ThunderOni,
        InfernoButterfly,
        DarkDragon,
        IceDragon,
        BabyDragon,
        LightDragon,
        Num
    }
    private Sprite[] nowEnemySprite; // 現在戦っている敵の画像
    public Sprite noneEnemy; // 敵がいないときの画像
    public TextMeshProUGUI mainText; // メインのテキスト

    public TextMeshProUGUI[] playerStatusText; // 画面に表示するプレイヤーのステータス
    public enum enumPlayerStatusText
    {
        Name, // プレイヤー名.
        Lv, // プレイヤーレベル.
        HP, // プレイヤーHP.
        MaxHP, // プレイヤー最大HP 
        SP, // プレイヤーSP.
        MaxSP, // プレイヤー最大SP
        ATK, // プレイヤー攻撃力.
        Num // ステータス表示UIの個数.
    }

    public TextMeshProUGUI[] enemyStatusText; // 画面に表示する敵のステータス
    public enum enumEnemyStatusText
    {
        Name, // 敵名
        Lv, // 敵レベル
        HP, // 敵HP
        MaxHP, // 敵最大HP
        ATK, // 敵攻撃力
        Num // 敵のステータス数
    }

    public GameObject[] windows; // 各ウィンドウ
    public enum enumWindows
    {
        EnemyStatus, // 敵のステータスウィンドウ
        ItemWindow, // アイテムウィンドウ
        ComandWindow, // コマンドウィンドウ
        SkillWindow, // スキルウィンドウ
        ItemUseSelect, // バトル時アイテム使用選択ウィンドウ
        Num // ウィンドウ数
    }

    public GameObject[] defaultButton; // 選択ウィンドウでの初期選択ボタン
    public enum enumDefaultButton
    {
        AttackButton, // 攻撃ボタン
        SkillBackButton, // スキルウィンドウでの初期選択ボタン
        ItemUseBackButton, // アイテムの仕様確認での初期選択ボタン
        Num // 初期選択ボタンの数
    }

    public bool[] buttonOn; // バトル時に使用するボタンを押しているかどうか
    public enum enumButtonOn
    {
        Attack, // 攻撃コマンド
        Skill, // スキルコマンド
        Item, // アイテムコマンド
        Skill1, // スキル１使用
        Skill2, // スキル２使用
        Skill3, // スキル３使用
        ItemUse, // アイテム使用ボタン
        Back, // 戻るボタン
        Num // ボタンを押しているかどうかの数
    }
    public bool skillUse; // スキルコマンドを表示
    public bool itemUse; // アイテムコマンドを表示
    public bool skillBack; // スキルコマンド表示から戻る

    void Start()
    {
        // ScriptableObjectの読み込み
        enemyStatusManager = Resources.Load<EnemyStatusManager>("ScriptableObject/EnemyStatusManager");
        playerStatusManager = Resources.Load<PlayerStatusManager>("ScriptableObject/PlayerStatusManager");

        // 各配列の初期化
        skillSlot = new int[(int)enumSkillSlot.Num];
        enemySprite = new Sprite[(int)enumEnemySprite.Num];
        playerStatusText = new TextMeshProUGUI[(int)enumPlayerStatusText.Num];
        enemyStatusText = new TextMeshProUGUI[(int)enumEnemyStatusText.Num];
        windows = new GameObject[(int)enumWindows.Num];
        defaultButton = new GameObject[(int)enumDefaultButton.Num];
        buttonOn = new bool[(int)enumButtonOn.Num];

        // 表示するImageコンポーネントの取得と各Spriteの読み込み
        floorBackImage = GameObject.Find("FloorImage").GetComponent<Image>();
        floorBackSprite = Resources.Load<Sprite>("Images/FloorBacks/DefaultBack");
        displayEnemyImage = GameObject.Find("EnemyImage").GetComponent<Image>();
        noneEnemy = Resources.Load<Sprite>("Images/Enemys/Unknown");

        // メインテキストのUIオブジェクト読み込み
        mainText = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>();

        // 敵画像のSpriteの読み込み
        enemySprite[(int)enumEnemySprite.Slime] = Resources.Load<Sprite>("Images/Enemys/Slime");
        enemySprite[(int)enumEnemySprite.IkeBat] = Resources.Load<Sprite>("Images/Enemys/IkeBat");
        enemySprite[(int)enumEnemySprite.HatGhost] = Resources.Load<Sprite>("Images/Enemys/HatGhost");
        enemySprite[(int)enumEnemySprite.GodADeath] = Resources.Load<Sprite>("Images/Enemys/GodADeath");
        enemySprite[(int)enumEnemySprite.Tornado] = Resources.Load<Sprite>("Images/Enemys/Tornado");
        enemySprite[(int)enumEnemySprite.ThunderOni] = Resources.Load<Sprite>("Images/Enemys/ThunderOni");
        enemySprite[(int)enumEnemySprite.InfernoButterfly] = Resources.Load<Sprite>("Images/Enemys/InfernoButterfly");
        enemySprite[(int)enumEnemySprite.DarkDragon] = Resources.Load<Sprite>("Images/Enemys/DarkDragon");
        enemySprite[(int)enumEnemySprite.IceDragon] = Resources.Load<Sprite>("Images/Enemys/IceDragon");
        enemySprite[(int)enumEnemySprite.BabyDragon] = Resources.Load<Sprite>("Images/Enemys/BabyDragon");
        enemySprite[(int)enumEnemySprite.LightDragon] = Resources.Load<Sprite>("Images/Enemys/LightDragon");

        // プレイヤーステータスを表示するUIオブジェクトの読み込み
        playerStatusText[(int)enumPlayerStatusText.Name] = GameObject.Find("PlayerNameText").GetComponent<TextMeshProUGUI>();
        playerStatusText[(int)enumPlayerStatusText.Lv] = GameObject.Find("PlayerLvText").GetComponent<TextMeshProUGUI>();
        playerStatusText[(int)enumPlayerStatusText.HP] = GameObject.Find("PlayerHPText").GetComponent<TextMeshProUGUI>();
        playerStatusText[(int)enumPlayerStatusText.MaxHP] = GameObject.Find("PlayerMaxHPText").GetComponent<TextMeshProUGUI>();
        playerStatusText[(int)enumPlayerStatusText.SP] = GameObject.Find("PlayerSPText").GetComponent<TextMeshProUGUI>();
        playerStatusText[(int)enumPlayerStatusText.MaxSP] = GameObject.Find("PlayerMaxSPText").GetComponent<TextMeshProUGUI>();
        playerStatusText[(int)enumPlayerStatusText.ATK] = GameObject.Find("PlayerATKText").GetComponent<TextMeshProUGUI>();

        // 敵ステータスを表示するUIオブジェクトの読み込み
        enemyStatusText[(int)enumEnemyStatusText.Name] = GameObject.Find("EnemyNameText").GetComponent<TextMeshProUGUI>();
        enemyStatusText[(int)enumEnemyStatusText.Lv] = GameObject.Find("EnemyLvText").GetComponent<TextMeshProUGUI>();
        enemyStatusText[(int)enumEnemyStatusText.HP] = GameObject.Find("EnemyHPText").GetComponent<TextMeshProUGUI>();
        enemyStatusText[(int)enumEnemyStatusText.MaxHP] = GameObject.Find("EnemyMaxHPText").GetComponent<TextMeshProUGUI>();
        enemyStatusText[(int)enumEnemyStatusText.ATK] = GameObject.Find("EnemyATKText").GetComponent<TextMeshProUGUI>();

        // 表示を切り替えるウィンドウの読み込み
        windows[(int)enumWindows.EnemyStatus] = GameObject.Find("enemyStatusWindow");
        windows[(int)enumWindows.ItemWindow] = GameObject.Find("itemWindow");
        windows[(int)enumWindows.ComandWindow] = GameObject.Find("comandWindow");
        windows[(int)enumWindows.SkillWindow] = GameObject.Find("skillWindow");
        windows[(int)enumWindows.ItemUseSelect] = GameObject.Find("ItemSelectWindow");

        // ウィンドウ切り替え時の選択ボタンの読み込み
        defaultButton[(int)enumDefaultButton.AttackButton] = GameObject.Find("attackButton");
        defaultButton[(int)enumDefaultButton.SkillBackButton] = GameObject.Find("SkillBackButton");
        defaultButton[(int)enumDefaultButton.ItemUseBackButton] = GameObject.Find("itemUseBackButton");

        // 初期非表示ウィンドウの設定
        windows[(int)enumWindows.EnemyStatus].SetActive(false);
        windows[(int)enumWindows.ComandWindow].SetActive(false);
        windows[(int)enumWindows.ItemUseSelect].SetActive(false);

        // プレイヤーのステータス初期化
        int baseCharacterStatus = PlayerPrefs.GetInt("Character");
        playerName = playerStatusManager.DataList[baseCharacterStatus].pNAME;
        playerLv = playerStatusManager.DataList[baseCharacterStatus].pLv;
        playerHP = playerStatusManager.DataList[baseCharacterStatus].pHP;
        playerMaxHP = playerStatusManager.DataList[baseCharacterStatus].pHP;
        playerSP = playerStatusManager.DataList[baseCharacterStatus].pSP;
        playerMaxSP = playerStatusManager.DataList[baseCharacterStatus].pSP;
        playerATK = playerStatusManager.DataList[baseCharacterStatus].pATK;
        skillSlot[0] = playerStatusManager.DataList[baseCharacterStatus].skillSlot[0];
        skillSlot[1] = playerStatusManager.DataList[baseCharacterStatus].skillSlot[1];
        skillSlot[2] = playerStatusManager.DataList[baseCharacterStatus].skillSlot[2];

        // プレイヤー名の表示
        playerStatusText[(int)enumPlayerStatusText.Name].text = playerName;

    }

    void Update()
    {
        // 各ステータスの更新
        playerStatusText[(int)enumPlayerStatusText.Lv].text = playerLv.ToString();
        playerStatusText[(int)enumPlayerStatusText.HP].text = playerHP.ToString();
        playerStatusText[(int)enumPlayerStatusText.MaxHP].text = playerMaxHP.ToString();
        playerStatusText[(int)enumPlayerStatusText.SP].text = playerSP.ToString();
        playerStatusText[(int)enumPlayerStatusText.MaxSP].text = playerMaxSP.ToString();
        playerStatusText[(int)enumPlayerStatusText.ATK].text = playerATK.ToString();

        enemyStatusText[(int)enumEnemyStatusText.Lv].text = enemyLv.ToString();
        enemyStatusText[(int)enumEnemyStatusText.HP].text = enemyHP.ToString();
        enemyStatusText[(int)enumEnemyStatusText.MaxHP].text = enemyMaxHP.ToString();
        enemyStatusText[(int)enumEnemyStatusText.ATK].text = enemyATK.ToString();

    }

    // 通常戦闘開始時の処理
    public IEnumerator BattleStart()
    {
        mainText.text = "バトルフロアだ！";
        floorBackImage.sprite = floorBackSprite;

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.Battle);
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);

        // 選択した難易度か進行状況で敵を変える
        nowEnemySprite = new Sprite[] { enemySprite[(int)enumEnemySprite.Slime], enemySprite[(int)enumEnemySprite.IkeBat], enemySprite[(int)enumEnemySprite.HatGhost] };
        int randomNumber = Random.Range(0, nowEnemySprite.Length);
        if (GameManager.Instance.floorNumber > GameManager.Instance.maxFloorNumber / 2 && PlayerPrefs.GetInt("Difficulty") >= 1)
        {
            randomNumber += 3;
        }
        Sprite selectedSprite = nowEnemySprite[randomNumber];
        displayEnemyImage.sprite = selectedSprite;
        enemyName = enemyStatusManager.DataList[randomNumber].eNAME;
        enemyLv = enemyStatusManager.DataList[randomNumber].eLv;
        enemyHP = enemyStatusManager.DataList[randomNumber].eHP;
        enemyMaxHP = enemyStatusManager.DataList[randomNumber].eHP;
        enemyATK = enemyStatusManager.DataList[randomNumber].eATK;
        enemyEXP = enemyStatusManager.DataList[randomNumber].eEXP;
        enemyStatusText[(int)enumEnemyStatusText.Name].text = enemyName;

        mainText.text = $"{enemyName}が現れた！";

        windows[(int)enumWindows.EnemyStatus].SetActive(true);
        windows[(int)enumWindows.ItemWindow].SetActive(false);

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        yield return StartCoroutine(Battle());

    }

    // 強敵戦闘開始時の処理
    public IEnumerator StrongStart()
    {
        mainText.text = "強敵フロアだ！";
        floorBackImage.sprite = floorBackSprite;
        GameManager.Instance.floorIconImage.sprite = GameManager.Instance.floorIconSprite[(int)GameManager.enumFloorIconSprite.StrongFloor];

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.StrongBattle);
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        nowEnemySprite = new Sprite[] { enemySprite[(int)enumEnemySprite.InfernoButterfly], enemySprite[(int)enumEnemySprite.DarkDragon], enemySprite[(int)enumEnemySprite.IceDragon] };
        int randomNumber = Random.Range(0, nowEnemySprite.Length);
        Sprite selectedSprite = nowEnemySprite[randomNumber];
        displayEnemyImage.sprite = selectedSprite;

        randomNumber += 6;
        enemyName = enemyStatusManager.DataList[randomNumber].eNAME;
        enemyLv = enemyStatusManager.DataList[randomNumber].eLv;
        enemyHP = enemyStatusManager.DataList[randomNumber].eHP;
        enemyMaxHP = enemyStatusManager.DataList[randomNumber].eHP;
        enemyATK = enemyStatusManager.DataList[randomNumber].eATK;
        enemyEXP = enemyStatusManager.DataList[randomNumber].eEXP;
        enemyStatusText[(int)enumEnemyStatusText.Name].text = enemyName;

        mainText.text = $"{enemyName}が現れた！";

        windows[(int)enumWindows.EnemyStatus].SetActive(true);
        windows[(int)enumWindows.ItemWindow].SetActive(false);

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        yield return StartCoroutine(Battle());

    }

    // ボス戦闘開始時の処理
    public IEnumerator BossStart()
    {
        mainText.text = "ボスフロアだ！";
        floorBackImage.sprite = floorBackSprite;
        GameManager.Instance.floorIconImage.sprite = GameManager.Instance.floorIconSprite[(int)GameManager.enumFloorIconSprite.BossFloor];
        bossBattle = true;

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlayBGM((int)SoundManager.enumBgmNumber.BossBattle);
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (PlayerPrefs.GetInt("Difficulty") == (int)TitleManager.enumDifficultyID.Easy)
        {
            displayEnemyImage.sprite = enemySprite[(int)enumEnemySprite.BabyDragon];

            enemyName = enemyStatusManager.DataList[9].eNAME;
            enemyLv = enemyStatusManager.DataList[9].eLv;
            enemyHP = enemyStatusManager.DataList[9].eHP;
            enemyMaxHP = enemyStatusManager.DataList[9].eHP;
            enemyATK = enemyStatusManager.DataList[9].eATK;
            enemyEXP = enemyStatusManager.DataList[9].eEXP;
            enemyStatusText[(int)enumEnemyStatusText.Name].text = enemyName;
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)TitleManager.enumDifficultyID.Normal)
        {
            displayEnemyImage.sprite = enemySprite[(int)enumEnemySprite.LightDragon];

            enemyName = enemyStatusManager.DataList[10].eNAME;
            enemyLv = enemyStatusManager.DataList[10].eLv;
            enemyHP = enemyStatusManager.DataList[10].eHP;
            enemyMaxHP = enemyStatusManager.DataList[10].eHP;
            enemyATK = enemyStatusManager.DataList[10].eATK;
            enemyEXP = enemyStatusManager.DataList[10].eEXP;
            enemyStatusText[(int)enumEnemyStatusText.Name].text = enemyName;
        }

        mainText.text = $"{enemyName}が現れた！";

        windows[(int)enumWindows.EnemyStatus].SetActive(true);
        windows[(int)enumWindows.ItemWindow].SetActive(false);

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        yield return StartCoroutine(Battle());

    }

    // 戦闘時の共通処理
    public IEnumerator Battle()
    {
        mainText.text = "コマンド？";

        yield return new WaitForSeconds(0.5f);

        windows[(int)enumWindows.ComandWindow].SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultButton[0]);

        while (!buttonOn[(int)enumButtonOn.Attack] && !buttonOn[(int)enumButtonOn.Skill] && !buttonOn[(int)enumButtonOn.Item])
        {
            yield return null;
        }

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        windows[(int)enumWindows.ComandWindow].SetActive(false);

        if (buttonOn[(int)enumButtonOn.Attack])
        {
            windows[(int)enumWindows.ComandWindow].SetActive(false);
            buttonOn[(int)enumButtonOn.Attack] = false;
            yield return StartCoroutine(Attack());
        }
        else if (buttonOn[(int)enumButtonOn.Skill])
        {
            windows[(int)enumWindows.ComandWindow].SetActive(false);
            buttonOn[(int)enumButtonOn.Skill] = false;
            windows[(int)enumWindows.SkillWindow].SetActive(true);
            SkillManager.Instance.skillDescriptionDisplay = true;
            EventSystem.current.SetSelectedGameObject(defaultButton[1]);

            while (!skillUse && !skillBack)
            {
                yield return null;
            }

            if (skillUse)
            {
                SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
                windows[(int)enumWindows.SkillWindow].SetActive(false);
                skillUse = false;
                SkillManager.Instance.skillDescriptionDisplay = false;
                yield return StartCoroutine(Skill());
            }
            else
            {
                SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Back);
                windows[(int)enumWindows.SkillWindow].SetActive(false);
                skillBack = false;
                SkillManager.Instance.skillDescriptionDisplay = false;
                yield return StartCoroutine(Battle());

            }

        }
        else if (buttonOn[(int)enumButtonOn.Item])
        {
            windows[(int)enumWindows.ComandWindow].SetActive(false);
            buttonOn[(int)enumButtonOn.Item] = false;

            yield return StartCoroutine(Item());

        }

    }

    // 攻撃コマンド選択時の処理
    public IEnumerator Attack()
    {
        yield return StartCoroutine(PlayerAttack());

        yield return StartCoroutine(EnemyAttack());

        yield return StartCoroutine(Battle());
    }

    // プレイヤーの通常攻撃の処理
    public IEnumerator PlayerAttack()
    {
        mainText.text = $"{playerName}の攻撃！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Attack);
        FlashManager.Instance.EnemyFlash(Color.red, 0.3f);
        mainText.text = $"{playerATK}のダメージ！";

        yield return new WaitForSeconds(0.5f);

        enemyHP = enemyHP - playerATK;
        if (enemyHP < 0)
        {
            enemyHP = 0;
        }

        if (powerUp)
        {
            powerUp = false;
            playerATK = baseAttack;
        }

        yield return StartCoroutine(NextProcess(1.0f));

        if (enemyHP == 0)
        {
            yield return StartCoroutine(PlayerWin());
        }
        else
        {
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        }

    }

    // 敵の攻撃の処理
    public IEnumerator EnemyAttack()
    {
        if (Random.Range(0, 3) == 0)
        {
            yield return StartCoroutine(EnemyActionManager.Instance.EnemyAction());
        }
        else
        {

            mainText.text = $"{enemyName}の攻撃！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Damage);
            FlashManager.Instance.FlashScreen(Color.red, 0.3f);
            mainText.text = $"{enemyATK}のダメージ！";

            yield return new WaitForSeconds(0.5f);

            playerHP = playerHP - enemyATK;
            if (playerHP < 0)
            {
                playerHP = 0;
            }

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
            if (playerHP == 0)
            {
                yield return StartCoroutine(PlayerLose());
            }
        }

    }

    // スキル使用時の処理
    public IEnumerator Skill()
    {
        // スキルごとの処理分岐
        if (buttonOn[(int)enumButtonOn.Skill1])
        {
            buttonOn[(int)enumButtonOn.Skill1] = false;
            SkillManager.Instance.useSkill[skillSlot[0]] = true;
            yield return StartCoroutine(SkillManager.Instance.UseSkill());
        }
        else if (buttonOn[(int)enumButtonOn.Skill2])
        {
            buttonOn[(int)enumButtonOn.Skill1] = false;
            SkillManager.Instance.useSkill[skillSlot[1]] = true;
            yield return StartCoroutine(SkillManager.Instance.UseSkill());
        }
        else if (buttonOn[(int)enumButtonOn.Skill3])
        {
            buttonOn[(int)enumButtonOn.Skill1] = false;
            SkillManager.Instance.useSkill[skillSlot[2]] = true;
            yield return StartCoroutine(SkillManager.Instance.UseSkill());
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        yield return StartCoroutine(EnemyAttack());

        yield return StartCoroutine(Battle());
    }

    // アイテム使用時の処理
    public IEnumerator Item()
    {
        if (!ItemManager.Instance.haveItem)
        {
            mainText.text = "アイテムを持っていない！";

            yield return StartCoroutine(NextProcess(1.0f));

            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
            yield return StartCoroutine(Battle());
        }
        else
        {
            windows[(int)enumWindows.ItemUseSelect].SetActive(true);
            EventSystem.current.SetSelectedGameObject(defaultButton[2]);
        }

        if (ItemManager.Instance.getItem[(int)ItemManager.enumGetItem.HPPotion])
        {
            mainText.text = "HPポーション:HPを30回復";
        }
        else if (ItemManager.Instance.getItem[(int)ItemManager.enumGetItem.SPPotion])
        {
            mainText.text = "SPポーション:SPを30回復";
        }
        else if (ItemManager.Instance.getItem[(int)ItemManager.enumGetItem.ATKPotion])
        {
            mainText.text = "攻撃ポーション:次の攻撃力が2倍";
        }
        else if (ItemManager.Instance.getItem[(int)ItemManager.enumGetItem.HealHerb])
        {
            mainText.text = "癒し草:HPを50回復";
        }
        else if (ItemManager.Instance.getItem[(int)ItemManager.enumGetItem.DamageBomb])
        {
            mainText.text = "ボム:敵に30のダメージ";
        }
        else if (ItemManager.Instance.getItem[(int)ItemManager.enumGetItem.ATKJewel])
        {
            mainText.text = "攻撃ジュエル:次の攻撃力が3倍";
        }

        while (!buttonOn[(int)enumButtonOn.ItemUse] && !buttonOn[(int)enumButtonOn.Back])
        {
            yield return null;
        }

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (buttonOn[(int)enumButtonOn.ItemUse])
        {
            buttonOn[(int)enumButtonOn.ItemUse] = false;
            windows[(int)enumWindows.ItemUseSelect].SetActive(false);
            yield return StartCoroutine(ItemManager.Instance.HaveItem());
        }
        else if (buttonOn[(int)enumButtonOn.Back])
        {
            buttonOn[(int)enumButtonOn.Back] = false;
            windows[(int)enumWindows.ItemUseSelect].SetActive(false);
            yield return StartCoroutine(Battle());
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        yield return StartCoroutine(EnemyAttack());

        yield return StartCoroutine(Battle());


    }

    // プレイヤー敗北時の処理
    public IEnumerator PlayerLose()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Lose);
        SoundManager.Instance.StopBGM();
        mainText.text = $"{playerName}は負けた";
        // 現在のフロアの一つ前のフロアまでをクリアフロア数として保存
        if (PlayerPrefs.GetInt("Difficulty") == (int)TitleManager.enumDifficultyID.Easy)
        {
            if (PlayerPrefs.GetInt("EasyClearFloor") < GameManager.Instance.floorNumber-1)
            {
                PlayerPrefs.SetInt("EasyClearFloor", (int)GameManager.Instance.floorNumber-1);
            }
        }
        else if (PlayerPrefs.GetInt("Difficulty") == (int)TitleManager.enumDifficultyID.Normal)
        {
            if (PlayerPrefs.GetInt("NormalClearFloor") < GameManager.Instance.floorNumber-1)
            {
                PlayerPrefs.SetInt("NormalClearFloor", (int)GameManager.Instance.floorNumber-1);
            }
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        Initiate.Fade("GameOverScene", Color.black, 1.0f);
        yield return new WaitForSeconds(5.0f);
    }

    // プレイヤー勝利時の処理
    public IEnumerator PlayerWin()
    {
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Win);
        SoundManager.Instance.StopBGM();
        
        if (powerUp)
        {
            powerUp = false;
            playerATK = baseAttack;
        }

        displayEnemyImage.sprite = noneEnemy;
        mainText.text = $"{playerName}の勝利！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        mainText.text = $"{enemyEXP}の経験値を獲得！";
        playerEXP += enemyEXP;

        yield return StartCoroutine(NextProcess(1.0f));

        // 経験値が一定に達したらレベルアップ
        if (playerEXP >= playerNextLvEXP)
        {
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.LvUp);
            mainText.text = "レベルが上がった！\nステータスが上昇！";
            if (PlayerPrefs.GetInt("Character") == (int)TitleManager.enumCharacterID.Warrior)
            {
                playerLv += 1;
                playerMaxHP += 10;
                playerMaxSP += 5;
                playerATK += 2;
                playerEXP = 0;
                playerNextLvEXP *= 2;
            }
            else if (PlayerPrefs.GetInt("Character") == (int)TitleManager.enumCharacterID.Magician)
            {
                playerLv += 1;
                playerMaxHP += 5;
                playerMaxSP += 10;
                playerATK += 1;
                playerEXP = 0;
                playerNextLvEXP *= 2;
            }

            if (playerLv >= 2)
            {
                SkillManager.Instance.useSkillButton[(int)SkillManager.enumSkillButton.Skill1].SetActive(true);
            }

            if (playerLv >= 4)
            {
                SkillManager.Instance.useSkillButton[(int)SkillManager.enumSkillButton.Skill2].SetActive(true);
            }

            if (playerLv >= 6)
            {
                SkillManager.Instance.useSkillButton[(int)SkillManager.enumSkillButton.Skill3].SetActive(true);
            }

            yield return StartCoroutine(NextProcess(1.0f));

        }

        // ボスバトルだった場合はクリアシーンへ
        if (bossBattle)
        {
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Win);
            mainText.text = "ダンジョンを制覇した！";

            yield return StartCoroutine(NextProcess(1.0f));

            if (PlayerPrefs.GetInt("Difficulty") == (int)TitleManager.enumDifficultyID.Easy)
            {
                if (PlayerPrefs.GetInt("EasyClearFloor") < GameManager.Instance.floorNumber)
                {
                    PlayerPrefs.SetInt("EasyClearFloor", (int)GameManager.Instance.floorNumber);
                }
            }
            else if (PlayerPrefs.GetInt("Difficulty") == (int)TitleManager.enumDifficultyID.Normal)
            {
                if (PlayerPrefs.GetInt("NormalClearFloor") < GameManager.Instance.floorNumber)
                {
                    PlayerPrefs.SetInt("NormalClearFloor", (int)GameManager.Instance.floorNumber);
                }
            }
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
            Initiate.Fade("GameClearScene", Color.black, 1.0f);
            yield return new WaitForSeconds(5.0f);
        }

        windows[(int)enumWindows.EnemyStatus].SetActive(false);
        windows[(int)enumWindows.ItemWindow].SetActive(true);
        yield return StartCoroutine(GameManager.Instance.NextFloor());
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

    // 攻撃コマンドのボタン
    public void AttackComand()
    {
        buttonOn[(int)enumButtonOn.Attack] = true;
    }

    // スキルコマンドのボタン
    public void SkillComand()
    {
        buttonOn[(int)enumButtonOn.Skill] = true;
    }

    // アイテムコマンドのボタン
    public void ItemComand()
    {
        buttonOn[(int)enumButtonOn.Item] = true;
    }

    // スキル１のボタン
    public void Skill1()
    {
        skillUse = true;
        buttonOn[(int)enumButtonOn.Skill1] = true;
    }

    // スキル２のボタン
    public void Skill2()
    {
        skillUse = true;
        buttonOn[(int)enumButtonOn.Skill2] = true;
    }

    // スキル３のボタン
    public void Skill3()
    {
        skillUse = true;
        buttonOn[(int)enumButtonOn.Skill3] = true;
    }

    // アイテム使用確認時の使用ボタン
    public void ItemUse()
    {
        buttonOn[(int)enumButtonOn.ItemUse] = true;
    }

    // アイテム使用確認時の戻るボタン
    public void ItemBack()
    {
        buttonOn[(int)enumButtonOn.Back] = true;
    }

    // スキル選択ウィンドウを閉じるボタン
    public void SkillBack()
    {
        skillBack = true;
    }
}


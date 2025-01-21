using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManager : Singleton<ShopManager>
{
    public ItemValueManager itemValueManager; // アイテムの各値管理用のスクリプト

    public TextMeshProUGUI mainText; // テキスト表示
    public GameObject selectWindow; // 選択ウィンドウ
    public bool yes, no; // 選択肢
    public GameObject defaultButton; // 選択肢表示でデフォルトで選択するボタン

    public Image floorImage; // フロア背景
    public Sprite[] floorSprite; // 背景画像
    public enum enumFloorSprite
    {
        HPPotion, // HPポーション
        SPPotion, // SPポーション
        ATKPotion, // 攻撃ポーション
        None, // 非表示用
        Num // 背景数
    }
    public int[] getItemNumber;
    public enum enumGetItemNumber
    {
        HPPotion,
        SPPotion,
        ATKPotion,
        Num
    }
    public int getItem;

    public void Start()
    {
        // ScriptableObjectの読み込み
        itemValueManager = Resources.Load<ItemValueManager>("ScriptableObject/ItemValueManager");

        // 各配列の初期化
        floorSprite = new Sprite[(int)enumFloorSprite.Num];
        getItemNumber = new int[(int)enumGetItemNumber.Num];

        // 各UIオブジェクトの読み込み
        mainText = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>();
        selectWindow = GameObject.Find("ShopSelectWindow");
        defaultButton = GameObject.Find("ShopSelectYes");
        floorImage = GameObject.Find("FloorImage").GetComponent<Image>();
        floorSprite[(int)enumFloorSprite.HPPotion] = Resources.Load<Sprite>("Images/FloorBacks/HPPotion");
        floorSprite[(int)enumFloorSprite.SPPotion] = Resources.Load<Sprite>("Images/FloorBacks/SPPotion");
        floorSprite[(int)enumFloorSprite.ATKPotion] = Resources.Load<Sprite>("Images/FloorBacks/ATKPotion");
        floorSprite[(int)enumFloorSprite.None] = Resources.Load<Sprite>("Images/Enemys/Unknown");

        // データリストからのアイテムIDの読み込み
        getItemNumber[(int)enumGetItemNumber.HPPotion] = itemValueManager.DataList[0].itemID;
        getItemNumber[(int)enumGetItemNumber.SPPotion] = itemValueManager.DataList[1].itemID;
        getItemNumber[(int)enumGetItemNumber.ATKPotion] = itemValueManager.DataList[2].itemID;

        // 初期非表示ウィンドウを非表示
        selectWindow.SetActive(false);

    }

    // HPポーション
    public IEnumerator HPShop()
    {
        floorImage.sprite = floorSprite[(int)enumFloorSprite.HPPotion];
        getItem = getItemNumber[(int)enumGetItemNumber.HPPotion];
        yield return StartCoroutine(PotionShop());
    }

    // SPポーション
    public IEnumerator SPShop()
    {
        floorImage.sprite = floorSprite[(int)enumFloorSprite.SPPotion];
        getItem = getItemNumber[(int)enumGetItemNumber.SPPotion];
        yield return StartCoroutine(PotionShop());
    }

    // 攻撃ポーション
    public IEnumerator ATKShop()
    {
        floorImage.sprite = floorSprite[(int)enumFloorSprite.ATKPotion];
        getItem = getItemNumber[(int)enumGetItemNumber.ATKPotion];
        yield return StartCoroutine(PotionShop());
    }

    // ショップの処理
    public IEnumerator PotionShop()
    {
        mainText.text = "いらっしゃい！\nここはポーションショップだよ";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        mainText.text = "HPを10分けてくれたら\nポーションを売ってあげよう";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (BattleManager.Instance.playerHP <= 10)
        {
            mainText.text = "おっと、HPが足りないね\nまた来ておくれ";
        }
        else
        {
            mainText.text = $"{itemValueManager.DataList[getItem].itemName}が必要かい？";

            yield return new WaitForSeconds(0.5f);

            selectWindow.SetActive(true);
            EventSystem.current.SetSelectedGameObject(defaultButton);

            while (!yes && !no)
            {
                yield return null;
            }
            SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
            selectWindow.SetActive(false);

            if (yes)
            {
                yes = false;

                if (ItemManager.Instance.haveItem)
                {
                    mainText.text = "もうアイテムを持ってるね\nアイテムを交換するかい？";

                    yield return new WaitForSeconds(0.5f);

                    selectWindow.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(defaultButton);

                    while (!yes && !no)
                    {
                        yield return null;
                    }
                    selectWindow.SetActive(false);

                    if (yes)
                    {
                        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Damage);
                        FlashManager.Instance.FlashScreen(Color.red, 0.3f);
                        yes = false;
                        mainText.text = "交換成立だ\nまた会えるといいね";
                        BattleManager.Instance.playerHP -= 10;
                        for (int i = 0; i < ItemManager.Instance.getItem.Length; i++)
                        {
                            ItemManager.Instance.getItem[i] = false;
                        }
                        ItemManager.Instance.getItem[getItem] = true;
                        ItemManager.Instance.itemText.text = itemValueManager.DataList[getItem].itemName;
                    }
                    else
                    {
                        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
                        no = false;
                        mainText.text = "後悔しないことを願うよ";
                    }

                }
                else
                {
                    SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Damage);
                    FlashManager.Instance.FlashScreen(Color.red, 0.3f);
                    mainText.text = "交換成立だ\nまた会えるといいね";
                    BattleManager.Instance.playerHP -= 10;
                    ItemManager.Instance.getItem[getItem] = true;
                    ItemManager.Instance.haveItem = true;
                    ItemManager.Instance.itemText.text = itemValueManager.DataList[getItem].itemName;
                }

            }
            else
            {
                SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
                no = false;
                mainText.text = "後悔しないことを願うよ";
            }
        }

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        SoundManager.Instance.StopBGM();
        floorImage.sprite = floorSprite[(int)enumFloorSprite.None];
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

    // 購入時の選択肢はい
    public void Yes()
    {
        yes = true;
    }

    // 購入時の選択肢いいえ
    public void No()
    {
        no = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TreasureManager : Singleton<TreasureManager>
{
    public ItemValueManager itemValueManager; // アイテムの各値管理用のスクリプト

    public TextMeshProUGUI mainText; // テキスト表示
    public GameObject selectWindow; // 選択ウィンドウ
    public bool yes, no; // 選択肢
    public GameObject defaultButton; // 選択肢表示でデフォルトで選択するボタン

    public Image floorImage; // フロア背景
    public Sprite[] floorSprite; // フロア背景
    public enum enumFloorSprite
    {
        NotOpenTreasure, // 宝箱未開封背景
        OpenTreasure, // 宝箱開封背景
        Num // フロア背景数
    }
    public int[] getItemNumber; // アイテムの番号
    public enum enumGetItemNumber
    {
        Healherb,
        DamageBomb,
        ATKJewel,
        Num
    }
    public int itemNumber;
    /*
    0:宝背景未開封
    1:宝背景開封
    */

    private void Start()
    {
        // ScriptableObjectの読み込み
        itemValueManager = Resources.Load<ItemValueManager>("ScriptableObject/ItemValueManager");

        // 各配列の初期化
        floorSprite = new Sprite[(int)enumFloorSprite.Num];
        getItemNumber = new int[(int)enumGetItemNumber.Num];

        // 各UIオブジェクトの読み込み
        mainText = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>();
        selectWindow = GameObject.Find("ItemChangeWindow");
        defaultButton = GameObject.Find("ItemChangeYes");
        floorImage = GameObject.Find("FloorImage").GetComponent<Image>();
        floorSprite[(int)enumFloorSprite.NotOpenTreasure] = Resources.Load<Sprite>("Images/FloorBacks/NotOpenTreasure");
        floorSprite[(int)enumFloorSprite.OpenTreasure] = Resources.Load<Sprite>("Images/FloorBacks/OpenTreasure");

        // データリストからの読み込み
        getItemNumber[(int)enumGetItemNumber.Healherb] = itemValueManager.DataList[3].itemID;
        getItemNumber[(int)enumGetItemNumber.DamageBomb] = itemValueManager.DataList[4].itemID;
        getItemNumber[(int)enumGetItemNumber.ATKJewel] = itemValueManager.DataList[5].itemID;

        // 初期非表示ウィンドウを非表示
        selectWindow.SetActive(false);

    }

    // ランダムにアイテムを抽選する処理
    public IEnumerator RandomItem()
    {
        int randomValue = Random.Range(0, 10);
        if (randomValue == 0)
        {
            itemNumber = getItemNumber[(int)enumGetItemNumber.Healherb];
            yield return StartCoroutine(Item());
        }
        else if (randomValue < 2)
        {
            itemNumber = getItemNumber[(int)enumGetItemNumber.DamageBomb];
            yield return StartCoroutine(Item());
        }
        else
        {
            itemNumber = getItemNumber[(int)enumGetItemNumber.ATKJewel];
            yield return StartCoroutine(Item());
        }

    }

    // 宝箱の処理
    public IEnumerator Item()
    {
        floorImage.sprite = floorSprite[(int)enumFloorSprite.NotOpenTreasure];

        mainText.text = "あなたは宝箱を見つけた！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        FlashManager.Instance.FlashScreen(Color.yellow, 0.3f);
        floorImage.sprite = floorSprite[(int)enumFloorSprite.OpenTreasure];

        mainText.text = $"宝箱には\n{itemValueManager.DataList[itemNumber].itemName}が入っていた！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        if (ItemManager.Instance.haveItem)
        {
            mainText.text = "アイテムを交換しますか？";

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
                for (int i = 0; i < ItemManager.Instance.getItem.Length; i++)
                {
                    ItemManager.Instance.getItem[i] = false;
                }
                ItemManager.Instance.getItem[itemNumber] = true;
                ItemManager.Instance.itemText.text = itemValueManager.DataList[itemNumber].itemName;
                mainText.text = $"{itemValueManager.DataList[itemNumber].itemName}を手に入れた！";
            }
            else if (no)
            {
                mainText.text = "あなたは宝箱をあきらめた";
            }
        }
        else
        {
            ItemManager.Instance.getItem[itemNumber] = true;
            ItemManager.Instance.haveItem = true;
            ItemManager.Instance.itemText.text = itemValueManager.DataList[itemNumber].itemName;
            mainText.text = $"{itemValueManager.DataList[itemNumber].itemName}を手に入れた！";
        }

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

    // 選択肢ボタンはい
    public void Yes()
    {
        yes = true;
    }

    // 選択肢ボタンいいえ
    public void No()
    {
        no = true;
    }

}

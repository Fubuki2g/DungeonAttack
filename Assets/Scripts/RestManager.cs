using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestManager : Singleton<RestManager>
{
    public TextMeshProUGUI mainText; // テキスト表示

    public Image floorImage; // フロア背景
    public Sprite floorSprite; // 背景画像

    private void Start()
    {
        // 各UIオブジェクトの読み込み
        mainText = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>();
        floorImage = GameObject.Find("FloorImage").GetComponent<Image>();
        floorSprite = Resources.Load<Sprite>("Images/FloorBacks/Rest");
    }

    // 休憩の処理
    public IEnumerator Rest()
    {
        floorImage.sprite = floorSprite;

        mainText.text = "休憩できそうな場所を見つけた！";

        yield return StartCoroutine(NextProcess(1.0f));

        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Select);
        mainText.text = "あなたはここで\n少し休憩することにした";

        yield return StartCoroutine(NextProcess(1.0f));

        mainText.text = "あなたの体力は回復した！";
        SoundManager.Instance.PlaySE((int)SoundManager.enumSENumber.Healing);
        FlashManager.Instance.FlashScreen(new Color(0.5f, 1f, 0f), 0.3f);
        BattleManager.Instance.playerHP = BattleManager.Instance.playerMaxHP;
        BattleManager.Instance.playerSP = BattleManager.Instance.playerMaxSP;

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

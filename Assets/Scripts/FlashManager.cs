using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashManager : Singleton<FlashManager>
{
    public Image playerFlashImage; // UI Imageの参照
    public Image enemyFlashImage; // EnemyImageの参照
    public Color flashColor; // 明滅する色
    public float flashDuration; // 明滅の持続時間

    private void Start()
    {
        // 明滅させるImageオブジェクトの読み込み
        playerFlashImage = GameObject.Find("FlashImage").GetComponent<Image>();
        enemyFlashImage = GameObject.Find("EnemyImage").GetComponent<Image>();

        if (playerFlashImage != null)
        {
            playerFlashImage.color = new Color(0, 0, 0, 0); // 初期状態で透明
        }
    }

    // 画面全体の明滅（プレイヤー用）
    public void FlashScreen(Color color, float duration)
    {
        flashColor = color;
        flashDuration = duration;
        StartCoroutine(FlashScreenRoutine());
    }

    // 敵画像の明滅（敵用）
    public void EnemyFlash(Color color, float duration)
    {
        flashColor = color;
        flashDuration = duration;
        StartCoroutine(EnemyFlashRoutine());
    }

    // 画面全体の明滅を制御するコルーチン
    private IEnumerator FlashScreenRoutine()
    {
        float elapsedTime = 0f;
        playerFlashImage.color = flashColor;

        // 明滅のフェードイン
        while (elapsedTime < flashDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / (flashDuration / 2));
            playerFlashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            yield return null;
        }

        elapsedTime = 0f;

        // 明滅のフェードアウト
        while (elapsedTime < flashDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / (flashDuration / 2));
            playerFlashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            yield return null;
        }

        playerFlashImage.color = new Color(0, 0, 0, 0);
    }

    // 敵画像の明滅を制御するコルーチン
    private IEnumerator EnemyFlashRoutine()
    {
        float elapsedTime = 0f;
        enemyFlashImage.color = flashColor;

        // 明滅のフェードイン
        while (elapsedTime < flashDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / (flashDuration / 2));
            enemyFlashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            yield return null;
        }

        elapsedTime = 0f;

        // 明滅のフェードアウト
        while (elapsedTime < flashDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / (flashDuration / 2));
            enemyFlashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            yield return null;
        }

        enemyFlashImage.color = new Color(1, 1, 1, 1);
    }
}


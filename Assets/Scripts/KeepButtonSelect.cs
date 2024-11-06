using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeepButtonSelect : MonoBehaviour
{
    private GameObject lastSelected;

    void Update()
    {
        // 現在選択されているボタンがない場合、前回の選択を復元する
        if (EventSystem.current.currentSelectedGameObject == null && Input.GetMouseButton(0))
        {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
        else
        {
            // 現在選択されているオブジェクトを記憶
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
    }
}
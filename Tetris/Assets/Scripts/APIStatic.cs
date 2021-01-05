using UnityEngine;

/// <summary>
/// 認識靜態的 API
/// 靜態 static
/// </summary>
public class APIStatic : MonoBehaviour
{
    /// <summary>
    /// 開始事件：播放後執行一次
    /// </summary>
    private void Start()
    {
        // 靜態屬性 Static Properties
        // 取得
        // 語法：類別名稱.靜態屬性名稱
        print(Mathf.PI);
        print(Mathf.Infinity);

        // 設定
        // 如果有 Read Only 代表不能設定
        // 語法：類別名稱.靜態屬性名稱 = 相同類型的值
        Time.timeScale = 0.5f;

        // 練習
        print("所有攝影機的數量：" + Camera.allCamerasCount);
        Cursor.visible = false;

        // 靜態方法 Static Methods
        // 使用
        // 語法：類別名稱.靜態方法名稱(對應的參數)
        int number = Mathf.Abs(-999);
        print("取得絕對值後的結果：" + number);

        print("隨機範圍 3 - 20.5：" + Random.Range(3.0f, 20.5f));
        print("隨機 1 - 100：" + Random.Range(1, 100));

        // 練習
        // Application.OpenURL("https://unity.com/");

        print("7.7 去小數點：" + Mathf.Floor(7.7f));
    }

    /// <summary>
    /// 更新事件：一秒執行約 60 次 (60FPS)
    /// </summary>
    private void Update()
    {
        // print("是否按任意鍵：" + Input.anyKey);
        // print("遊戲時間：" + Time.time);
        print("是否按下空白鍵：" + Input.GetKeyDown("space"));
    }
}

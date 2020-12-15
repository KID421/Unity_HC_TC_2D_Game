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
        // 靜態屬性
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
    }

    /// <summary>
    /// 更新事件：一秒執行約 60 次 (60FPS)
    /// </summary>
    private void Update()
    {
        print("是否按任意鍵：" + Input.anyKey);
        print("遊戲時間：" + Time.time);
    }
}

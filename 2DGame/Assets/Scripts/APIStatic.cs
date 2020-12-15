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
    }
}

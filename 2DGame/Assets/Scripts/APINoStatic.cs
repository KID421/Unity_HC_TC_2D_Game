using UnityEngine;

public class APINoStatic : MonoBehaviour
{
    // 使用一般屬性
    // 類型 欄位名稱
    public Transform traA;
    public Transform traB;

    public GameObject myObject;

    public Transform myTra;

    private void Start()
    {
        // 一般屬性
        // 取得
        // 語法：
        // 類型欄位名稱 的 一般屬性
        print("物件 A 的座標" + traA.position);
        // 設定
        // 語法：
        // 類型欄位名稱 的 一般屬性 = 對應的值；
        traB.position = new Vector3(1, 3, 5);

        print("我的物件圖層為：" + myObject.layer);

        myObject.layer = 4;
    }

    // 一秒執行約 60 次事件
    private void Update()
    {
        // 一般方法
        // 使用
        // 語法：
        // 類型欄位名稱 的 方法(對應的參數)
        myTra.Rotate(0, 0, 3);
        myTra.Translate(1, 0, 0);
    }
}

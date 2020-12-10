using UnityEngine;

public class Car : MonoBehaviour
{
    #region 欄位
    // 單行註解 (說明)
    // 數值 = 欄位 Field
    // 語法：
    // 修飾詞 類型 名稱 (指定 值)；

    // 四大類型 - Unity 常用
    // 整　數 int：沒有小數點的數值，例：０、１、－９９
    // 浮點數 float：有小數點的數值，例：０．１、０．１２３４５６、－１．９
    // 字　串 string：ＢＭＷ、賓士、＠＃＄
    // 布林值 bool：有 true、沒有 false

    // 指定符號 =

    // 私人 private 不會顯示在屬性面板上 - 預設值
    // 公開 public  會顯示在屬性面板上

    // 符號成對：() [] {} '' "" <>
    // 欄位的屬性
    // 標題 Header  - 字串
    // 提示 Tooltip - 字串
    // 範圍 Range   - 數值 整數與浮點數 (最小值，最大值)
    [Header("這是車子的尺寸"), Range(50, 150)]
    public int size = 100;
    [Header("這是車子的重量"), Tooltip("這是車子的重量，單位是噸。"), Range(0.5f, 3.5f)]
    public float weight = 1.5f;
    [Header("品牌"), Tooltip("這是車子的品牌")]
    public string brand = "BMW";
    [Header("是否有天窗"), Tooltip("這台車有沒有天窗")]
    public bool hasWindow = true;

    // 常用其他類型
    // 顏色 Color
    public Color colorA = Color.blue;                   // 使用內建顏色
    public Color colorB = new Color(0.5f, 0.3f, 0);     // 自訂顏色 R，G，B 值：0 - 1
    public Color colorC = new Color(1, 0, 0, 0.5f);     // 自訂顏色 R，G，B，A

    // 向量 Vector2 - 4
    public Vector2 v2A = Vector2.zero;
    public Vector2 v2B = Vector2.one;
    public Vector2 v2C = new Vector2(1.5f, 99.9f);

    public Vector3 v3A = new Vector3(1, 2, 3);
    public Vector4 v4A = new Vector4(1, 2, 3, 4);

    // 音效片段 AudioClip
    public AudioClip soundExplosion;
    // 圖片 Sprite - 改片 2D 圖片與 介面圖片
    public Sprite spriteA;

    // 遊戲物件與預製物 GameOjbect
    public GameObject goA;
    public GameObject goB;

    // 元件：屬性面板上的粗體字
    public Transform tra;
    public Camera cam;
    #endregion

    #region 事件
    // 事件：開始事件 - 播放後執行一次
    private void Start()
    {
        print("哈囉，沃德~");

        // 取得 欄位 (抓出資料)
        print(size);
        print(brand);

        // 設定 欄位 (修改資料)
        weight = 1.3f;
        hasWindow = false;

        // 呼叫自訂方法
        // 呼叫方法語法：自訂方法名稱()；
        MethodA();
        MethodA();

        // 區域變數
        // 類型 區域變數名稱 指定 值
        // 僅限於此區塊使用 (大括號)
        int intA = MethodB();
        print("傳回整數：" + intA);

        float pi = PI();
        print("拍：" + pi);

        Vector3 v123 = V123();
        print("三維向量 123：" + v123);
    }
    #endregion

    #region 方法
    // 欄位語法：
    // 修飾詞 類型 欄位名稱 指定 值；

    // 方法語法：
    // 修飾詞 傳回類型 方法名稱 () {}
    // 無傳回類型 void - 沒有任何傳回資料
    // 自訂方法
    // ※ 必須被呼叫才會執行
    private void MethodA()
    {
        print("嗨，我是方法 A~");
    }

    // 有傳回 方法
    // 如果不是無傳回
    // 必須使用關鍵字 return 傳回
    // 而且必須在 return 後 加上傳回類型的資料
    private int MethodB()
    {
        return 123;
    }

    private float PI()
    {
        return 3.14f;
    }

    private Vector3 V123()
    {
        return new Vector3(1, 2, 3);
    }

    #endregion
}

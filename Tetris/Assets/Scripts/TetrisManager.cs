using UnityEngine;
using UnityEngine.UI;       // 引用介面 API
using System.Linq;          // 查詢語言
using System.Collections;   // 引用 系統.集合 API - 協同程序

public class TetrisManager : MonoBehaviour
{
    #region 欄位
    [Header("掉落時間"), Range(0.1f, 3)]
    public float timeFall = 1.5f;
    [Header("目前分數")]
    public int score;
    [Header("等級")]
    public int lv = 1;
    [Header("結束畫面")]
    public GameObject objFinal;
    [Header("音效：地落、移動、消除與結束")]
    public AudioClip soundFall;
    public AudioClip soundMove;
    public AudioClip soundClear;
    public AudioClip soundLose;
    [Header("下一個俄羅斯方塊區域")]
    public Transform traNextArea;
    [Header("生成俄羅斯方塊的父物件")]
    public Transform traTetrisParent;
    [Header("生成的起始位置")]
    public Vector2[] posSpawn =
    {
        new Vector2(20, 400),
        new Vector2(20, 360),
        new Vector2(0, 360),
        new Vector2(0, 380),
        new Vector2(0, 380),
        new Vector2(20, 360),
        new Vector2(20, 360)
    };
    [Header("分數判定區域")]
    public Transform traScoreArea;

    /// <summary>
    /// 記錄所有再分數判定區域的小方塊
    /// </summary>
    private RectTransform[] rectSmall;
    /// <summary>
    /// 要刪除的列數
    /// </summary>
    private bool[] destroyRow = new bool[17];
    /// <summary>
    /// 剩下的方塊要掉落的高度
    /// </summary>
    private float[] downHeight;
    /// <summary>
    /// 下一顆俄羅斯方塊編號
    /// </summary>
    private int indexNext;
    /// <summary>
    /// 目前俄羅斯方塊
    /// </summary>
    private RectTransform currentTetris;
    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;
    /// <summary>
    /// 是否快速落下
    /// </summary>
    private bool fastDown;
    #endregion

    #region 事件
    // 開始事件：開始時候執行一次
    private void Start()
    {
        RandomTetris();
    }

    // 更新事件：一秒執行約 60 次
    private void Update()
    {
        ControlTertis();
        FastDown();
    }
    #endregion

    #region 方法：實作完成
    /// <summary>
    /// 控制俄羅斯方塊
    /// </summary>
    private void ControlTertis()
    {
        // 如果 已經有 目前的俄羅斯方塊
        if (currentTetris)
        {
            timer += Time.deltaTime;        // 計時器 累加 一幀的時間 - 累加時間

            if (timer >= timeFall)
            {
                timer = 0;
                currentTetris.anchoredPosition -= new Vector2(0, 40);
            }

            #region 控制俄羅斯方塊 左右、旋轉與加速
            // 取得 目前俄羅斯方塊的 Tetris 腳本
            Tetris tetris = currentTetris.GetComponent<Tetris>();

            // 如果 X 座標 小於 280 才能往右移動
            // if (currentTetris.anchoredPosition.x < 280)
            // 如果 目前俄羅斯方塊 沒有 碰到右邊牆壁
            if (!tetris.wallRight && !tetris.smallRight)
            {
                // 或者 ||
                // 按下 D 或者 右鍵 往右 50 
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentTetris.anchoredPosition += new Vector2(40, 0);
                }
            }
            // 如果 X 座標 小於 280 才能往右移動
            // if (currentTetris.anchoredPosition.x > -280)
            // 如果 目前俄羅斯方塊 沒有 碰到左邊牆壁
            if (!tetris.wallLeft && !tetris.smallLeft)
            {
                // 或者 ||
                // 按下 D 或者 右鍵 往右 50 
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentTetris.anchoredPosition -= new Vector2(40, 0);
                }
            }

            // 如果 俄羅斯方塊 可以旋轉
            // 按下 W 逆時針轉 90 度
            if (tetris.canRotate)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    // 屬性面板上面的 rotation 必須用 eulerAngles 控制
                    currentTetris.eulerAngles += new Vector3(0, 0, 90);

                    tetris.Offset();
                }
            }

            if (!fastDown)          // 如果 沒有在快速落下 才能 加速
            {
                // 如果 玩家 按住 S 就加速
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    timeFall = 0.1f;
                }
                // 否則 就恢復速度
                else
                {
                    timeFall = timeFllMax;
                }
            }
            #endregion

            // 如果 俄羅斯方塊 碰到了地板 就重新開始 - 生成下一顆
            // 或者 碰到其他方塊
            if (tetris.wallDown || tetris.smallBottom)
            {
                SetGround();                        // 設定為地板
                StartCoroutine(CheckTetris());      // 檢查並開始消除判定
                StartGame();                        // 生成下一顆
                StartCoroutine(ShakeEffect());      // 晃動效果
            }
        }
    }

    /// <summary>
    /// 設定掉落後變為方塊
    /// </summary>
    private void SetGround()
    {
        int count = currentTetris.childCount;                       // 取得 目前 方塊 的子物件數量

        for (int i = 0; i < count; i++)                             // 迴圈 執行 子物件數量次數
        {
            currentTetris.GetChild(i).name = "方塊";                // 名稱改為方塊
            currentTetris.GetChild(i).gameObject.layer = 10;        // 圖層改為方塊
        }

        // 將俄羅斯方塊小方塊搬到 分數區域
        for (int i = 0; i < count; i++)
        {
            currentTetris.GetChild(0).SetParent(traScoreArea);
        }
        // 刪除 父物件
        Destroy(currentTetris.gameObject);
    }

    /// <summary>
    /// 生成俄羅斯方塊
    /// 1. 隨機顯示一個下一顆俄羅斯方塊 0 - 6
    /// </summary>
    private void RandomTetris()
    {
        // 下一顆編號 = 隨機 的 範圍(最小，最大) - 整數不會等於最大值
        indexNext = Random.Range(0, 7);

        // 測試
        //indexNext = 0;

        // 下一個俄羅斯方塊區域 . 取得子物件(子物件編號) . 轉為遊戲物件 . 啟動設定(顯示)
        traNextArea.GetChild(indexNext).gameObject.SetActive(true);
    }

    /// <summary>
    /// 開始遊戲
    /// 1. 生成俄羅斯方塊要放在正確位置
    /// 2. 上一次俄羅斯方塊隱藏
    /// 3. 隨機取得下一個
    /// </summary>
    public void StartGame()
    {
        fastDown = false;               // 碰到地板後，沒有快速落下

        // 1. 生成俄羅斯方塊要放在正確位置
        // 保存上一次的俄羅斯方塊
        GameObject tetris = traNextArea.GetChild(indexNext).gameObject;
        // 目前俄羅斯方塊 = 生成物件(物件，父物件)
        GameObject current = Instantiate(tetris, traTetrisParent);
        // GetComponent<任何元件>()
        // <T> 泛型 - 指的是所有類型
        // 目前俄羅斯方塊 . 取得元件<介面變形>() . 座標 = 生成座標陣列[編號]
        current.GetComponent<RectTransform>().anchoredPosition = posSpawn[indexNext];

        // 2. 上一次俄羅斯方塊隱藏
        tetris.SetActive(false);
        // 3. 隨機取得下一個
        RandomTetris();

        // 將生成的俄羅斯方塊 RectTransform 元件儲存
        currentTetris = current.GetComponent<RectTransform>();
    }

    // 協同程序
    // IEnumerator 傳回類型 - 時間
    private IEnumerator ShakeEffect()
    {
        // 取得震動效果物件的 Rect
        RectTransform rect = traTetrisParent.GetComponent<RectTransform>();

        // 晃動 向上 30 > 0 > 20 > 0
        // 等待 0.05
        float interval = 0.05f;

        rect.anchoredPosition += Vector2.up * 30;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition += Vector2.up * 20;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(interval);
    }

    /// <summary>
    /// 快速掉落功能
    /// </summary>
    private void FastDown()
    {
        if (currentTetris && !fastDown)             // 如我 有 目前方塊 並且 還沒快速落下
        {
            if (Input.GetKeyDown(KeyCode.Space))    // 如果 按下 空白鍵
            {
                fastDown = true;                    // 正在快速落下
                timeFall = 0.0178f;                 // 掉落時間
            }
        }
    }

    /// <summary>
    /// 檢查方塊是否連線
    /// </summary>
    private IEnumerator CheckTetris()
    {
        rectSmall = new RectTransform[traScoreArea.childCount];                     // 指定數量跟子物件相同

        for (int i = 0; i < traScoreArea.childCount; i++)                           // 利用迴圈將子物件儲存
        {
            rectSmall[i] = traScoreArea.GetChild(i).GetComponent<RectTransform>();
        }

        int row = 17;                   //總共幾列：17

        for (int i = 0; i < row; i++)
        {
            float bottom = -300;        // 最底層的位置
            float step = 40;            // 每一層的間距

            // 檢查有幾棵位置在 -300 正負 10 - 避免誤差值
            var small = rectSmall.Where(x => x.anchoredPosition.y >= bottom + step * i - 10 && x.anchoredPosition.y <= bottom + step * i + 10);

            // 消除
            if (small.ToArray().Length == 16)
            {
                yield return StartCoroutine(Shine(small.ToArray()));                // 等待 開始閃爍 完成
                destroyRow[i] = true;                                               // 紀錄要刪除的列數
                AddScore(1000);
            }
        }

        downHeight = new float[traScoreArea.childCount];                            // 紀錄有幾顆刪除後剩下的方塊

        for (int i = 0; i < downHeight.Length; i++) downHeight[i] = 0;              // 先將掉落高度歸零

        // 計算每顆剩下方塊要掉落的高度
        for (int i = 0; i < destroyRow.Length; i++)
        {
            if (!destroyRow[i]) continue;                                       // 如果 此列 沒有要刪除 就跳過繼續下一列

            for (int j = 0; j < rectSmall.Length; j++)                          // 迴圈 執行 每一顆剩下的方塊
            {
                if (rectSmall[j].anchoredPosition.y > -300 + 40 * i - 10)       // 如果 此方塊 Y 大於 要刪除的列
                {
                    downHeight[j] -= 40;                                        // 座標 遞減 40
                }
            }

            destroyRow[i] = false;                                               // 恢復為不刪除
        }

        // 更新小方塊的高度：往下掉
        for (int i = 0; i < rectSmall.Length; i++)
        {
            rectSmall[i].anchoredPosition += Vector2.up * downHeight[i];
        }
    }

    /// <summary>
    /// 閃爍效果，閃爍後刪除
    /// </summary>
    /// <param name="smalls">要閃爍與刪除的列</param>
    private IEnumerator Shine(RectTransform[] smalls)
    {
        // 閃爍
        float interval = 0.05f;
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = true;

        // 刪除
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) Destroy(smalls[i].gameObject);

        // 重新取得小方塊：避免 Missing 導致錯誤
        yield return new WaitForSeconds(interval);
        rectSmall = new RectTransform[traScoreArea.childCount];                     // 指定數量跟子物件相同

        for (int i = 0; i < traScoreArea.childCount; i++)                           // 利用迴圈將子物件儲存
        {
            rectSmall[i] = traScoreArea.GetChild(i).GetComponent<RectTransform>();
        }
    }
    #endregion

    #region 方法：尚未實作

    [Header("分數文字")]
    public Text textScore;
    [Header("等級文字")]
    public Text textLv;
    /// <summary>
    /// 掉落時間最大值
    /// </summary>
    private float timeFllMax = 1.5f;

    /// <summary>
    /// 添加分數
    /// </summary>
    /// <param name="add">要添加的分數</param>
    public void AddScore(int add)
    {
        score += add;                           // 分數累加
        textScore.text = "分數：" + score;       // 更新介面

        lv = 1 + score / 1000;                  // 等級公式
        textLv.text = "等級：" + lv;             // 更新介面

        timeFllMax = 1.5f - lv / 2;             // 速度提升公式

        timeFllMax = Mathf.Clamp(timeFllMax, 0.1f, 99f);

        timeFall = timeFllMax;
    }

    private void GameOver()
    {

    }

    public void ReplayGame()
    {

    }

    public void QuitGame()
    {

    }
    #endregion
}

using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    [Header("掉落時間"), Range(0.1f, 3)]
    public float timeFall = 1.5f;
    [Header("目前分數")]
    public int score;
    [Header("最高分數")]
    public int scoreHight;
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
    [Header("畫布")]
    public Transform traCanvas;

    /// <summary>
    /// 下一顆俄羅斯方塊編號
    /// </summary>
    public int indexNext;

    private void Start()
    {
        RandomTetris();
    }

    /// <summary>
    /// 生成俄羅斯方塊
    /// 1. 隨機顯示一個下一顆俄羅斯方塊 0 - 6
    /// </summary>
    private void RandomTetris()
    {
        // 下一顆編號 = 隨機 的 範圍(最小，最大) - 整數不會等於最大值
        indexNext = Random.Range(0, 7);
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
        // 1. 生成俄羅斯方塊要放在正確位置
        // 保存上一次的俄羅斯方塊
        GameObject tetris = traNextArea.GetChild(indexNext).gameObject;
        // 目前俄羅斯方塊 = 生成物件(物件，父物件)
        GameObject current = Instantiate(tetris, traCanvas);
        // GetComponent<任何元件>()
        // <T> 泛型 - 指的是所有類型
        // 目前俄羅斯方塊 . 取得元件<介面變形>() . 座標 = 二維向量
        current.GetComponent<RectTransform>().anchoredPosition = new Vector2(40, 400);

        // 2. 上一次俄羅斯方塊隱藏
        tetris.SetActive(false);
        // 3. 隨機取得下一個
        RandomTetris();
    }

    /// <summary>
    /// 添加分數
    /// </summary>
    /// <param name="add">要添加的分數</param>
    public void AddScore(int add)
    {

    }

    private void GameTime()
    {

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
}

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

    /// <summary>
    /// 下一顆俄羅斯方塊編號
    /// </summary>
    public int indexNext;

    private void Start()
    {
        SpawnTetris();
    }

    /// <summary>
    /// 生成俄羅斯方塊
    /// 1. 隨機顯示一個下一顆俄羅斯方塊 0 - 6
    /// </summary>
    private void SpawnTetris()
    {
        // 下一顆編號 = 隨機 的 範圍(最小，最大) - 整數不會等於最大值
        indexNext = Random.Range(0, 7);
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

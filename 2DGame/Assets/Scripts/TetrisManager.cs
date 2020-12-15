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
    /// 生成俄羅斯方塊
    /// </summary>
    private void SpawnTetris()
    {

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

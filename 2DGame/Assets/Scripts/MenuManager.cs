using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // public 公開，允許按鈕呼叫
    /// <summary>
    /// 開始遊戲
    /// </summary>
    public void StartGame()
    {
        // 載入指定場景
        // 場景管理器 的 載入場景("場景名稱")
        // 場景管理器 的 載入場景(1)
        SceneManager.LoadScene("遊戲場景");
    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void QuitGame()
    {
        // 退出遊戲
        // 應用程式 的 離開遊戲()
        Application.Quit();
    }
}

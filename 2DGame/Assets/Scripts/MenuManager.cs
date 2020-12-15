using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // public 公開，允許按鈕呼叫
    /// <summary>
    /// 延遲呼叫，等音效播完
    /// </summary>
    public void DelayStartGame()
    {
        // 延遲呼叫("方法名稱"，延遲秒數)；
        // Invoke();
        Invoke("StartGame", 0.9f);
    }

    /// <summary>
    /// 延遲呼叫，等音效播完
    /// </summary>
    public void DelayQuitGame()
    {
        Invoke("QuitGame", 0.9f);
    }

    /// <summary>
    /// 開始遊戲
    /// </summary>
    private void StartGame()
    {
        // 載入指定場景
        // 場景管理器 的 載入場景("場景名稱")
        // 場景管理器 的 載入場景(1)
        SceneManager.LoadScene("遊戲場景");
    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    private void QuitGame()
    {
        // 退出遊戲
        // 應用程式 的 離開遊戲()
        Application.Quit();
    }
}

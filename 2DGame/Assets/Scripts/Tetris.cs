using UnityEngine;

public class Tetris : MonoBehaviour
{
    [Header("角度為零，線條的長度")]
    public float length0;
    [Header("角度為九十度，線條的長度")]
    public float length90;

    /// <summary>
    /// 紀錄目前長度
    /// </summary>
    public float length;

    /// <summary>
    /// 繪製圖示
    /// </summary>
    private void OnDrawGizmos()
    {
        // 將浮點數角度 轉為 整數 - 去小數點
        int z = (int)transform.eulerAngles.z;

        if (z == 0 || z == 180)
        {
            // 儲存目前長度
            length = length0;
            // 圖示 的 繪製射線(起點，方向 * 長度)
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * length0);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, -Vector3.right * length0);
        }
        else if (z == 90 || z == 270)
        {
            // 儲存目前長度
            length = length90;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * length90);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, -Vector3.right * length90);
        }
    }

    private void Start()
    {
        // 儲存遊戲開始的角度
        length = length0;
    }

    private void Update()
    {
        CheckWall();
    }

    /// <summary>
    /// 是否碰到右邊牆壁
    /// </summary>
    public bool wallRight;
    /// <summary>
    /// 是否碰到左邊牆壁
    /// </summary>
    public bool wallLeft;

    /// <summary>
    /// 檢查射線是否碰到牆壁
    /// </summary>
    private void CheckWall()
    {
        // 2D 物理碰撞資訊 區域變數名稱 = 2D 物理.射線碰撞(起點，方向，長度，圖層)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, length, 1 << 8);

        // 並且 &&
        // 如果 碰到東西 並且 名稱 為 牆壁：右邊
        if (hit && hit.transform.name == "牆壁：右邊")
        {
            wallRight = true;
        }
        else
        {
            wallRight = false;
        }

        // 2D 物理碰撞資訊 區域變數名稱 = 2D 物理.射線碰撞(起點，方向，長度，圖層)
        RaycastHit2D hitL = Physics2D.Raycast(transform.position, -Vector3.right, length, 1 << 8);

        // 並且 &&
        // 如果 碰到東西 並且 名稱 為 牆壁：右邊
        if (hitL && hitL.transform.name == "牆壁：左邊")
        {
            wallLeft = true;
        }
        else
        {
            wallLeft = false;
        }
    }
}

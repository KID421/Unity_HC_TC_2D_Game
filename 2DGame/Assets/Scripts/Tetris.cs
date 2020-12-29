using UnityEngine;

public class Tetris : MonoBehaviour
{
    [Header("角度為零，線條的長度")]
    public float length0;
    [Header("角度為九十度，線條的長度")]
    public float length90;
    [Header("旋轉位移左右")]
    public int offsetX;
    [Header("旋轉位移上下")]
    public int offsetY;

    /// <summary>
    /// 紀錄目前長度
    /// </summary>
    private float length;
    private float lengthDown;

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
            // 繪製向下線條
            lengthDown = length90;
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position, -Vector3.up * length90);
        }
        else if (z == 90 || z == 270)
        {
            // 儲存目前長度
            length = length90;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * length90);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, -Vector3.right * length90);
            // 繪製向下線條
            lengthDown = length0;
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position, -Vector3.up * length0);
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
    /// 是否碰到下方地板
    /// </summary>
    public bool wallDown;

    /// <summary>
    /// 檢查射線是否碰到牆壁
    /// </summary>
    private void CheckWall()
    {
        // 2D 物理碰撞資訊 區域變數名稱 = 2D 物理.射線碰撞(起點，方向，長度，圖層)
        RaycastHit2D hitR = Physics2D.Raycast(transform.position, Vector3.right, length, 1 << 8);

        // 並且 &&
        // 如果 碰到東西 並且 名稱 為 牆壁：右邊
        if (hitR && hitR.transform.name == "牆壁：右邊")
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

        // 2D 物理碰撞資訊 區域變數名稱 = 2D 物理.射線碰撞(起點，方向，長度，圖層)
        RaycastHit2D hitD = Physics2D.Raycast(transform.position, -Vector3.up, lengthDown, 1 << 9);

        // 並且 &&
        // 如果 碰到東西 並且 名稱 為 牆壁：右邊
        if (hitD && hitD.transform.name == "地板")
        {
            wallDown = true;
        }
        else
        {
            wallDown = false;
        }
    }
}

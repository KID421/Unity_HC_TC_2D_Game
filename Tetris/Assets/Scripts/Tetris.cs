using UnityEngine;
using System.Linq;          // 引用 系統.查詢語言 API - 偵測陣列、清單內的資料

public class Tetris : MonoBehaviour
{
    #region 欄位
    [Header("角度為零，線條的長度")]
    public float length0;
    [Header("角度為九十度，線條的長度")]
    public float length90;
    [Header("旋轉位移左右")]
    public int offsetX;
    [Header("旋轉位移上下")]
    public int offsetY;
    [Header("偵測是否能旋轉")]
    public float lengthRotate0r;
    public float lengthRotate0l;
    public float lengthRotate90r;
    public float lengthRotate90l;

    /// <summary>
    /// 紀錄目前長度
    /// </summary>
    private float length;
    private float lengthDown;
    private float lengthRotateR;
    private float lengthRotateL;

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
    /// 是否能旋轉
    /// </summary>
    public bool canRotate = true;

    private RectTransform rect;

    /// <summary>
    /// 小方塊底部碰撞
    /// </summary>
    public bool smallBottom;
    /// <summary>
    /// 右邊是否有方塊
    /// </summary>
    public bool smallRight;
    /// <summary>
    /// 所有方塊右邊是否有其他方塊
    /// </summary>
    public bool[] smallRightAll;
    /// <summary>
    /// 左邊是否有方塊
    /// </summary>
    public bool smallLeft;
    /// <summary>
    /// 所有方塊左邊是否有其他方塊
    /// </summary>
    public bool[] smallLeftAll;
    [Header("每一顆小方塊的射線長度"), Range(0f, 2f)]
    public float smallLength = 0.5f;
    #endregion

    #region 事件
    private void SettingLength()
    {
        #region 判定牆壁與地板
        // 將浮點數角度 轉為 整數 - 去小數點
        int z = (int)transform.eulerAngles.z;

        if (z == 0 || z == 180)
        {
            // 儲存目前長度
            length = length0;
            
            // 繪製向下線條
            lengthDown = length90;

            // 繪製旋轉射線
            lengthRotateR = lengthRotate0r;
            lengthRotateL = lengthRotate0l;
        }
        else if (z == 90 || z == 270)
        {
            // 儲存目前長度
            length = length90;
            // 繪製向下線條
            lengthDown = length0;

            // 繪製旋轉射線
            lengthRotateR = lengthRotate90r;
            lengthRotateL = lengthRotate90l;
        }
        #endregion
    }

    /// <summary>
    /// 繪製圖示
    /// </summary>
    private void OnDrawGizmos()
    {
        #region 判定牆壁與地板
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

            // 繪製旋轉射線
            lengthRotateR = lengthRotate0r;
            lengthRotateL = lengthRotate0l;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, Vector3.right * lengthRotate0r);
            Gizmos.DrawRay(transform.position, -Vector3.right * lengthRotate0l);
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
            
            // 繪製旋轉射線
            lengthRotateR = lengthRotate90r;
            lengthRotateL = lengthRotate90l;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, Vector3.right * lengthRotate90r);
            Gizmos.DrawRay(transform.position, -Vector3.right * lengthRotate90l);
        }
        #endregion

        #region 每一顆判定
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.down * smallLength);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.right * smallLength);
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.left * smallLength);
        }
        #endregion
    }

    private void Start()
    {
        // 儲存遊戲開始的長度
        length = length0;

        rect = GetComponent<RectTransform>();

        // 偵測有己的子物件(小方塊) 就新增幾個陣列
        smallRightAll = new bool[transform.childCount];
        smallLeftAll = new bool[transform.childCount];
    }

    private void Update()
    {
        SettingLength();
        CheckWall();
        CheckBottom();
        CheckLeftAndRight();
    }
    #endregion

    /// <summary>
    /// 檢查左邊與右邊是否有其他方塊
    /// </summary>
    private void CheckLeftAndRight()
    {
        // 迴圈執行每一顆小方塊
        for (int i = 0; i < transform.childCount; i++)
        {
            // 每一顆小方塊 射線(每一顆小方塊的中心點，向下，長度，圖層)
            RaycastHit2D hitR = Physics2D.Raycast(transform.GetChild(i).position, Vector3.right, smallLength, 1 << 10);
            // 如果 右邊有方塊 就 將陣列對應的格子勾選
            if (hitR && hitR.collider.name == "方塊") smallRightAll[i] = true;
            else smallRightAll[i] = false;

            RaycastHit2D hitL = Physics2D.Raycast(transform.GetChild(i).position, Vector3.left, smallLength, 1 << 10);

            if (hitL && hitL.collider.name == "方塊") smallLeftAll[i] = true;
            else smallLeftAll[i] = false;
        }

        // 檢查陣列內 等於 true 的資料
        // 陣列.哪裡(代名詞 => 條件)
        // var 無類型
        var allRight = smallRightAll.Where(x => x == true);
        smallRight = allRight.ToArray().Length > 0;

        var allLeft = smallLeftAll.Where(x => x == true);
        smallLeft = allLeft.ToArray().Length > 0;
    }

    /// <summary>
    /// 檢查底部是否有其他方塊
    /// </summary>
    private void CheckBottom()
    {
        // 迴圈執行每一顆小方塊
        for (int i = 0; i < transform.childCount; i++)
        {
            // 每一顆小方塊 射線(每一顆小方塊的中心點，向下，長度，圖層)
            RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(i).position, Vector3.down, smallLength, 1 << 10);

            if (hit && hit.collider.name == "方塊") smallBottom = true;
        }
    }

    #region 方法
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

        // 旋轉射線
        RaycastHit2D hitRotateR = Physics2D.Raycast(transform.position, Vector3.right, lengthRotateR, 1 << 8);
        RaycastHit2D hitRotateL = Physics2D.Raycast(transform.position, -Vector3.right, lengthRotateL, 1 << 8);

        if (hitRotateR && hitRotateR.transform.name == "牆壁：右邊" || hitRotateL && hitRotateL.transform.name == "牆壁：左邊")
        {
            canRotate = false;
        }
        else
        {
            canRotate = true;
        }


        // 2D 物理碰撞資訊 區域變數名稱 = 2D 物理.射線碰撞(起點，方向，長度，圖層)
        RaycastHit2D hitD = Physics2D.Raycast(transform.position, -Vector3.up, lengthDown, 1 << 9);

        // 並且 &&
        if (hitD && hitD.transform.name == "地板")
        {
            wallDown = true;
        }
        else
        {
            wallDown = false;
        }
    }

    /// <summary>
    /// 旋轉後位移處理
    /// </summary>
    public void Offset()
    {
        // 將浮點數角度 轉為 整數 - 去小數點
        int z = (int)transform.eulerAngles.z;

        if (z == 90 || z == 270)
        {
            rect.anchoredPosition -= new Vector2(offsetX, offsetY);
        }
        else if (z == 0 || z == 180)
        {
            rect.anchoredPosition += new Vector2(offsetX, offsetY);
        }
    }
    #endregion
}

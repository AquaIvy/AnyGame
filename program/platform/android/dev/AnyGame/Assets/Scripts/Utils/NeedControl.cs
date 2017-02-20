using UnityEngine;

/// <summary>
/// 将该脚本挂载到需要交互（操作）的游戏物体上，并给与唯一名字
/// 程序会自动生成该游戏物体的结构路径
/// 以便游戏运行时可根据名字而映射出结构路径查找游戏物体
/// 解决了美术更改UI结构时，程序必须同步修改的麻烦问题
/// </summary>
public class NeedControl : MonoBehaviour
{
    public string CtrlName = "未赋值";

    void Reset()
    {
        if (transform.GetComponent<NeedControl>().CtrlName == "未赋值")
        {
            transform.GetComponent<NeedControl>().CtrlName = transform.name;
        }
    }
}

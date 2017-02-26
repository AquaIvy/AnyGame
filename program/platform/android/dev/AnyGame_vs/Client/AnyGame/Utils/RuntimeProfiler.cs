using System.Diagnostics;
using UnityEngine;

public class RuntimeProfiler : MonoBehaviour
{
    public static bool isShowInfo = true;
    public static int FPS { get; private set; }

    private int frames = 0;

    private Stopwatch stopwatch;


    private Rect rect = new Rect(2, 2, 150f / 1280 * Screen.width, 100f / 720 * Screen.height);
    GUIStyle style = null;
    string info = "";

    void Start()
    {
        stopwatch = new Stopwatch();
        stopwatch.Reset();
        stopwatch.Start();

        style = new GUIStyle();
        style.normal.background = null;    //这是设置背景填充的
        style.normal.textColor = new Color(1, 0, 0);   //设置字体颜色的
        style.fontSize = (int)(Screen.width / 32f);       //当然，这是字体颜色
    }

    void Update()
    {
        ++frames;
        if (stopwatch == null)
        {
            stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
        }
        if (stopwatch.ElapsedMilliseconds >= 1000)
        {
            FPS = frames;
            frames = 0;

            stopwatch.Start();
            stopwatch.Reset();
            stopwatch.Start();
        }

    }


    void OnGUI()
    {
        if (!isShowInfo)
        {
            return;
        }

        if (Time.frameCount % 10 == 0)
        {
            info = "   " + FPS + "帧      \n"
                + (Profiler.GetTotalAllocatedMemory() / 1024.0f / 1024.0f).ToString("0.0") + " MB";
            //+ (Profiler.GetTotalReservedMemory() / 1024.0f / 1024.0f).ToString("0.0") + " M 保留\r\n"
            //+ (Profiler.GetTotalUnusedReservedMemory() / 1024.0f / 1024.0f).ToString("0.0") + " M 未使用\r\n";
        }

        GUI.Box(rect, string.Empty);
        GUI.Label(rect, info, style);
    }
}


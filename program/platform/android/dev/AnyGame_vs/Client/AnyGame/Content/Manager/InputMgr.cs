using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputMgr : MonoBehaviour
{
    public event EventHandler<TouchBeginEventArgs> OnTouchBegin;
    public event EventHandler<TouchStationaryEventArgs> OnTouchStationary;
    public event EventHandler<TouchMovedEventArgs> OnTouchMoved;
    public event EventHandler<TouchFirstMovedEventArgs> OnTouchFirstMoved;
    public event EventHandler<TouchEndEventArgs> OnTouchEnd;


    private bool isFirstMove = false;
    private Vector2 moveDistance = Vector2.zero;       //手指滑动的距离
    private MoveDirection moveDirection = MoveDirection.Horizontal;
    //private bool isStationary = false;                 //是否触发过固定

    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                moveDistance = Vector2.zero;
                isFirstMove = true;
                //isStationary = false;

                if (OnTouchBegin != null)
                {
                    OnTouchBegin(this, new TouchBeginEventArgs
                    {
                        position = Input.GetTouch(0).position
                    });
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Stationary)  //固定
            {
                if (OnTouchStationary != null)
                {
                    OnTouchStationary(this, new TouchStationaryEventArgs
                    {
                        position = Input.GetTouch(0).position,
                        deltaTime = Input.GetTouch(0).deltaTime
                    });
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {

                float x = Input.GetTouch(0).deltaPosition.x;
                float y = Input.GetTouch(0).deltaPosition.y;


                //触发第一次滑动，判断滑动方向
                if (isFirstMove)
                {
                    isFirstMove = false;

                    if (OnTouchFirstMoved != null)
                    {
                        bool isVertical = (Mathf.Abs(y) * 1.0f / Mathf.Abs(x)) > 1.0f;
                        this.moveDirection = isVertical ? MoveDirection.Vertical : MoveDirection.Horizontal;

                        OnTouchFirstMoved(this, new TouchFirstMovedEventArgs
                        {
                            moveDirection = this.moveDirection
                        });
                    }
                }

                moveDistance.x += x;
                moveDistance.y += y;

                if (OnTouchMoved != null)
                {
                    OnTouchMoved(this, new TouchMovedEventArgs
                    {
                        position = Input.GetTouch(0).position,
                        deltaPosition = Input.GetTouch(0).deltaPosition,
                        moveDirection = this.moveDirection,
                        moveDistance = this.moveDistance
                    });
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (OnTouchEnd != null)
                {
                    OnTouchEnd(this, new TouchEndEventArgs
                    {
                        position = Input.GetTouch(0).position,
                        moveDirection = this.moveDirection,
                        moveDistance = moveDistance
                    });
                }
            }
        }
    }
}



/// <summary>
/// 开始触碰屏幕
/// </summary>
public class TouchBeginEventArgs : EventArgs
{
    public Vector2 position { get; internal set; }
}

/// <summary>
/// 按压屏幕不动
/// </summary>
public class TouchStationaryEventArgs : EventArgs
{
    public Vector2 position { get; internal set; }

    public float deltaTime { get; internal set; }
}


/// <summary>
/// 第一下滑动屏幕时触发
/// </summary>
public class TouchFirstMovedEventArgs : EventArgs
{
    public MoveDirection moveDirection { get; internal set; }
}

/// <summary>
/// 滑动屏幕
/// </summary>
public class TouchMovedEventArgs : EventArgs
{
    public Vector2 position { get; internal set; }
    public Vector2 deltaPosition { get; internal set; }
    public MoveDirection moveDirection { get; internal set; }
    public Vector2 moveDistance { get; internal set; }
}
public enum MoveDirection
{
    Horizontal = 0,
    Vertical
}

/// <summary>
/// 抬起手指
/// </summary>
public class TouchEndEventArgs : EventArgs
{
    public Vector2 position { get; internal set; }
    public MoveDirection moveDirection { get; internal set; }
    public Vector2 moveDistance { get; internal set; }
}




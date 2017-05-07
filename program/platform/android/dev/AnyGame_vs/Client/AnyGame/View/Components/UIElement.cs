using AnyGame.View;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.View.Components
{
    public abstract class UIElement
    {
        public Game Game { get; internal set; }

        private string _name;
        public string Name { get { return _name; } set { _name = value; go.name = value; } }

        public GameObject go { get; set; }


        private RectTransform _rt;
        public RectTransform rt
        {
            get
            {
                if (go == null)
                {
                    return null;
                }

                if (_rt == null)
                {
                    //_rt = go.GetComponent<RectTransform>();

                    //_rt.anchorMin = UIUtils.UpperLeft;
                    //_rt.anchorMax = UIUtils.UpperLeft;
                    //pivot = UIUtils.UpperLeft;

                    Debug.LogError("RectTransform is null.");
                }

                return _rt;
            }

            set
            {
                _rt = value;
            }
        }


        public object tag { get; set; }

        /// <summary>
        /// 中心点（影响旋转）
        /// </summary>
        public virtual Vector2 Pivot
        {
            get { return rt.pivot; }
            set { rt.pivot = value; }
        }

        //private Vector2 _anchorMin = UIUtils.UpperLeft;
        //private Vector2 _anchorMax = UIUtils.UpperLeft;

        /// <summary>
        /// 相对于父物体的哪一个位置，标记为自己的(0,0)点   [0,1]
        /// </summary>
        public Vector2 anchorMin
        {
            get { return rt.anchorMin; }
            set { rt.anchorMin = value; }
        }

        /// <summary>
        /// 相对于父物体的哪一个位置，标记为自己的(0,0)点   [0,1]
        /// </summary>
        public Vector2 anchorMax
        {
            get { return rt.anchorMax; }
            set { rt.anchorMax = value; }
        }


        public float x
        {
            get { return Position.x; }
            set { SetXY(value, Position.y); }
        }

        public float y
        {
            get { return Position.y; }
            set { SetXY(Position.x, value); }
        }

        public virtual float Alpha { get; set; }

        public UIElement SetXY(float x, float y)
        {
            Position = new Vector3(x, y, Position.z);

            return this;
        }


        private Vector3 _position = Vector3.zero;
        public Vector3 Position
        {
            get { return _position; }
            set
            {
                //方案一  不转坐标  向下y减小
                //rt.anchoredPosition3D = value;
                //_position = rt.anchoredPosition3D;

                //方案二  转坐标   向下y减小
                rt.anchoredPosition3D = new Vector3(value.x, -value.y, value.z);
                _position = value;
            }
        }


        public virtual float Width
        {
            get { return Size.x; }
            set { Size = new Vector2(value, Size.y); }
        }

        public virtual float Height
        {
            get { return Size.y; }
            set { Size = new Vector2(Size.x, value); }
        }

        public Vector2 Size
        {
            get { return rt.sizeDelta; }
            set { rt.sizeDelta = value; }
        }


        public virtual float HarfWidth { get { return Width / 2f; } }
        public virtual float HarfHeight { get { return Height / 2f; } }



        private Vector3 _scale = Vector3.one;
        public Vector3 Scale
        {
            get { return rt.localScale; }
            set { rt.localScale = value; _scale = value; }
        }


        public UIElement()
        {
            go = new GameObject();

            _rt = go.AddComponent<RectTransform>();

            _rt.anchorMin = UIUtils.UpperLeft;
            _rt.anchorMax = UIUtils.UpperLeft;
        }


        private List<UIElement> lstChild = new List<UIElement>();
        public int childCount { get { return lstChild.Count; } }


        private UIElement _parent;
        public UIElement Parent
        {
            get { return _parent; }
            set
            {
                if (_parent == value)
                    return;


                //1.移除原先parent的计数
                if (_parent != value && _parent != null)
                {
                    _parent.lstChild.Remove(this);
                }
                if (rt == null)
                    return;

                //2.设置新parent
                if (value != null)
                {
                    this.rt.SetParent(value.rt);
                    value.lstChild.Add(this);
                    _parent = value;
                }
                else
                {
                    this.rt.SetParent(null);
                    _parent = null;
                }

                //3.重新设定transform信息
                rt.localScale = _scale;
                this.Position = _position;
            }
        }


        private bool _visible = true;
        /// <summary>
        /// 获取或设置Element的显示状态
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { go.SetActive(value); _visible = go.activeSelf; }
        }

        /// <summary>
        /// 获取gameObject的显示状态
        /// </summary>
        public bool gameobjectVisible { get { return go.activeSelf; } }


        public virtual bool enable { get; set; }



        public virtual UIElement AddChild(UIElement element)
        {
            if (element.Parent != null)
            {
                throw new InvalidOperationException(element.Name + "对象已被AddChild到其他节点上");
            }

            element.Parent = this;

            element.anchorMin = element.anchorMax = this.Pivot;

            element.SetXY(element.Position.x, element.Position.y);

            return element;
        }

        public virtual void RemoveChild(UIElement element)
        {
            if (element.Parent != this) return;

            element.Parent = null;
        }



        /// <summary>
        /// 窗体显示了多久的时间，单位:毫秒
        /// </summary>
        public int ElapseMilliseconds { get; private set; }

        protected virtual void Update(int milliseconds)
        {
            ElapseMilliseconds += milliseconds;

            //这里用倒序更新   是正确逻辑么？
            for (int i = lstChild.Count - 1; i >= 0; i--)
            {
                lstChild[i].Update(milliseconds);
            }
        }



        public virtual void Dispose()
        {
            if (lstChild.Count > 0)
            {
                for (int i = lstChild.Count - 1; i >= 0; i--)
                {
                    lstChild[i].Dispose();
                }
            }

            if (Parent != null)
            {
                Parent.RemoveChild(this);
            }

            if (go != null)
            {
                //Logs.Error("destroy 1  " + name);
                GameObject.Destroy(go);
                go = null;
            }

        }

        public virtual void DisposeAllChilds()
        {
            for (int i = lstChild.Count - 1; i >= 0; i--)
            {
                lstChild[i].Dispose();
            }
        }

        ~UIElement()
        {
            if (go != null)
            {
                //放到主线程去销毁
                Task.Invoke(t =>
                {
                    if (go != null)
                    {
                        Logs.Error("destroy 2  " + Name);
                        GameObject.DestroyImmediate(go, true);
                        go = null;
                    }
                    return true;
                });
            }
        }

        /// <summary>
        /// 将pivot和anchor都设置为中心
        /// </summary>
        public void SetCenter()
        {
            Pivot = UIUtils.MiddleCenter;
            anchorMax = anchorMin = UIUtils.MiddleCenter;
        }

        /// <summary>
        /// 将anchor锚点设置为左上角
        /// </summary>
        public void SetUpperLeft()
        {
            anchorMax = anchorMin = UIUtils.UpperLeft;
        }

        /// <summary>
        /// 设置渲染顺序
        /// </summary>
        /// <param name="index"></param>
        public void SetSiblingIndex(int index)
        {
            rt.SetSiblingIndex(index);
        }
    }
}

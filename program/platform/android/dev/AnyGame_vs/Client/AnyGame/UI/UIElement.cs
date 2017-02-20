using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.UI
{
    abstract class UIElement
    {
        public Game Game { get; internal set; }

        public string name
        {
            get { return go.name; }
            set { go.name = value; }
        }


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
                    _rt = go.GetComponent<RectTransform>();

                    anchorMin = UIUtils.UpperLeft;
                    anchorMax = UIUtils.UpperLeft;
                    //pivot = UIUtils.UpperLeft;

                    //Debug.LogError("RectTransform is null.");
                }

                return _rt;
            }
        }


        public object tag { get; set; }


        public virtual Vector2 pivot
        {
            get { return rt.pivot; }
            set { rt.pivot = value; }
        }

        /// <summary>
        /// 相对于父物体计算(0,0)点的锚点   [0,1]
        /// </summary>
        public Vector2 anchorMin
        {
            get { return rt.anchorMin; }
            set { rt.anchorMin = value; }
        }

        /// <summary>
        /// 相对于父物体计算(0,0)点的锚点   [0,1]
        /// </summary>
        public Vector2 anchorMax
        {
            get { return rt.anchorMax; }
            set { rt.anchorMax = value; }
        }


        public float x
        {
            get { return position.x; }
            set { SetXY(value, position.y); }
        }
        public float y
        {
            get { return position.y; }
            set { SetXY(position.x, value); }
        }


        public UIElement SetXY(float x, float y)
        {
            position = new Vector3(x, y, position.z);

            return this;
        }


        private Vector3 _position = Vector3.zero;
        public Vector3 position
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


        public virtual float width
        {
            get { return size.x; }
            set { size = new Vector2(value, size.y); }
        }

        public virtual float height
        {
            get { return size.y; }
            set { size = new Vector2(size.x, value); }
        }

        public Vector2 size
        {
            get { return rt.sizeDelta; }
            set { rt.sizeDelta = value; }
        }


        public virtual float harfWidth { get { return width / 2f; } }
        public virtual float harfHeight { get { return height / 2f; } }



        private Vector3 _scale = Vector3.one;
        public Vector3 scale
        {
            get { return rt.localScale; }
            set { rt.localScale = value; _scale = value; }
        }


        public UIElement()
        {
            go = new GameObject();
            //_rt = go.AddComponent<RectTransform>();
        }


        public int childCount { get { return lstChild.Count; } }


        private List<UIElement> lstChild = new List<UIElement>();


        private UIElement _parent;
        public UIElement parent
        {
            get { return _parent; }
            set
            {
                //1.移除原先parent的计数
                if (_parent != value && _parent != null) { _parent.lstChild.Remove(this); }
                if (rt == null) return;

                //2.设置新parent
                if (value != null)
                {
                    _parent = value;
                    rt.SetParent(parent.rt);
                    parent.lstChild.Add(this);
                }
                else
                {
                    _parent = null;
                    rt.SetParent(null);
                }

                //3.重新设定transform信息
                rt.localScale = _scale;
                position = _position;
            }
        }


        private bool _visible = true;
        public bool visible
        {
            get { return _visible; }
            set { go.SetActive(value); _visible = go.activeSelf; }
        }

        public bool gameobjectVisible { get { return go.activeSelf; } }


        public virtual bool enable { get; set; }



        public virtual UIElement AddChild(UIElement element)
        {
            if (element.parent != null)
            {
                throw new InvalidOperationException(element.name + "对象已被AddChild到其他节点上");
            }

            element.parent = this;
            //if (this.pivot == UIUtils.MiddleCenter)
            //{
            //    element.anchorMin = element.anchorMax = UIUtils.MiddleCenter;
            //}

            element.anchorMin = element.anchorMax = this.pivot;

            element.SetXY(element.position.x, element.position.y);

            return element;
        }

        public virtual void RemoveChild(UIElement element)
        {
            if (element.parent != this)
            {
                return;
            }

            element.parent = null;
        }



        /// <summary>
        /// 窗体显示了多久的时间，单位:毫秒
        /// </summary>
        public int ElapseMilliseconds { get; private set; }

        protected virtual void Update(int milliseconds)
        {
            ElapseMilliseconds += milliseconds;

            //这里才用倒序更新   是正确逻辑么？
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

            if (parent != null)
            {
                parent.RemoveChild(this);
            }

            if (go != null)
            {
                //Logs.Error("destroy 1  " + name);
                GameObject.Destroy(go);
                //GameObject.DestroyImmediate(go, true);
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
                        Logs.Error("destroy 2  " + name);
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
        public void Center()
        {
            pivot = UIUtils.MiddleCenter;
            anchorMax = anchorMin = UIUtils.MiddleCenter;
        }

        /// <summary>
        /// 将锚点设置为左上角
        /// </summary>
        public void UpperLeft()
        {
            anchorMax = anchorMin = UIUtils.UpperLeft;
        }
    }
}

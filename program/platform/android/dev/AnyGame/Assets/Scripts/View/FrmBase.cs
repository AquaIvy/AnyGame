using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class FrmBase : IDisposable
    {
        protected GameObject gameObject { get; set; }
        protected Transform transform { get; set; }

        public bool IsShowing { get; private set; }

        public virtual void Show()
        {
            var root = GameObject.Find("/Canvas").transform;

            if (transform != null)
            {
                transform.SetParent(root);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                transform.localScale = Vector3.one;

                gameObject.SetActive(true);
                IsShowing = true;
            }
        }

        public virtual void Dispose()
        {


            if (gameObject != null)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}

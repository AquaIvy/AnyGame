using UnityEngine;

public class DontDestroyThisGameObject : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);

        //string tag = transform.gameObject.tag;
        //GameObject[] go = GameObject.FindGameObjectsWithTag(tag);
        //for (int i = 1; i < go.Length; i++)
        //{
        //    Destroy(go[i]);
        //}
    }
}


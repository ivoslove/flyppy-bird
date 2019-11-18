using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToViewportPoint(obj.transform.position);
        Debug.LogError(string.Format("pos:({0},{1},{2})", pos.x, pos.y, pos.z));
    }
}

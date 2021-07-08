using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDamage : MonoBehaviour
{

    public float destroyTime = 3f;
    Vector3 offset = new Vector3(1,3.5f,-1);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        //Debug.Log(transform.localPosition);
        //transform.localPosition += offset;
        //Debug.Log(transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

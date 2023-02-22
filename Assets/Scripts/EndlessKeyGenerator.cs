using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessKeyGenerator : MonoBehaviour
{
    public GameObject replicateObject;
    public Vector3 Offset;

    public GameObject trackedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trackedObject!=null)
        {
            if(trackedObject.transform.position.y<-30.0f)
            {
                Destroy(trackedObject);
                trackedObject = null;
            }
        }else
        {
            CreateNew();
        }
    }

    public void CreateNew()
    {
        GameObject g = Instantiate(replicateObject);
        g.transform.position = Offset+transform.position;
        trackedObject = g;
    }
}

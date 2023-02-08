using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyAbleMesh : MonoBehaviour
{
    public MeshFilter meshFilter;
    // Start is called before the first frame update
    void Start()
    {
        if(meshFilter!=null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }
    }
    /*
    public Vector3 GetObjectSpacePos(Vector3 worldSpace)
    {
        transform.in
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}

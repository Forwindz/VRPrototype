using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPosition : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject pivot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pivot.transform.position;
                    transform.rotation= pivot.transform.rotation;

    }
}

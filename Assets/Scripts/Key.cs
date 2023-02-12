using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Latern attachedLaten;
    public Latern[] avaliableLaterns;
    // Start is called before the first frame update
    void Start()
    {
        if(avaliableLaterns == null || avaliableLaterns.Length==0)
        {
            avaliableLaterns = FindObjectsOfType<Latern>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(attachedLaten!=null)
        {
            return;
        }
        if(avaliableLaterns==null)
        {
            return;
        }
        foreach(Latern latern in avaliableLaterns)
        {
            if(latern.attachedKey!=null)
            {
                continue;
            }
            latern.CheckKey(this);
        }
    }
}

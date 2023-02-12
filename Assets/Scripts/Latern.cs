using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latern : MonoBehaviour
{
    public Key attachedKey;
    public float acceptRange = 5.0f;
    public Vector3 offsetPos;
    public List<IKeyListener> triggers = new List<IKeyListener>();

    public void CheckKey(Key k)
    {
        float distance = Vector3.Distance(transform.position+offsetPos, k.transform.position);
        float scales = (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3.0f;
        Debug.Log(distance);
        if(distance<acceptRange*scales)
        {
            AttachKey(k);
        }
    }

    public void AttachKey(Key k)
    {
        attachedKey = k;
        if(k==null)
        {
            TriggerKey();
            return;
        }
        k.attachedLaten = this;
        k.transform.position = transform.position + offsetPos;
        TriggerKey();
    }

    public void TriggerKey()
    {
        foreach(IKeyListener ikl in triggers)
        {
            if(ikl==null)
            {
                continue;
            }
            ikl.OnKeyStatus(this, attachedKey);
        }
    }

    public void Update()
    {
        if(attachedKey!=null)
        {
            attachedKey.transform.position = transform.position + offsetPos;
        }
    }
}

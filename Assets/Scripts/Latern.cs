using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latern : IKeyListener
{
    public bool isEnable = true;
    public Key attachedKey;
    public float acceptRange = 5.0f;
    public Vector3 offsetPos;
    public List<IKeyListener> triggers = new List<IKeyListener>();

    public void CheckKey(Key k)
    {
        if(!isEnable)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position+offsetPos, k.transform.position);
        float scales = (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3.0f;
        //Debug.Log(distance);
        if(distance<acceptRange*scales)
        {
            AttachKey(k);
        }
    }

    public void AttachKey(Key k)
    {
        if (!isEnable)
        {
            return;
        }
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
            ikl.OnTriggerSomething(attachedKey != null);
        }
    }

    public void Update()
    {
        if(attachedKey!=null)
        {
            attachedKey.transform.position = transform.position + offsetPos;
        }
    }

    public override void OnKeyStatus(Latern latern, Key key)
    {
        base.OnKeyStatus(latern, key);
        isEnable = latern.attachedKey;
    }

    public override void OnTriggerSomething(bool b)
    {
        base.OnTriggerSomething(b);
        isEnable = b;
    }
}

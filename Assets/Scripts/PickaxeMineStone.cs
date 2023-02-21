using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeMineStone : IKeyListener
{
    public Fracture fracture;
    public GameObject pickaxe;
    public List<IKeyListener> triggers = new List<IKeyListener>();
    public AudioSource audioSource;
    public AudioClip crackSound;

    public float curDurability = 10;
    // Start is called before the first frame update
    void Start()
    {
        if(fracture==null)
        {
            fracture = GetComponent<Fracture>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject==pickaxe || pickaxe==null && collision.gameObject.name.ToLower().Contains("pickaxe"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            float energy = rb.velocity.sqrMagnitude*rb.mass;
            curDurability -= energy;
            if(audioSource!=null && crackSound != null)
                audioSource.PlayOneShot(crackSound,Mathf.Min(1.2f,energy/5.0f));
            if (curDurability<=0)
            {
                fracture.FractureObject();
                if(audioSource != null && crackSound != null)
                    audioSource.PlayOneShot(crackSound, Mathf.Min(1.2f, energy / 5.0f));
                TriggerKey();
            }
        }
    }

    public override void OnKeyStatus(Latern latern, Key key)
    {
        base.OnKeyStatus(latern, key);
        if(latern.attachedKey!=null)
        {
            fracture.FractureObject();
            TriggerKey();
        }
    }

    public override void OnTriggerSomething(bool b)
    {
        base.OnTriggerSomething(b);
        if(b)
        {
            fracture.FractureObject();
            TriggerKey();
        }
    }

    public void TriggerKey()
    {
        foreach (IKeyListener ikl in triggers)
        {
            if (ikl == null)
            {
                continue;
            }
            ikl.OnTriggerSomething(true);
        }
    }
}

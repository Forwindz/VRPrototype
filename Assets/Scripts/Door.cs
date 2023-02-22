using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : IKeyListener
{
    public bool isOpen = false;
    public float openDownDistance = 5.0f;
    public float speed = 1.0f;
    protected float curOpenDownDistance = 0.0f;
    public AudioSource source;

    protected bool lastState = false;

    // Start is called before the first frame update
    void Start()
    {
        if(source==null)
            source = GetComponent<AudioSource>();
        lastState = isOpen;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = speed * Time.deltaTime;
        if (isOpen)
        {
            if(curOpenDownDistance>=openDownDistance)
            {
                return;
            }
            dt *= (openDownDistance - curOpenDownDistance)*0.7f + dt * 0.3f ;
            curOpenDownDistance += dt;
            transform.position += Vector3.down * dt;
        }
        else
        {
            if (curOpenDownDistance <= 0.0f)
            {
                return;
            }
            dt *= curOpenDownDistance*0.7f+dt*0.3f;
            curOpenDownDistance -= dt;
            transform.position -= Vector3.down * dt;
        }
        if(lastState!=isOpen)
        {
            lastState = isOpen;
            source.Play();
        }
    }

    public override void OnKeyStatus(Latern latern, Key key)
    {
        base.OnKeyStatus(latern, key);
        isOpen = key != null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshModifyInteraction : MonoBehaviour
{
    public GameObject summonObjectTemplate = null;
    public GameObject curSummonObject = null;
    public enum State
    {
        NONE,
        CONTROL
    }

    public State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (state)
        {
            case State.NONE:
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        Debug.DrawLine(ray.origin, hitInfo.point);
                        FirstHitPos(hitInfo);
                    }
                }
                break;
            case State.CONTROL:
                curSummonObject.transform.localScale = new Vector3(curX, curY, curZ);
                curSummonObject.transform.position = orgPos + normal * curY * 0.5f;
                if (Input.GetMouseButton(1))
                {
                    EndHitPos();
                }
                break;
        }
    }

    public Vector3 normal,orgPos;
    public float curX, curY, curZ;

    public void FirstHitPos(RaycastHit hit)
    {
        curX = 0.05f;
        curY = 0.05f;
        curZ = 0.05f;
        state = State.CONTROL;
        curSummonObject = Instantiate(summonObjectTemplate);
        curSummonObject.transform.position = hit.point;
        orgPos = hit.point;
        Debug.Log(hit.point);
        normal = hit.normal;
    }

    public void EndHitPos()
    {
        curSummonObject = null;
        curX = curY = curZ = 0.0f;
        state = State.NONE;
    }


}

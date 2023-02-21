using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MeshModifyInteraction : MonoBehaviour
{
    public GameObject summonObjectTemplate = null;
    protected GameObject curSummonObject = null;

    public SteamVR_Action_Boolean summonAction;
    public SteamVR_Action_Boolean destoryAction;
    public float speed = 5.0f;
    public float basicX = 2.0f;
    public float basicY = 2.0f;
    public float basicZ = 0.05f;

    public Transform referenceController;
    Vector3 referencePos;

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

    bool SearchHit(Ray ray, out RaycastHit hitInfo)
    {
        while (Physics.Raycast(ray, out hitInfo))
        {
            if(hitInfo.collider.gameObject.isStatic)
            {
                return true;
            }
            else
            {
                ray.origin += (hitInfo.distance + 0.01f) * ray.direction;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.NONE:
                
                if (Input.GetMouseButton(0) || summonAction.stateDown)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (SearchHit(ray, out hitInfo))
                    {
                        Debug.DrawLine(ray.origin, hitInfo.point);
                        
                        FirstHitPos(ray.direction, hitInfo);
                    }
                }
                if (Input.GetMouseButton(1) || destoryAction.stateDown)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (SearchHit(ray, out hitInfo))
                    {
                        ModifyAbleMesh mesh = hitInfo.collider.gameObject.GetComponent<ModifyAbleMesh>();
                        if (mesh != null)
                        {
                            Destroy(mesh.gameObject);
                        }
                    }
                    
                }
                break;
            case State.CONTROL:
                curSummonObject.transform.localScale = new Vector3(curX, curY, curZ);
                curSummonObject.transform.position = orgPos + normal * curZ * 0.5f;
                curSummonObject.transform.rotation = Quaternion.LookRotation(normal);
                Vector3 dPos = referenceController.position - referencePos;
                curZ = Vector3.Dot(dPos, normal)*speed + basicZ;
                curX = Vector3.Dot(dPos, right)*speed + basicX;
                curY = Vector3.Dot(dPos, forward)*speed + basicY;
                
                if (Input.GetMouseButton(1) || summonAction.stateUp)
                {
                    EndHitPos();
                }
                break;
        }
    }

    [SerializeField]
    protected Vector3 normal,orgPos,right,forward;
    [SerializeField]
    protected float curX, curY, curZ;

    public void FirstHitPos(Vector3 rayDir, RaycastHit hit)
    {
        curX = basicX;
        curY = basicY;
        curZ = 0.5f;
        state = State.CONTROL;
        curSummonObject = Instantiate(summonObjectTemplate);
        curSummonObject.transform.position = hit.point;
        curSummonObject.AddComponent<ModifyAbleMesh>();
        orgPos = hit.point;
        Debug.Log(hit.point);
        normal = hit.normal;
        right = Vector3.Cross(hit.normal, rayDir);
        forward = Vector3.Cross(hit.normal, right);
        referencePos = referenceController.position;
    }

    public void EndHitPos()
    {
        curSummonObject = null;
        curX = curY = curZ = 0.0f;
        state = State.NONE;
    }


}

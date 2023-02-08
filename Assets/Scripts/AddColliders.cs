using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColliders : MonoBehaviour
{
    public GameObject targetObject;
    public List<string> containKeyWordForBoxCollider;
    public List<string> excludeKeyWordForCollider;
    //left objects will be applied with MeshCollider;
    public void TryToAddCollider()
    {
        InternalAddCollider(targetObject);
    }

    protected void InternalAddCollider(GameObject gameObject)
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        if(mr!=null)
        {
            bool skip = false;
            bool isMeshCollider = false;
            BoxCollider t = gameObject.GetComponent<BoxCollider>();
            if(t!=null)
            {
                DestroyImmediate(t);
            }

            MeshCollider t2 = gameObject.GetComponent<MeshCollider>();
            if (t2 != null)
            {
                DestroyImmediate(t2);
            }

            if (containString(gameObject.name,containKeyWordForBoxCollider))
            {
                gameObject.AddComponent<BoxCollider>();
            }
            else if (!containString(gameObject.name,excludeKeyWordForCollider))
            {
                gameObject.AddComponent<MeshCollider>();
                isMeshCollider = true;
            }
            else
            {
                skip = true;
            }
            if(!skip)
            {
                Rigidbody rb2 = gameObject.GetComponent<Rigidbody>();
                if (rb2==null)
                {
                    rb2 = gameObject.AddComponent<Rigidbody>();
                }
                rb2.constraints = RigidbodyConstraints.FreezeAll;
                rb2.useGravity = false;
                rb2.isKinematic = true;

            }
        }
        int count = gameObject.transform.childCount;
        for(int i=0;i<count;i++)
        {
            Transform sub = gameObject.transform.GetChild(i);
            InternalAddCollider(sub.gameObject);
        }
    }

    protected bool containString(string s, List<string> pool)
    {
        s = s.ToLower();
        foreach(string refer in pool)
        {
            if(s.Contains(refer.ToLower()))
            {
                return true;
            }
        }
        return false;
    }

}

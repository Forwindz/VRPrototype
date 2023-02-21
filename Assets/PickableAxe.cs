using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PickableAxe : MonoBehaviour

    
{
    Interactable interactable;

    private bool shouldAlign = false;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  //  public void showController() { }

   // public void HideController() { }


    public void alignToHand() { }


    public void OnPicked() { }

    public void OnDropped() {
    
    }

}

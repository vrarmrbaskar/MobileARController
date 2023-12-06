using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class ImageTargetEvents : DefaultObserverEventHandler
{
   



    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is for point the 3D object and pick the same
public class PointAndPick : MonoBehaviour
{
    public GameObject imgTgt;
    public GameObject boundingBox;

    public ReceiveMessage msgFromCtrller;

    //Reference variable to check the boundedness
    int bounded = 0;


    // Start is called before the first frame update
    void Start()
    {   
        //Image target : used for displaying 3D object on recognition of image.
        imgTgt = GameObject.FindGameObjectWithTag("ImgTarget");
        //This variable to handle ReceiveMessage from AR Controller
        msgFromCtrller = GameObject.FindObjectOfType<ReceiveMessage>();
        //  boundingBox = GameObject.FindGameObjectWithTag("bounded");
        //   boundBox.SetActive(false);
        bounded = 0;
    }



    private void OnTriggerEnter(Collider collider)
    {
        //On Trigger enter , set the bounded value as '1'
        bounded = 1;
        if (collider.tag == "ImgTarget")
        {

            //      boundingBox.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //On Trigger exit , set the bounded value as '0'
        bounded = 0;
        if (collider.tag == "ImgTarget")
        {
            //     boundingBox.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        // Bounded condition 
        if (bounded == 1)
        {
            Debug.Log("==== BOUNDED =====");
            //  boundingBox.SetActive(true);
            Vector3 pos = imgTgt.transform.position;
            pos = Vector3.Lerp(imgTgt.transform.position, transform.position, Time.deltaTime * 30);
            imgTgt.transform.position = pos;

            //Scaling purpose , Scale the 3D object on every click of SCALE button
            if (msgFromCtrller.currMessage == "Scale")
            {
                Debug.Log("====== Scale =====");
                // Scale 
                //    imgTgt.transform.localScale= ;
                Vector3 objScale = imgTgt.transform.localScale;
                imgTgt.transform.localScale = new Vector3(objScale.x * 2,
                                                            objScale.y * 2,
                                                            objScale.z * 2);


            }
            else if (msgFromCtrller.currMessage == "Rotate")  //Rotate purpose , Rotae the 3D object on every click of Rotate button
            {
                Debug.Log("===== Rotate by 90degree across Y axis ====");
                Vector3 rotationToAdd = new Vector3(0, 90, 0);
                imgTgt.transform.Rotate(rotationToAdd);
            }
            else if(msgFromCtrller.currMessage == "Rotation")
            {
                float degreesPerSecond = 20;
                imgTgt.transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
            }
            else if (msgFromCtrller.currMessage == "StopRotation")
            {
                float degreesPerSecond = 0;
                imgTgt.transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
            }

        }

    }
}

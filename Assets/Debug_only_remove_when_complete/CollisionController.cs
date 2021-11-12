using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CollisionController : MonoBehaviour
{
    public GameObject eqn;
    //If your GameObject starts to collide with another GameObject with a Collider
    void OnCollisionEnter(Collision collision)
    {
        //Output the Collider's GameObject's name
        Debug.Log(collision.collider.name);
    }

    //If your GameObject keeps colliding with another GameObject with a Collider, do something
    void OnCollisionStay(Collision collision)
    {
        //Check to see if the Collider's name is "Chest"
        if (collision.collider.name == "Chest")
        {
            //Output the message
            Debug.Log("Chest is here!");
        }
    }
    public void OnObjSelected(){
        Debug.Log("Selected");
        eqn.GetComponent<MeshRenderer>().material.color=Color.cyan;
    }
    public void OnSelectExited(){
        Debug.Log("Select  Exited");
    }
    public void OnSelectCancelled(){
        Debug.Log("Select cancelled");
    }
    public void OnHover(){
        eqn.GetComponent<MeshRenderer>().material.color=Color.red;
        eqn.transform.GetComponentInChildren<Transform>()
            .transform.GetComponentInChildren<Transform>()
                    .GetComponentInChildren<TEXDraw>().text="Hello world";
    }
    public void OnHoverExit(){
        eqn.GetComponent<MeshRenderer>().material.color=Color.white;
    }
}

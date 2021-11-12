using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YVR.Football
{
    public class CoordinatesController : MonoBehaviour
    {
        public GameObject planeX,planeY;
        // Update is called once per frame
        void Update()
        {
            Vector3 camPos=Camera.main.transform.position;
            planeX.transform.position
            =new Vector3(planeX.transform.position.x,
                        planeX.transform.position.y,camPos.z);
            planeY.transform.position
            =new Vector3(camPos.x,planeY.transform.position.y,
            planeY.transform.position.z);
        }
    }
}
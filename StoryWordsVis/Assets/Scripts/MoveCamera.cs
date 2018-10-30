using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    public float moveSpeed = 50;
    public float turnSpeed = 20;
    
	
	// Update is called once per frame
	//void Update () {
 //       if(Input.GetKey(KeyCode.W)){
 //           transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
 //       }
 //       if(Input.GetKey(KeyCode.S)){
 //           transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
 //       }
 //       if(Input.GetKey(KeyCode.A)){
 //           transform.Rotate(Vector3.down * Time.deltaTime * turnSpeed);
 //       }
 //       if(Input.GetKey(KeyCode.D)){
 //           transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
 //       }
 //       if(Input.GetKey(KeyCode.Q)){
 //           transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed);
 //       }
 //       if(Input.GetKey(KeyCode.E)){
 //           transform.Rotate(Vector3.left * Time.deltaTime * turnSpeed);
 //       }

 //       Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
 //       RaycastHit hit;
 //       if (Physics.Raycast(ray, out hit)){
 //           Node node = hit.transform.parent.gameObject.GetComponent<Node>();
 //           if( node != null){
 //               //print(node.name);
 //           }

	//	    if (Input.GetMouseButtonDown(0)){ // if left button pressed...
 //               print(hit.transform.gameObject.name);
 //               transform.position = hit.transform.position;
 //           }

 //       }

        

	//}

    
}

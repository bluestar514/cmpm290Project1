using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkMoveCamera : MonoBehaviour {
    public float moveSpeed = 25;
    public float turnSpeed = 20;

    public GameObject selectedObjectName;
    public GameObject selectedObjectFreq;

    public GameObject actualCamera;

    public NetworkManager mnger;
    
    void Update() {
        //if(Input.GetKey(KeyCode.W)) {
        //    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        //}
        //if(Input.GetKey(KeyCode.S)) {
        //    transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        //}
        //if(Input.GetKey(KeyCode.A)) {
        //    transform.Rotate(Vector3.down * Time.deltaTime * turnSpeed);
        //}
        //if(Input.GetKey(KeyCode.D)) {
        //    transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
        //}
        //if(Input.GetKey(KeyCode.Q)) {
        //    transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed);
        //}
        //if(Input.GetKey(KeyCode.E)) {
        //    transform.Rotate(Vector3.left * Time.deltaTime * turnSpeed);
        //}



        transform.position += actualCamera.transform.forward * Input.GetAxis("CONTROLLER_LEFT_STICK_VERTICAL") * moveSpeed * Time.deltaTime;
        transform.position += actualCamera.transform.right * Input.GetAxis("CONTROLLER_RIGHT_STICK_HORIZONTAL") * moveSpeed * Time.deltaTime;
        transform.position += actualCamera.transform.up * Input.GetAxis("CONTROLLER_RIGHT_STICK_VERTICAL") * moveSpeed * Time.deltaTime;
        //transform.Translate(Input.GetAxis("CONTROLLER_LEFT_STICK_VERTICAL") * moveSpeed * Time.deltaTime);

        Ray ray = actualCamera.GetComponent<Camera>().ScreenPointToRay(actualCamera.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            BNode node = hit.transform.parent.gameObject.GetComponent<BNode>();
            /*
            if(node != null) {
                print(node.name);
            }
            */

            if(Input.GetButtonDown("XBOX_A")) { // if left button pressed...
                print(hit.transform.gameObject.name);
                transform.position = hit.transform.position;
            }
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position,.1f);

        foreach(Collider collider in hitColliders){
            BNode node = collider.transform.parent.GetComponent<BNode>();
            //print(collider.transform.parent.gameObject.name);
            selectedObjectName.GetComponent<Text>().text = node.wordData.getWord();
            selectedObjectFreq.GetComponent<Text>().text = node.wordData.getFreq().ToString();

            mnger.ClearConnections();

            foreach(KeyValuePair<WordFreq, GameObject> obj in node.connectorDict){
                obj.Value.GetComponent<Connection>().reSetColors();
            }
        }
    }


}

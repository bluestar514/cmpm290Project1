using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour {
    public float moveSpeed = 50;
    public float turnSpeed = 20;

    public GameObject selectedObjectName;
    public GameObject selectedObjectFreq;

    public Manager mnger;
    
    void Update() {
        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        if(Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }
        if(Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.down * Time.deltaTime * turnSpeed);
        }
        if(Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
        }
        if(Input.GetKey(KeyCode.Q)) {
            transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed);
        }
        if(Input.GetKey(KeyCode.E)) {
            transform.Rotate(Vector3.left * Time.deltaTime * turnSpeed);
        }

        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            Node node = hit.transform.parent.gameObject.GetComponent<Node>();
            if(node != null) {
                //print(node.name);
            }

            if(Input.GetMouseButtonDown(0)) { // if left button pressed...
                print(hit.transform.gameObject.name);
                transform.position = hit.transform.position;
            }
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position,.1f);

        foreach(Collider collider in hitColliders){
            Node node = collider.transform.parent.GetComponent<Node>();
            print(collider.transform.parent.gameObject.name);
            selectedObjectName.GetComponent<Text>().text = node.wordData.getWord();
            selectedObjectFreq.GetComponent<Text>().text = node.wordData.getFreq().ToString();

            mnger.ClearConnections();

            foreach(KeyValuePair<WordFreq, GameObject> obj in node.connectorDict){
                obj.Value.GetComponent<Connection>().reSetColors();
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarMoveCamera : MonoBehaviour {
    public float moveSpeed = 25;
    public float turnSpeed = 20;

    public GameObject selectedObjectName;
    public GameObject selectedObjectFreq;

    public GameObject positionalCamera;
    public GameObject actualCamera;

    public BarManager mnger;
    
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

        Debug.DrawRay(actualCamera.transform.position, actualCamera.transform.forward*1000, Color.red, .1f, true);
        RaycastHit hit;
        if(Physics.Raycast(actualCamera.transform.position, actualCamera.transform.forward, out hit, Mathf.Infinity)) {
            Bar bar = hit.transform.GetComponent<Bar>();

            print(hit.transform.gameObject.name);
            
            selectedObjectName.GetComponent<Text>().text = bar.wordData.getWord();
            selectedObjectFreq.GetComponent<Text>().text = bar.wordData.getFreq().ToString();
        }

    }


}

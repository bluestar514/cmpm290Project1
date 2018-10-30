using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour {
    public GameObject firstNode;
    public GameObject secondNode;

    public void setEndPoints(GameObject start, GameObject end){
        firstNode = start;
        secondNode = end;

        reSetPositions(start.transform.position, end.transform.position);
        reSetColors();
    }

    public void reSetPositions(Vector3 start, Vector3 end){
        Vector3[] positions = new Vector3[2];
        positions[0] = start;
        positions[1] = end;
        GetComponent<LineRenderer>().positionCount = positions.Length;
        GetComponent<LineRenderer>().SetPositions(positions);
    }

    public void reSetColors(){
        string firstPOS = firstNode.GetComponent<Node>().wordData.getCommonPos();
        string secondPOS = secondNode.GetComponent<Node>().wordData.getCommonPos();
        Color firstColor = PosCatHelpers.getColor(PosCatHelpers.getSimplePos(firstPOS));
        firstColor.a = 0.5f;
        Color secondColor = PosCatHelpers.getColor(PosCatHelpers.getSimplePos(secondPOS));
        secondColor.a = 0.5f;

        GetComponent<LineRenderer>().startColor = firstColor;
        GetComponent<LineRenderer>().endColor = secondColor;
    }

    public void setLineAlpha(float alpha){
        alpha = Mathf.Clamp(alpha, 0, 1);
        setLineAlpha(alpha, alpha);
    }

    public void setLineAlpha(float start, float end){
        var color = GetComponent<LineRenderer>().startColor;
        color.a = start;
        GetComponent<LineRenderer>().startColor = color;

        color = GetComponent<LineRenderer>().endColor;
        color.a = end;
        GetComponent<LineRenderer>().endColor = color;
    }
}

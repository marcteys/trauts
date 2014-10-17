using UnityEngine;
using System.Collections;

public class TransformLookAt : MonoBehaviour {

 public GameObject targetObj;
 float speed = 5f;
 
    void Update(){

        var targetRotation = Quaternion.LookRotation(targetObj.transform.position - transform.position);
        if (Vector3.Distance(targetObj.transform.position, transform.position) < 1f)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward);
        } 
       
        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
 
}

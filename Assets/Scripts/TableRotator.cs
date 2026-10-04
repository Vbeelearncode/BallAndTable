using UnityEngine;

public class TableRotator : MonoBehaviour
{
    float rotationSpeed = 30;
    int angularLimit = 45;

    void Start() {

    }

    void Update() {
        float newXAxisRotation = transform.rotation.eulerAngles.x;
        float newZAxisRotation = transform.rotation.eulerAngles.z;

        if (Input.GetKey(KeyCode.LeftArrow)) 
            newZAxisRotation += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow)) 
            newZAxisRotation -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow)) 
            newXAxisRotation += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow)) 
            newXAxisRotation -= rotationSpeed * Time.deltaTime;

        enforceAngularLimit(ref newXAxisRotation);
        enforceAngularLimit(ref newZAxisRotation);

        Vector3 newRotationEulars = new Vector3(newXAxisRotation, transform.rotation.eulerAngles.y, newZAxisRotation);
        transform.rotation = Quaternion.Euler(newRotationEulars);
    }

    void enforceAngularLimit(ref float newAngle) {
        newAngle = (newAngle < 90 && newAngle > angularLimit) ? angularLimit : newAngle;

        newAngle = (newAngle > 90 && newAngle < 360 - angularLimit) ? 360 - angularLimit : newAngle;
    }
}

using UnityEngine;

public class PanningCamera : MonoBehaviour
{

    public float moveSpeed = 5;
    public float SprintMulti = 2;


    void Update()
    {
        float moveSpeedUsed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift) 
            ? moveSpeed * SprintMulti : moveSpeed;

        transform.Translate(moveSpeedUsed * Time.deltaTime * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) );
        transform.position = new(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y, transform.transform.position.z);
    }
}

using UnityEngine;

public class BilBoard : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class HitchcockCamera : MonoBehaviour
{
    Camera camera;
    float vw;
    float tan => Mathf.Tan(Mathf.Deg2Rad * camera.fieldOfView / 2);

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        vw = -transform.position.z * tan;
    }

    // Update is called once per frame
    void Update()
    {
        var d = vw / tan;
        var p = transform.position;
        p.z = -d;
        transform.position = p;
        transform.LookAt(Vector3.zero);
    }
}

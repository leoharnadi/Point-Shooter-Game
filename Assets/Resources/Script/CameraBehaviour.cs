using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        cam.ResetAspect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

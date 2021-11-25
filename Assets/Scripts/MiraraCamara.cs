using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiraraCamara : MonoBehaviour
{

    [SerializeField]
    Transform Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("AR Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Mirar();
    }

    void Mirar()
    {

        transform.LookAt(Camera.transform);
        var rotationVector = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotationVector);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}

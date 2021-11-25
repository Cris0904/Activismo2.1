using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesa : MonoBehaviour
{
    [SerializeField]
    Transform Camera;

    [SerializeField]
    GameObject Lejos;


    [SerializeField]
    Fase3Script fase3script;





    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("AR Camera").GetComponent<Transform>();
        fase3script = GameObject.Find("Fase3DOA").GetComponent<Fase3Script>();
    }

    // Update is called once per frame
    void Update()
    {

        if(calcularDistancia3d() < 1.1f)
        {
            if (Lejos.activeSelf)
            {
                Lejos.SetActive(false);
                fase3script.JuegoDOA();
            }
        }
    }

    public float calcularDistancia3d()
    {
        Vector3 Objeto1 = Camera.position;
        Vector3 Objeto2 = gameObject.transform.position;
        float distancia = Vector3.Distance(Objeto1, Objeto2);

        return distancia;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NombreCerdo : MonoBehaviour
{
    [SerializeField]
    GuardarAnimal guardarAnimal;

    [SerializeField]
    string msg1;
    [SerializeField]
    string msg2;
    [SerializeField]
    Text msg;
    // Start is called before the first frame update
    void Start()
    {
        guardarAnimal = GameObject.Find("Foto").GetComponent<GuardarAnimal>();
    }

    // Update is called once per frame
    void Update()
    {
        textoNombre();
    }

    public void textoNombre()
    {
        msg.text = msg1 + guardarAnimal.Nombretxt + msg2;
    }
}

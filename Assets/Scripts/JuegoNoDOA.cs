using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuegoNoDOA : MonoBehaviour
{
    [SerializeField]
    GameObject Inicio;
    
    [SerializeField]
    GuardarAnimal guardarAnimal;

    bool contar = false;
    bool setChange;
    float momentoInicial;
    float momentoActual;

    // Start is called before the first frame update
    [SerializeField]
    GameObject PanTop; // 0
    [SerializeField]
    GameObject PanBot; // 5
    [SerializeField]
    GameObject Carne; // 2
    [SerializeField]
    GameObject Lechuga; // 3
    [SerializeField]
    GameObject Queso; //1
    [SerializeField]
    GameObject Tomate; // 4

    [SerializeField]
    GameObject btnScena;

    [SerializeField]
    Cerdo instanceScript;    
    [SerializeField]
    GameObject Siguiente;

    [SerializeField]
    Animator animator;

    int Orden;

    void Start()
    {
        Orden = 0;
        setChange = false;
        instanceScript = GameObject.Find("AR Session Origin").GetComponent<Cerdo>();
        momentoInicial = Time.deltaTime;
        momentoActual = momentoInicial;
    }

    // Update is called once per frame
    void Update()
    {

        UIManager();
    }

    public void poner(int comida)
    {
        instanceScript.aparecerComida(PanBot, 0.04f);
        instanceScript.aparecerComida(Queso, -0.008f);
        instanceScript.aparecerComida(Carne, -0.016f);
        instanceScript.aparecerComida(Lechuga, 0.033f);
        instanceScript.aparecerComida(Tomate, 0.012f);
        instanceScript.aparecerComida(PanTop, 0);
        Orden = 6;
    }

    void evaluar(GameObject x)
    {
        Instantiate(x);
    }

    public void destruir()
    {
        instanceScript.desaparecerComida();
    }

    void UIManager()
    {
        if (Orden > 5)
        {
            Inicio.SetActive(true);
            contar = true;
            
        }

        momentoActual += Time.deltaTime;

        if (momentoActual > momentoInicial + 20f)
        {
            btnScena.SetActive(true);
        }
    }

    public void cargarSiguienteScena()
    {

    }
}

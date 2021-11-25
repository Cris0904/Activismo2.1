using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuegoDOA : MonoBehaviour
{
    [SerializeField]
    GameObject Uitext1;
    [SerializeField]
    GameObject Uitext2;

    [SerializeField]
    GuardarAnimal guardarAnimal;

    [SerializeField]
    RectTransform GOphoto;

    [SerializeField]
    CanvasGroup photoFilter;

    [SerializeField]
    GameObject Seguir3;

    [SerializeField]
    GameObject Guia;

    bool contar = false;
    bool setChange;
    float momentoInicial;
    float momentoActual;

    public bool carnecerdo;

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
    GameObject Error;
    [SerializeField]
    GameObject Acierto;

    [SerializeField]
    Cerdo instanceScript;

    [SerializeField]
    GameObject PanTopBtn; // 0
    [SerializeField]
    GameObject PanBotBtn; // 5
    [SerializeField]
    GameObject CarneBtn; // 2
    [SerializeField]
    GameObject CarneVegBtn; // 2
    [SerializeField]
    GameObject LechugaBtn; // 3
    [SerializeField]
    GameObject QuesoBtn; //1
    [SerializeField]
    GameObject QuesoVegBtn; //1
    [SerializeField]
    GameObject TomateBtn; // 4

    [SerializeField]
    GameObject Siguiente;

    [SerializeField]
    Animator animator;

    int Orden;
    public bool final = false;

    void Start()
    {
        Orden = 0;
        Siguiente.SetActive(false);
        setChange = false;
        
    }


    // Update is called once per frame
    void Update()
    {
        
         UIManager();
        
    }

    public void poner(int comida)
    {
        if(Orden == comida)
        {
            switch (Orden)
            {
                case 0:
                    instanceScript.aparecerComida(PanBot, 0.04f);
                    PanBotBtn.SetActive(false);
                    break;
                case 1:
                    instanceScript.aparecerComida(Queso, -0.008f);
                    QuesoBtn.SetActive(false);
                    QuesoVegBtn.SetActive(false);
                    break;
                case 2:
                    instanceScript.aparecerComida(Carne, -0.016f);
                    CarneBtn.SetActive(false);
                    CarneVegBtn.SetActive(false);
                    break;
                case 3:
                    instanceScript.aparecerComida(Lechuga, 0.033f);
                    LechugaBtn.SetActive(false);
                    break;
                case 4:
                    instanceScript.aparecerComida(Tomate, 0.012f);
                    TomateBtn.SetActive(false);
                    break;
                case 5:
                    instanceScript.aparecerComida(PanTop, 0);
                    PanTopBtn.SetActive(false);
                    Siguiente.SetActive(true);
                    animator = GameObject.FindGameObjectWithTag("cerdo").GetComponent<Animator>();
                    animator.SetInteger("Index", 3);
                    photo();
                    break;
                default:
                    break;
            }
            Orden++;
            evaluar(Acierto);
        }
        else
        {
            evaluar(Error);
        }
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
        if (!instanceScript.interactuable && instanceScript.isCerca && !setChange)
        { 
            setChange = true;
            Uitext1.SetActive(false);
            final = true;

        }

        if (contar)
        {
            momentoActual += Time.deltaTime;
        }

        if (final)
        {
            final = false;
            contar = true;
            Uitext2.SetActive(true);
            momentoActual = momentoInicial;
        }

        if(momentoActual> momentoInicial + 5f)
        {
            Uitext2.SetActive(false);
            Seguir3.SetActive(true);
        }
    }

    void photo()
    {
        //GOphoto.position = new Vector3(0, 766,0);
        GOphoto.localPosition = new Vector3(0,766,0);
        photoFilter.alpha = 0.3f;
        guardarAnimal.mostrarDespues();
        Guia.SetActive(false);
    }

    public void usaCarneCerdo()
    {
        carnecerdo = true;
    }

}

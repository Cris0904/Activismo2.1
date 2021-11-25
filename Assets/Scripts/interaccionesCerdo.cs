using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


//Al terminar de correr hace la animacion de tocar y no se evalua el numero random
//Mensajes después de cada iteración

public class interaccionesCerdo : MonoBehaviour
{
    [SerializeField]
    GameObject MenuInteractivo;

    [SerializeField]
    GameObject MenuDeath;

    [SerializeField]
    Fase3Script Fase3;

    [SerializeField]
    GameObject Inicio;

    [SerializeField]
    GameObject prefab;

    [SerializeField]
    Text Dist;

    [SerializeField]
    Cerdo instanceScript;

    [SerializeField]
    Text MsgInteraccion;

    [SerializeField]
    GameObject GOMsgInteraccion;

    public float distanciaInteraccion = 2f;

    bool seAleja;
    public bool independiente = false;
    public bool dependiente;
    public bool correr = false;
    public bool tocar = false;
    public bool acercarse = false;
    public bool alejarse = false;
    public bool alejarsewalk = false;
    float velocidadIndependiente = 0.1f;

    bool ocupado = false;

    float rotini = 0;
    bool interactuable;
    

    float momentoInicial;
    float momentoActual;

    float momentoInicialI;
    float momentoActualI = 0;


    public float movementSpeed = 0.5f;

    [SerializeField]
    Text MensjeInicial;

    [SerializeField]
    GameObject GOMensajeInicial;

    [SerializeField]
    Transform Camera;

    [SerializeField]
    GuardarAnimal guardarAnimal;

    [SerializeField]
    TextMesh Nombre;

    [SerializeField]
    TextMesh Nombre2;

    [SerializeField]
    Animator animator;

    [SerializeField]
    JuegoDOA juegoDoa;

    public int interacciones;

    System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        seAleja = true;
        interacciones = 0;
        desaparecer();
        Camera = GameObject.Find("AR Camera").GetComponent<Transform>();
        guardarAnimal = GameObject.Find("Foto").GetComponent<GuardarAnimal>();
        Fase3 = GameObject.Find("Fase3DOA").GetComponent<Fase3Script>();
        instanceScript = GameObject.Find("AR Session Origin").GetComponent<Cerdo>();
        //Dist = GameObject.Find("Dist").GetComponent<Text>();
        HacerMensajeInicial();
        animator = GetComponent<Animator>();
        interactuable = true;
        momentoInicialI = Time.deltaTime;
        GOMsgInteraccion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Nombre.text = guardarAnimal.Nombretxt;
        interactuable = instanceScript.interactuable;

        if(animator.GetInteger("index") == 3)
        {
            ocupado = true;
            acercarse = false;
            animator.SetInteger("index", 3);
            movementSpeed = 0;
            velocidadIndependiente = 0;

        }


        if(interacciones > 0 && GOMensajeInicial.activeInHierarchy)
        {
            GOMensajeInicial.SetActive(false);
            GOMsgInteraccion.SetActive(true);
        }


        //Mostrar menu al acercarse
        if(calcularDistancia3d() < distanciaInteraccion)
        {
            MirarACamara(true);
            if (!instanceScript.interactuable )
            {
                MenuInteractivo.SetActive(false);
                MenuDeath.SetActive(true);
                Nombre2.text = guardarAnimal.Nombretxt;
            }
            else
            {
                MenuInteractivo.SetActive(true);
            }


            if (Inicio.activeSelf)
            {
                Inicio.SetActive(false);
            }            
        }
        else
        { 
            MenuInteractivo.SetActive(false);
        }

        if (calcularDistancia3d() < 1)
        {
            if (!instanceScript.interactuable)
            {

                MenuDeath.SetActive(true);
            }
                
        }
            
        //Cerdo desaparece despues de morir
        if (!instanceScript.isCerdoAlive)
        {
            gameObject.SetActive(false);
        }


        if (interactuable)
        {
            if (calcularDistancia() <= 0.8f && !ocupado && interacciones < 5)
            {
                if (seAleja)
                {
                    independiente = false;
                    if (interacciones > 2) //Amigable
                    {
                        Correr();
                        ocupado = true;
                        interacciones++;
                    }

                    if (interacciones <= 2) //Asustadizo
                    {
                        alejarse = true;
                        alejarRun();
                        ocupado = true;
                        interacciones++;
                    }

                    seAleja = false;

                    switch (interacciones)
                    {
                        case 1:
                            MsgInteraccion.text = "¡Vaya! Parece que " + guardarAnimal.Nombretxt + " no te reconoce \n ¡Sigue intentando acercarte!";
                            break;
                        case 2:
                            MsgInteraccion.text = guardarAnimal.Nombretxt + " parece ser un poco asustadiz@ \n ¡Dale otra oportunidad!";
                            break;
                        case 3:
                            MsgInteraccion.text = "¡Ya casi!\n" + guardarAnimal.Nombretxt + " parece algo curios@  ";
                            break;
                        case 4:
                            MsgInteraccion.text = "¡Genial!\n " + guardarAnimal.Nombretxt + " ya permite que le acaricies! ";
                            break;
                        case 5:
                            GOMsgInteraccion.SetActive(false);
                            break;
                        default:
                            MsgInteraccion.text = "";
                            break;
                    }
                }
            }
            else
            {
                seAleja = true;
            }
            //Comportamiento Organico periódico///////////////////////////
            momentoActualI += Time.deltaTime;
            if (!ocupado && (interacciones==3 || interacciones > 4))
            {
                if (independiente) { transform.position += transform.forward * velocidadIndependiente * Time.deltaTime; }

                if (!independiente)
                {
                    caminarIndependiente();
                    independiente = true;
                    velocidadIndependiente = 0.1f;
                }
                


                
                if(calcularDistancia() >= 2f)
                {
                    //velocidadIndependiente = 0.2f;
                    acercar();
                    acercarse = true;
                    independiente = false;
                    ocupado = true;
                    
                }

                /*if(calcularDistancia()<= 0.5f)
                {
                    acercarse = false;
                    independiente = false;
                }*/
            }


          
            //Corre y da una vuelta
            if (correr)
            {
                
                Vector3 to = new Vector3(0, 360, 0);
                //transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
                transform.Rotate(Vector3.up, 360 * Time.deltaTime * 0.2f);
                transform.position += transform.forward * 1f * Time.deltaTime;
                if (vuelta(Mathf.Round(transform.rotation.eulerAngles.y), Mathf.Round(rotini)))
                {
                    correr = false;
                    animator.SetInteger("Index", -1);
                    MirarACamara();
                    tocar = true;
                    Tocar();

                }
            }

            if (alejarse)
            {       
                transform.position += transform.forward * 0.8f * Time.deltaTime;

                if (calcularDistancia() > 1.3f)
                {
                    alejarse = false;
                    alejarsewalk = true;
                    ocupado = false;
                    alejarWalk();
                }                
            }

            if (alejarsewalk)
            {
                transform.position += transform.forward * 0.2f * Time.deltaTime;

                if (calcularDistancia() > 1.6f)
                {
                    Tocar();
                    tocar = true;
                    alejarsewalk = false;
                }
            }
            //Lo tocan
            if (tocar)
            {
                momentoActual += Time.deltaTime;

                if (momentoActual >= momentoInicial + 2f)
                {
                    animator.SetInteger("Index", -1);
                    tocar = false;
                    ocupado = false;
                }
            }

        }
        

        if (interacciones > 4)
        {
            StartFase3();
            
        }        

        if (acercarse)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;

            if (calcularDistancia() <= 0.4f)
            {
                ocupado = false;
                acercarse = false;
                if (interacciones < 4)
                {
                    animator.SetInteger("Index", -1);
                }
                Debug.Log("Sale");
                independiente = false;
            }
        }
        
        
        //Dist.text = posIni.ToString() +  "////" + transform.position.ToString();

    }

    public void aparecer() //Mostrar nombre 3d
    {
        MenuInteractivo.SetActive(true);
        MenuInteractivo.transform.LookAt(Camera);
        MenuInteractivo.transform.Rotate(new Vector3(0, 180, 0));
        Nombre.text = guardarAnimal.Nombretxt;
        Nombre2.text = guardarAnimal.Nombretxt;
    }

    public void desaparecer()
    {
        MenuInteractivo.SetActive(false);
    }

    public void Correr()
    {
        animator.SetInteger("Index", 0);
        correr = true;
        rotini = transform.rotation.eulerAngles.y;
    }

    public void Tocar()
    {
        animator.SetInteger("Index", 1);
        tocar = true;
        momentoInicial = Time.deltaTime;
        momentoActual = Time.deltaTime;
    }

    bool vuelta(float rotactual, float rotinicial)
    {
        float x = rotinicial - 10;
        //MensjeInicial.text = rotactual + "//// " + x;

        if (rotactual > x  && rotactual < rotinicial)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
   

    void HacerMensajeInicial()
    {
        MensjeInicial.text = "¡¡Tu amig@ " + guardarAnimal.Nombretxt + " se ha convertido en un cerdo en este mundo virtual!! \n \n\n¡ACERCATE Y CONOCELE!";
    }
    void MirarACamara()
    {
        transform.LookAt(Camera.transform);
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = 0;
        rotationVector.x = 0;
        //rotationVector.y -= 30;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    public void MirarACamara(bool x)
    {
        MenuInteractivo.transform.LookAt(Camera.transform);
        var rotationVector = MenuInteractivo.transform.rotation.eulerAngles;
        MenuInteractivo.transform.rotation = Quaternion.Euler(rotationVector);
        MenuInteractivo.transform.Rotate(new Vector3(0, 180, 0));
    }

    void StartFase3()
    {
        Fase3.start();
    }

    public float calcularDistancia()
    {        
            Vector2 Objeto1 = new Vector2(Camera.position.x, Camera.position.z);
            Vector2 Objeto2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
            float distancia = Vector2.Distance(Objeto1, Objeto2);

            return distancia;
    }

    void acercar()
    {
        
        transform.LookAt(Camera.transform);
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = 0;
        rotationVector.x = 0;
        acercarse = true;
        transform.rotation = Quaternion.Euler(rotationVector);
        animator.SetInteger("Index", 2);
    }

    void alejarRun()
    {
        int x = random.Next(90, 180);
        transform.LookAt(Camera.transform);
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = 0;
        rotationVector.x = 0;
        rotationVector.y += x;
        transform.rotation = Quaternion.Euler(rotationVector);
        animator.SetInteger("Index", 0);
    }

    void alejarWalk()
    {
        //int x = random.Next(-180, -90);
        //transform.LookAt(Camera.transform);
        //var rotationVector = transform.rotation.eulerAngles;
       // rotationVector.z = 0;
        //rotationVector.x = 0;
        //rotationVector.y += x;
        //transform.rotation = Quaternion.Euler(rotationVector);
        animator.SetInteger("Index", 2);
    }

    public float calcularDistancia3d()
    {
        Vector3 Objeto1 = Camera.position;
        Vector3 Objeto2 = gameObject.transform.position;
        float distancia = Vector3.Distance(Objeto1, Objeto2);

        return distancia;
    }

    void correryparar()
    {
        Vector3 to = new Vector3(0, 360, 0);
        //transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
        transform.Rotate(Vector3.up, 360 * Time.deltaTime * 0.2f);
        transform.position += transform.forward * 1f * Time.deltaTime;
        if (vuelta(Mathf.Round(transform.rotation.eulerAngles.y), Mathf.Round(rotini)))
        {
            correr = false;
            animator.SetInteger("Index", -1);
            MirarACamara();
            Tocar();
            tocar = true;
        }
    }

    void caminarIndependiente()
    {
        int x = random.Next(0, 360);
        transform.LookAt(Camera.transform);
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = 0;
        rotationVector.x = 0;
        rotationVector.y += x;
        transform.rotation = Quaternion.Euler(rotationVector);
        animator.SetInteger("Index", 2);
    }

}

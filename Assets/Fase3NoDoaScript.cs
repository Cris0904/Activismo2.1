using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase3NoDoaScript : MonoBehaviour
{
    [SerializeField]
    CanvasGroup Canvas;

    [SerializeField]
    GameObject Ingredientes;


    [SerializeField]
    GameObject Ingredientes2d;



    bool jugar;

    // Start is called before the first frame update
    void Start()
    {
        Canvas.alpha = 0;
        Ingredientes.SetActive(false);
        jugar = false;
        Ingredientes2d.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void start()
    {
        Canvas.alpha = 1;
        Ingredientes.SetActive(true);
    }



    public void JuegoDOA()
    {
            jugar = true;
            Ingredientes2d.SetActive(true);
            gameObject.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardarAnimal : MonoBehaviour
{
    [SerializeField]
    CanvasGroup UIcanvas;

    [SerializeField]
    GameObject UI;

    [SerializeField]
    GameObject repetirfotoBtn;

    [SerializeField]
    Text Test;

    [SerializeField]
    InputField Nombre;

    [SerializeField]
    InputField Especie;

    

    public string Nombretxt;
    public string Especietxt;

 

    private void Start()
    {
        UIcanvas.alpha = 0;
        repetirfotoBtn.SetActive(false);
    }
    // Update is called once per frame

    private void Update()
    {
        Test.text = Nombretxt;
    }


    public void Guardar()
    {
        
        Nombretxt = Nombre.text;
        Especietxt = Especie.text;
        UI.SetActive(false);

    }




    public void aparecerUI()
    {
        UIcanvas.alpha = 1;
        UI.SetActive(true);

    }


    public void esconder()
    {
        UIcanvas.alpha = 0;
    }


    public void mostrar()
    {
        UIcanvas.alpha = 1;
    }

    public void mostrarDespues()
    {
        UIcanvas.alpha = 1;
        repetirfotoBtn.SetActive(false);
        
    }

    

}

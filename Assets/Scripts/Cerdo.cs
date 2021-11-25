using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using UnityEngine.AI;

public class Cerdo : MonoBehaviour
{
    [SerializeField]
    GameObject m_PlacedPrefab;

    [SerializeField]
    GameObject mesaPrefab;

    [SerializeField]
    Transform Camera;

    [SerializeField]
    GameObject VegPrefab;


    [SerializeField]
    Text OpcionVeganatxt;

    public bool interactuable; //Cuando es true se puede interactuar con el cerdo    
    public bool isCerdoAlive; // Cuando es false el cerdo desaparece del juego
    public bool calcularSuelo; // Cuando es false el suelo deja de calcularse
    public bool isCerca; //Cuando es true, el jugador está cerca del cerdo

    [SerializeField]
    Camera camera;
    //public bool Colocar;
    float suelo = -1.33f;
    Quaternion rotacion = new Quaternion(0, 0, 0, 0);

    public float movementSpeed = 1f;
    public float distanciaMover = 1f;

    Vector3 posCerdo;
    Vector3 posUsu;


    [SerializeField]
    Text dist;

    bool tocarmesa;

    static Vector3 tabletop;
    static Vector3 tablePos;

    Vector2 touchPosition = default;

    [SerializeField]
    interaccionesCerdo interaccionesCerdo;
    
    [SerializeField]
    PhysicsRaycaster raycasterPhysics;

    [SerializeField]
    GuardarAnimal guardarAnimal;

    [SerializeField]
    Text MsgBtnJuegoDoa;


    [SerializeField]
    Fase3Script fase3script;

    [SerializeField]
    GameObject CerdoFinal;


    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }



    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    public GameObject spawnedObject2 { get; private set; }

    public List<GameObject> spawnedObject3;

    public GameObject spawnedObject4 { get; private set; }

    public GameObject spawnedObject5 { get; private set; }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        tocarmesa = true;
        interactuable = true;
        isCerdoAlive = true;
        calcularSuelo = true;
        guardarAnimal = GameObject.Find("Foto").GetComponent<GuardarAnimal>();
        isCerca = false;
        Camera = GameObject.Find("AR Camera").GetComponent<Transform>();

    }

    void Update()
    {
        dist.text = calcularDistancia3d().ToString() + "   " + interactuable +"   "+ isCerca;
        if ((calcularDistancia3d()) < 1f)
        {
            isCerca = true;
        }
        else
        {
            isCerca = false;
        }


        if (MsgBtnJuegoDoa != null)
        {
            MsgBtnJuegoDoa.text = "¡Agáchate y mira a " + guardarAnimal.Nombretxt + "!";
        }

        PhysicsRayvasting();
        
        if (spawnedObject == null)
        {
            if (m_RaycastManager.Raycast(new Vector2(Random.Range(100, 1000), Random.Range(300, 1000)), s_Hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = s_Hits[0].pose;
                /*if (calcularSuelo)
                {
                    dist.text = Hallarsuelo(hitPose.position.y).ToString();
                }*/
                //dist.text = hitPose.position.x.ToString() + " /// " + hitPose.position.y.ToString();
                rotacion = hitPose.rotation;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            //dist.text = touchPosition.x.ToString() + " /// " + touchPosition.y.ToString();

            //Vector3 dir = new Vector3 (touchPosition.x, touchPosition.y,50f);
            Ray ray = camera.ScreenPointToRay(touch.position);
            RaycastHit hitObject;
            //dist.text = "0";
            if (Physics.Raycast(ray, out hitObject))
            {/*
                //dist.text = "1";
                interaccionesCerdo interaccionesScript = hitObject.transform.gameObject.GetComponent<interaccionesCerdo>();
                
                if (interaccionesScript != null)
                {
                    //dist.text = "2";
                    interaccionesScript.aparecer();
                }
                else
                {
                    Mesa mesa = hitObject.transform.gameObject.GetComponent<Mesa>();

                    dist.text = "XD";
                    if (mesa != null && tocarmesa)
                    {

                        dist.text = "Tocaaaa";
                        fase3script.JuegoDOA();
                        tocarmesa = false;
                    }
                }
                */
                /*else
                {
                    Index indexScript = hitObject.transform.gameObject.GetComponent<Index>();

                    if(indexScript != null)
                    {
                        interaccionesCerdo interaccionesScriptGet = GameObject.Find("Cerdo").GetComponentInChildren<interaccionesCerdo>();
                        interaccionesScriptGet.MenuManager(indexScript.index);

                    }
                }*/

            }
        }
      
    }

    float Hallarsuelo(float hitposeY)
    {
        if(hitposeY < suelo)
        {
            suelo = hitposeY;
        }
        return suelo;
    }

    public void aparecerComida(GameObject comida, float size)
    {
        spawnedObject3.Add(Instantiate(comida, tabletop, Quaternion.Euler(0, 0, 0)));
        tabletop = new Vector3(tabletop.x, tabletop.y + size, tabletop.z);
        //dist.text = spawnedObject3.name;
        if(spawnedObject3.Count > 5)
        {
            interactuable = false;
        }
    }

    public void aparecerMsgFinal()
    {
        Vector3 pos = tabletop;
        pos.y += 0.2f; 
        spawnedObject3.Add(Instantiate(VegPrefab, pos, Quaternion.Euler(0, 0, 0)));

        Vector3 cerca = new Vector3(camera.transform.position.x, suelo, camera.transform.position.z + 1.5f);
        spawnedObject5 = Instantiate(CerdoFinal, cerca, rotacion);
        spawnedObject5.transform.LookAt(camera.transform);
        var rotationVector = spawnedObject5.transform.rotation.eulerAngles;
        rotationVector.z = 0;
        rotationVector.x = 0;
        rotationVector.y += 45;
        spawnedObject5.transform.rotation = Quaternion.Euler(rotationVector);
    }

    public void desaparecerComida()
    {
        for (int i = 0; i < spawnedObject3.Count; i++)
        {
            Destroy(spawnedObject3[i]);
            tabletop.y = tablePos.y + 0.38f;
        }
    }

    public void aparecerCerdo()
    {
        if (spawnedObject == null)
        {

            Vector3 cerca = new Vector3(camera.transform.position.x, suelo, camera.transform.position.z + 1.5f);
            spawnedObject = Instantiate(m_PlacedPrefab, cerca, rotacion);
            spawnedObject.transform.LookAt(camera.transform);
            var rotationVector = spawnedObject.transform.rotation.eulerAngles;
            rotationVector.z = 0;
            rotationVector.x = 0;
            rotationVector.y += 45;
            spawnedObject.transform.rotation = Quaternion.Euler(rotationVector);
        }
    }

    public void aparecerMesa()
    {
        tablePos = new Vector3(camera.transform.position.x+0.5f, suelo+0.4f, camera.transform.position.z + 1.7f);
        spawnedObject2 = Instantiate(mesaPrefab, tablePos, mesaPrefab.transform.rotation);
        tabletop = new Vector3(tablePos.x, tablePos.y + 0.38f, tablePos.z);

        OpcionVeganatxt.text = "¿Te gustaría probar una opción \n que no involucre a " + guardarAnimal.Nombretxt + "?";
    }


    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    ARRaycastManager m_RaycastManager;

    void PhysicsRayvasting() {
        PointerEventData data = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycasterPhysics.Raycast(data, results);

        results.RemoveAll(r => r.gameObject.tag == "plane");
    }

    public void CerdoDie()
    {
        isCerdoAlive = false;
    }

    public void SueloSet()
    {
        calcularSuelo = false;
    }

    public float calcularDistancia3d()
    {
        float distancia = 0f;
        if (spawnedObject != null)
        {
            Vector3 Objeto1 = Camera.position;
            Vector3 Objeto2 = spawnedObject.transform.GetChild(0).gameObject.transform.position;
            //Vector3 Objeto2 = spawnedObject.transform.position;
            distancia = Vector3.Distance(Objeto1, Objeto2);
        }

        return distancia;
    }
}
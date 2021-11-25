using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;
using System.IO;
using UnityEngine.UI;

namespace UnityEngine.XR.ARFoundation.Samples
{

    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;
        public Transform camera;


        [SerializeField]
        Text distancia;

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

        

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

     
        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        void Update()
        {
            
            //distancia.text = camera.position.x + "///" + camera.position.y + "///" + camera.position.z;


            /*if (spawnedObject != null)
            {
                //distancia.text = spawnedObject.transform.position.x + "---" + spawnedObject.transform.position.y + "---" + spawnedObject.transform.position.z;
            }*/

            /*if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;*/


            //distancia.text = touchPosition.x.ToString() + "////" + touchPosition.y.ToString();

            if(spawnedObject == null) {

                if (m_RaycastManager.Raycast(new Vector2(Random.Range(400f,800f), Random.Range(500f, 800f)), s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    // Raycast hits are sorted by distance, so the first one
                    // will be the closest hit.
                    var hitPose = s_Hits[0].pose;

                    if (spawnedObject == null)
                    {
                        Vector3 cerca = new Vector3(camera.position.x + 0.3f, hitPose.position.y, camera.position.z+0.3f);

                        spawnedObject = Instantiate(m_PlacedPrefab, cerca, hitPose.rotation);
                        //spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                        spawnedObject.transform.LookAt(camera);
                        var rotationVector = spawnedObject.transform.rotation.eulerAngles;
                        rotationVector.z = 0;  //this number is the degree of rotation around Z Axis
                        rotationVector.x = 0;
                        rotationVector.y += 30;
                        spawnedObject.transform.rotation = Quaternion.Euler(rotationVector);
                        /*float r = spawnedObject.transform.rotation.y;

                        spawnedObject.transform.eulerAngles = new Vector3(0, r, 0);*/
                        //LookAtCamera(spawnedObject);

                    }
                    else
                    {
                        Vector3 cerca = new Vector3(camera.position.x + 1, hitPose.position.y, camera.position.z);

                        spawnedObject.transform.position = cerca;

                        //spawnedObject.transform.position = hitPose.position;
                        spawnedObject.transform.LookAt(camera);
                        var rotationVector = spawnedObject.transform.rotation.eulerAngles;
                        rotationVector.z = 0;  //this number is the degree of rotation around Z Axis
                        rotationVector.x = 0;
                        spawnedObject.transform.rotation = Quaternion.Euler(rotationVector);
                        /*float r = spawnedObject.transform.rotation.y;

                        spawnedObject.transform.eulerAngles = new Vector3(0, r, 0);*/
                        //cabeza.transform.LookAt(camera);
                        //LookAtCamera(spawnedObject);
                    }


                }

            }
        }

        /*void LookAtCamera(GameObject spawnedObject)
        {
            spawnedObject.transform.LookAt(camera);
            float r = spawnedObject.transform.rotation.y;

            spawnedObject.transform.eulerAngles = new Vector3(0, r, 0);
        }*/

        


        

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
}
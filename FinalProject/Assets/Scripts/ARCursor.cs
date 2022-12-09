using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject gamePlaneToPlace;
    public GameObject prefabToPlace;
    public ARRaycastManager raycastManager;

    public bool useCursor = true;
    public bool gamePlaced = false;

    void Start()
    {
        cursorChildObject.SetActive(useCursor);
    }

    void Update()
    {
        if (gamePlaced == false)
        {
            GamePlace();
        }
        if (gamePlaced == true)
        {
            ObsticlePlace();
        }
    }

    void UpdateGameCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
    void UpdateObsticleCursor()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
            transform.rotation = new Quaternion(hit.transform.localRotation.x, hit.transform.localRotation.y, hit.transform.localRotation.z, 1);
        }
    }

    void GamePlace()
    {
        UpdateGameCursor();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !gamePlaced)
        {
            if (useCursor)
            {
                GameObject.Instantiate(gamePlaneToPlace, transform.position, new Quaternion(0f, transform.rotation.y, 0f, 1));
            }
            gamePlaced = true;
        }
    }

    void ObsticlePlace()
    {
        UpdateObsticleCursor();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && gamePlaced)
        {
            GameObject.Instantiate(prefabToPlace, GetPrefabPosition(), GetPrefabRotation());
        }
    }

    private Vector3 GetPrefabPosition()
    { 

        Vector3 objectPosition = new Vector3((-10.4f * (gamePlaneToPlace.transform.position.x - transform.position.x)),
                                             -10.4f * (gamePlaneToPlace.transform.position.y - transform.position.y) + 8,
                                             0);      
        return objectPosition;
    }
    private Quaternion GetPrefabRotation()
    {
        Quaternion objectRotation = new Quaternion(0f, 0f, gamePlaneToPlace.transform.rotation.z - transform.rotation.z, 1);
        return objectRotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public ARRaycastManager raycastManager;

    public bool useCursor = true;
    public bool gamePlaced = false;

    void Start()
    {
        cursorChildObject.SetActive(useCursor);
    }

    void Update()
    {
        if(useCursor)
        {
            UpdateCursor();
        }
        else
        {
            cursorChildObject.SetActive(false);
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !gamePlaced)
        {
            if(useCursor)
            {
                GameObject.Instantiate(objectToPlace, transform.position, new Quaternion(0f, transform.rotation.y, 0f, 1));
                gamePlaced = true;
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if(hits.Count > 0)
                {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, new Quaternion(0f, hits[0].pose.rotation.y, 0f, 1));
                    gamePlaced = true;
                    useCursor = false;
                }
            }
            gamePlaced = true;
        }
    }

    void UpdateCursor()
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
}

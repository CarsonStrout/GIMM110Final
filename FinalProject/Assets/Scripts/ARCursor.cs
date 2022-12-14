using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
    public GameObject gameCursorChildObject;
    public GameObject controlUI;
    public GameObject gamePlaneToPlace;
    public GameObject[] prefabToPlace;

    private GameObject obj;

    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    public TurnUIScript turnUI;
    public GameManager gameManager;

    public bool useGameCursor = true;
    public bool gamePlaced = false;
    public bool canPlaceObstacle = false;
    public bool created = false;


    void Start()
    {
        planeManager.planePrefab.SetActive(false);
        gameCursorChildObject.SetActive(useGameCursor);
        controlUI.SetActive(false);
    }

    void Update()
    {
        if (!gamePlaced)
        {
            GamePlace();
        }
        if (canPlaceObstacle)
        {
            ObstaclePlace();
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

    void UpdateObstacleCursor()
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
        turnUI.PlaceGame.SetActive(true);
        UpdateGameCursor();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !gamePlaced)
        {
            if (useGameCursor)
            {
                GameObject.Instantiate(gamePlaneToPlace, transform.position, new Quaternion(0f, transform.rotation.y, 0f, 1));
            }
            gamePlaced = true;
            useGameCursor = false;
            gameCursorChildObject.SetActive(useGameCursor);
            gameManager.previousPlacement = 1;
            turnUI.PlaceGame.SetActive(false);
            turnUI.PreTurn1();
        }
    }

    void ObstaclePlace()
    {
        UpdateObstacleCursor();

        if (!created)
        {
            obj = GameObject.Instantiate(prefabToPlace[Random.Range(0, prefabToPlace.Length)], GetPrefabPosition(), GetPrefabRotation());
            created = true;
        }

        obj.transform.position = GetPrefabPosition();
        obj.transform.rotation = GetPrefabRotation();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            GameManagerPostPlacement();
        }
    }

    private void GameManagerPostPlacement()
    {
        canPlaceObstacle = false;
        turnUI.PlaceObject1.SetActive(false);
        turnUI.PlaceObject2.SetActive(false);
        gameManager.turn = 0;
        gameManager.PlayerTurns();
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

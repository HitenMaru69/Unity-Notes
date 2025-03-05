using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneTrackingDemo : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] GameObject SpawnObject;
    [SerializeField] ARPlaneManager planeManager;
    
    private List<ARRaycastHit> hits =  new List<ARRaycastHit>();
    
    private void Awake()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        planeManager = FindObjectOfType<ARPlaneManager>();
    }


    private void Update()
    {
        if(Input.touchCount > 0)
        {
            DetecedPlane();
        }
    }

    private void DetecedPlane()
    {
        Touch touch = Input.GetTouch(0);

        if (SpawnObject != null)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                
                var pos = hits[0].pose;
                Instantiate(SpawnObject, pos.position, pos.rotation);
                SpawnObject = null;

            }
        }
    }

    // Button

    public void CubePrefebButton(GameObject obj)
    {
        SpawnObject = obj;
    }

    public void SquerePrefebButton(GameObject obj)
    {
        SpawnObject = obj;
    }

    public void DesableAndEnablePlaneTrackingButton(TextMeshProUGUI txt)
    {
       planeManager.enabled = !planeManager.enabled;

        if (planeManager.enabled) {

            DeactivePlane(true);
            txt.text = "Disable";
        }
        else
        {
            DeactivePlane(false);
            txt.text = "Enable";
        }

    }

    private void DeactivePlane(bool isActive)   
    {
        foreach (var plane in planeManager.trackables) //  planeManager.trackables -> trackebale is the game object which is spawn as  plane reference
        {
            plane.gameObject.SetActive(isActive);
        }
    }
}

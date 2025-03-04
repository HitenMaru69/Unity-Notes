using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingDemo : MonoBehaviour
{
    // Note : - Object that you want to Spawn And Image both name will be the same 

    [SerializeField] List<GameObject> prefeb; // Add All Object that you want to Saw when image is tracked
    private ARTrackedImageManager arTrackedimageManager;
    private Dictionary<string,GameObject> matchObject = new Dictionary<string,GameObject>();

    private void Awake()
    {
        arTrackedimageManager = FindObjectOfType<ARTrackedImageManager>();
        SpwanAllPrebAtStart();
    }

    private void OnEnable()
    {
        arTrackedimageManager.trackedImagesChanged += OnImageChange;
        
    }

    private void OnDisable()
    {
        arTrackedimageManager.trackedImagesChanged -= OnImageChange;
    }

    private void OnImageChange(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage aRTrackedImage in obj.added)
        {
            SawObjectAccrodingImage(aRTrackedImage);
        }
        foreach (ARTrackedImage aRTrackedImage in obj.updated)
        {
            if(aRTrackedImage.trackingState == TrackingState.Tracking)
            {
                SawObjectAccrodingImage(aRTrackedImage);
            }
            else
            {
                HideObjectAccordingToImage(aRTrackedImage);
            }

        }
        foreach (ARTrackedImage aRTrackedImage in obj.removed)
        {
            matchObject[aRTrackedImage.referenceImage.name].SetActive(false);
        }
    }

    private void SpwanAllPrebAtStart()
    {
        foreach (GameObject obj in prefeb)
        {
            GameObject spwanPrefeb = Instantiate(obj, Vector3.zero, Quaternion.identity);
            spwanPrefeb.name = obj.name;
            matchObject.Add(spwanPrefeb.name, spwanPrefeb);
            spwanPrefeb.SetActive(false);

        }
    }

    private void SawObjectAccrodingImage(ARTrackedImage aRTrackedImage)
    {
        string nameofObject  = aRTrackedImage.referenceImage.name;
        Vector3 pos = aRTrackedImage.transform.position;

        if (matchObject.ContainsKey(nameofObject))
        {
            GameObject obj = matchObject[nameofObject];
            obj.transform.position = pos;
            obj.SetActive(true);
        }
    }

    private void HideObjectAccordingToImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (matchObject.ContainsKey(imageName))
        {
            matchObject[imageName].SetActive(false);
        }
    }
}

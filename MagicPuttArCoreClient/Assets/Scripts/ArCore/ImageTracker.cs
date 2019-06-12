using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ImageTracker : MonoBehaviour
{
    public GameObject GolfCourse;

    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    bool isTracked;
    Anchor anchor; 


    // Start is called before the first frame update
    void Start()
    {
        //GolfCourse.SetActive(false);
        isTracked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.All);

                // Create visualizers and anchors for updated augmented images that are tracking and do
                // not previously have a visualizer. Remove visualizers for stopped images.
                foreach (var image in m_TempAugmentedImages)
                {
                    if (image.TrackingState == TrackingState.Tracking)
                    {
                        // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                        anchor = image.CreateAnchor(image.CenterPose);
                        GolfCourse.transform.position = anchor.transform.position;
                        GolfCourse.transform.rotation = anchor.transform.rotation;
                        GolfCourse.transform.parent = anchor.transform;
                        GolfCourse.SetActive(true);
                    }
                }
            }
        }
    }
}

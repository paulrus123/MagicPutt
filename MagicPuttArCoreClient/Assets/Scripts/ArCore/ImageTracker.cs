using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using UnityEngine.UI;

public class ImageTracker : MonoBehaviour
{
    public GameObject GolfCourse;

    public AugmentedImageVisualizer AugmentedImageVisualizerPrefab;
    private Dictionary<int, AugmentedImageVisualizer> m_Visualizers = new Dictionary<int, AugmentedImageVisualizer>();
    public GameObject FitToScanOverlay;

    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    bool shouldTrack = true;

    [SerializeField]
    MyButtonHandler placeGolfCourseButton = default;

    private void Start()
    {
        GolfCourse.SetActive(false);
    }

    public void ToggleTracking()
    {
        shouldTrack = !shouldTrack;
    }

    // Update is called once per frame
    void Update()
    {
        Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

        if (shouldTrack)
        {
            // Create visualizers and anchors for updated augmented images that are tracking and do
            // not previously have a visualizer. Remove visualizers for stopped images.
            foreach (var image in m_TempAugmentedImages)
            {
                AugmentedImageVisualizer visualizer = null;
                m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
                if (image.TrackingState == TrackingState.Tracking && visualizer == null)
                {
                    // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                    Anchor anchor = image.CreateAnchor(image.CenterPose);
                    visualizer = (AugmentedImageVisualizer)Instantiate(AugmentedImageVisualizerPrefab, anchor.transform);
                    visualizer.Image = image;
                    m_Visualizers.Add(image.DatabaseIndex, visualizer);
                }
                else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
                {
                    m_Visualizers.Remove(image.DatabaseIndex);
                    Destroy(visualizer.gameObject);
                }
                if (placeGolfCourseButton.buttonPressed && image.TrackingState == TrackingState.Tracking)
                {
                    //World Anchor
                    Pose pose = image.CenterPose;
                    Anchor worldAnchor = Session.CreateAnchor(pose);
                    GolfCourse.transform.position = worldAnchor.transform.position;
                    GolfCourse.transform.rotation = worldAnchor.transform.rotation;
                    GolfCourse.transform.parent = worldAnchor.transform;
                    GolfCourse.SetActive(true);
                }
            }
            FitToScanOverlay.SetActive(true);
        }
        else
        {
            foreach (var image in m_TempAugmentedImages)
            {
                AugmentedImageVisualizer visualizer = null;
                m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
                if (visualizer != null)
                {
                    m_Visualizers.Remove(image.DatabaseIndex);
                    Destroy(visualizer.gameObject);
                }

            }
            FitToScanOverlay.SetActive(false);
        }
    }
}
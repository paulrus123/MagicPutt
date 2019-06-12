using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;

public class ImageTracker : MonoBehaviour
{
    public GameObject GolfCourse;

    public AugmentedImageVisualizer AugmentedImageVisualizerPrefab;
    private Dictionary<int, AugmentedImageVisualizer> m_Visualizers
    = new Dictionary<int, AugmentedImageVisualizer>();
    public GameObject FitToScanOverlay;

    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    private void Start()
    {
        GolfCourse.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

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

            if(Input.touchCount > 0)
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

        foreach (var visualizer in m_Visualizers.Values)
        {
            if (visualizer.Image.TrackingState == TrackingState.Tracking)
            {
                FitToScanOverlay.SetActive(false);
                return;
            }
        }

        FitToScanOverlay.SetActive(true);
    }
}
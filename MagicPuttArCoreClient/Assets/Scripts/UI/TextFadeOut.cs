using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeOut : MonoBehaviour
{
    public float timeout = 1;
    public float startFadeTime = 0.2f;
    float currentTime = 0f;

    [SerializeField]
    Text text = default;

    [SerializeField]
    Image image = default;
    Vector4 textColor;
    Vector4 imageColor;
    float initialImageAlpha;


    private void Start()
    {
        textColor = text.color;
        imageColor = image.color;
        initialImageAlpha = image.color.a;
    }

    private void OnEnable()
    {
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= startFadeTime)
        {
            textColor.w = Mathf.Lerp(1, 0, (currentTime - startFadeTime) / (timeout - startFadeTime));
            imageColor.w = Mathf.Lerp(initialImageAlpha, 0, (currentTime - startFadeTime) / (timeout - startFadeTime));

        }
        else
        {
            textColor.w = 1;
            imageColor.w = initialImageAlpha;
        }
        text.color = textColor;
        image.color = imageColor;

        if (currentTime >= timeout)
        {
            gameObject.SetActive(false);
        }
    }
}

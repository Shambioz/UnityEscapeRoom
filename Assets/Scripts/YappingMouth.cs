using System.Collections;
using UnityEngine;

public class YappingMouth : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer; // Reference to the SkinnedMeshRenderer
    private float minWeight = 0f; // Minimum BlendShape weight
    private float maxWeight = 100f; // Maximum BlendShape weight
    public float speed = 1f; // Speed of the animation

    private bool isAnimating = false; // Toggle for animation

    private Coroutine animationCoroutine;

    // Start the animation
    public void StartAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            animationCoroutine = StartCoroutine(AnimateBlendShape());
        }
    }

    // Stop the animation
    public void StopAnimation()
    {
        if (isAnimating)
        {
            isAnimating = false;
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
        }
    }

    // Toggle the animation on or off
    public void ToggleAnimation()
    {
        if (isAnimating)
        {
            StopAnimation();
        }
        else
        {
            StartAnimation();
        }
    }

    // Coroutine to animate the BlendShape
    private IEnumerator AnimateBlendShape()
    {
        float weight = minWeight;
        bool increasing = true;

        while (isAnimating)
        {
            // Update the BlendShape weight
            skinnedMeshRenderer.SetBlendShapeWeight(0, weight);
            skinnedMeshRenderer.SetBlendShapeWeight(1, 100-weight);

            // Adjust the weight
            if (increasing)
            {
                weight += speed * Time.deltaTime;
                if (weight >= maxWeight)
                {
                    weight = maxWeight;
                    increasing = false;
                }
            }
            else
            {
                weight -= speed * Time.deltaTime;
                if (weight <= minWeight)
                {
                    weight = minWeight;
                    increasing = true;
                }
            }

            yield return null; // Wait for the next frame
        }
    }
}

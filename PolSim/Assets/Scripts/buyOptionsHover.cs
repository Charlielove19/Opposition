using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buyOptionsHover : MonoBehaviour
{
    /// <summary>
    /// -- WW --
    /// </summary>
    private Vector3 startPos;
    private bool keepOpen = false;
    public bool isOpen = false;
    public void Start()
    {
        // startPos = transform.position;
        startPos = new Vector3(2095f, 679f, 0f);
        // Debug.Log("Start position: " + startPos);
    }
    // On hover transform the object left
    public void OnMouseEnter()
    {
        StartCoroutine(AnimateTransform(new Vector3(1740f, 679f, 0f), 0.1f));
        this.isOpen = true;
        // Debug.Log(new Vector3(transform.position.x - 355f, transform.position.y, transform.position.z));
    }
    public void KeepOpen(bool? keepOpen)
    {
        this.keepOpen = keepOpen ?? true;
    }
    // On exit transform the object right
    public void OnMouseExit()
    {
        if (!keepOpen) {
            StartCoroutine(AnimateTransform(startPos, 0.1f));
            this.isOpen = false;
        }
    }
    private IEnumerator AnimateTransform(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

}

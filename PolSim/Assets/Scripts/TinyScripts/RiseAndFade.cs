using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RiseAndFade : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- this script manages the cant afford icon's rise and color fade mechanics
    /// </summary>
    private UI_Controller uIController;
    void Start()
    {
        uIController = GameObject.Find("UI Manager").GetComponent<UI_Controller>();
    }
    void Update()
    {
        transform.Translate(Vector3.up * 0.5f);
        TextMeshProUGUI textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        Color currentColor = textMesh.color;
        float newAlpha = currentColor.a - 0.7f * Time.deltaTime; 
        newAlpha = Mathf.Clamp(newAlpha, 0f, 1f); 
        textMesh.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha); 
    }
}

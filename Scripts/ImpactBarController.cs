
using UnityEngine;

public class ImpactBarController : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- this script is applied to all UI bars within game and sets their dimensions based off relevant values
    /// it also accounts for negative values through a rotation of 180 degrees
    /// </summary>
    public RectTransform statsBar; 
    private RectTransform RectSelf => GetComponent<RectTransform>(); 
    [SerializeField] private float showPValue;
    public void SetHeight(float p)
    {
         if (p<0)
        {
            statsBar.rotation = Quaternion.Euler(0,0,180);
        }
        else
        {
            statsBar.rotation = Quaternion.Euler(0,0,0);
        }
        showPValue = p;
        statsBar.sizeDelta = new Vector2(statsBar.sizeDelta.x, Mathf.Abs(p*0.75f));
    }
    public void SetWidth(float i)
    {
         if (i<0)
        {
            statsBar.rotation = Quaternion.Euler(0,0,180);
        }
        else
        {
            statsBar.rotation = Quaternion.Euler(0,0,0);
        }
        RectSelf.sizeDelta = new Vector2(Mathf.Abs(i*0.75f), statsBar.sizeDelta.y);
    }
}

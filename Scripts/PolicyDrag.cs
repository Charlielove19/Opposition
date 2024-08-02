using UnityEngine;
using UnityEngine.EventSystems;


public class PolicyDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// WW
    /// </summary>
    private Vector2 offset; // offset from the mouse to the object
    private float clickScaleUpAmount= 1.05f;
    private Vector3 defaultScale => transform.localScale;

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.localScale = defaultScale*clickScaleUpAmount;
        offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position - offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localScale = defaultScale*(1/clickScaleUpAmount);
    }

}

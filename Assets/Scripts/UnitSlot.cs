using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    private RectTransform rectTransform;
    private GameObject unit;

    [SerializeField]
    private Canvas canvas;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            if (unit != null)
            {
                GameObject.Destroy(unit);
            }
            unit = GameObject.Instantiate(eventData.pointerDrag);
            unit.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            unit.GetComponent<RectTransform>().SetParent(gameObject.transform);
            unit.GetComponent<CanvasGroup>().alpha = 1f;
        }
    }

    public void OnPointerDown(PointerEventData eventData) { }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}

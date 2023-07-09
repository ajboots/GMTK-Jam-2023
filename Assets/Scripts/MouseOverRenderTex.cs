using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOverRenderTex : MonoBehaviour
{
    [SerializeField]
    private Camera minimapCam;

    [SerializeField]
    private GraphicRaycaster raycaster;

    [SerializeField]
    private TextMeshProUGUI highlightText;

    [SerializeField]
    private GameObject textParent;

    [SerializeField]
    private Image img;

    private List<RaycastResult> results;

    private GameObject highlightedObject;
    private bool objectScaled;
    private string objectText;

    private void Update()
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = Input.mousePosition;
        results = new List<RaycastResult>();
        raycaster.Raycast(data, results);

        //Debug.Log(data.position);

        for (int i = 0; i < results.Count; i++)
        {
            //bool foundObj = false;

            if (results[i].gameObject.name == "MiniMap")
            {
                Vector2 localClick;
                RectTransform rectTransform = results[i].gameObject.GetComponent<RawImage>().rectTransform;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, null, out localClick);

                //Debug.Log(localClick);

                Vector2 viewportClick = new Vector2(localClick.x / rectTransform.rect.xMax, localClick.y / rectTransform.rect.yMin);
                viewportClick.x += 1;
                viewportClick.x /= 2;
                viewportClick.y /= -2;
                viewportClick.y += 0.5f;

                //Debug.Log(viewportClick);

                if (!minimapCam) minimapCam = GameObject.FindGameObjectWithTag("miniMapCam").GetComponent<Camera>();
                Vector2 worldClick = minimapCam.ViewportToWorldPoint(viewportClick);

                //Debug.Log(worldClick);

                RaycastHit2D hit = Physics2D.Raycast(worldClick, new Vector3(0, 0, 1), float.PositiveInfinity, LayerMask.GetMask("Highlightable"));

                // Scale down if object is not selected
                if (highlightedObject != null && (hit.collider == null || highlightedObject != hit.collider.gameObject) && objectScaled)
                {
                    textParent.SetActive(false);
                    highlightedObject.transform.localScale /= 1.5f;
                    objectScaled = false;
                    objectText = null;
                }

                // Scale up moused over object if found.
                if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Highlightable") && !objectScaled)
                {
                    textParent.SetActive(true);
                    highlightedObject = hit.collider.gameObject;
                    highlightedObject.transform.localScale *= 1.5f;
                    objectScaled = true;
                    objectText = highlightedObject.GetComponent<HighlightText>().GetText();
                    highlightText.text = objectText;

                    Sprite s = highlightedObject.GetComponent<SpriteRenderer>().sprite;

                    img.sprite = s;
                    float imgWidth = s.textureRect.width;
                    float imgHeight = s.textureRect.height;
                    
                    if (imgWidth > imgHeight)
                    {
                        img.rectTransform.sizeDelta = new Vector2(82 * (imgWidth / imgHeight), 82 * (imgHeight / imgWidth));
                    } else
                    {
                        img.rectTransform.sizeDelta = new Vector2(82 * (imgWidth / imgHeight), 82 * (imgHeight / imgWidth));
                    }
                }
            }
        }
    }
}

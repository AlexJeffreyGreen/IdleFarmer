using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmingCard : MonoBehaviour
{
    public Transform Transform;
    private Vector3 defaultPosition;
    public float hoveredFixedHeight;
    public float cardFloatSpeed;
    //private Vector3 velocity = Vector3.zero;
    private bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        this.Transform = this.gameObject.GetComponent<Transform>();
        defaultPosition = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 tempPosition = new Vector3(this.defaultPosition.x, this.defaultPosition.y + hoveredFixedHeight);
        //this.Transform.localPosition = tempPosition;//Vector3.SmoothDamp(this.defaultPosition, tempPosition, ref velocity, cardFloatSpeed);//tempPosition;
        //this.Transform.position = Vector3.MoveTowards(defaultPosition, tempPosition, cardFloatSpeed * Time.deltaTime);
        //if(IsPointerOverGameObject(this.gameObject))
        //{
        //    //Debug.Log("Pointer over object");
        //    Vector3 tempPosition = new Vector3(this.defaultPosition.x, this.defaultPosition.y + hoveredFixedHeight);
        //    this.rectTransform.localPosition = tempPosition;//Vector3.SmoothDamp(this.defaultPosition, tempPosition, ref velocity, cardFloatSpeed);//tempPosition;

        //    if(Input.GetMouseButtonDown(0))
        //    {
        //        Debug.Log("Clicked on Card!");
        //    }

        //    //this.rectTransform.Translate(tempPosition * cardFloatSpeed * (Time.deltaTime/2));//transform.Translate(move * speed * Time.deltaTime);
        //}
        //else
        //{
        //    //Debug.Log("Pointer left object");
        //    Vector3 tempPostion = this.defaultPosition;
        //    this.rectTransform.localPosition = tempPostion;
        //}
    }

    //private void OnMouseOver()
    //{
    //    Vector3 localPosition = this.rectTransform.localPosition;
    //    localPosition = new Vector3(localPosition.x, localPosition.y + 50f);
    //    this.rectTransform.localPosition = localPosition;
    //}

    //private void OnMouseExit()
    //{
    //    Vector3 localPosition = this.rectTransform.localPosition;
    //    localPosition = new Vector3(localPosition.x, localPosition.y - 50f);
    //    this.rectTransform.localPosition = localPosition;
    //}
    private void OnMouseDown()
    {
        Debug.Log("Mouse click!");
        selected = !selected;
        if(!selected)
        {
            Vector3 tempPostion = this.defaultPosition;
            this.Transform.localPosition = tempPostion;
        }
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Mouse over test");
        Vector3 tempPosition = new Vector3(this.defaultPosition.x, this.defaultPosition.y + hoveredFixedHeight);
        this.Transform.localPosition = tempPosition;//Vector3.SmoothDamp(this.defaultPosition, tempPosition, ref velocity, cardFloatSpeed);//tempPosition;
        //this.Transform.position = Vector3.MoveTowards(defaultPosition, tempPosition, cardFloatSpeed * Time.deltaTime);
    }


    private void OnMouseExit()
    {
        if(!selected)
        {
            //Debug.Log("Mouse exit test");
            //float step = cardFloatSpeed * Time.deltaTime;
            //this.Transform.position = Vector3.MoveTowards(this.Transform.localPosition, this.defaultPosition, step);

            Vector3 tempPostion = this.defaultPosition;
            this.Transform.localPosition = tempPostion;
        }
    }

    public static bool IsPointerOverGameObject(GameObject gameObject)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults.Any(x => x.gameObject == gameObject);
    }
}

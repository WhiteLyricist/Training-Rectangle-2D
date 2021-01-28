using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineData : MonoBehaviour
{
    private GameObject _Rectangle1;
    private GameObject _Rectangle2;
    private LineRenderer _lineRenderer;

    public void SetupLine(GameObject target1, GameObject target2) //Функция для запоминания какие два объекта связывает линия.
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _Rectangle1 = target1;
        _Rectangle2 = target2;      
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateCollider(GameObject target1, GameObject target2) //Функция для созадния коллайдера для линии.
    {
        if (gameObject.GetComponent<PolygonCollider2D>() == null)
        { 
            PolygonCollider2D _polCol = gameObject.AddComponent<PolygonCollider2D>();
        }
        Vector2[] points = new Vector2[4];
        points[0] = new Vector2(target1.transform.position.x, target1.transform.position.y + _lineRenderer.startWidth / 2);
        points[1] = new Vector2(target1.transform.position.x, target1.transform.position.y - _lineRenderer.startWidth / 2);
        points[2] = new Vector2(target2.transform.position.x, target2.transform.position.y - _lineRenderer.endWidth / 2);
        points[3] = new Vector2(target2.transform.position.x, target2.transform.position.y + _lineRenderer.endWidth / 2);
        gameObject.GetComponent<PolygonCollider2D>().SetPath(0, points);
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
    }
       


    // Update is called once per frame
    void Update()
    {
        _lineRenderer.SetPosition(0, _Rectangle1.transform.position);  //Перересовка линии в зависимости от положения объектов к которым она прикреплена.
        _lineRenderer.SetPosition(1, _Rectangle2.transform.position);
        UpdateCollider(_Rectangle1,_Rectangle2);

       /* if (Input.GetMouseButtonUp(1)) 
        {
            if ((this.transform.gameObject.tag == "tagLine")&&(this.gameObject.GetComponent<LineData>()))
            {
                Destroy(this.transform.gameObject);
            }           
        }*/
    }
}

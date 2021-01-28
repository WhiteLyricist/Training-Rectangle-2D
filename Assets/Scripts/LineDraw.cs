using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LineDraw : MonoBehaviour
{
    [SerializeField] private Material _material;
    private LineRenderer _line;
    private Vector3 CurMousePos;
    private GameObject hit1, hit2, LineObject;
    static string tagLine = "tagLine";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 HitMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Координаты курсора.
            RaycastHit2D rayHit = Physics2D.Raycast(HitMousePos, Vector2.zero); //Луч исходящий из курсора.
            if ((rayHit == true) && rayHit.transform.gameObject.tag != "tagLine")
            {
                if (_line == null)
                {
                    LineObject = CreateLine();
                }
                hit1 = rayHit.transform.gameObject;
                CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CurMousePos.z = 1f;
                _line.SetPosition(0, CurMousePos);
                _line.SetPosition(1, CurMousePos);
            }
        }
        else if (Input.GetMouseButtonUp(1) && _line)
        {
            Vector2 HitMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Координаты курсора.
            RaycastHit2D rayHit = Physics2D.Raycast(HitMousePos, Vector2.zero); //Луч исходящий из курсора.
            if (rayHit == true)
            {
                hit2 = rayHit.transform.gameObject;
                if (hit1.transform.position != hit2.transform.position)
                {
                    var _lineObject = LineObject.AddComponent<LineData>();
                    _lineObject.SetupLine(hit1, hit2); //Сохраняем информацию о двух объектах между котороыми установлена связь.
                    hit1.GetComponent<RectangleController>().AddToList(LineObject);  //Добовление связи в список для каждолго из объектов.
                    hit2.GetComponent<RectangleController>().AddToList(LineObject);
                    CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    CurMousePos.z = 1f;
                    _line.SetPosition(1, CurMousePos);
                    _lineObject.UpdateCollider(hit1, hit2);
                    _line = null;
                }
                else 
                {
                    Destroy(LineObject); //Удаляем связь если для самого себя.
                }
            }
            else 
            {
                Destroy(LineObject); //Удаляем связь если она не ведёт на другой объект.
            }
        }
        else if (Input.GetMouseButton(1) && _line)
        {
            CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CurMousePos.z = 1f;
            _line.SetPosition(1, CurMousePos); //Ведём линию за мышкой.
        }
    }

    GameObject CreateLine()
    {
        GameObject Line = new GameObject("Line");
        _line = Line.AddComponent<LineRenderer>();
        Line.tag = tagLine;
        _line.material = _material;
        _line.positionCount = 2;
        _line.startWidth = 0.15f;
        _line.endWidth = 0.15f;
        _line.useWorldSpace = false;
        _line.numCapVertices = 50;
        return Line;
    }

}

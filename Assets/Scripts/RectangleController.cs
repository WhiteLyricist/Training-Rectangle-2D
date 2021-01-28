using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RectangleController : MonoBehaviour
{

    private Rigidbody2D _body;
    private List<GameObject> _linesList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _body=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToList(GameObject line) //Функция для добавления связи в список для объекта.
    {
        _linesList.Add(line); 
    }

    public void OnDestroy() //Удалении всех линий при удалении объекта.
    {
        foreach (GameObject line in _linesList)
        { 
             Destroy(line);
        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnMouseDrag()
    {
        Vector2 CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Координаты курсора.
        RaycastHit2D rayHit = Physics2D.Raycast(CurMousePos, Vector2.zero); //Луч исходящий из курсора.
        if (rayHit.transform != null) //Проверкав на наличие попадания луча.
        {
            _body.constraints = RigidbodyConstraints2D.None; //При перемещение объекта снимаем с него заморозку по всем координатам.
            _body.constraints = RigidbodyConstraints2D.FreezeRotation; //И обратно включаем заморозко вращения.
            _body.MovePosition(CurMousePos); //Перемещаем объект в след за курсором.
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _body.constraints = RigidbodyConstraints2D.FreezeAll; //Запрещаем объекту перемещаться в случае столкновения с ним другого объекта.
    }
}

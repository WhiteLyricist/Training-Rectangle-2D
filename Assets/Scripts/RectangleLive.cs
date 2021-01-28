using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleLive : MonoBehaviour
{

    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip _Delete;
    [SerializeField] private AudioClip _Create;

    private float doubleClickTimeLimit = 0.15f;

    [SerializeField] private GameObject RectanglePrefab; //Переменная для связи с шаблоном.
    private GameObject _Rectangle; //Переменная для слежения за экземпляром прямоугольника.

    private Vector3 mousePos; //Вектор для нахождения координат курсора.

    protected void Start()
    {
        StartCoroutine(InputListener());
    }

    //Обновление вызывается один раз за кадр/
    private IEnumerator InputListener()
    {
        while (enabled)
        { 
            if (Input.GetMouseButtonDown(0))
                yield return ClickEvent();

            yield return null;
        }
    }

    private IEnumerator ClickEvent()
    {
        //Пауза, чтобы вы не подхватили одно и то же событие мыши.
        yield return new WaitForEndOfFrame();

        float count = 0f;
        while (count < doubleClickTimeLimit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DoubleClick();
                yield break;
            }
            count += Time.deltaTime;//Увеличить счетчик за счет изменения времени между кадрами
            yield return null; //Ждать следующего кадра
        }
        SingleClick();
    }


    private void SingleClick()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Получения координат курсора.

        var layerMask = 1 << 8;

        Color random = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)); //Получение рандомного цвета для прямоугольника.

        if (Physics2D.OverlapBox(mousePos, new Vector2(20f * 0.16f, 10f * 0.16f), 0, layerMask, -5f, 5f) == null)
        {
            _Rectangle = Instantiate(RectanglePrefab) as GameObject; //Метод копирующий объект-шаблон.
            _Rectangle.transform.position = new Vector3(mousePos.x, mousePos.y, -4f); //Установка нового прямогольника на месте курсора.
            _Rectangle.GetComponent<SpriteRenderer>().material.color = random; //Присваивание новому прмяугольнику рандомный цвет.
            soundSource.PlayOneShot(_Create);
        }
    }

    private void DoubleClick()
    {
        Vector2 CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Координаты курсора.
        RaycastHit2D rayHit = Physics2D.Raycast(CurMousePos, Vector2.zero); //Луч исходящий из курсора.
        if (rayHit.transform != null) //Проверкав на наличие попадания луча.
        {
            GameObject hitObject = rayHit.transform.gameObject;
            Destroy(hitObject); //Удаление объекта в который попал луч.
            soundSource.PlayOneShot(_Delete);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    
    public float tileSize;

    public Tilemap tilemap ;
    public bool skipMove;


    private Enemy instance;
    private LayerMask maskCombined = 1 << 8 | 1 << 9;
    private LayerMask maskWalls = 1 << 8;
    private Vector3 destination;
    private Coroutine coroutine;

    private Vector3 currentPosition;
    private Vector3 lastPosition;
    private bool ready = true;
    private bool readyAttack = true;
    private bool endBattle = false;
    GameObject[] gos; // массив всех врагов
    private float waitMove; // будем перемещать юнитов с задержкой
    private GameObject closestObject; //ближайший враг

    public void Start()
    {
        currentPosition = transform.localPosition; // сохраняем текущую позицию
        lastPosition = currentPosition; // сохраняем последную позицию юнита.
        // определяем с какой задержкой будет двигаться юнит
        waitMove = UnityEngine.Random.Range(0.4f, 0.65f);
    }


    public void Update()
    {
            if (ready)
            {
                //ищем всех врагов, заранее пометив их тегом Enemy
                gos = GameObject.FindGameObjectsWithTag("OurObjects");
                if (gos.Length == 0)
                {
                    endBattle = true; // если врагов нету, то конец сражения
                    print("End Battle");
                }

                //еслі есть врагі, то ходім
                if (!endBattle)
                {
                    //находим ближайшего врага и его координаты
                    GameObject goClosestEnemy = findClosestEnemy();
                    int targetX, targetY;
                    targetX = (int)goClosestEnemy.transform.localPosition.x;
                    targetY = (int)goClosestEnemy.transform.localPosition.y;

                    int[,] cMap = findWave(targetX, targetY); // находим путь до ближайшего врага

                    if (!stopMove(targetX, targetY)) // двигаемся, если цель не на соседней клетке
                        // вызываем каротину для перемещения с задержкой
                        StartCoroutine(move(cMap, targetX, targetY));

                    if (readyAttack)
                    {//атакуем, если дошли до цели
                        if (stopMove(targetX, targetY))
                        {
                            StartCoroutine(attack());
                        }
                    }
                }

                // запоминаем новую позицию после перемещения и делаем ее текущей
                currentPosition = transform.localPosition;
                //помечаем, что клетка занята воином
               // Battlefield.battlefield[(int)currentPosition.x, (int)currentPosition.y] = 1;

                //если мы переместились, то на старой клетки пишем, что она освободилась
               // if (currentPosition != lastPosition)
                //{
                 //   Battlefield.battlefield[(int)lastPosition.x, (int)lastPosition.y] = 0;
                  //  lastPosition = currentPosition; // запоминаем текущее рассположение как последнее
               // }
            }
        }

    private IEnumerator attack()
    {
        readyAttack = false; // для того, чтобы юнит атакова 1 раз в 0.8 секунды
        yield return new WaitForSeconds(0.8f);
        ////////
        ////////РЕАЛИЗУЕМ МЕТОД АТАКИ ВРАГА
        ////////
        readyAttack = true;
    }
    public void MoveEnemy()
    {
       
    }

    private IEnumerator move(int[,] cMap, int targetX, int targetY)
    {
        
        ready = false;
        int[] neighbors = new int[8]; //значение весов соседних клеток
                                      // будем хранить в векторе координаты клетки в которую нужно переместиться
        Vector3 moveTO = new Vector3(-1, 0, 10);

        // да да да, можно было сделать через цикл for
        neighbors[0] = cMap[(int)currentPosition.x + 1, (int)currentPosition.y + 1];
        neighbors[1] = cMap[(int)currentPosition.x, (int)currentPosition.y + 1];
        neighbors[2] = cMap[(int)currentPosition.x - 1, (int)currentPosition.y + 1];
        neighbors[3] = cMap[(int)currentPosition.x - 1, (int)currentPosition.y];
        neighbors[4] = cMap[(int)currentPosition.x - 1, (int)currentPosition.y - 1];
        neighbors[5] = cMap[(int)currentPosition.x, (int)currentPosition.y - 1];
        neighbors[6] = cMap[(int)currentPosition.x + 1, (int)currentPosition.y - 1];
        neighbors[7] = cMap[(int)currentPosition.x + 1, (int)currentPosition.y];
        for (int i = 0; i < 8; i++)
        {
            if (neighbors[i] == -2)
                // если клетка является непроходимой, делаем так, чтобы на нее юнит точно не попал
                // делаем этот костыль для того, чтобы потом было удобно брать первый элемент в
                // отсортированом по возрастанию массиве
                neighbors[i] = 99999;
        }
        Array.Sort(neighbors); //первый элемент массива будет вес клетки куда нужно двигаться

        //ищем координаты клетки с минимальным весом. 
        for (int x = (int)currentPosition.x - 1; x <= (int)currentPosition.x + 1; x++)
        {
            for (int y = (int)currentPosition.y + 1; y >= (int)currentPosition.y - 1; y--)
            {
                if (cMap[x, y] == neighbors[0])
                {
                    // и указываем вектору координаты клетки, в которую переместим нашего юнита
                    moveTO = new Vector3(x, y, 10);
                }
            }
        }
        //если мы не нашли куда перемещать юнита, то оставляем его на старой позиции.
        // это случается, если вокруг юнита, во всех 8 клетках, уже размещены другие юниты
        if (moveTO == new Vector3(-1, 0, 10))
            moveTO = new Vector3(currentPosition.x, currentPosition.y, 10);

        //и ура, наконец-то мы перемещаем нашего юнита
        // теперь он на 1 клетку ближе к врагу
        transform.localPosition = moveTO;

        //устанавливаем задержку.
        yield return new WaitForSeconds(waitMove);
        ready = true;
    }

    //Ищмем путь к врагу
    //TargetX, TargetY - координаты ближайшего врага
    public int[,] findWave(int targetX, int targetY)
    {
        bool add = true; // условие выхода из цикла
        // делаем копию карты локации, для дальнейшей ее разметки
        foreach (var instanceAvailablePlace in Field.instance.availablePlaces)
        {
            int[,] cMap = new int[200, 200];
            int x, y, step = 0; // значение шага равно 0
            //for (x = 0; x < instanceAvailablePlace.x; x++)
            //{
               // for (y = 0; y < instanceAvailablePlace.y; y++)
                //{
                 //   if (Battlefield.battlefield[x, y] == 1)
                  //      cMap[x, y] = -2; //если ячейка равна 1, то это стена (пишим -2)
                   // else cMap[x, y] = -1; //иначе еще не ступали сюда
               // }
            //}

            //начинаем отсчет с финиша, так будет удобней востанавливать путь
            cMap[targetX, targetY] = 0;
            while (add == true)
            {
                add = false;
                    for (x = 0; x < instanceAvailablePlace.x; x++)
                    {
                        for (y = 0; y < instanceAvailablePlace.y; y++)
                        {
                            if (cMap[x, y] == step)
                            {
                                // если соседняя клетка не стена, и если она еще не помечена
                                // то помечаем ее значением шага + 1
                                if (y - 1 >= 0 && cMap[x, y - 1] != -2 && cMap[x, y - 1] == -1)
                                    cMap[x, y - 1] = step + 1;
                                if (x - 1 >= 0 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == -1)
                                    cMap[x - 1, y] = step + 1;
                                if (y + 1 >= 0 && cMap[x, y + 1] != -2 && cMap[x, y + 1] == -1)
                                    cMap[x, y + 1] = step + 1;
                                if (x + 1 >= 0 && cMap[x + 1, y] != -2 && cMap[x + 1, y] == -1)
                                    cMap[x + 1, y] = step + 1;
                            }
                        }
                    }
                    step++;
                    add = true;
                    if (cMap[(int)transform.localPosition.x, (int)transform.localPosition.y] > 0) //решение найдено
                        add = false;
                    if (step > instanceAvailablePlace.x * instanceAvailablePlace.y) //решение не найдено, если шагов больше чем клеток
                        add = false;
            }

            return cMap;
        }

        return null; // возвращаем помеченную матрицу, для востановления пути в методе move()
    }

    public bool stopMove(int targetX, int targetY)
    {
        bool move = false;
        for (int x = (int)currentPosition.x - 1; x <= (int)currentPosition.x + 1; x++)
        {
            for (int y = (int)currentPosition.y + 1; y >= (int)currentPosition.y - 1; y--)
            {
                if (x == targetX && y == targetY)
                {
                    move = true;
                }
            }
        }
        return move;
    }

    GameObject findClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            float curDistance = Vector3.Distance(go.transform.position, position);
            if (curDistance < distance)
            {
                closestObject = go;
                distance = curDistance;
            }
        }
        return closestObject;
    }

    bool[,] tilesmap = new bool[7,9];

    
}

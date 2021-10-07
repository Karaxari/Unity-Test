using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform panel;
    public GameObject[] allCars = null;
    public PathCreation.PathCreator path_1;
    public PathCreation.PathCreator path_2;
    public PathCreation.EndOfPathInstruction endOfPathInstruction;
    public float speed = 50;
    public float distance = 15f;
    public int numStep = 10;
    public int countCar = 10;
    public int countClone = 3;

    public GameObject sphere;
    public Material correctAnswer;
    public Material wrongAnswer;
    public Material noneAnswer;

    int [] allIndexCars = null;

    List<GameObject> allObject = new List<GameObject>();
    List<GameObject> rightObject = new List<GameObject>();
    List<GameObject> leftObject = new List<GameObject>();
    List<GameObject> queue—ars = new List<GameObject>();
    List<GameObject> allObjectCars = new List<GameObject>();
    //[RequireComponent(typeof("PathFollower"))]
    // Start is called before the first frame update
    void Start()
    {
        allIndexCars = new int[allCars.Length];
        for (int i = 0; i<allCars.Length - 1; i++)
        {
            allIndexCars[i] = i;
        }
        allIndexCars = ShuffleArray(allIndexCars);

        for (int i = 0; i < countCar; i++)
        {
            int index = allIndexCars[i];
            for (int j = 0; j < countClone; j++)
            {
                GameObject car = Instantiate(allCars[index], allCars[index].transform.position, Quaternion.identity);
                car.name = allCars[index].name + "_" + j.ToString();
                car.transform.parent = panel.transform;
                car.transform.position = new Vector3(panel.position.x, panel.position.y, panel.position.z - 40);
                car.transform.Rotate(car.transform.rotation.x, car.transform.rotation.y + 90, car.transform.rotation.z, Space.World);
                car.AddComponent<PathCreation.Examples.PathFollower>();
                car.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = null;
                car.GetComponent<PathCreation.Examples.PathFollower>().endOfPathInstruction = endOfPathInstruction;
                car.GetComponent<PathCreation.Examples.PathFollower>().speed = speed;
                car.GetComponent<PathCreation.Examples.PathFollower>().distance = distance;
                car.GetComponent<PathCreation.Examples.PathFollower>().numStep = numStep;
                allObjectCars.Add(car);
            }
            GameObject car_ = Instantiate(allCars[index], allCars[index].transform.position, Quaternion.identity);
            car_.transform.localScale = new Vector3(25f, 25f, 25f);
            allObject.Add(car_);
        }

        foreach (GameObject obj in allObject)
        {
            int r = Random.Range(0, 2);
            Debug.Log("Right 0, Left 1: " + r.ToString());
            switch (r) {
                case 0: rightObject.Add(obj); break;
                case 1: leftObject.Add(obj); break;
            }

        }

        Vector3 panelTrans = panel.transform.position;
        ////sphere = Instantiate(indicationAnswers, indicationAnswers.transform.position, Quaternion.identity);
        sphere.transform.parent = panel.transform;
        sphere.transform.position = new Vector3(panelTrans.x, panelTrans.y, panelTrans.z + 15);
        refreshSphere();

        spavnObject();

        //GameObject cube = Instantiate(allCars[0], allCars[0].transform.position, Quaternion.identity);
        //cube.transform.position = new Vector3(0f, 0f, 0f);
        //cube.transform.Rotate(cube.transform.rotation.x, cube.transform.rotation.y + 90, cube.transform.rotation.z, Space.World);
        //cube.AddComponent<PathCreation.Examples.PathFollower>();
        //cube.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = path_1;
        //cube.GetComponent<PathCreation.Examples.PathFollower>().endOfPathInstruction = endOfPathInstruction;
        //cube.GetComponent<PathCreation.Examples.PathFollower>().speed = speed;


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveLeft();
        }
    }

    void spavnObject()
    {
        int n = 0;
        foreach (GameObject obj in rightObject)
        {
            obj.transform.position = new Vector3(panel.position.x + 9, panel.position.y + n, panel.position.z + 30);
            obj.transform.Rotate(obj.transform.rotation.x, obj.transform.rotation.y + 45, obj.transform.rotation.z, Space.World);
            n++;
        }

        n = 0;
        foreach (GameObject obj in leftObject)
        {
            obj.transform.position = new Vector3(panel.position.x - 9, panel.position.y + n, panel.position.z + 30);
            obj.transform.Rotate(obj.transform.rotation.x, obj.transform.rotation.y + 135, obj.transform.rotation.z, Space.World);
            n++;
        }

        allIndexCars = ShuffleArray(allIndexCars);

        for (int i = 0; i < countClone; i++)
        {
            allObjectCars[allIndexCars[i]].transform.position = new Vector3(panel.position.x, panel.position.y, panel.position.z + 40 + (i * distance));
            queue—ars.Add(allObjectCars[allIndexCars[i]]);
        }
    }

    void addCar()
    {
        //int rand = Random.Range(0, countCar - 1);
        //GameObject car = Instantiate(allCars[rand], allCars[rand].transform.position, Quaternion.identity);
        //car.transform.parent = panel.transform;
        //car.transform.position = new Vector3(panel.position.x, panel.position.y, panel.position.z + 40 + ((countCar - 1) * distance));
        //car.transform.Rotate(car.transform.rotation.x, car.transform.rotation.y + 90, car.transform.rotation.z, Space.World);
        //car.AddComponent<PathCreation.Examples.PathFollower>();
        //car.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = null;
        //car.GetComponent<PathCreation.Examples.PathFollower>().endOfPathInstruction = endOfPathInstruction;
        //car.GetComponent<PathCreation.Examples.PathFollower>().speed = speed;
        //car.GetComponent<PathCreation.Examples.PathFollower>().distance = distance;
        //car.GetComponent<PathCreation.Examples.PathFollower>().numStep = numStep;
        //allObject.Add(car);

        //Debug.Log("Size count allObjectCars: " + allObjectCars.Count);
        int rand = Random.Range(0, allObjectCars.Count);
        while (queue—ars.Contains(allObjectCars[rand]))
        {
            //Debug.Log(allObjectCars[rand].name + " ”ÊÂ ÂÒÚ¸ ‚ Ó˜ÂÂ‰Ë!");
            rand = Random.Range(0, allObjectCars.Count);
        }

        
        queue—ars.RemoveAt(0);


        //allObjectCars[rand].GetComponent<PathCreation.Examples.PathFollower>().endOfPathInstruction = PathCreation.EndOfPathInstruction.Loop;
        //Debug.Log(PathCreation.EndOfPathInstruction.Loop);
        //allObjectCars[rand].GetComponent<PathCreation.Examples.PathFollower>().pathCreator = path_1;
        //allObjectCars[rand].GetComponent<PathCreation.Examples.PathFollower>().pathCreator = null;
        //allObjectCars[rand].GetComponent<PathCreation.Examples.PathFollower>().endOfPathInstruction = endOfPathInstruction;
        //Debug.Log(endOfPathInstruction);
        //GameObject car = allObjectCars[rand];

        allObjectCars[rand].transform.position = new Vector3(panel.position.x, panel.position.y, panel.position.z + 40 + ((countClone - 1) * distance));
        allObjectCars[rand].transform.rotation = Quaternion.Euler(0.0f, 90f, 0.0f);
        allObjectCars[rand].GetComponent<PathCreation.Examples.PathFollower>().reloadDistance();
        allObjectCars[rand].GetComponent<PathCreation.Examples.PathFollower>().pathCreator = null;
        allObjectCars[rand].GetComponent<PathCreation.Examples.PathFollower>().endOfPathInstruction = endOfPathInstruction;

        queue—ars.Add(allObjectCars[rand]);
        //Debug.Log(allObjectCars[rand].name);
        //Debug.Log("Pocition: " + allObjectCars[rand].transform.position.x.ToString() + " " + allObjectCars[rand].transform.position.z.ToString());
        //Debug.Log(queue—ars[0].name);
    }

    public void checkResult(int num)
    {
        switch (num)
        {
            case 0: moveRight(); break;
            case 1: moveLeft(); break;
        }
    }

    void moveRight()
    {
        Debug.Log("Queue size: " + queue—ars.Count.ToString());
        Debug.Log("Right");
        checkCar(rightObject);
        //Debug.Log(queue—ars[0].name);
        if (queue—ars.Count > 0)
        {
            queue—ars[0].GetComponent<PathCreation.Examples.PathFollower>().pathCreator = path_1;
            for (int i = 1; i<queue—ars.Count; i++)
            {
                queue—ars[i].GetComponent<PathCreation.Examples.PathFollower>().moveCar();
            }
            addCar();       
        }
    }

    void moveLeft()
    {
        Debug.Log("Queue size: " + queue—ars.Count.ToString());
        Debug.Log("Left");
        checkCar(leftObject);
        //Debug.Log(queue—ars[0].name);
        if (queue—ars.Count > 0)
        {
            queue—ars[0].GetComponent<PathCreation.Examples.PathFollower>().pathCreator = path_2;
            for (int i = 1; i < queue—ars.Count; i++)
            {
                queue—ars[i].GetComponent<PathCreation.Examples.PathFollower>().moveCar();
            }
            addCar();
        }
    }
   

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    void checkRightCar()
    {
        Debug.Log(queue—ars[0].name);
        foreach (GameObject obj in rightObject)
        {
            if (queue—ars[0].name.Contains(obj.name))
            {
                Debug.Log(obj.name);
                Debug.Log("Good!");
                return;
            }
        }
        Debug.Log("Loos!");
    }

    void checkLeftCar()
    {
        Debug.Log(queue—ars[0].name);
        foreach (GameObject obj in leftObject)
        {
            if (queue—ars[0].name.Contains(obj.name))
            {
                Debug.Log(obj.name);
                Debug.Log("Good!");
                return;
            }
        }
        Debug.Log("Loos!");
    }

    void checkCar(List<GameObject> RigLefObject)
    {
        string name1 = queue—ars[0].name.Remove(queue—ars[0].name.LastIndexOf("_"));
        Debug.Log(name1);
        foreach (GameObject obj in RigLefObject)
        {
            string name2 = obj.name.Remove(obj.name.IndexOf("(Clone"));
            if (name1 == name2)
            {
                Debug.Log(obj.name);
                Debug.Log("Good!");
                sphere.GetComponent<MeshRenderer>().material = correctAnswer;
                return;
            }
            Debug.Log(obj.name);
        }
        sphere.GetComponent<MeshRenderer>().material = wrongAnswer;
        Debug.Log("Loos!");
    }

    public void refreshSphere()
    {
        sphere.GetComponent<MeshRenderer>().material = noneAnswer;
    }
}

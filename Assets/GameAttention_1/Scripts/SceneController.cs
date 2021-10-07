using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Transform panel;
    public Canvas canvas;
    public ResponseIndication sphere;
    public int sizePanel = 4; // размер игрвой сетки
    public int numCube = 4; // количество активных кубов
    public float sizeCude = 100f; // размер куба
    public float gapCube = 100f; //удаленность кубов

    [SerializeField] private Material[] materials;
    [SerializeField] private Sprite[] images;
    [SerializeField] GameObject buutonPrefab;

    //private const int sizeArr = sizePanel;
    Object[] allObjects = null;
    int[] arrIndex = null;
    int[] oldArrIndex = null;
    int[] numbers = null;

    int correctAnswer;


    // Start is called before the first frame update
    void Start()
    {
       
        allObjects = new GameObject[sizePanel * sizePanel];
        numbers = new int[sizePanel * sizePanel];
        arrIndex = new int[numCube];
        oldArrIndex = new int[numCube];
        float pos;
        if ((sizePanel % 2) == 0)
        {
            pos = (sizePanel / 2 * gapCube) - gapCube / 2;
        }
        else
        {
            pos = (sizePanel / 2 * gapCube);
        }    
        Vector3 panelTrans = panel.transform.position;
        for (int i = 0; i < sizePanel; i++)
        {
            for (int j = 0; j < sizePanel; j++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.parent = panel.transform;
                cube.transform.position = new Vector3(panelTrans.x + (i * gapCube) + (gapCube - sizeCude) - pos, panelTrans.y + (j * gapCube) - pos, panelTrans.z);
                cube.transform.localScale = new Vector3(sizeCude, sizeCude, sizeCude);
                cube.GetComponent<MeshRenderer>().material = materials[0];
                allObjects[(i * sizePanel) + j] = cube;
                numbers[(i * sizePanel) + j] = (i * sizePanel) + j;
            }
        }
        
        string str1 = "";
        for(int i = 0; i<sizePanel*sizePanel; i++)
        {
            str1 = str1 + numbers[i].ToString() + ", ";
        }
        Debug.Log(str1);


        //activeCube();
        numbers = ShuffleArray(numbers);
        for (int i = 0; i < numCube; i++)
        {
            arrIndex[i] = numbers[i];
            //Debug.Log(numbers[i]);
            GameObject ob = (GameObject)allObjects[numbers[i]];
            ob.GetComponent<MeshRenderer>().material = materials[1];
        }
        Debug.Log("---------------------------------");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void activeCube()
    {
        //numbers = ShuffleArray(numbers);
        //for(int i=0; i<numCube; i++)
        //{
        //    arrIndex[i] = numbers[i];
        //    //Debug.Log(numbers[i]);
        //    GameObject ob = (GameObject)allObjects[numbers[i]];
        //    ob.GetComponent<MeshRenderer>().material = materials[1];
        //}
        //Debug.Log("---------------------------------");

        numbers = ShuffleArray(numbers);
        int rand = Random.Range(0, numCube + 1);

        correctAnswer = rand;

        Debug.Log("---------------------------------");
        Debug.Log("ѕравильный ответ: " + rand.ToString());
        int i = 0;
        while (rand > 0)
        {
            if (checkArray(numbers[i])){
                arrIndex[rand - 1] = numbers[i];
                rand--;
            }
            i++;
        }

        for(int j = 0; j < numCube; j++)
        {
            GameObject ob = (GameObject)allObjects[arrIndex[j]];
            ob.GetComponent<MeshRenderer>().material = materials[1];
        }
    }

    bool checkArray(int n)
    {
        for (int i = 0; i < numCube; i++)
        {
            if (oldArrIndex[i] == n)
            {
                return false;
            }
        }
        return true;
    }


    void deactiveCube()
    {
        //oldArrIndex = arrIndex;
        for(int k =0; k<numCube; k++)
        {
            oldArrIndex[k] = arrIndex[k];
        }
        for (int i = 0; i < numCube; i++)
        {
            GameObject ob = (GameObject)allObjects[arrIndex[i]];
            ob.GetComponent<MeshRenderer>().material = materials[0];
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

    public void RefreshCube()
    {
        deactiveCube();
        activeCube();
    }

    public int GetActiveCube()
    {
        return numCube;
    }

    public void checkResult(int num)
    {
        //int col = 0;
        //for (int i = 0; i < numCube; i++)
        //{
        //    for (int j = 0; j < numCube; j++)
        //    {
        //        if (oldArrIndex[i] == arrIndex[j])
        //        {
        //            col++;
        //            break;
        //        }
        //    }
        //}
        //col = numCube - col;
        //Debug.Log("ќтвет программы: " + col.ToString());
        //if (num == col)
        //{
        //    //Debug.Log("Good!");
        //    sphere.GoodIndication();
        //}
        //else
        //{
        //    //Debug.Log("Loos!");
        //    sphere.LossIndication();
        //}


        if (num == correctAnswer)
        { 
            sphere.GoodIndication();
        }
        else
        {
            sphere.LossIndication();
        }
        string arr1 = "", arr2 = "";
        for(int i = 0; i < numCube; i++)
        {
            arr1 = arr1 + arrIndex[i].ToString() + ", ";
            arr2 = arr2 + oldArrIndex[i].ToString() + ", ";
        }
        Debug.Log("arrundex: " + arr1);
        Debug.Log("oldArrIndex: " + arr2);
    }
}

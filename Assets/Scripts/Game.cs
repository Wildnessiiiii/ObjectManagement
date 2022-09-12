using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Game : MonoBehaviour
{
    public Transform prefab;
    public KeyCode creatKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;

    private List<Transform> objects;
    private string savePath;

    private void Awake()
    {
        objects = new List<Transform>();
        savePath = Path.Combine(Application.persistentDataPath, "saveFile");
        //Debug.Log(Application.persistentDataPath);
        //Debug.Log(savePath);
    }

    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(creatKey))
        {
            CreatObject();
        }
        else if(Input.GetKeyDown(newGameKey))
        {
            BeginNewGame();
        }
    }

    void Save()
    {
        File.Open(savePath, FileMode.Create);
    }

    void BeginNewGame()
    {
        for(int i =0;i<objects.Count;i++)
        {
            Destroy(objects[i].gameObject);
        }
        objects.Clear();
    }

    void CreatObject()
    {
        Transform t = Instantiate(prefab);
        t.localPosition = Random.insideUnitSphere * 5f;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        objects.Add(t);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Game : PersistableObject
{
    public ShapeFactory shapeFactory;
    public KeyCode creatKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveKey = KeyCode.S;
    public KeyCode loadKey = KeyCode.L;

    private List<PersistableObject> shapes;

    public PersistentStorage storage;

    private void Awake()
    {
        shapes = new List<PersistableObject>();       
    }

    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(creatKey))
        {
            CreatShape();
        }
        else if(Input.GetKeyDown(newGameKey))
        {
            BeginNewGame();
        }
        else if (Input.GetKeyDown(saveKey))
        {
            Debug.Log("Save");
            storage.Save(this);
        }
        else if (Input.GetKeyDown(loadKey))
        {
            storage.Load(this);
        }
    }

    void BeginNewGame()
    {
        for(int i =0;i< shapes.Count;i++)
        {
            Destroy(shapes[i].gameObject);
        }
        shapes.Clear();
    }

    void CreatShape()
    {
        Shape instance = shapeFactory.GetRandom();

        Transform t = instance.transform;
        t.localPosition = Random.insideUnitSphere * 5f;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        shapes.Add(instance);
    }

    public override void Save(GameDataWriter writer)
    {
        writer.Write(shapes.Count);
        for(int i =0;i< shapes.Count;i++)
        {
            shapes[i].Save(writer);
        }
    }

    public override void Load(GameDataReader reader)
    {
        int count = reader.ReadInt();
        for (int i = 0; i < count; i++)
        {
            Shape instance = shapeFactory.Get(1);
            instance.Load(reader);
            shapes.Add(instance);
        }
    }
}

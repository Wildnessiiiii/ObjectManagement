using System.Collections.Generic;
using UnityEngine;

public class Game : PersistableObject
{
    public PersistableObject prefab;
    public KeyCode creatKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveKey = KeyCode.S;
    public KeyCode loadKey = KeyCode.L;

    private List<PersistableObject> objects;

    public PersistentStorage storage;

    private void Awake()
    {
        objects = new List<PersistableObject>();       
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
        for(int i =0;i<objects.Count;i++)
        {
            Destroy(objects[i].gameObject);
        }
        objects.Clear();
    }

    void CreatObject()
    {
        PersistableObject o = Instantiate(prefab);

        Transform t = o.transform;
        t.localPosition = Random.insideUnitSphere * 5f;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        objects.Add(o);
    }

    public override void Save(GameDataWriter writer)
    {
        writer.Write(objects.Count);
        for(int i =0;i<objects.Count;i++)
        {
            objects[i].Save(writer);
        }
    }

    public override void Load(GameDataReader reader)
    {
        int count = reader.ReadInt();
        for (int i = 0; i < count; i++)
        {
            PersistableObject o = Instantiate(prefab);
            o.Load(reader);
            objects.Add(o);
        }
    }
}

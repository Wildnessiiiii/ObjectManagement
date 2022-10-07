using UnityEngine;

public class Shape : PersistableObject
{
    MeshRenderer meshRenderer;
    static int colorPropertyId = Shader.PropertyToID("_Color");
    static MaterialPropertyBlock propertyBlock;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private Color color;
    public int ShapeId
    {
        get
        {
            return shapeId;
        }
        set
        {
            if(shapeId== int.MinValue && value!= int.MinValue)
            {
                shapeId = value;
            }
            else
            {
                //Debug.LogError("Not allowed to change shapeId.");
            }
        }
    }

    int shapeId = int.MinValue;

    public int MaterialId { get; private set; }

    public void SetMaterial(Material material, int materialId)
    {
        meshRenderer.material = material;
        MaterialId = materialId;
    }

    public void SetColor(Color color)
    {
        this.color = color;
 
        if(propertyBlock==null)
        {
            propertyBlock = new MaterialPropertyBlock();
        }
        propertyBlock.SetColor(colorPropertyId, color);
        meshRenderer.SetPropertyBlock(propertyBlock);
    }

    public override void Save(GameDataWriter writer)
    {
        base.Save(writer);
        writer.Write(color);
    }

    public override void Load(GameDataReader reader)
    {
        base.Load(reader);
        SetColor(reader.ReadColor());
    }
}
;
using UnityEngine;

[DisallowMultipleComponent]
public class PersistableObject : MonoBehaviour
{
    public virtual void Save(GameDataWriter writer)
    {
        writer.Write(transform.localPosition);
        writer.Write(transform.localRotation);
        writer.Write(transform.localScale);
    }
    //npm config set cache "E:\nodejs\node_cache"
    //npm config set prefix "E:\nodejs\node_global"
    public virtual void Load(GameDataReader reader)
    {
        transform.localPosition = reader.ReadVector3();
        transform.localRotation = reader.ReadQuaternion();
        transform.localScale = reader.ReadVector3();
    }
}

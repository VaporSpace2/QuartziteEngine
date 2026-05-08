namespace Quartzite
{
    public class GameObject(string name)
    {
        public string Name = name;
        public int SelfID { get; internal set; }

        internal List<int> ComponentIDs = new();

        public void Destroy()
        {
            EntityComponentManager.DestroyGameObject(SelfID);
        }
        public int AddComponent(Component component)
        {
            EntityComponentManager.AddComponent(this, component);
            return component.SelfID;
        }

        public void RemoveComponent(int componentID)
        {
            EntityComponentManager.DestroyComponent(componentID);
        }
    }

    public abstract class Component()
    {
        public bool Active = true;
        public int SelfID { get; internal set; }
        public int OwnerID { get; internal set; }

        public virtual void Init() { }
        public virtual void DeInit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}
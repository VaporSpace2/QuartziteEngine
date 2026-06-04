namespace Quartzite
{
    public class GameObject(int id)
    {
        public int SelfID = id;

        public int[] Components = new int[16];

        public void AddComponent(Component component)
        {
            gameObject.AddComponent(this, component);
        }

        public void Destroy()
        {
            gameObject.DestroyGameObject(SelfID);
        }
    }

    public struct Position(float x, float y)
    {
        public float X = x;
        public float Y = y;
    }

    public struct Velocity(float x, float y)
    {
        public float VelocityX = x;
        public float VelocityY = y;
    }

    public abstract class Component()
    {
        public bool Active = true; // arbitrary value
        public int SelfID { get; internal set; }
        public int OwnerID { get; internal set; }

        public virtual void Init() { } // called when the component is created
        public virtual void Awake() { } // called right before the first wave of Update() methods are called
        public virtual void DeInit() { } // called when the component is destroyed
        public virtual void Update() { } // called every frame
        public virtual void FixedUpdate() { } // called every physics frame

        public void Destroy()
        {
            gameObject.DestroyComponent(SelfID);
        }
    }
}
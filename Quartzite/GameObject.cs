namespace Quartzite
{
    public struct GameObject(int id)
    {
        public int SelfID = id;

        public int[] Components = new int[16];
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
        public bool Active = true;
        public int SelfID { get; internal set; }
        public int OwnerID { get; internal set; }

        public virtual void Init() { }
        public virtual void DeInit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}
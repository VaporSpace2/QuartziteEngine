namespace Quartzite
{
    public struct MK2GameObject(int id)
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
}
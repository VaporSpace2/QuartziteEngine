using Quartzite;
using testingScene;


// Testing code here //

Console.WriteLine("Testing time");

gameObject.InitEcsManager();

GameObject gameObjectSpam = gameObject.NewGameObject("asd");
await Task.Delay(1000 - DateTime.Now.Millisecond);
for (int i = 1; i != 30000; i++)
{
    gameObject.AddComponent(gameObjectSpam, new customComponent());
}
int time = DateTime.Now.Millisecond;
for (int i = 1; i != 30000; i++)
{
    gameObject.DeactivateComponent(i);
}
Console.WriteLine(time + ", " + DateTime.Now.Millisecond);

// Testing code here //


namespace testingScene
{
    public class customComponent() : Component
    {
        public override void Init()
        {
            //Console.WriteLine("Hello I am component " + SelfID);
        }

        public override void DeInit()
        {
            //Console.WriteLine("Bye bye I am component " + SelfID);
        }
    }
}
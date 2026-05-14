using Quartzite;
using testingScene;

// Testing code here //

Console.WriteLine("Testing time");

MK2EntityComponentManager.InitEcsManager();

MK2GameObject gameObjectSpam;

await Task.Delay(1000 - DateTime.Now.Millisecond);
int time = DateTime.Now.Millisecond;
for (int i = 1; i != 30000; i++)
{
    //MK2EntityComponentManager.AddComponent(gameObject1, new customComponent());

    gameObjectSpam = MK2EntityComponentManager.NewGameObject("ads");
    //Console.WriteLine(i);
}
Console.WriteLine(time);
Console.WriteLine(DateTime.Now.Millisecond);

// Testing code here //

namespace testingScene
{
    public class customComponent() : Component
    {
        public override void Init()
        {
            Console.WriteLine("Hello I am component " + SelfID);
        }

        public override void DeInit()
        {
            Console.WriteLine("Bye bye I am component " + SelfID);
        }
    }
}
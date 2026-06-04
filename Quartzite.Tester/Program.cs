using Quartzite;
using testingScene;
using Raylib_cs;


// Testing code here //

/*
Console.WriteLine("Testing time");

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
*/

gameObject.InitEcsManager();

GameObject gameObjectSpam = gameObject.NewGameObject("asdasd");
gameObjectSpam.AddComponent(new customComponent());

Game.RunGame(900, 506, "Test!!!!!!", 60, false, Color.Black);

// Testing code here //


namespace testingScene
{
    public class customComponent() : Component
    {
        public int i = 0;

        public override void DeInit()
        {
            Console.WriteLine("goodbye");
        }
        public override void FixedUpdate()
        {
            i++;
            Console.WriteLine(i);

            if (i == 40)
            {
                Destroy();
            }
        }
    }
}
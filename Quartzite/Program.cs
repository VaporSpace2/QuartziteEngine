using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Quartzite
{
    public static class Game
    {
        public static float PhysicsInterval = 0.025f; // 40 phys updates per second
        public static float PhysicsDeltaTime = 0;

        public static int CacheFPS = 0;

        public static Color BackroundColor;
        public static Vector2 CameraPosition = new Vector(0, 0);
        public static Vector2 CameraSize;
        public static Camera2D Camera = new Camera2D(Vector2.Zero, Vector2.Zero, 0, 1); // target is center of rot and zoom, offset is render offset

        public static void RunGame(int screenWidth, int screenHeight, string windowName, int targetFPS, bool fullScreen, Color backroundColor)
        {
            if (!gameObject.Instantiated)
                gameObject.InitEcsManager();

            Console.WriteLine("Booting... " + screenWidth + ", " + screenHeight + " @ " + targetFPS + " FPS");

            BackroundColor = backroundColor;
            CameraSize = new Vector2(screenWidth, screenHeight);

            InitWindow(screenWidth, screenHeight, windowName);
            SetTargetFPS(targetFPS);

            if (fullScreen)
                ToggleFullscreen();

            Awake();

            while (!WindowShouldClose())
            {
                PhysicsDeltaTime += GetFrameTime();

                if (PhysicsDeltaTime >= PhysicsInterval)
                {
                    PhysicsUpdate();
                }

                BeginDrawing();
                ClearBackground(backroundColor);

                //BeginMode2D(Camera);
                Update();
                //EndMode2D();

                gameObject.ClearComponentActiveList();
                gameObject.ClearComponentDestroyList();

                DrawText("FPS: " + CacheFPS, 250, 250, 25, Color.White);
                EndDrawing();
            }

            CloseWindow();
        }

        private static void PhysicsUpdate()
        {
            CacheFPS = GetFPS();
            foreach (int index in gameObject.ActiveComponentIndexes)
            {
                gameObject.Components[index].FixedUpdate();
            }
        }

        private static void Update()
        {
            foreach (int index in gameObject.ActiveComponentIndexes)
            {
                gameObject.Components[index].Update();
            }
        }

        private static void Awake()
        {
            foreach (int index in gameObject.ActiveComponentIndexes)
            {
                gameObject.Components[index].Awake();
            }
        }
    }
}
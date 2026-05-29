namespace Quartzite
{
    public static class gameObject
    {
        private static Queue<int> Indexes = new(); // holds 'unused' or 'empty' available spots in the GameObjects array

        public static void InitEcsManager()
        {
            for (int i = 0; i < 32; i++)
            {
                Indexes.Enqueue(i);
            }
        }

        // GameObject related fields //

        private static int NextGameObjectID = 0;
        private static GameObject[] GameObjects = new GameObject[32];
        private static string[] ObjectNames = new string[32];
        private static Quartzite.Position[] ObjectPositions = new Position[32];
        private static Velocity[] ObjectVelocities = new Velocity[32];
        public static int[] GameObjectIDs = new int[32];
        private static Queue<int> FreeGameObjectIDs = new();

        // Component related fields //

        private static int NextComponentID = 0;
        private static Component[] Components = new Component[128];
        private static int[] ActiveComponentIndexes = new int[128];
        public static int[] ComponentIDs = new int[128];
        public static Queue<int> FreeComponentIDs = new();

        // GameObject related functions //

        public static GameObject NewGameObject(string name)
        {
            int id = GenGameObjectID();

            GameObject gameObject = new(id);
            int index = 0;

            if (Indexes.Count > 0)
            {
                index = Indexes.Dequeue();
            }
            else
            {
                int ResizeSize = GameObjects.Length * 2;
                Array.Resize(ref GameObjects, ResizeSize);
                Array.Resize(ref GameObjectIDs, ResizeSize);
                Array.Resize(ref ObjectNames, ResizeSize);
                Array.Resize(ref ObjectPositions, ResizeSize);
                Array.Resize(ref ObjectVelocities, ResizeSize);

                for (int i = ResizeSize / 2 - 1; i < ResizeSize; i++)
                {
                    Indexes.Enqueue(i);
                }

                index = Indexes.Dequeue();
            }

            GameObjects[index] = gameObject;
            GameObjectIDs[index] = id;
            ObjectNames[index] = name;
            ObjectPositions[index] = new Position(0, 0);
            ObjectVelocities[index] = new Velocity(0, 0);

            return gameObject;
        }

        public static void DestroyGameObject(int gameObjectID)
        {
            int index = Array.IndexOf(GameObjectIDs, gameObjectID);
            if (index == -1)
                return;

            GameObject gameObject = GameObjects[index];

            foreach (int componentID in gameObject.Components)
            {
                DestroyComponent(componentID);
            }

            Array.Clear(ObjectVelocities, index, 1);
            Array.Clear(ObjectPositions, index, 1);
            Array.Clear(ObjectNames, index, 1);
            Array.Clear(GameObjectIDs, index, 1);

            FreeGameObjectIDs.Enqueue(gameObjectID);
        }

        public static GameObject GetGameObject(int gameObjectID)
        {
            return GameObjects[Array.IndexOf(GameObjectIDs, gameObjectID)];
        }

        public static int GenGameObjectID()
        {
            if (FreeGameObjectIDs.Count > 0)
                return FreeGameObjectIDs.Dequeue();

            return NextGameObjectID++;
        }

        // Component related functions //

        public static void AddComponent(GameObject gameObject, Component component)
        {
            int id = GenComponentID();

            component.SelfID = id;
            component.OwnerID = gameObject.SelfID;

            int index = Array.IndexOf(Components, null);

            if (index == -1)
            {
                int ResizeSize = Components.Length * 2;
                Array.Resize(ref Components, ResizeSize);
                Array.Resize(ref ComponentIDs, ResizeSize);
                index = Array.IndexOf(Components, null);
            }

            Components[index] = component;
            ComponentIDs[index] = id;

            index = Array.IndexOf(gameObject.Components, 0); // 0 shall be used to signify an empty/null element

            if (index == -1)
            {
                int ResizeSize = gameObject.Components.Length * 2;
                Array.Resize(ref gameObject.Components, ResizeSize);
                index = Array.IndexOf(gameObject.Components, 0);
            }

            gameObject.Components[index] = id;
        }

        public static void DestroyComponent(int componentID)
        {
            int index = Array.IndexOf(ComponentIDs, componentID);

            if (index == -1)
                return;

            int indexOfComponent = Array.IndexOf(GameObjects[Components[index].OwnerID].Components, componentID);
            if (indexOfComponent != -1)
            {
                GameObjects[index].Components[indexOfComponent] = 0;
            }

            Components[index].DeInit();
            Array.Clear(Components, index, 1);
            ComponentIDs[index] = 0;
            FreeComponentIDs.Enqueue(componentID);
        }

        public static int GenComponentID()
        {
            if (FreeComponentIDs.Count > 0)
                return FreeComponentIDs.Dequeue();

            return NextComponentID++;
        }
    }
}
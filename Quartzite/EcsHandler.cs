using System.Linq; // dangit man

namespace Quartzite
{
    public static class EntityComponentManager
    {
        private static int NextGameObjectID = 0;
        private static int NextComponentID = 0;

        private static Dictionary<int, GameObject> GameObjects = new();
        private static Dictionary<int, Component> Components = new();

        private static Queue<int> FreeGameObjectIDs = new();
        private static Queue<int> FreeComponentIDs = new();

        // GAMEOBJECTS
        public static GameObject NewGameObject(string objectName)
        {
            int id = GenGameObjectID();

            GameObject gameObject = new(objectName) { SelfID = id };

            GameObjects[id] = gameObject;

            return gameObject;
        }

        public static void DestroyGameObject(int EntityID)
        {
            if (!GameObjects.TryGetValue(EntityID, out var gameObject))
                return;

            foreach (int SelfComponentID in gameObject.ComponentIDs)
            {
                DestroyComponent(SelfComponentID);
            }

            GameObjects.Remove(EntityID);
            FreeGameObjectIDs.Enqueue(EntityID);
        }

        public static int GenGameObjectID()
        {
            if (FreeGameObjectIDs.Count > 0)
                return FreeGameObjectIDs.Dequeue();

            return NextGameObjectID++;
        }

        public static void AddComponent(GameObject gameObject, Component component)

        {
            int id = GenComponentID();

            component.SelfID = id;
            component.OwnerID = gameObject.SelfID;

            Components[id] = component;

            while (id > gameObject.ComponentIDs.Count())
                gameObject.ComponentIDs.Add(-1);

            gameObject.ComponentIDs.Insert(id, id);

            component.Init();
        }

        public static void DestroyComponent(int EntityID)
        {
            if (!Components.TryGetValue(EntityID, out var component))
                return;

            if (GameObjects.TryGetValue(component.OwnerID, out var gameObject))
            {
                gameObject.ComponentIDs.Remove(EntityID);
            }
            Components[EntityID].DeInit();
            Components.Remove(EntityID);
            FreeComponentIDs.Enqueue(EntityID);
        }

        public static Component? GetComponent(GameObject gameObject, int componentID)
        {
            int idIndex = gameObject.ComponentIDs.IndexOf(componentID);

            if (idIndex == -1)
                return null;

            return Components[idIndex];
        }

        public static int GenComponentID()
        {
            if (FreeComponentIDs.Count > 0)
                return FreeComponentIDs.Dequeue();

            return NextComponentID++;
        }
    }
}
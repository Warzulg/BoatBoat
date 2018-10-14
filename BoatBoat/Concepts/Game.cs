using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBoat.Concepts.Interface;
using BoatBoat.Concepts.People;
using BoatBoat.Concepts.Building;
using BoatBoat.Concepts.System;

namespace BoatBoat.Concepts {
    public class Game : IGameEntity {
        private Dictionary<string, IGameEntity> Entities;

        public Resources Resources;
        public int CurrentTick;

        public Game() {
            Entities = new Dictionary<string, IGameEntity>();
            Resources = new Resources();
            Building.Building.SetResources(Resources);
            CurrentTick = 0;
        }

        public string AddEntity(IGameEntity entity) {
            string id = GenerateUID();
            Entities[id] = entity;
            return id;
        }

        public List<IGameEntity> GetEntities<IGameEntity>() {
            return Entities.Values.OfType<IGameEntity>().ToList();
        }

        public IGameEntity GetEntity<IGameEntity>() {
            return GetEntities<IGameEntity>().FirstOrDefault();
        }

        public IGameEntity GetEntity(string uid) {
            return GetEntity(int.Parse(uid));
        }
        public IGameEntity GetEntity(int uid) {
            return Entities[UID.FromNumber(uid)];
        }

        private string GetEntityUID(IGameEntity entity) {
            foreach (var kvPair in Entities)
                if (kvPair.Value == entity)
                    return kvPair.Key;
            return null;
        }

        private string GenerateUID() {
            return UID.GetNext();
        }

        public void PrintState() {
            Console.CursorLeft = 0;
            Console.WriteLine(
                $" Resources: \n   Wheat: {Resources.Wheat.ToString()}\n   Lumber: {Resources.Lumber.ToString()}"
                );
            Console.WriteLine(
                $" Workers: \n   {String.Join("\n   ", GetEntities<Worker>().Select((w, i) => $"id: {GetEntityUID(w)} (sta: {w.Stamina.ToString()})"))}\n"
                );
            Console.WriteLine(
                $" Buildings: \n   {String.Join("\n   ", GetEntities<Building.Building>().Select((b, i) => $"id: {GetEntityUID(b)} (wrkrs: {b.Workers.Count.ToString()} | type: {b.GetType().Name})"))}\n"
                );
            Console.WriteLine("============================");
        }

        public void Tick() {
            ++CurrentTick;
            foreach (var entry in Entities) {
                entry.Value.Tick();
            }
        }
    }
}

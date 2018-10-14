using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBoat.Concepts.Interface;
using BoatBoat.Concepts.People;

namespace BoatBoat.Concepts.Building {
    public class ResourceAmount {
        public int Wheat;
        public int Lumber;

        public ResourceAmount(int wheat = 0, int lumber = 0) {
            Wheat = wheat;
            Lumber = lumber;
        }
    }

    public abstract class Building : IBuilding {
        private static Resources Resources;

        public List<Worker> Workers { get; private set; }
        protected ResourceAmount Yield { get; set; }
        public int StaminaStrain { get; private set; }
        public bool IsActive { get; private set; }

        public Building(int capacity, int strain, ResourceAmount cost = null) {
            Workers = new List<Worker>(capacity);
            StaminaStrain = strain;

            if (cost == null)
                cost = new ResourceAmount();
            Resources.SettleCost(cost);

            IsActive = true;
        }

        public static void SetResources(Resources reference) {
            Resources = reference;
        }

        public bool AddWorker(Worker worker) {
            if (Workers.Count < Workers.Capacity) {
                var success = worker.AddToBuilding(this);
                if (success) {
                    Workers.Add(worker);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveWorker() {
            Workers.Sort(new Comparison<Worker>((a, b) => a.Stamina.CompareTo(b.Stamina)));
            Worker weakest = Workers.First();
            return Workers.Remove(weakest);
        }
        public bool RemoveWorker(Worker worker) {
            bool success = Workers.Remove(worker);
            return success;
        }

        public void Evacuate() {
            while (Workers.Count > 0)
                Workers.First().LeaveCurrentBuilding();
        }

        public void Tick() {
            if (IsActive)
                Workers.ForEach(worker => {
                    if (worker.IsAlive)
                        Resources.AddYield(Yield);
                });
        }
    }
}

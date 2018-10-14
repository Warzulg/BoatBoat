using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBoat.Concepts.People;

namespace BoatBoat.Concepts.Interface {
    public interface IBuilding : IGameEntity {
        List<Worker> Workers { get; }
        int StaminaStrain { get; }

        bool AddWorker(Worker worker);
        bool RemoveWorker(Worker worker);
        bool RemoveWorker();
        void Evacuate();
    }
}

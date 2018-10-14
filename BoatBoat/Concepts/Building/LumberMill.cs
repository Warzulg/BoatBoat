using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBoat.Concepts.Interface;
using BoatBoat.Concepts.People;

namespace BoatBoat.Concepts.Building {
    public class LumberMill : Building {
        private const int _WheatCost = 500;
        private const int _LumberCost = 0;

        private const int _LumberYield = 10;

        public LumberMill() : base(3, 15, new ResourceAmount(_WheatCost, _LumberCost)) {
            Yield = new ResourceAmount(0, _LumberYield);
        }

        new public void Tick() {

        }
    }
}

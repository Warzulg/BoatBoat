using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBoat.Concepts.Interface;
using BoatBoat.Concepts.People;

namespace BoatBoat.Concepts.Building {
    public class WheatField : Building {
        private const int _WheatCost = 0;
        private const int _LumberCost = 0;

        private const int _WheatYield = 10;

        public WheatField() : base(4, 10, new ResourceAmount(_WheatCost, _LumberCost)) {
            Yield = new ResourceAmount(_WheatYield, 0);
        }

        new public void Tick() {
            
        }
    }
}

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
    public class Resources {
        private const int _DefaultStartingWheat = 1000;
        private const int _DefaultStartingLumber = 1000;
        private const float _DefaultReturnFactor = .5f;

        public int Wheat { get; private set; }
        public int Lumber { get; private set; }

        public Resources(int startingWheat = _DefaultStartingWheat, int startingLumber = _DefaultStartingLumber) {
            Wheat = startingWheat;
            Lumber = startingLumber;
        }

        public Resources AddWheat(int amount = 1) {
            Wheat += amount;
            return this;
        }

        public Resources AddLumber(int amount = 1) {
            Lumber += amount;
            return this;
        }

        public void SettleCost(ResourceAmount cost, SettleMode mode = SettleMode.Pay) {
            switch (mode) {
                case SettleMode.Pay:
                    Wheat -= cost.Wheat;
                    Lumber -= cost.Lumber;
                    break;
                case SettleMode.Refund:
                    Wheat += (int)(cost.Wheat * _DefaultReturnFactor);
                    Lumber += (int)(cost.Lumber * _DefaultReturnFactor);
                    break;
                default:
                    goto case SettleMode.Pay;
            }
        }

        public void AddYield(ResourceAmount yield) {
            Wheat += yield.Wheat;
            Lumber += yield.Lumber;
        }
    }

    public enum SettleMode {
        Pay, Refund
    }
}

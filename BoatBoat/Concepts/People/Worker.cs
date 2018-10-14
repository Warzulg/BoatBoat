using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBoat.Concepts.Interface;

namespace BoatBoat.Concepts.People {
    public class Worker : IGameEntity {
        private const int _DefaultStaminaRecovery = 2;
        private const int _DefaultStartingStamina = 1000;
        private const int _DefaultMaxStamina = 1200;

        public float Stamina;
        public bool IsAlive;

        private IBuilding CurrentBuilding;

        public Worker() {
            Stamina = _DefaultStartingStamina;
            IsAlive = true;
        }

        public bool AddToBuilding(IBuilding buildng) {
            if (!IsOccupied()) {
                CurrentBuilding = buildng;
                buildng.AddWorker(this);
                return true;
            }
            return false;
        }

        public bool LeaveCurrentBuilding() {
            if (IsOccupied()) {
                var success = CurrentBuilding.RemoveWorker(this);
                if (success) {
                    CurrentBuilding = null;
                    return true;
                }
            }
            return false;
        }

        public bool IsOccupied() {
            return CurrentBuilding != null;
        }

        private void ResolveStamina() {
            if (IsAlive) {
                if (IsOccupied()) {
                    Stamina -= CurrentBuilding.StaminaStrain;
                } else {
                    Stamina += _DefaultStaminaRecovery;
                    if (Stamina > _DefaultMaxStamina)
                        Stamina = _DefaultMaxStamina;
                }
            }
            if (Stamina <= 0)
                IsAlive = false;
        }

        public void Tick() {
            ResolveStamina();
        }
    }
}

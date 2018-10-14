using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using BoatBoat.Concepts.Interface;
using BoatBoat.Concepts.Building;
using BoatBoat.Concepts.People;
using BoatBoat.Concepts.System;
using BoatBoat.Concepts;

namespace BoatBoat {
    class Program {
        static Game game = new Game();
        static TimeSpan tickLength = new TimeSpan(0, 0, 2);

        static void Main(string[] args) {
            DebugCommand.SetGame(game);

            game.AddEntity(new Worker());
            game.AddEntity(new Worker());
            game.AddEntity(new Worker());
            game.AddEntity(new WheatField());
            game.AddEntity(new WheatField());
            game.AddEntity(new WheatField());
            game.AddEntity(new LumberMill());

            game.GetEntity<WheatField>()
                .AddWorker(game.GetEntities<Worker>().Where(worker => !worker.IsOccupied()).First());
            game.GetEntity<WheatField>()
                .AddWorker(game.GetEntities<Worker>().Where(worker => !worker.IsOccupied()).First());
            game.GetEntity<LumberMill>()
                .AddWorker(game.GetEntities<Worker>().Where(worker => !worker.IsOccupied()).First());

            while (true) {
                //Thread.Sleep(tickLength);
                game.PrintState();

                DebugCommand.AwaitCommand();

                game.Tick();
            }
        }
    }
}

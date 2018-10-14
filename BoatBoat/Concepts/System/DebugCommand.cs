using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using BoatBoat.Concepts.Interface;

namespace BoatBoat.Concepts.System {
    public static class DebugCommand {
        private static Game game;

        public static void SetGame(Game game) {
            DebugCommand.game = game;
        }

        public static void AwaitCommand() {
            ParseCommand(Console.ReadLine());
        }

        private static void ParseCommand(string input) {
            var splitInput = input.Split('.').ToList();
            if (splitInput.Count < 2)
                return;
            string entityUID = splitInput.First();
            var splitFunctionCall = splitInput.Last().Split(' ');
            string functionName = splitFunctionCall.First();
            dynamic paramObject = null;
            if (splitFunctionCall.Length > 1) {
                string param = splitFunctionCall.Last();
                paramObject = game.GetEntity(param);
            }

            IGameEntity entity = game.GetEntity(entityUID);
            if (entity == null)
                return;
            var entityType = entity.GetType();

            MethodInfo functionMethod;
            var entityMethods = entityType.GetMethods();
            functionMethod = entityMethods.ToList()
                .Where(method => method.Name.ToLower().IndexOf(functionName.ToLower()) == 0).FirstOrDefault();
            if (functionMethod == null) {
                return;
            }

            if (paramObject != null) {
                functionMethod.Invoke(entity, new[] { paramObject });
            } else {
                functionMethod.Invoke(entity, null);
            }

            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n-> {entity.GetType().Name}.{functionMethod.Name} executed\n");
            Console.ForegroundColor = prevColor;
        }
    }
}

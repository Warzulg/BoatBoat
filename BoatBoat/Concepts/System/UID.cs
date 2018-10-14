using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBoat.Concepts.Interface;

namespace BoatBoat.Concepts.System {
    public static class UID {
        private static int Counter = -1;
        public static string GetNext() {
            Counter++;
            return FromNumber(Counter);
        }
        public static string FromNumber(int num) {
            return $"{num:00000}"; // warum nicht einfach int UIDs? warum gerade 5 nuller auffüllen? keine ahnung. iwann zu richtigem object machen
        }
    }
}

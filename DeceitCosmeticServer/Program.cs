using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebSocketSharp.Server;

namespace DeceitCosmeticServer {
    public class Program {
        private const string SERVER_HOSTNAME = "ws://127.0.0.1:9851";
        public static uint[] UNLOCKS = Array.Empty<uint>();
        private static void ParseUnlocks() {
            while(!File.Exists("unlocks.csv")) {
                Console.WriteLine($"Couldn't find \'unlocks.csv\'. Please put it in the directory of the server.\nPress any key to continue...");
                Console.ReadKey();
            }
            string[] lines = File.ReadAllLines("unlocks.csv").ToArray();
            List<uint> unlocks = new(lines.Length);
            for (int i = 0; i < lines.Length; i++) {
                if (!uint.TryParse(lines[i].Split(',')[0], out uint unlockId)) continue;
                if (!unlocks.Contains(unlockId)) unlocks.Add(unlockId);
            }
            UNLOCKS = unlocks.ToArray();
        }
        static void Main(string[] _) {
            ParseUnlocks();
            WebSocketServer server = new(SERVER_HOSTNAME);
            server.Log.Output = (x, y) => { };
            server.AddWebSocketService<PartyEndpoint>("/party");
            server.Start();
            Console.WriteLine($"Listening on {SERVER_HOSTNAME}");
            while (true) ;
        }
    }
}

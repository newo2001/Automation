using System;

namespace Automation {
    public static class Program {
        [STAThread]
        public static void Main() {
            using var game = new AutomationGame();
            game.Run();
        }
    }
}
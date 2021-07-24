using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uno.Runtime.Domain
{
    public class Turn
    {
        readonly Player[] players;
        int currentPlayerIndex;
        
        #region Properties
        public bool IsForwards { get; private set; } = true;
        
        public Player CurrentPlayer => players[currentPlayerIndex];
        public Player NextPlayer => players[currentPlayerIndex + 1];
        #endregion
        
        #region Constructors
        public Turn(IEnumerable<Player> players)
        {
            this.players = players.ToArray();
            currentPlayerIndex = 0;
        }
        #endregion
        
        public void SwitchDirection()
        {
            IsForwards = !IsForwards;
        }

        public void ToNext()
        {
            currentPlayerIndex++;
        }

        #region Formatting members
        public override string ToString()
        {
            var built = new StringBuilder();

            built.AppendLine("TURN");
            built.Append(FormatDirection());
            
            return built.ToString();
        }

        string FormatDirection()
        {
            return $"Direction: {(IsForwards ? "Forwards" : "Backwards")}";
        }
        #endregion
    }
}
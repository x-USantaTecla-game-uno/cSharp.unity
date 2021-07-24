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
        public Player NextPlayer => players[GetNextIndex()];
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
            currentPlayerIndex = GetNextIndex();
        }

        #region Support methods
        int GetNextIndex()
        {
            var nextIndex = currentPlayerIndex;

            if(IsForwards)
                nextIndex++;
            else
                nextIndex--;
            
            if(nextIndex < 0)
                return players.Length - 1;
            if(nextIndex >= players.Length)
                return nextIndex % players.Length;
            return nextIndex;
        }
        #endregion

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
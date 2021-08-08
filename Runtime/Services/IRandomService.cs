using System.Collections.Generic;
using System.Linq;

namespace Kalendra.Commons.Runtime.Architecture.Services
{
    public interface IRandomService
    {
        int Seed { set; }

        int Next(int min, int exclusiveMax);

        float Next();
        float Next(float min, float max);
        
        bool TossUp();
        bool TossUp(float chanceToWin);
        bool TossUpToBeat(float chanceToLose);
        bool TossUpPercentage(float percentageChangeToWin);

        int RollDie();
        int RollDieOfFaces(int facesAmount);

        T GetRandom<T>(IEnumerable<T> collection);
    }
    
    /// <summary>
    /// Base template method to autocomplete sugar syntax.
    /// </summary>
    public abstract class AbstractRandomService : IRandomService
    {
        public abstract int Seed { set; }
        
        #region Constructors
        protected AbstractRandomService() { }
        protected AbstractRandomService(int seed) => Seed = seed;
        #endregion

        #region Random providing (hooks)
        public abstract int Next(int min, int exclusiveMax);
        public abstract float Next();
        public abstract float Next(float min, float max);
        #endregion

        #region Coin sugar syntax
        public bool TossUp() => TossUp(.5f);
        public bool TossUp(float chanceToWin) => Next() <= chanceToWin;
        public bool TossUpToBeat(float chanceToLose) => TossUp(1 - chanceToLose);
        public bool TossUpPercentage(float percentageChangeToWin) => TossUp(percentageChangeToWin / 100);
        #endregion

        #region Dice sugar syntax
        public int RollDie() => RollDieOfFaces(6);
        public int RollDieOfFaces(int facesAmount) => Next(1, facesAmount + 1);
        #endregion

        public T GetRandom<T>(IEnumerable<T> collection)
        {
            var listedCollection = collection.ToList();
            var randomMemberIndex = Next(0, listedCollection.Count);
            return listedCollection[randomMemberIndex];
        }
    }
}
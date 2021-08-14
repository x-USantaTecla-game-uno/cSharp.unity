using System;

namespace Uno.Runtime.Services
{
    public sealed class SystemRandomService : AbstractRandomService
    {
        Random systemRandom = new Random();

        #region Constructors
        public SystemRandomService() { }
        public SystemRandomService(int seed) : base(seed) { }
        #endregion
        
        #region Accesors
        public override int Seed { set => systemRandom = new Random(value); }
        #endregion

        #region Random providing
        public override int Next(int min, int exclusiveMax) => systemRandom.Next(min, exclusiveMax);

        public override float Next() => (float) systemRandom.NextDouble();

        public override float Next(float min, float max) => (float) (systemRandom.NextDouble() * (max - min) + min);
        #endregion
    }
}
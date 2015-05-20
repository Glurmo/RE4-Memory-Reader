using System;
using System.Linq;
using System.Reflection;

namespace RE4
{
    class ResidentEvilMemory
    {
        public ResidentEvilData CurrentState { get; private set; }
        public ResidentEvilData PreviousState { get; private set; }
        public ResidentEvilData PreviousValues { get; private set; }

        public ResidentEvilMemory()
        {
            Reset();
        }

        public void Reset()
        {
            CurrentState = new ResidentEvilData();
            PreviousState = new ResidentEvilData();
            PreviousValues = new ResidentEvilData();
        }

        public void Populate(MemoryReader memoryReader)
        {
            // Copy the current state to the previous state
            var properties = CurrentState.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanWrite);
            foreach (var propertyInfo in properties)
            {
                propertyInfo.SetValue(PreviousState, propertyInfo.GetValue(CurrentState));
            }

            // Setup the new state
            CurrentState.DynamicDifficultyLevel = memoryReader.ReadByte(0x085BE78);
            CurrentState.DynamicDifficultyScale = memoryReader.ReadInt16(0x085BE74);
            CurrentState.HealthTotal = memoryReader.ReadInt16(0x085BE96);
            CurrentState.HealthRemaining = memoryReader.ReadInt16(0x085BE94);

            CurrentState.ChapterKills = memoryReader.ReadInt16(0x085F344);
            CurrentState.ChapterShots = memoryReader.ReadInt16(0x085F354);
            CurrentState.ChapterShotsOnTarget = memoryReader.ReadInt16(0x085F34C);
            CurrentState.ChapterDeaths = memoryReader.ReadInt16(0x085F340);

            CurrentState.TotalKills = memoryReader.ReadInt16(0x085F348);
            CurrentState.TotalShots = memoryReader.ReadInt16(0x085F358);
            CurrentState.TotalShotsOnTarget = memoryReader.ReadInt16(0x085F350);
            CurrentState.TotalDeaths = memoryReader.ReadInt16(0x085F342);

            CurrentState.LoadingAreaDeaths = memoryReader.ReadInt16(0x085BE80);
            CurrentState.Pesetas = memoryReader.ReadInt32(0x085BE88);

            // Save the changed values
            foreach (var propertyInfo in properties)
            {
                var currentValue = propertyInfo.GetValue(CurrentState);
                var previousValue = propertyInfo.GetValue(PreviousState);
                if (!currentValue.Equals(previousValue))
                {
                    propertyInfo.SetValue(PreviousValues, previousValue);
                }
            }

        }
    }
}

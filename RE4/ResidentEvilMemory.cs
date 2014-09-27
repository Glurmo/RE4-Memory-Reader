using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RE4
{
    class ResidentEvilMemory
    {
        public int DynamicDifficultyLevel { get; set; }
        public int DynamicDifficultyScale { get; set; }
        public int HealthTotal { get; set; }
        public int HealthCurrent { get; set; }

        private int? _healthPrevious;

        public int HealthPercentage
        {
            get
            {
                if (HealthTotal == 0)
                {
                    return 100; 
                }
                return (int)Math.Round((HealthCurrent / (double)HealthTotal) * 100);
            }
        }

        public int ChapterKills { get; set; }
        public int ChapterShots { get; set; }
        public int ChapterShotsOnTarget { get; set; }
        public int ChapterDeaths { get; set; }

        public int ChapterAccuracy
        {
            get
            {
                if (ChapterShots == 0)
                {
                    return 100;
                }
                return (int)Math.Round((ChapterShotsOnTarget / (double)ChapterShots) * 100);
            }
        }

        public int TotalKills { get; set; }
        public int TotalShots { get; set; }
        public int TotalShotsOnTarget { get; set; }
        public int TotalDeaths { get; set; }

        public int TotalAccuracy
        {
            get
            {
                if (TotalShots == 0)
                {
                    return 100;
                }
                return (int)Math.Round((TotalShotsOnTarget / (double)TotalShots) * 100);
            }
        }

        public int Pesetas { get; set; }

        public int LoadingAreaDeaths { get; set; }

        // Calculated values, under some circumstances (e.g. multiple hits in quick succession, these may be inaccurate)
        // Deaths/retries will cause miscalculated values for these too
        public int PreviousHealthDelta { get; set; }

        private int? _difficultyScalePrevious;
        public int PreviousDynamicDifficultyScaleDelta { get; set; }

        public void Populate(MemoryReader memoryReader)
        {
            DynamicDifficultyLevel = memoryReader.ReadByte(0x085BE78);
            DynamicDifficultyScale = memoryReader.ReadInt16(0x085BE74);
            HealthTotal = memoryReader.ReadInt16(0x085BE96);
            HealthCurrent = memoryReader.ReadInt16(0x085BE94);

            ChapterKills = memoryReader.ReadInt16(0x085F344);
            ChapterShots = memoryReader.ReadInt16(0x085F354);
            ChapterShotsOnTarget = memoryReader.ReadInt16(0x085F34C);
            ChapterDeaths = memoryReader.ReadInt16(0x085F340);

            TotalKills = memoryReader.ReadInt16(0x085F348);
            TotalShots = memoryReader.ReadInt16(0x085F358);
            TotalShotsOnTarget = memoryReader.ReadInt16(0x085F350);
            TotalDeaths = memoryReader.ReadInt16(0x085F342);

            LoadingAreaDeaths = memoryReader.ReadInt16(0x085BE80);
            Pesetas = memoryReader.ReadInt32(0x085BE88);

            _calculateHealthDelta();
            _calculateDynamicDifficultyScaleDelta();
        }

        private void _calculateHealthDelta()
        {
            _healthPrevious = _healthPrevious ?? HealthCurrent;
            if (_healthPrevious.Value != HealthCurrent)
            {
                PreviousHealthDelta = -(_healthPrevious.Value - HealthCurrent);
                _healthPrevious = HealthCurrent;
            }
        }

        private void _calculateDynamicDifficultyScaleDelta()
        {
            _difficultyScalePrevious = _difficultyScalePrevious ?? DynamicDifficultyScale;
            if (_difficultyScalePrevious.Value != DynamicDifficultyScale)
            {
                PreviousDynamicDifficultyScaleDelta = -(_difficultyScalePrevious.Value - DynamicDifficultyScale);
                _difficultyScalePrevious = DynamicDifficultyScale;
            }
        }
    }
}

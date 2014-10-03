using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4
{
    public class ResidentEvilData
    {
        public int DynamicDifficultyLevel { get; set; }
        public int DynamicDifficultyScale { get; set; }
        public int HealthTotal { get; set; }
        public int HealthRemaining { get; set; }
        public int HealthPercentage
        {
            get
            {
                if (HealthTotal == 0)
                {
                    return 100;
                }
                return (int)Math.Round((HealthRemaining / (double)HealthTotal) * 100);
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
        public bool IsEndOfChapter { get; set; }
    }
}

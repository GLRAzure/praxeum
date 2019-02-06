using System.Collections.Generic;

namespace Praxeum.Domain
{
    public class ExperiencePointsCalculator : IExperiencePointsCalculator
    {
        public Dictionary<int, int> Levels { get; set; }

        public ExperiencePointsCalculator()
        {
            this.Levels =
                new Dictionary<int, int>();

            this.Levels.Add(1, 0);
            this.Levels.Add(2, 1800);
            this.Levels.Add(3, 4300);
            this.Levels.Add(4, 8000);
            this.Levels.Add(5, 13300);
            this.Levels.Add(6, 21000);
            this.Levels.Add(7, 32300);
            this.Levels.Add(8, 48500);
            this.Levels.Add(9, 72100);
        }

        public int Calculate(
            int level,
            int currentExperiencePoints)
        {
            int experiencePoints;

            if (level >= this.Levels.Count)
            {
                experiencePoints = this.Levels[this.Levels.Count - 1];
            }
            else
            {
                experiencePoints = this.Levels[level - 1];
            }

            experiencePoints += currentExperiencePoints;

            return experiencePoints;
        }
    }
}

﻿using System.Collections.Generic;

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
            this.Levels.Add(10, 106300);
            this.Levels.Add(11, 155900);
            this.Levels.Add(12, 227800);
            this.Levels.Add(13, 332000);
            this.Levels.Add(14, 483200);
            this.Levels.Add(15, 702400);
            this.Levels.Add(16, 1020200);
            this.Levels.Add(17, 1481100);
            this.Levels.Add(18, 2149300);
            this.Levels.Add(19, 3118200);
            this.Levels.Add(20, 4523200);
        }

        public int Calculate(
            int level,
            int currentExperiencePoints)
        {
            int experiencePoints;

            if (level >= this.Levels.Count)
            {
                experiencePoints = this.Levels[this.Levels.Count];
            }
            else
            {
                experiencePoints = this.Levels[level];
            }

            experiencePoints += currentExperiencePoints;

            return experiencePoints;
        }
    }
}

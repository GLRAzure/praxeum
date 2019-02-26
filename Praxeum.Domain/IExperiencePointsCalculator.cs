namespace Praxeum.Domain
{
    public interface IExperiencePointsCalculator
    {
        int Calculate(int level, int currentPoints);
    }
}

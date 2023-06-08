using Giant_Robot_Killer.Robots;

namespace Giant_Robot_Killer
{
    public interface IRobotFactory
    {
        Executioner CreateExecutionerRobot();
        Healer CreateHealerRobot();
        Gunslinger CreateGunslingerRobot();
    }
}
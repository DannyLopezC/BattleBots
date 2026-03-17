using System;

namespace BattleBots.Robot
{
    public class RobotStatsCalculator
    {
        public RobotStatsModel calculate(RobotModel robot)
        {
           RobotStatsModel stats = new RobotStatsModel();

            stats.totalMass = robot.BaseMass;
            stats.totalHP = robot.BaseHP;

            foreach(RobotPartModel part in robot.parts)
            {
                if(part == null || part.definition == null) continue;

                stats.totalMass += part.definition.mass;
                stats.totalHP += part.currentHp;

                switch (part.definition.partType)
                {
                    case PartCategory.WEAPON:
                        stats.weaponPower += part.definition.weaponPower;
                        break;
                    case PartCategory.WHEEL:
                        stats.drivePower += part.definition.drivePower;
                        break;
                    default:
                        break;
                }
            }
            
            return stats;
        }
    }
}

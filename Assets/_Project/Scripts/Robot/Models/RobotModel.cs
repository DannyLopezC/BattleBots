using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

namespace BattleBots.Robot
{
    public class RobotModel
    {
        public int id;
        public List<RobotPartModel> parts;
        public List<SocketModel> sockets;
        public RobotStatsModel stats;
        
        public float BaseMass { get; private set; }
        public float BaseHP { get; private set; }

        public RobotModel(List<SocketModel> sockets, float baseMass, float baseHP)
        {
            this.sockets = sockets;
            parts = new List<RobotPartModel>();
            stats = new RobotStatsModel();

            BaseMass = baseMass;
            BaseHP = baseHP;
        }
        public void SetStats(RobotStatsModel stats)
        {
            this.stats = stats;
        }
    }
}
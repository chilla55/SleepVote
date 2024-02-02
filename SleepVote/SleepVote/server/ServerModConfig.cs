using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SleepVote.Shared;
using Vintagestory.API.Common;

namespace SleepVote.server;

public class ServerModConfig : CommonModConfig
{
    public override string ModCode => "SleepVote";
    public float Sleepprecentage { get; set; } = 0.5f;
    public bool DisableSleeping { get; set; } = false;
}
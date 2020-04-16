using System;

using Server;
using Server.Mobiles;
using Server.Misc;

namespace MidnightWatch
{
  /*
  * "I assume most people will either token up to 70, or manually train there, since 70 is pretty easy to reach"
  * "So I'm looking for 2 months from 70 to 90, I guess?"
  * "So 200 skill increases average in 60 days"
  *
  * hours_in_60_days = 60 * 24
  *                  = 1440
  *
  * interval = 1440 / 200
  *          = 7.2
  */
  public static class PassiveSkillGain
  {
    private static readonly Timer m_Timer;
    private static readonly TimeSpan m_Delay;

    static PassiveSkillGain()
    {
      m_Delay = TimeSpan.FromHours(1);
      
      m_Timer = Timer.DelayCall(m_Delay, m_Delay, Tick);
      m_Timer.Stop();
    }

    public static void Initialize()
    {
      m_Timer.Start();
    }

    private static void Tick()
    {
      foreach (Mobile player in World.Mobiles.Values)
      {
        if (player != null && player is PlayerMobile)
        {
          PlayerMobile pm = (PlayerMobile)player;
          ProcessPlayerSkillGain(pm);
        }
      }
    }

    private static void ProcessPlayerSkillGain(PlayerMobile player)
    {
      Console.WriteLine("PassiveSkillGain: {0}", player.Name);
      Skills skillList = player.Skills;
      for (int i = 0; i < skillList.Length; ++i)
      {
        Skill skill = skillList[i];

        // Skip over locked skills
        if (skill.Lock != SkillLock.Up)
        {
          continue;
        }

        // No passive skill gain above 90
        if (skill.Base >= 90)
        {
          continue;
        }

        if (skill.Base >= 70)
        {
          if (Utility.RandomMinMax(1, 7) == 1)
          {
            SkillCheck.Gain(player, skill);
          }
        }
        else
        {
          SkillCheck.Gain(player, skill);
        }

        double currentValue = skill.Base;
        Console.WriteLine("Currently: {0}", currentValue);
      }
      Console.WriteLine("PassiveSkillGain: finished");
    }
  }
}

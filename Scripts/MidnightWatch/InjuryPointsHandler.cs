using System;

using Server;
using Server.Mobiles;

namespace MidnightWatch
{
  public static class InjuryPointDecayTimer
  {
    private static readonly Timer m_Timer;
    private static readonly TimeSpan m_Delay;

    static InjuryPointDecayTimer()
    {
      m_Delay = TimeSpan.FromHours(4);
      
      m_Timer = Timer.DelayCall(m_Delay, m_Delay, Tick);
      m_Timer.Stop();
    }

    public static void Initialize()
    {
      m_Timer.Start();
    }

    private static void Tick()
    {
      Console.WriteLine("InjuryPoints: healing for all eligible players");
      foreach (Mobile player in World.Mobiles.Values)
      {
        if (player != null && player is PlayerMobile)
        {
          PlayerMobile pm = (PlayerMobile)player;
          pm.HealInjuryPoint();
        }
      }
      Console.WriteLine("InjuryPoints: done healing");
    }
  }

  public class InjuryPointsHandler
  {
    public static void HandleDeath(PlayerMobile victim, Mobile killer)
    {      
      int points = 0;
      if (killer is BaseCreature)
      {
        BaseCreature bc = (BaseCreature)killer;
        Console.WriteLine("{0} has merked {1}", bc.Name, victim.Name);
        points = 3;
      }

      victim.AddInjuryPoints(points);
    }

    public static void InformPlayerTheyCantBeResurrected(PlayerMobile player)
    {
      player.SendMessage("Your injuries have proven fatal.");
    }
  }
}

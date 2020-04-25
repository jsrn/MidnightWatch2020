using Server;
using Server.Mobiles;

namespace MidnightWatch
{
  class ResurrectionBuffs
  {
    public static void Apply(PlayerMobile player)
    {
      if (player.HitsMax < 70)
      {
        player.Hits = player.HitsMax;
      }
      else
      {
        player.Hits = 70;
      }
    }
  }
}
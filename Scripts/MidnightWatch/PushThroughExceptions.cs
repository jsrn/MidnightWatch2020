using Server;
using Server.Mobiles;

namespace MidnightWatch
{
  /* We have push through on, but not everything should block a player.
  * Some animals would be small enough to step over, skirt around, or simply power
  * through regardless of your stamina.
  */
  class PushThroughExceptions
  {
    public static bool IsIncluded(Mobile m)
    {
      return m is Bird || m is Cat || m is TropicalBird ||
        m is Rat || m is Rabbit || m is Squirrel ||
        m is Snake || m is Chicken || m is Sewerrat;
    }
  }
}
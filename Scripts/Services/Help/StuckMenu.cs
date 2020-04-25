using System;
using Server.Gumps;
using Server.Network;
using System.Linq;
using Server.Mobiles;
using Server.Spells;

namespace Server.Menus.Questions
{
    public class StuckMenuEntry
    {
        private readonly string m_Name;
        private readonly Point3D[] m_Locations;
        public StuckMenuEntry(string name, Point3D[] locations)
        {
            m_Name = name;
            m_Locations = locations;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }
        public Point3D[] Locations
        {
            get
            {
                return m_Locations;
            }
        }
    }

    public class StuckMenu : Gump
    {
        private static readonly StuckMenuEntry[] m_Entries = new StuckMenuEntry[]
        {
            new StuckMenuEntry("Altmere", new Point3D[]
            {
                new Point3D(2117, 868, 1),
                new Point3D(2168, 858, 1)
            })
        };

        private static readonly StuckMenuEntry[] m_T2AEntries = new StuckMenuEntry[]
        {
            new StuckMenuEntry("Papua", new Point3D[]
            {
                new Point3D(5720, 3109, -1),
                new Point3D(5677, 3176, -3),
                new Point3D(5678, 3227, 0),
                new Point3D(5769, 3206, -2),
                new Point3D(5777, 3270, -1)
            }),

            new StuckMenuEntry("Delucia", new Point3D[]
            {
                new Point3D(5216, 4033, 37),
                new Point3D(5262, 4049, 37),
                new Point3D(5284, 4006, 37),
                new Point3D(5189, 3971, 39),
                new Point3D(5243, 3960, 37)
            })
        };

        private static readonly StuckMenuEntry[] m_TerMurEntries = new StuckMenuEntry[]
        {
            new StuckMenuEntry("Royal City", new Point3D[]
            {
                new Point3D(750, 3440, -20),
                new Point3D(709, 3444, -20),
                new Point3D(802, 3431, -10),
                new Point3D(848, 3450, -19),
                new Point3D(738, 3486, -19)
            }),

            new StuckMenuEntry("Holy City", new Point3D[]
            {
                new Point3D(997, 3869, -42),
                new Point3D(961, 3921, -42),
                new Point3D(996, 3962, -42)
            })
        };

        private readonly Mobile m_Mobile;
        private readonly Mobile m_Sender;
        private readonly bool m_MarkUse;
        private Timer m_Timer;

        public StuckMenu(Mobile beholder, Mobile beheld, bool markUse)
            : base(150, 50)
        {
            m_Sender = beholder;
            m_Mobile = beheld;
            m_MarkUse = markUse;

            Closable = false;
            Dragable = false;
            Disposable = false;

            AddBackground(0, 0, 270, 320, 2600);

            AddHtmlLocalized(50, 20, 250, 35, 1011027, false, false); // Chose a town:

            StuckMenuEntry[] entries = IsTerMur(beheld) ? m_TerMurEntries : IsInSecondAgeArea(beheld) ? m_T2AEntries : m_Entries;

            for (int i = 0; i < entries.Length; i++)
            {
                StuckMenuEntry entry = entries[i];

                AddButton(50, 55 + 35 * i, 208, 209, i + 1, GumpButtonType.Reply, 0);
                AddHtml(75, 55 + 35 * i, 335, 40, entry.Name, false, false);
            }

            AddButton(55, 263, 4005, 4007, 0, GumpButtonType.Reply, 0);
            AddHtmlLocalized(90, 265, 200, 35, 1011012, false, false); // CANCEL
        }

        public void BeginClose()
        {
            StopClose();

            m_Timer = new CloseTimer(m_Mobile);
            m_Timer.Start();

            m_Mobile.Frozen = true;
        }

        public void StopClose()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            m_Mobile.Frozen = false;
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            StopClose();

            if (info.ButtonID == 0)
            {
                if (m_Mobile == m_Sender)
                    m_Mobile.SendLocalizedMessage(1010588); // You choose not to go to any city.
            }
            else
            {
                int index = info.ButtonID - 1;
                StuckMenuEntry[] entries = IsTerMur(m_Mobile) ? m_TerMurEntries : IsInSecondAgeArea(m_Mobile) ? m_T2AEntries : m_Entries;

                if (index >= 0 && index < entries.Length)
                    Teleport(entries[index]);
            }
        }

        private static bool IsInSecondAgeArea(Mobile m)
        {
            if (m.Map != Map.Trammel && m.Map != Map.Felucca)
                return false;

            if (m.X >= 5120 && m.Y >= 2304)
                return true;

            if (m.Region.IsPartOf("Terathan Keep"))
                return true;

            return false;
        }

        private static bool IsTerMur(Mobile m)
        {
            return m.Map == Map.TerMur && !SpellHelper.IsEodon(m.Map, m.Location);
        }

        private void Teleport(StuckMenuEntry entry)
        {
            if (m_MarkUse)
            {
                m_Mobile.SendLocalizedMessage(1010589); // You will be teleported within the next two minutes.

                new TeleportTimer(m_Mobile, entry, TimeSpan.FromSeconds(10.0 + (Utility.RandomDouble() * 110.0))).Start();

                m_Mobile.UsedStuckMenu();
            }
            else
            {
                new TeleportTimer(m_Mobile, entry, TimeSpan.Zero).Start();
            }
        }

        private class CloseTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly DateTime m_End;
            public CloseTimer(Mobile m)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(1.0))
            {
                m_Mobile = m;
                m_End = DateTime.UtcNow + TimeSpan.FromMinutes(3.0);
            }

            protected override void OnTick()
            {
                if (m_Mobile.NetState == null || DateTime.UtcNow > m_End)
                {
                    m_Mobile.Frozen = false;
                    m_Mobile.CloseGump(typeof(StuckMenu));

                    Stop();
                }
                else
                {
                    m_Mobile.Frozen = true;
                }
            }
        }

        private class TeleportTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly StuckMenuEntry m_Destination;
            private readonly DateTime m_End;
            public TeleportTimer(Mobile mobile, StuckMenuEntry destination, TimeSpan delay)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(1.0))
            {
                Priority = TimerPriority.TwoFiftyMS;

                m_Mobile = mobile;
                m_Destination = destination;
                m_End = DateTime.UtcNow + delay;
            }

            private void MovePetsOfLoggedCharacter(Point3D dest, Map destMap)
            {
                Map fromMap = m_Mobile.LogoutMap;
                Point3D fromLoc = m_Mobile.LogoutLocation;

                var move = fromMap.GetMobilesInRange(fromLoc, 3).Where(m => m is BaseCreature).Cast<BaseCreature>()
                    .Where(pet => pet.Controlled && pet.ControlMaster == m_Mobile && pet.ControlOrder == OrderType.Guard || pet.ControlOrder == OrderType.Follow || pet.ControlOrder == OrderType.Come).ToList();

                move.ForEach(x => x.MoveToWorld(dest, destMap));
            }

            protected override void OnTick()
            {
                if (DateTime.UtcNow < m_End)
                {
                    m_Mobile.Frozen = true;
                }
                else
                {
                    m_Mobile.Frozen = false;
                    Stop();

                    int idx = Utility.Random(m_Destination.Locations.Length);
                    Point3D dest = m_Destination.Locations[idx];

                    Map destMap;
                    if (m_Mobile.Map == Map.Trammel || SpellHelper.IsEodon(m_Mobile.Map, m_Mobile.Location))
                        destMap = Map.Trammel;
                    else if (m_Mobile.Map == Map.Felucca)
                        destMap = Map.Felucca;
                    else if (m_Mobile.Map == Map.TerMur && !SpellHelper.IsEodon(m_Mobile.Map, m_Mobile.Location))
                        destMap = Map.TerMur;
                    else if (m_Mobile.Map == Map.Internal)
                        destMap = m_Mobile.LogoutMap == Map.Felucca ? Map.Felucca : Map.Trammel;
                    else
                        destMap = m_Mobile.Murderer ? Map.Felucca : Map.Trammel;

                    if (destMap == Map.Trammel && Siege.SiegeShard)
                        destMap = Map.Felucca;

                    if (m_Mobile.Map != Map.Internal)
                    {
                        Mobiles.BaseCreature.TeleportPets(m_Mobile, dest, destMap);
                        m_Mobile.MoveToWorld(dest, destMap);
                    }
                    else
                    {
                        // for shards without auto stabling
                        MovePetsOfLoggedCharacter(dest, destMap);

                        m_Mobile.LogoutLocation = dest;
                        m_Mobile.LogoutMap = destMap;
                    }
                }
            }
        }
    }
}

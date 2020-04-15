using System;
using System.Collections.Generic;
using System.Linq;

using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Necromancy
{
    public class WitherSpell : NecromancerSpell
    {
        public override DamageType SpellDamageType { get { return DamageType.SpellAOE; } }

        private static readonly SpellInfo m_Info = new SpellInfo(
            "Wither", "Kal Vas An Flam",
            203,
            9031,
            Reagent.NoxCrystal,
            Reagent.GraveDust,
            Reagent.PigIron);
        public WitherSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(1.5);
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 60.0;
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 23;
            }
        }
        public override bool DelayedDamage
        {
            get
            {
                return false;
            }
        }
        public override void OnCast()
        {
            if (this.CheckSequence())
            {
                /* Creates a withering frost around the Caster,
                * which deals Cold Damage to all valid targets in a radius of 5 tiles.
                */
                Map map = this.Caster.Map;

                if (map != null)
                {
                    Effects.PlaySound(this.Caster.Location, map, 0x1FB);
                    Effects.PlaySound(this.Caster.Location, map, 0x10B);
                    Effects.SendLocationParticles(EffectItem.Create(this.Caster.Location, map, EffectItem.DefaultDuration), 0x37CC, 1, 40, 97, 3, 9917, 0);

                    foreach (var id in AcquireIndirectTargets(Caster.Location, 4))
                    {
                        Mobile m = id as Mobile;

                        this.Caster.DoHarmful(id);

                        if (m != null)
                        {
                            m.FixedParticles(0x374A, 1, 15, 9502, 97, 3, (EffectLayer)255);
                        }
                        else
                        {
                            Effects.SendLocationParticles(id, 0x374A, 1, 30, 97, 3, 9502, 0);
                        }

                        double damage = Utility.RandomMinMax(30, 35);
                        int karma = m != null ? m.Karma / 100 : 0;

                        damage *= 300 + karma + (this.GetDamageSkill(this.Caster) * 10);
                        damage /= 1000;

                        int sdiBonus = SpellHelper.GetSpellDamageBonus(Caster, m, CastSkill, m is PlayerMobile);

                        damage *= (100 + sdiBonus);
                        damage /= 100;

                        SpellHelper.Damage(this, id, damage, 0, 0, 100, 0, 0);
                    }
                }
            }

            this.FinishSequence();
        }
    }
}

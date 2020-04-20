using System;
using Server.Items;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.PirateArcher")]
    public class PirateArcher : BaseCreature
    {
        private DateTime m_NextBomb;
        private int m_Thrown;
        [Constructable]
        public PirateArcher()
            : base(AIType.AI_Archer, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "the pirate bombardier";
            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
                AddItem(new Skirt(Utility.RandomNeutralHue()));
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(new ShortPants(Utility.RandomNeutralHue()));
            }

            SetStr(80, 90);
            SetDex(90, 100);
            SetInt(61, 75);

            SetSkill(SkillName.Fencing, 75.0);
            SetSkill(SkillName.Macing, 75.0);
            SetSkill(SkillName.Swords, 75.0);
            SetSkill(SkillName.Archery, 85.0);
            SetSkill(SkillName.Tactics, 80.0);
            SetSkill(SkillName.Anatomy, 80.0);
            SetSkill(SkillName.Wrestling, 50.0);
            SetSkill(SkillName.Parry, 75.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new ThighBoots(Utility.RandomNeutralHue()));
            AddItem(new Shirt(Utility.RandomNeutralHue()));
            AddItem(new SkullCap(Utility.RandomNeutralHue()));
			AddItem( new Crossbow() );
			PackItem( new Bolt( Utility.RandomMinMax( 50, 70 ) ) );

            Utility.AssignRandomHair(this);
        }

        public PirateArcher(Serial serial)
            : base(serial)
        {
        }

        public override bool ClickTitle
        {
            get
            {
                return false;
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }

        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        public override void OnActionCombat()
        {
            Mobile combatant = this.Combatant as Mobile;

            if (combatant == null || combatant.Deleted || combatant.Map != this.Map || !this.InRange(combatant, 12) || !this.CanBeHarmful(combatant) || !this.InLOS(combatant))
                return;

            if (DateTime.UtcNow >= this.m_NextBomb)
            {
                this.ThrowBomb(combatant);

                this.m_Thrown++;

                if (0.75 >= Utility.RandomDouble() && (this.m_Thrown % 2) == 1) // 75% chance to quickly throw another bomb
                    this.m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(3.0);
                else
                    this.m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(5.0 + (10.0 * Utility.RandomDouble())); // 5-15 seconds
            }
        }

        public void ThrowBomb(Mobile m)
        {
            this.DoHarmful(m);

            this.MovingParticles(m, 0x1C19, 1, 0, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0);

            new InternalTimer(m, this).Start();
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly Mobile m_From;
            public InternalTimer(Mobile m, Mobile from)
                : base(TimeSpan.FromSeconds(1.0))
            {
                this.m_Mobile = m;
                this.m_From = from;
                this.Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                this.m_Mobile.PlaySound(0x11D);
                AOS.Damage(this.m_Mobile, this.m_From, Utility.RandomMinMax(10, 20), 0, 100, 0, 0, 0);
            }
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.75)
                c.DropItem(new SeveredHumanEars());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
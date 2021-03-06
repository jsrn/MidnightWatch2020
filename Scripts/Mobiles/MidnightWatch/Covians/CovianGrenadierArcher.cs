using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a covian grenadier corpse" )]
    public class CovianGrenadierArcher : BaseCreature
    {
        private DateTime m_NextBomb;
        private int m_Thrown;
        [Constructable]
        public CovianGrenadierArcher()
            : base(AIType.AI_Archer, FightMode.Strongest, 10, 1, 0.2, 0.4)
        {
            Name = "A Covian Grenadier";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 150;
            Team = 200;

            SetStr(120);
            SetDex(90, 100);
            SetInt(61, 75);

            SetResistance(ResistanceType.Physical, 70);
            SetResistance(ResistanceType.Fire, 70);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 70);

            SetSkill(SkillName.Macing, 120.0);
            SetSkill(SkillName.MagicResist, 50.0);
            SetSkill(SkillName.Swords, 120.0);
            SetSkill(SkillName.Archery, 120.0);
            SetSkill(SkillName.Tactics, 120.0);
            SetSkill(SkillName.Anatomy, 120.0);
            SetSkill(SkillName.Parry, 100.0);
            SetSkill(SkillName.Tinkering, 900.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots() {Hue = 2012});
            AddItem(new BodySash() {Hue = 648});
            AddItem(new Kilt() {Hue = 648});
            AddItem(new PlateHelm() {Hue = 2406});
            AddItem(new PlateGorget() {Hue = 2406});
            AddItem(new ChainChest() {Hue = 2406});
            AddItem(new PlateArms() {Hue = 2418});
            AddItem(new PlateLegs() {Hue = 2406});
            AddItem(new RingmailGloves() {Hue = 2418});
            AddItem(new LeatherNinjaBelt());

            PackItem(new GreaterExplosionPotion(2));
            PackItem(new Bolt(10));

            switch ( Utility.Random(2))
            {
                case 0:
                    AddItem(new Crossbow());
                    break;
                case 1:
                    AddItem(new HeavyCrossbow());
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public CovianGrenadierArcher(Serial serial)
            : base(serial)
        {
        }

		public override bool BardImmune { get { return true; } }

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

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.75)
                c.DropItem(new SeveredHumanEars());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
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
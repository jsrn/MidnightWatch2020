using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a covian corpse" )]
    public class CovianWatchman : BaseCreature
    {
        [Constructable]
        public CovianWatchman()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "A Covian Watchman";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 75;
            Team = 200;

            SetStr(85, 90);
            SetDex(80, 85);
            SetInt(60, 75);

            SetResistance(ResistanceType.Physical, 55);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Macing, 80.0);
            SetSkill(SkillName.MagicResist, 30.0);
            SetSkill(SkillName.Swords, 70.0);
            SetSkill(SkillName.Tactics, 80.0);
            SetSkill(SkillName.Anatomy, 80.0);
            SetSkill(SkillName.Parry, 70.0);
            SetSkill(SkillName.Tinkering, 900.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots() {Hue = 2012});
            AddItem(new BodySash() {Hue = 648});
            AddItem(new Kilt() {Hue = 648});
            AddItem(new CloseHelm() {Hue = 2406});
            AddItem(new PlateGorget() {Hue = 2406});
            AddItem(new ChainChest() {Hue = 2406});
            AddItem(new RingmailArms() {Hue = 2406});
            AddItem(new ChainLegs() {Hue = 2406});
            AddItem(new RingmailGloves() {Hue = 2406});

            switch ( Utility.Random(5))
            {
                case 0:
                    AddItem(new Longsword());
                    AddItem(new MetalShield() {Hue = 2406});
                    break;
                case 1:
                    AddItem(new Axe());
                    break;
                case 2:
                    AddItem(new VikingSword());
                    AddItem(new MetalShield() {Hue = 2406});
                    break;
                case 3:
                    AddItem(new Mace());
                    AddItem(new MetalShield() {Hue = 2406});
                    break;
                case 4:
                    AddItem(new WarAxe());
                    AddItem(new MetalShield() {Hue = 2406});
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public CovianWatchman(Serial serial)
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
using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a vesperian corpse" )]
    public class VesperFirebrand : BaseCreature
    {
        [Constructable]
        public VesperFirebrand()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "A Vesperian Firebrand";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 75;
            Team = 400;

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

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots() {Hue = 1644});
            AddItem(new BodySash() {Hue = 1644});
            AddItem(new Kilt() {Hue = 1644});
            AddItem(new StrawHat() {Hue = 1644});
            AddItem(new LeatherGorget() {Hue = 2418});
            AddItem(new LeatherChest() {Hue =2418});
            AddItem(new PlateArms() {Hue = 2418});
            AddItem(new LeatherLegs() {Hue = 2418});
            AddItem(new PlateGloves() {Hue = 2418});

            switch ( Utility.Random(5))
            {
                case 0:
                    AddItem(new Longsword());
                    AddItem(new ChaosShield());
                    break;
                case 1:
                    AddItem(new BattleAxe());
                    break;
                case 2:
                    AddItem(new VikingSword());
                    AddItem(new ChaosShield());
                    break;
                case 3:
                    AddItem(new Maul());
                    AddItem(new ChaosShield());
                    break;
                case 4:
                    AddItem(new Mace());
                    AddItem(new ChaosShield());
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public VesperFirebrand(Serial serial)
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
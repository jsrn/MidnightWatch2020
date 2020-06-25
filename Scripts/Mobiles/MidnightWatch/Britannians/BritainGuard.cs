using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a royalist corpse" )]
    public class BritainGuard : BaseCreature
    {
        [Constructable]
        public BritainGuard()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "A Britannian Guard";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 100;
            Team = 300;

            SetStr(100);
            SetDex(90, 95);
            SetInt(61, 75);

            SetResistance(ResistanceType.Physical, 60);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Macing, 90.0);
            SetSkill(SkillName.MagicResist, 50.0);
            SetSkill(SkillName.Swords, 90.0);
            SetSkill(SkillName.Tactics, 90.0);
            SetSkill(SkillName.Anatomy, 90.0);
            SetSkill(SkillName.Parry, 80.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots() {Hue = 2012});
            AddItem(new BodySash() {Hue = 1308});
            AddItem(new Kilt() {Hue = 1308});
            AddItem(new ChainCoif() {Hue = 0});
            AddItem(new PlateGorget() {Hue = 0});
            AddItem(new ChainChest() {Hue = 0});
            AddItem(new PlateArms() {Hue = 0});
            AddItem(new PlateLegs() {Hue = 0});
            AddItem(new RingmailGloves() {Hue = 0});

            switch ( Utility.Random(5))
            {
                case 0:
                    AddItem(new Longsword());
                    AddItem(new MetalShield() {Hue = 0});
                    break;
                case 1:
                    AddItem(new Axe());
                    break;
                case 2:
                    AddItem(new VikingSword());
                    AddItem(new MetalShield() {Hue = 0});
                    break;
                case 3:
                    AddItem(new Mace());
                    AddItem(new MetalShield() {Hue = 0});
                    break;
                case 4:
                    AddItem(new WarAxe());
                    AddItem(new MetalShield() {Hue = 0});
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public BritainGuard(Serial serial)
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
            AddLoot(LootPack.Average);
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
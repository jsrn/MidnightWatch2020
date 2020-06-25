using System;
using Server.Items;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.ArkwrightThug")]
    public class ArkwrightThug : BaseCreature
    {
        [Constructable]
        public ArkwrightThug()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "An Arkwright Thug";
            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
            }
            else
            {
                Body = 0x190;
            }

            SetStr(80, 90);
            SetDex(90, 100);
            SetInt(61, 75);

            SetResistance(ResistanceType.Physical, 50);
            SetResistance(ResistanceType.Fire, 40);
            SetResistance(ResistanceType.Cold, 40);
            SetResistance(ResistanceType.Poison, 40);
            SetResistance(ResistanceType.Energy, 40);

            SetSkill(SkillName.Fencing, 80.0);
            SetSkill(SkillName.Macing, 80.0);
            SetSkill(SkillName.Swords, 80.0);
            SetSkill(SkillName.Archery, 80.0);
            SetSkill(SkillName.Tactics, 80.0);
            SetSkill(SkillName.Anatomy, 80.0);
            SetSkill(SkillName.Wrestling, 50.0);
            SetSkill(SkillName.Parry, 80.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots() {Hue = 2012});
            AddItem(new BodySash() {Hue = 2004});
            AddItem(new Bandana() {Hue = 2004});
            AddItem(new LeatherGorget() {Hue = 2019});
            AddItem(new LeatherChest() {Hue = 2019});
            AddItem(new LeatherArms() {Hue = 2019});
            AddItem(new LeatherLegs() {Hue = 2019});
            AddItem(new LeatherGloves() {Hue = 2019});

            switch ( Utility.Random(6))
            {
                case 0:
                    AddItem(new Kryss());
                    break;
                case 1:
                    AddItem(new Cutlass());
                    break;
                case 2:
                    AddItem(new Scimitar());
                    break;
                case 3:
                    AddItem(new Hatchet());
                    break;
                case 4:
                    AddItem(new Club());
                    break;
                case 5:
                    AddItem(new Spear());
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public ArkwrightThug(Serial serial)
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
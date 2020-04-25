using System;
using Server.Items;

namespace Server.Mobiles
{
    public class BrigandArcher : BaseCreature
    { //base(AIType.AI_Type, FightMode.Mode, ??, ??, Speed, Speed)
        [Constructable]
        public BrigandArcher()
            : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "the brigand archer";
            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
                AddItem(new ShortPants(Utility.RandomNeutralHue()));
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(new ShortPants(Utility.RandomNeutralHue()));
            }

            SetStr(86, 95);
            SetDex(81, 95);
            SetInt(61, 75);

            SetSkill(SkillName.Archery, 75.0);
            SetSkill(SkillName.Anatomy, 75.0);
            SetSkill(SkillName.Tactics, 75.0);
            SetSkill(SkillName.Wrestling, 15.0, 37.5);
            SetSkill(SkillName.Tinkering, 500.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots(Utility.RandomNeutralHue()));
            AddItem(new FancyShirt(Utility.RandomNeutralHue()));
            AddItem(new Bandana(Utility.RandomNeutralHue()));
            AddItem(new Bow());
            PackItem(new Arrow(10));
            PackItem(new Hides(5));

            Utility.AssignRandomHair(this);
        }

        public BrigandArcher(Serial serial)
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
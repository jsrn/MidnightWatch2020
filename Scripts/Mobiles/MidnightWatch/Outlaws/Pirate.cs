using System;
using Server.Items;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.Pirate")]
    public class Pirate : BaseCreature
    {
        [Constructable]
        public Pirate()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "the pirate";
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

            SetDamage(15, 25);

            SetSkill(SkillName.Fencing, 75.0);
            SetSkill(SkillName.Macing, 75.0);
            SetSkill(SkillName.Swords, 75.0);
            SetSkill(SkillName.Archery, 75.0);
            SetSkill(SkillName.Tactics, 75.0);
            SetSkill(SkillName.Anatomy, 75.0);
            SetSkill(SkillName.Wrestling, 50.0);
            SetSkill(SkillName.Parry, 75.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new ThighBoots(Utility.RandomNeutralHue()));
            AddItem(new Shirt(Utility.RandomNeutralHue()));
            AddItem(new SkullCap(Utility.RandomNeutralHue()));

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

        public Pirate(Serial serial)
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
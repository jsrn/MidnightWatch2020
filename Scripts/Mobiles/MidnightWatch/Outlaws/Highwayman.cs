using System;
using Server.Items;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.Highwayman")]
    public class Highwayman : BaseCreature
    { //base(AIType.AI_Type, FightMode.Mode, ??, ??, Speed, Speed)
        [Constructable]
        public Highwayman()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "the highwayman";
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

            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Hits = 200;

            SetStr(100);
            SetDex(110);
            SetInt(75);

            SetSkill(SkillName.Fencing, 100.0);
            SetSkill(SkillName.Macing, 100.0);
            SetSkill(SkillName.Swords, 100.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Anatomy, 100.0);
            SetSkill(SkillName.Wrestling, 15.0, 37.5);
            SetSkill(SkillName.Parry, 100.0);

            Fame = 2000;
            Karma = -2000;

            AddItem(new Boots(Utility.RandomNeutralHue()));
            AddItem(new FancyShirt(Utility.RandomNeutralHue()));
            AddItem(new Doublet(Utility.RandomNeutralHue()));
            AddItem(new FeatheredHat(Utility.RandomNeutralHue()));

            switch ( Utility.Random(7))
            {
                case 0:
                    AddItem(new Longsword());
                    break;
                case 1:
                    AddItem(new Cutlass());
                    break;
                case 2:
                    AddItem(new Broadsword());
                    break;
                case 3:
                    AddItem(new Kryss());
                    break;
                case 4:
                    AddItem(new Club());
                    break;
                case 5:
                    AddItem(new Mace());
                    break;
                case 6:
                    AddItem(new Hatchet());
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public Highwayman(Serial serial)
            : base(serial)
        {
        }

		public override bool BardImmune { get { return true; } }

        public override Poison HitPoison
        {
            get
            {
                return Poison.Regular;
            }
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
            AddLoot(LootPack.Average);
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
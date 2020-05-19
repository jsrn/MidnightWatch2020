using System;
using Server.Items;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.PirateSneak")]
    public class PirateSneak : BaseCreature
    {
        [Constructable]
        public PirateSneak()
            : base(AIType.AI_Ninja, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "the cutthroat";
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

            SetSkill(SkillName.Fencing, 90.0);
            SetSkill(SkillName.Macing, 90.0);
            SetSkill(SkillName.Swords, 90.0);
            SetSkill(SkillName.Archery, 90.0);
            SetSkill(SkillName.Tactics, 90.0);
            SetSkill(SkillName.Anatomy, 90.0);
            SetSkill(SkillName.Wrestling, 50.0);
            SetSkill(SkillName.Parry, 90.0);
            SetSkill(SkillName.Hiding, 90.0);
            SetSkill(SkillName.Stealth, 90.0);

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
                    AddItem(new SkinningKnife());
                    break;
                case 2:
                    AddItem(new Cutlass());
                    break;
                case 3:
                    AddItem(new AssassinSpike());
                    break;
                case 4:
                    AddItem(new ButcherKnife());
                    break;
                case 5:
                    AddItem(new Dagger());
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public PirateSneak(Serial serial)
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
using System;
using Server.Items;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.DreadCaptain")]
    public class DreadCaptain : BaseCreature
    {
        [Constructable]
        public DreadCaptain()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "the pirate captain";
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

            SetStr(100, 120);
            SetDex(100, 120);
            SetInt(61, 100);

            SetDamage(15, 25);

            SetResistance(ResistanceType.Physical, 55);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Fencing, 100.0);
            SetSkill(SkillName.Macing, 100.0);
            SetSkill(SkillName.Swords, 100.0);
            SetSkill(SkillName.Archery, 100.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Anatomy, 100.0);
            SetSkill(SkillName.Wrestling, 50.0);
            SetSkill(SkillName.Parry, 100.0);
            SetSkill(SkillName.Tinkering, 900.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new ThighBoots(Utility.RandomNeutralHue()));
            AddItem(new FancyShirt(Utility.RandomNeutralHue()));
            AddItem(new Doublet(Utility.RandomNeutralHue()));
            AddItem(new TricorneHat(Utility.RandomNeutralHue()));

            switch ( Utility.Random(4))
            {
                case 0:
                    AddItem(new Kryss());
                    AddItem(new WoodenShield());
                    break;
                case 1:
                    AddItem(new Cutlass());
                    AddItem(new WoodenShield());
                    break;
                case 2:
                    AddItem(new Scimitar());
                    AddItem(new WoodenShield());
                    break;
                case 3:
                    AddItem(new Hatchet());
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public DreadCaptain(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility GetWeaponAbility()
        {
            return Utility.RandomBool() ? WeaponAbility.ArmorIgnore : WeaponAbility.ParalyzingBlow;
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

            if (Utility.RandomDouble() < 0.100)
                c.DropItem(new SeveredHumanEars());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
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
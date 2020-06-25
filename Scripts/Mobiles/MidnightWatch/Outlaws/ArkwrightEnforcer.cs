using System;
using Server.Items;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.ArkwrightEnforcer")]
    public class ArkwrightEnforcer : BaseCreature
    {
        [Constructable]
        public ArkwrightEnforcer()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "An Arkwright Enforcer";
            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
            }
            else
            {
                Body = 0x190;
            }

            SetStr(100, 120);
            SetDex(100, 120);
            SetInt(100);

            SetDamage(15, 25);

            SetResistance(ResistanceType.Physical, 50);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Fencing, 110.0);
            SetSkill(SkillName.Macing, 110.0);
            SetSkill(SkillName.Swords, 110.0);
            SetSkill(SkillName.Tactics, 110.0);
            SetSkill(SkillName.Anatomy, 110.0);
            SetSkill(SkillName.Wrestling, 50.0);
            SetSkill(SkillName.Parry, 100.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots() {Hue = 2012});
            AddItem(new BodySash() {Hue = 2004});
            AddItem(new Cap() {Hue = 2004});
            AddItem(new LeatherGorget() {Hue = 2019});
            AddItem(new LeatherChest() {Hue = 2019});
            AddItem(new LeatherArms() {Hue = 2019});
            AddItem(new LeatherLegs() {Hue = 2019});
            AddItem(new LeatherGloves() {Hue = 2019});

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

        public ArkwrightEnforcer(Serial serial)
            : base(serial)
        {
        }

		public override bool BardImmune { get { return true; } }

        public override WeaponAbility GetWeaponAbility()
        {
            return Utility.RandomBool() ? WeaponAbility.ArmorIgnore : WeaponAbility.Disarm;
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
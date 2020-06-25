using System;
using Server.Items;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.ArkwrightSneak")]
    public class ArkwrightSneak : BaseCreature
    {
        [Constructable]
        public ArkwrightSneak()
            : base(AIType.AI_Ninja, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "An Arkwright Cutthroat";
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

            SetResistance(ResistanceType.Physical, 40);
            SetResistance(ResistanceType.Fire, 30);
            SetResistance(ResistanceType.Cold, 30);
            SetResistance(ResistanceType.Poison, 30);
            SetResistance(ResistanceType.Energy, 30);

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

        public ArkwrightSneak(Serial serial)
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

        public override WeaponAbility GetWeaponAbility()
        {
            return Utility.RandomBool() ? WeaponAbility.Disarm : WeaponAbility.BleedAttack;
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
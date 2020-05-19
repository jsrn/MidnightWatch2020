using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a templar corpse" )]
    public class ChurchTemplar : BaseCreature
    {
        private DateTime m_NextBomb;
        private int m_Thrown;
        [Constructable]
        public ChurchTemplar()
            : base(AIType.AI_Paladin, FightMode.Strongest, 10, 1, 0.2, 0.4)
        {
            Name = "A Templar";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 250;
            Team = 300;

            SetStr(200);
            SetDex(100);
            SetInt(75);

            SetResistance(ResistanceType.Physical, 70);
            SetResistance(ResistanceType.Fire, 60);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 60);

            SetSkill(SkillName.Macing, 120.0);
            SetSkill(SkillName.MagicResist, 100.0);
            SetSkill(SkillName.Swords, 120.0);
            SetSkill(SkillName.Tactics, 120.0);
            SetSkill(SkillName.Anatomy, 120.0);
            SetSkill(SkillName.Parry, 100.0);
            SetSkill(SkillName.Tinkering, 900.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Surcoat() {Hue = 0});
            AddItem(new PlateHelm() {Hue = 0});
            AddItem(new PlateGorget() {Hue = 0});
            AddItem(new ChainChest() {Hue = 0});
            AddItem(new PlateArms() {Hue = 0});
            AddItem(new PlateLegs() {Hue = 0});
            AddItem(new PlateGloves() {Hue = 0});
            AddItem(new Cloak() {Hue = 1175});

            switch ( Utility.Random(5))
            {
                case 0:
                    AddItem(new Longsword());
                    AddItem(new WoodenKiteShield() {Hue = 0});
                    break;
                case 1:
                    AddItem(new Broadsword());
                    AddItem(new WoodenKiteShield() {Hue = 0});
                    break;
                case 2:
                    AddItem(new VikingSword());
                    AddItem(new WoodenKiteShield() {Hue = 0});
                    break;
                case 3:
                    AddItem(new Mace());
                    AddItem(new WoodenKiteShield() {Hue = 0});
                    break;
                case 4:
                    AddItem(new WarAxe());
                    AddItem(new WoodenKiteShield() {Hue = 0});
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public ChurchTemplar(Serial serial)
            : base(serial)
        {
        }

		public override bool BardImmune { get { return true; } }

        public override WeaponAbility GetWeaponAbility()
        {
            return Utility.RandomBool() ? WeaponAbility.CrushingBlow : WeaponAbility.BleedAttack;
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
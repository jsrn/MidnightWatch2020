using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a blood fox corpse")]
    public class BloodFox : BaseCreature
    {
        [Constructable]
        public BloodFox() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Blood Fox";
            Body = 0x58f;
            Female = true;

            this.SetStr(60, 80);
            this.SetDex(70, 90);
            this.SetInt(11, 25);

            this.SetHits(34, 48);
            this.SetMana(0);

            this.SetDamage(5, 9);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 15, 20);
            this.SetResistance(ResistanceType.Fire, 5, 10);
            this.SetResistance(ResistanceType.Cold, 10, 15);
            this.SetResistance(ResistanceType.Poison, 5, 10);
            this.SetResistance(ResistanceType.Energy, 5, 10);

            this.SetSkill(SkillName.MagicResist, 27.6, 45.0);
            this.SetSkill(SkillName.Tactics, 30.1, 50.0);
            this.SetSkill(SkillName.Wrestling, 40.1, 60.0);

            this.Fame = 450;
            this.Karma = 0;

            this.Tamable = true;
            this.ControlSlots = 1;
            this.MinTameSkill = 23.1;
        }

        public BloodFox(Serial serial) : base(serial)
        {
        }

        public override int Meat { get { return 5; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override bool CanAngerOnTame { get { return true; } }
        public override bool StatLossAfterTame { get { return true; } }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (version == 0)
            {
                SetSpecialAbility(SpecialAbility.GraspingClaw);
                SetWeaponAbility(WeaponAbility.BleedAttack);
            }
        }
    }
}
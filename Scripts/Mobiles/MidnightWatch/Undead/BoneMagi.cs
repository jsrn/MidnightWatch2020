using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a skeletal corpse")]
    public class BoneMagi : BaseCreature
    {
        [Constructable]
        public BoneMagi()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a bone mage";
            Body = 148;
            BaseSoundID = 451;

            SetStr(76, 100);
            SetDex(56, 75);
            SetInt(186, 210);

            SetHits(120);

            SetDamage(5, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 50);
            SetResistance(ResistanceType.Fire, 20, 20);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 20, 70);
            SetResistance(ResistanceType.Energy, 30, 50);

            SetSkill(SkillName.EvalInt, 80.0);
            SetSkill(SkillName.Magery, 90.0);
            SetSkill(SkillName.MagicResist, 80.0);
            SetSkill(SkillName.Tactics, 80.0);
            SetSkill(SkillName.Wrestling, 80.0);
            SetSkill(SkillName.Necromancy, 120.0);
            SetSkill(SkillName.SpiritSpeak, 90.0);

            Fame = 3000;
            Karma = -3000;

            PackReg(3);
            PackNecroReg(3, 10);
            PackItem(new Bone());
        }

        public BoneMagi(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmune { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Regular; } }
        public override TribeType Tribe { get { return TribeType.Undead; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.Potions);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}

using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a ghostly corpse")]
    public class Spectre : BaseCreature
    {
        [Constructable]
        public Spectre()
            : base(AIType.AI_Ninja, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            this.Name = "a spectre";
            this.Body = 26;
            this.Hue = 0x4001;
            this.BaseSoundID = 0x482;

            this.SetStr(100);
            this.SetDex(95);
            this.SetInt(60);

            this.SetHits(100);

            this.SetDamage(10, 20);

            this.SetDamageType(ResistanceType.Physical, 50);
            this.SetDamageType(ResistanceType.Cold, 50);

            this.SetResistance(ResistanceType.Physical, 25, 30);
            this.SetResistance(ResistanceType.Cold, 15, 25);
            this.SetResistance(ResistanceType.Poison, 10, 20);

            this.SetSkill(SkillName.EvalInt, 80.0);
            this.SetSkill(SkillName.Magery, 80.0);
            this.SetSkill(SkillName.MagicResist, 50.0);
            this.SetSkill(SkillName.Tactics, 80.0);
            this.SetSkill(SkillName.Wrestling, 80.0);
            this.SetSkill(SkillName.Anatomy, 80.0);
            this.SetSkill(SkillName.Hiding, 100.0);
            this.SetSkill(SkillName.Stealth, 100.0);
            this.SetSkill(SkillName.Ninjitsu, 70.0);
            this.SetSkill(SkillName.Tinkering, 900.0);

            this.Fame = 4000;
            this.Karma = -4000;

            this.PackReg(10);
        }

        public Spectre(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmune
        {
            get
            {
                return true;
            }
        }

        public override Poison HitPoison
        {
            get
            {
                return Poison.Lesser;
            }
        }

        public override TribeType Tribe { get { return TribeType.Undead; } }

        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lethal;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Average);
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

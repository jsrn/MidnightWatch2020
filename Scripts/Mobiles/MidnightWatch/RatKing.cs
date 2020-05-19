using System;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a royal ratman corpse")]
    public class RatmanKing : BaseCreature
    {
        [Constructable]
        public RatmanKing()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = NameList.RandomName("ratman");
            this.Title = "the Rat King";
            this.Body = 0x8F;
            this.BaseSoundID = 437;

            this.SetStr(146, 180);
            this.SetDex(101, 180);
            this.SetInt(200);

            this.SetHits(500);

            this.SetDamage(10, 30);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 70);
            this.SetResistance(ResistanceType.Fire, 20);
            this.SetResistance(ResistanceType.Cold, 50);
            this.SetResistance(ResistanceType.Poison, 80);
            this.SetResistance(ResistanceType.Energy, 50);

            this.SetSkill(SkillName.EvalInt, 100.0);
            this.SetSkill(SkillName.Magery, 100.0);
            this.SetSkill(SkillName.MagicResist, 100.0);
            this.SetSkill(SkillName.Tactics, 100.0);
            this.SetSkill(SkillName.Wrestling, 100.0);
            this.SetSkill(SkillName.Focus, 100.0);
            this.SetSkill(SkillName.Meditation, 100.0);
            this.SetSkill(SkillName.Tinkering, 900.0);

            this.Fame = 7500;
            this.Karma = -7500;

            this.PackReg(6);

            if (0.02 > Utility.RandomDouble())
                this.PackStatue();

			switch (Utility.Random(9))
            {
                case 0: PackItem(new AnimateDeadScroll()); break;
                case 1: PackItem(new BloodOathScroll()); break;
                case 2: PackItem(new CorpseSkinScroll()); break;
                case 3: PackItem(new CurseWeaponScroll()); break;
				case 4: PackItem(new EvilOmenScroll()); break;
				case 5: PackItem(new HorrificBeastScroll()); break;
				case 6: PackItem(new MindRotScroll()); break;
				case 7: PackItem(new PainSpikeScroll()); break;
				case 8: PackItem(new WraithFormScroll()); break;
			}
        }

        public RatmanKing(Serial serial)
            : base(serial)
        {
        }

		public override bool BardImmune { get { return true; } }

        public override InhumanSpeech SpeechType
        {
            get
            {
                return InhumanSpeech.Ratman;
            }
        }
        public override bool CanRummageCorpses
        {
            get
            {
                return true;
            }
        }
		public override int TreasureMapLevel
        {
            get
            {
                return 2;
            }
        }
        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override int Hides
        {
            get
            {
                return 20;
            }
        }
        public override HideType HideType
        {
            get
            {
                return HideType.Spined;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Rich);
            this.AddLoot(LootPack.HighScrolls);
            this.AddLoot(LootPack.MedScrolls);
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
using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles 
{
    public class BlackwellArcher : BaseHire 
    {
        [Constructable] 
        public BlackwellArcher()
            : base(AIType.AI_Archer)
        {
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool()) 
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else 
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
            }

            Name = "An Altmerian Archer";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 75;
            Team = 300;

            SetStr(85, 90);
            SetDex(80, 85);
            SetInt(60, 75);

            SetResistance(ResistanceType.Physical, 50);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Archery, 70.0);
            SetSkill(SkillName.MagicResist, 30.0);
            SetSkill(SkillName.Tactics, 80.0);
            SetSkill(SkillName.Anatomy, 80.0);
            SetSkill(SkillName.DetectHidden, 50.0);

            Fame = 1000;
            Karma = 1000;

            AddItem(new Boots() {Hue = 2012});
            AddItem(new BodySash() {Hue = 1308});
            AddItem(new Kilt() {Hue = 1308});
            AddItem(new Bandana() {Hue = 1308});
            AddItem(new PlateGorget() {Hue = 0});
            AddItem(new ChainChest() {Hue = 0});
            AddItem(new ChainLegs() {Hue = 0});
            AddItem(new RingmailGloves() {Hue = 0});
            AddItem(new Bow());
            PackItem(new Arrow(10));

            Utility.AssignRandomHair(this);
            PackGold(25, 100);
        }

        public BlackwellArcher(Serial serial)
            : base(serial)
        {
        }

        public override bool BardImmune { get { return true; } }

        public override bool ClickTitle
        {
            get
            {
                return false;
            }
        }
        public override void Serialize(GenericWriter writer) 
        {
            base.Serialize(writer);

            writer.Write((int)0);// version 
        }

        public override void Deserialize(GenericReader reader) 
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
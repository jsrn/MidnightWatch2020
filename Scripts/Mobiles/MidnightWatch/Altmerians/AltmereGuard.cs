using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles 
{
    public class AltmereGuard : BaseHire 
    {
        [Constructable] 
        public AltmereGuard()
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

            Name = "An Altmerian Guard";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 100;
            Team = 100;            

            SetStr(100);
            SetDex(90, 95);
            SetInt(61, 75);

            SetResistance(ResistanceType.Physical, 60);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Macing, 90.0);
            SetSkill(SkillName.MagicResist, 50.0);
            SetSkill(SkillName.Swords, 90.0);
            SetSkill(SkillName.Fencing, 90.0);
            SetSkill(SkillName.Tactics, 90.0);
            SetSkill(SkillName.Anatomy, 90.0);
            SetSkill(SkillName.Parry, 80.0);

            Fame = 1000;
            Karma = 1000;

            AddItem(new Boots() {Hue = 2012});
            AddItem(new BodySash() {Hue = 1445});
            AddItem(new Kilt() {Hue = 1445});
            AddItem(new Bandana() {Hue = 1445});
            AddItem(new PlateGorget() {Hue = 0});
            AddItem(new ChainChest() {Hue = 0});
            AddItem(new ChainLegs() {Hue = 0});
            AddItem(new RingmailGloves() {Hue = 0});

            switch ( Utility.Random(5))
            {
                case 0:
                    AddItem(new Longsword());
                    AddItem(new WoodenShield());
                    break;
                case 1:
                    AddItem(new Pitchfork());
                    break;
                case 2:
                    AddItem(new VikingSword());
                    AddItem(new BronzeShield());
                    break;
                case 3:
                    AddItem(new Spear());
                    break;
                case 4:
                    AddItem(new Mace());
                    AddItem(new MetalShield());
                    break;
            }
        }
        public AltmereGuard(Serial serial)
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
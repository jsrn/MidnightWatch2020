using System;
using Server.Items;

namespace Server.Mobiles
{
    public class Traveller : BaseCreature
    {
        [Constructable]
        public Traveller()
            : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4)
        {
            InitStats(31, 41, 51);

            SpeechHue = Utility.RandomDyedHue();

			SetSkill(SkillName.Begging, 64.0, 100.0);
            SetSkill(SkillName.Cooking, 65, 88);
            SetSkill(SkillName.Snooping, 65, 88);
            SetSkill(SkillName.Stealing, 65, 88);

            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
                AddItem(new Kilt(Utility.RandomDyedHue()));
                AddItem(new Shirt(Utility.RandomDyedHue()));
                AddItem(new ThighBoots());
                Title = "the traveller";
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(new ShortPants(Utility.RandomNeutralHue()));
                AddItem(new Shirt(Utility.RandomDyedHue()));
                AddItem(new Sandals());
                Title = "the traveller";
            }

            AddItem(new Bandana(Utility.RandomDyedHue()));
            AddItem(new Dagger());

            Utility.AssignRandomHair(this);

            Container pack = new Backpack();

            pack.DropItem(new Gold(250, 300));

            pack.Movable = false;

            this.AddItem(pack);
        }

        public Traveller(Serial serial)
            : base(serial)
        {
        }

        public override bool CanTeach
        {
            get
            {
                return true;
            }
        }
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

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a yewish corpse" )]
    public class YewishArcher : BaseCreature
    {
        [Constructable]
        public YewishArcher()
            : base(AIType.AI_Archer, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            Name = "A Yewish Marksman";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 150;
            Team = 300;

            SetStr(100);
            SetDex(95);
            SetInt(75);

            SetResistance(ResistanceType.Physical, 60);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Archery, 100.0);
            SetSkill(SkillName.MagicResist, 60.0);
            SetSkill(SkillName.Tactics, 90.0);
            SetSkill(SkillName.Anatomy, 90.0);
            SetSkill(SkillName.DetectHidden, 80.0);
            SetSkill(SkillName.Tinkering, 900.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots());
            AddItem(new BodySash() {Hue = 48});
            AddItem(new Kilt() {Hue = 902});
            AddItem(new Cloak() {Hue = 902});
            AddItem(new FeatheredHat() {Hue = 902});
            AddItem(new PlateGorget());
            AddItem(new ChainChest());
            AddItem(new PlateArms() {Hue = 2213});
            AddItem(new ChainLegs());
            AddItem(new RingmailGloves());
            AddItem(new LeatherNinjaBelt());
            AddItem(new CompositeBow());
            PackItem(new Arrow(10));


            Utility.AssignRandomHair(this);
        }

        public YewishArcher(Serial serial)
            : base(serial)
        {
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
using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a yewish officer corpse" )]
    public class YewishOfficer : BaseCreature
    {
        [Constructable]
        public YewishOfficer()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "A Yewish Officer";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 100;
            Team = 300;

            SetStr(110);
            SetDex(110);
            SetInt(61, 75);

            SetResistance(ResistanceType.Physical, 70);
            SetResistance(ResistanceType.Fire, 65);
            SetResistance(ResistanceType.Cold, 65);
            SetResistance(ResistanceType.Poison, 65);
            SetResistance(ResistanceType.Energy, 65);

            SetSkill(SkillName.Macing, 120.0);
            SetSkill(SkillName.Swords, 120.0);
            SetSkill(SkillName.Tactics, 120.0);
            SetSkill(SkillName.Wrestling, 100.0);
            SetSkill(SkillName.Anatomy, 120.0);
            SetSkill(SkillName.Parry, 120.0);
            SetSkill(SkillName.MagicResist, 70.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots());
            AddItem(new BodySash() {Hue = 48});
            AddItem(new Kilt() {Hue = 902});
            AddItem(new Cap(902));
            AddItem(new PlateGorget());
            AddItem(new ChainChest());
            AddItem(new PlateArms());
            AddItem(new PlateLegs());
            AddItem(new RingmailGloves());
            AddItem(new LeatherNinjaBelt());

            switch ( Utility.Random(5))
            {
                case 0:
                    AddItem(new Longsword());
                    AddItem(new OrderShield());
                    break;
                case 1:
                    AddItem(new Axe());
                    break;
                case 2:
                    AddItem(new VikingSword());
                    AddItem(new OrderShield());
                    break;
                case 3:
                    AddItem(new Mace());
                    AddItem(new OrderShield());
                    break;
                case 4:
                    AddItem(new WarAxe());
                    AddItem(new OrderShield());
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public YewishOfficer(Serial serial)
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
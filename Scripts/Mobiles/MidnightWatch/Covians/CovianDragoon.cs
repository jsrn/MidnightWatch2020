using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a covian corpse" )]
    public class CovianDragoon : BaseCreature
    {
        [Constructable]
        public CovianDragoon()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.175, 0.350)
        {
            Name = "A Covian Dragoon";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 100;
            Team = 200;

            SetStr(110);
            SetDex(95);
            SetInt(75);

            SetResistance(ResistanceType.Physical, 60);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Macing, 90.0);
            SetSkill(SkillName.MagicResist, 50.0);
            SetSkill(SkillName.Swords, 90.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Anatomy, 100.0);
            SetSkill(SkillName.Parry, 80.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots() {Hue = 2012});
            AddItem(new BodySash() {Hue = 648});
            AddItem(new Kilt() {Hue = 648});
            AddItem(new PlateHelm() {Hue = 2406});
            AddItem(new PlateGorget() {Hue = 2406});
            AddItem(new ChainChest() {Hue = 2406});
            AddItem(new PlateArms() {Hue = 0});
            AddItem(new PlateLegs() {Hue = 2406});
            AddItem(new PlateGloves() {Hue = 0});
            AddItem(new LeatherNinjaBelt());

            switch ( Utility.Random(5))
            {
                case 0:
                    AddItem(new Longsword());
                    AddItem(new ChaosShield() {Hue = 2406});
                    break;
                case 1:
                    AddItem(new Maul());
                    AddItem(new ChaosShield() {Hue = 2406});
                    break;
                case 2:
                    AddItem(new VikingSword());
                    AddItem(new ChaosShield() {Hue = 2406});
                    break;
                case 3:
                    AddItem(new Mace());
                    AddItem(new ChaosShield() {Hue = 2406});
                    break;
                case 4:
                    AddItem(new WarAxe());
                    AddItem(new ChaosShield() {Hue = 2406});
                    break;
            }

            new DragoonHorse().Rider = this;

            Utility.AssignRandomHair(this);
        }

        public CovianDragoon(Serial serial)
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

        public override bool OnBeforeDeath()
        {
            IMount mount = this.Mount;

            if (mount != null)
                mount.Rider = null;

            if (mount is Mobile)
                ((Mobile)mount).Delete();

            return base.OnBeforeDeath();
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
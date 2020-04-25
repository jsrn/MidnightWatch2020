using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a waywatcher corpse" )]
    public class YewishWaywatcher : BaseCreature
    {
        [Constructable]
        public YewishWaywatcher()
            : base(AIType.AI_Ninja, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            Name = "A Yewish Waywatcher";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 100;
            Team = 300;

            SetStr(85, 90);
            SetDex(100, 120);
            SetInt(61, 75);

            SetResistance(ResistanceType.Physical, 60);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Fencing, 120.0);
            SetSkill(SkillName.Swords, 120.0);
            SetSkill(SkillName.Tactics, 90.0);
            SetSkill(SkillName.Anatomy, 90.0);
            SetSkill(SkillName.Archery, 90.0);
            SetSkill(SkillName.Hiding, 90.0);
            SetSkill(SkillName.Stealth, 90.0);
            SetSkill(SkillName.Parry, 50.0);
            SetSkill(SkillName.MagicResist, 50.0);
            SetSkill(SkillName.Tinkering, 900.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots() {Hue = 2212});
            AddItem(new BodySash() {Hue = 48});
            AddItem(new Kilt() {Hue = 902});
            AddItem(new Bandana() {Hue = 902});
            AddItem(new LeatherGorget() {Hue = 2212});
            AddItem(new LeatherChest() {Hue = 2212});
            AddItem(new LeatherArms() {Hue = 2212});
            AddItem(new LeatherLegs() {Hue = 2212});
            AddItem(new LeatherGloves() {Hue = 2212});
            AddItem(new LeatherNinjaBelt());

            PackItem(new GreaterPoisonPotion());
            PackItem(new GreaterCurePotion());

            switch ( Utility.Random(3))
            {
                case 0:
                    AddItem(new Kryss());
                    break;
                case 1:
                    AddItem(new AssassinSpike());
                    break;
                case 2:
                    AddItem(new Spear());
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public YewishWaywatcher(Serial serial)
            : base(serial)
        {
        }

        public override Poison HitPoison
        {
            get
            {
                return Poison.Regular;
            }
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
            switch ( Utility.Random(5))
            {
                case 0:
                    PackItem(new DisguiseKit());
                    break;
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
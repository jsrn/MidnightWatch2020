using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a vesperian corpse" )]
    public class VesperBlackguard : BaseCreature
    {
        [Constructable]
        public VesperBlackguard()
            : base(AIType.AI_Ninja, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            Name = "A Vesperian Blackguard";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 100;
            Team = 400;

            SetStr(85, 90);
            SetDex(100, 120);
            SetInt(61, 75);

            SetResistance(ResistanceType.Physical, 60);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Fencing, 100.0);
            SetSkill(SkillName.Swords, 100.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Anatomy, 100.0);
            SetSkill(SkillName.Archery, 90.0);
            SetSkill(SkillName.Hiding, 90.0);
            SetSkill(SkillName.Stealth, 90.0);
            SetSkill(SkillName.MagicResist, 50.0);
            SetSkill(SkillName.Ninjitsu, 70.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots() {Hue = 1175});
            AddItem(new BodySash() {Hue = 1644});
            AddItem(new Kilt() {Hue = 1175});
            AddItem(new Cap() {Hue = 1175});
            AddItem(new LeatherGorget() {Hue = 2406});
            AddItem(new LeatherChest() {Hue = 2406});
            AddItem(new LeatherArms() {Hue = 2406});
            AddItem(new LeatherLegs() {Hue = 2406});
            AddItem(new LeatherGloves() {Hue = 2406});

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
                    AddItem(new WarFork());
                    break;
            }
            

            Utility.AssignRandomHair(this);
        }

        public VesperBlackguard(Serial serial)
            : base(serial)
        {
        }

		public override bool BardImmune { get { return true; } }

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
            switch ( Utility.Random(10))
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
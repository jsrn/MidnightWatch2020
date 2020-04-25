using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a covian corpse" )]
    public class CovianArcanist : BaseCreature
    {
        [Constructable]
        public CovianArcanist()
            : base(AIType.AI_Archer, FightMode.Strongest, 10, 1, 0.2, 0.4)
        {
            Name = "A Covian Arcanist";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Body = 400;
            Hits = 120;
            Team = 200;

            SetStr(85);
            SetDex(100);
            SetInt(120);
            
            SetDamage(10, 20);

            SetResistance(ResistanceType.Physical, 60);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Magery, 100.0);
            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Meditation, 100.0);
            SetSkill(SkillName.Inscribe, 100.0);
            SetSkill(SkillName.Focus, 100.0);
            SetSkill(SkillName.MagicResist, 100.0);
            SetSkill(SkillName.Meditation, 100.0);
            SetSkill(SkillName.Focus, 100.0);
            SetSkill(SkillName.Tinkering, 500.0);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots() {Hue = 2012});
            AddItem(new BodySash() {Hue = 648});
            AddItem(new Kilt() {Hue = 648});
            AddItem(new Cap() {Hue = 648});
            AddItem(new LeatherGorget() {Hue = 2406});
            AddItem(new LeatherChest() {Hue = 2406});
            AddItem(new LeatherArms() {Hue = 2430});
            AddItem(new LeatherLegs() {Hue = 2406});
            AddItem(new LeatherGloves() {Hue = 2430});
            AddItem(new LeatherNinjaBelt());

            AddItem(new Spellbook());
            PackItem(new BlackPearl(20));
            PackItem(new Garlic(20));
            PackItem(new Ginseng(20));
            PackItem(new Bloodmoss(20));
            PackItem(new MandrakeRoot(20));
            PackItem(new SpidersSilk(20));
            PackItem(new SulfurousAsh(20));

            Utility.AssignRandomHair(this);
        }

        public CovianArcanist(Serial serial)
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
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.HighScrolls);
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
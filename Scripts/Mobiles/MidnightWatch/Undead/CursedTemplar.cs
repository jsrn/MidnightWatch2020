using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "a shadow corpse" )]
    public class CursedTemplar : BaseCreature
    {
        [Constructable]
        public CursedTemplar()
            : base(AIType.AI_Berserk, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "A Cursed Templar";
            SpeechHue = Utility.RandomDyedHue();
            Hue = 0x4001;
            HairHue = 0x4001;
            Body = 400;
            Hits = 250;

            SetStr(200);
            SetDex(100);
            SetInt(75);

            SetDamageType(ResistanceType.Energy, 100);

            SetResistance(ResistanceType.Physical, 70);
            SetResistance(ResistanceType.Fire, 40);
            SetResistance(ResistanceType.Cold, 70);
            SetResistance(ResistanceType.Poison, 70);
            SetResistance(ResistanceType.Energy, 40);

            SetSkill(SkillName.Macing, 120.0);
            SetSkill(SkillName.MagicResist, 120.0);
            SetSkill(SkillName.Swords, 120.0);
            SetSkill(SkillName.Tactics, 120.0);
            SetSkill(SkillName.Anatomy, 120.0);
            SetSkill(SkillName.Parry, 100.0);
            SetSkill(SkillName.Tinkering, 900.0);

            Fame = 1000;
            Karma = -1000;

            BaseWeapon melee = null;

            switch (Utility.Random(3))
            {
                case 0:
                    melee = new Mace();
                    break;
                case 1:
                    melee = new Broadsword();
                    break;
                case 2:
                    melee = new VikingSword();
                    break;
            }

            melee.Movable = false;
            AddItem(melee);
            melee.Hue = 0x4001;

            PlateHelm helm = new PlateHelm();
            helm.Movable = false;
            helm.Hue = 0x4001;
            AddItem(helm);

            ChainChest chest = new ChainChest();
            chest.Movable = false;
            chest.Hue = 0x4001;
            AddItem(chest);

            PlateArms arms = new PlateArms();
            arms.Movable = false;
            arms.Hue = 0x4001;
            AddItem(arms);

            PlateGloves gloves = new PlateGloves();
            gloves.Movable = false;
            gloves.Hue = 0x4001;
            AddItem(gloves);

            PlateLegs legs = new PlateLegs();
            legs.Movable = false;
            legs.Hue = 0x4001;
            AddItem(legs);

            MetalKiteShield shield = new MetalKiteShield();
            shield.Movable = false;
            shield.Hue = 0x4001;
            AddItem(shield);

            Surcoat middletorso = new Surcoat();
            middletorso.Movable = false;
            middletorso.Hue = 0x4001;
            AddItem(middletorso);

            switch ( Utility.Random(4))
            {
                case 0:
                    AddItem(new Longsword() {Hue = 16385});
                    AddItem(new MetalKiteShield() {Hue = 16385});
                    break;
                case 1:
                    AddItem(new VikingSword() {Hue = 16385});
                    AddItem(new MetalKiteShield() {Hue = 16385});
                    break;
                case 2:
                    AddItem(new Mace() {Hue = 16385});
                    AddItem(new MetalKiteShield() {Hue = 16385});
                    break;
                case 3:
                    AddItem(new WarAxe() {Hue = 16385});
                    AddItem(new MetalKiteShield() {Hue = 16385});
                    break;
            }
        }

        public CursedTemplar(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmune { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override WeaponAbility GetWeaponAbility()
        {
            return Utility.RandomBool() ? WeaponAbility.CrushingBlow : WeaponAbility.BleedAttack;
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
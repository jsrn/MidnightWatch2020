using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a sentinel's corpse")]
    public class ExodusSentinel : BaseCreature
    {
        private bool m_FieldActive;

        [Constructable]
        public ExodusSentinel() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "Exodus Sentinel";
            this.Body = 0x2FB;
            this.Hue = 0xA92;
            this.SetStr(854, 934);
            this.SetDex(81, 90);
            this.SetInt(81, 105);

            this.SetHits(516, 564);

            this.SetDamage(16, 22);

            this.SetDamageType(ResistanceType.Physical, 60);
            this.SetDamageType(ResistanceType.Energy, 40);

            this.SetResistance(ResistanceType.Physical, 60, 70);
            this.SetResistance(ResistanceType.Fire, 40, 50);
            this.SetResistance(ResistanceType.Cold, 25, 35);
            this.SetResistance(ResistanceType.Poison, 25, 35);
            this.SetResistance(ResistanceType.Energy, 25, 35);

            this.SetSkill(SkillName.MagicResist, 91.5, 99.6);
            this.SetSkill(SkillName.Tactics, 91.9, 99.4);
            this.SetSkill(SkillName.Wrestling, 90.1, 98.9);

            this.Fame = 18000;
            this.Karma = -18000;
            this.VirtualArmor = 65;

            this.PackItem(new PowerCrystal());
            this.PackItem(new ArcaneGem());
            this.PackItem(new ClockworkAssembly());

            this.m_FieldActive = this.CanUseField;
        }

        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Rich);
        }

        public override void OnKilledBy(Mobile m)
        {
            base.OnKilledBy(m);

            if (Utility.RandomDouble() < 0.1)
            {
                ExodusChest.GiveRituelItem(m);
            }            
        }

        public ExodusSentinel(Serial serial) : base(serial)
        {
        }

        public bool FieldActive { get { return this.m_FieldActive; } }
        public bool CanUseField { get { return this.Hits >= this.HitsMax * 9 / 10; } }// TODO: an OSI bug prevents to verify this
        public override bool IsScaredOfScaryThings { get { return false; } }
        public override bool IsScaryToPets { get { return true; } }
        public override bool BardImmune { get { return false; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        
        public override int GetIdleSound() { return 0x218; }
        public override int GetAngerSound() { return 0x26C; }
        public override int GetDeathSound() { return 0x211; }
        public override int GetAttackSound() { return 0x232; }
        public override int GetHurtSound() { return 0x140; }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (this.m_FieldActive)
                damage = 0; // no melee damage when the field is up
        }

        public override void AlterSpellDamageFrom(Mobile from, ref int damage)
        {
            if (!this.m_FieldActive)
                damage = 0; // no spell damage when the field is down
        }

        public override void OnDamagedBySpell(Mobile from)
        {
            if (from != null && from.Alive && 0.4 > Utility.RandomDouble())
            {
                this.SendEBolt(from);
            }

            if (!this.m_FieldActive)
            {
                // should there be an effect when spells nullifying is on?
                this.FixedParticles(0, 10, 0, 0x2522, EffectLayer.Waist);
            }
            else if (this.m_FieldActive && !this.CanUseField)
            {
                this.m_FieldActive = false;

                // TODO: message and effect when field turns down; cannot be verified on OSI due to a bug
                this.FixedParticles(0x3735, 1, 30, 0x251F, EffectLayer.Waist);
            }
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (this.m_FieldActive)
            {
                this.FixedParticles(0x376A, 20, 10, 0x2530, EffectLayer.Waist);

                this.PlaySound(0x2F4);

                attacker.SendAsciiMessage("Your weapon cannot penetrate the creature's magical barrier");
            }

            if (attacker != null && attacker.Alive && attacker.Weapon is BaseRanged && 0.4 > Utility.RandomDouble())
            {
                this.SendEBolt(attacker);
            }
        }

        public override void OnThink()
        {
            base.OnThink();

            // TODO: an OSI bug prevents to verify if the field can regenerate or not
            if (!this.m_FieldActive && !this.IsHurt())
                this.m_FieldActive = true;
        }

        public override bool Move(Direction d)
        {
            bool move = base.Move(d);

            if (move && this.m_FieldActive && this.Combatant != null)
                this.FixedParticles(0, 10, 0, 0x2530, EffectLayer.Waist);

            return move;
        }

        public void SendEBolt(Mobile to)
        {
            this.MovingParticles(to, 0x379F, 7, 0, false, true, 0xBE3, 0xFCB, 0x211);
            to.PlaySound(0x229);
            this.DoHarmful(to);
            AOS.Damage(to, this, 50, 0, 0, 0, 0, 100);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            this.m_FieldActive = this.CanUseField;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.RareGoodsVendor")]
    public class RareGoodsVendor : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public RareGoodsVendor()
            : base("the Rare Goods Vendor")
        { 
            this.SetSkill(SkillName.Alchemy, 80.0, 100.0);
            this.SetSkill(SkillName.Cooking, 80.0, 100.0);
            this.SetSkill(SkillName.TasteID, 80.0, 100.0);
        }

        public RareGoodsVendor(Serial serial)
            : base(serial)
        { 
        }

        public override NpcGuild NpcGuild
        {
            get
            {
                return NpcGuild.MagesGuild;
            }
        }
        public override VendorShoeType ShoeType
        {
            get
            {
                return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals;
            }
        }
        protected override List<SBInfo> SBInfos
        {
            get
            {
                return this.m_SBInfos;
            }
        }
        public override void InitSBInfo() 
        { 
            this.m_SBInfos.Add(new SBRareGoodsVendor()); 
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
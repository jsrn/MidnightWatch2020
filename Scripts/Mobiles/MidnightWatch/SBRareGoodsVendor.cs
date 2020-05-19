using System;
using Server;
using System.Collections.Generic;
using Server.Items;
using Server.Engines.VeteranRewards;
using Server.Gumps;
using System.Linq;
using Server.ContextMenus;
using Server.Multis;

namespace Server.Mobiles
{
    public class SBRareGoodsVendor : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBRareGoodsVendor()
        {
        }

        public override IShopSellInfo SellInfo
        {
            get
            {
                return m_SellInfo;
            }
        }
        public override List<GenericBuyInfo> BuyInfo
        {
            get
            {
                return m_BuyInfo;
            }
        }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                Add(new GenericBuyInfo("Crafting Glass With Glassblowing", typeof(GlassblowingBook), 2500, 30, 0xFF4, 0));
                Add(new GenericBuyInfo("Finding Glass-Quality Sand", typeof(SandMiningBook), 2500, 30, 0xFF4, 0));
                Add(new GenericBuyInfo("Making Valuables With StoneCrafting", typeof(MasonryBook), 2500, 30, 0xFBE, 0));
                Add(new GenericBuyInfo("Mining For Quality Stone", typeof(StoneMiningBook), 2500, 10, 0xFBE, 0));
                Add(new GenericBuyInfo("1044515", typeof(MalletAndChisel), 50, 50, 0x12B3, 0));
                Add(new GenericBuyInfo("1044608", typeof(Blowpipe), 50, 100, 0xE8A, 0x3B9));
                Add(new GenericBuyInfo("Heritage Token", typeof(HeritageToken), 500, 10, 0x367A, 0));
                Add(new GenericBuyInfo("Special Dye Tub", typeof(SpecialDyeTub), 500, 10, 0xFAB, 0));
                Add(new GenericBuyInfo("Leather Dye Tub", typeof(LeatherDyeTub), 500, 10, 0xFAB, 0));
                Add(new GenericBuyInfo("Raised Garden Bed", typeof(RaisedGardenDeed), 500, 10, 0x14F0, 0));
                Add(new GenericBuyInfo("1041280", typeof(InteriorDecorator), 500, 20, 0xFC1, 0));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(GlassblowingBook), 1000);
                Add(typeof(SandMiningBook), 1000);
                Add(typeof(Blowpipe), 10);
            }
        }
    }
}
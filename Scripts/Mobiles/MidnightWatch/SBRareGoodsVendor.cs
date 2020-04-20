using System;
using System.Collections.Generic;
using Server.Items;

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
                Add(new GenericBuyInfo("Crafting Glass With Glassblowing", typeof(GlassblowingBook), 2000, 30, 0xFF4, 0));
                Add(new GenericBuyInfo("Finding Glass-Quality Sand", typeof(SandMiningBook), 2000, 30, 0xFF4, 0));
                Add(new GenericBuyInfo("1044608", typeof(Blowpipe), 21, 100, 0xE8A, 0x3B9));
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
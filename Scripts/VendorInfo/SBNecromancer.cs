using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBNecromancer : SBInfo
	{
		private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
		public SBNecromancer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BlackPearl ), 5, 20, 0xF7A, 0 ) );
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 7, 20, 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, 20, 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, 20, 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, 20, 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 4, 20, 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, 20, 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 4, 20, 0xF8C, 0 ) );

				Add( new GenericBuyInfo( typeof( BatWing ), 4, 200, 0xF78, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveDust ), 4, 200, 0xF8F, 0 ) );
				Add( new GenericBuyInfo( typeof( DaemonBlood ), 4, 200, 0xF7D, 0 ) );
				Add( new GenericBuyInfo( typeof( NoxCrystal ), 4, 200, 0xF8E, 0 ) );
				Add( new GenericBuyInfo( typeof( PigIron ), 4, 200, 0xF8A, 0 ) );

				Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 150, 10, 0x2253, 0 ) );
				
                Add(new GenericBuyInfo(typeof(ScribesPen), 8, 10, 0xFBF, 0));
                Add(new GenericBuyInfo(typeof(BlankScroll), 5, 20, 0x0E34, 0));
				Add( new GenericBuyInfo( typeof( Spellbook ), 50, 10, 0xEFA, 0 ) );

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( WizardsHat ), 15 );
				Add( typeof( Runebook ), 1250 );
				Add( typeof( BlackPearl ), 3 ); 
				Add( typeof( Bloodmoss ),4 ); 
				Add( typeof( MandrakeRoot ), 2 ); 
				Add( typeof( Garlic ), 2 ); 
				Add( typeof( Ginseng ), 2 ); 
				Add( typeof( Nightshade ), 2 ); 
				Add( typeof( SpidersSilk ), 2 ); 
				Add( typeof( SulfurousAsh ), 2 );
				Add( typeof( RecallRune ), 13 );
				Add( typeof( Spellbook ), 25 );
				
				Add( typeof( PigIron ), 2 );
				Add( typeof( DaemonBlood ), 3 );
				Add( typeof( NoxCrystal ), 3 );
				Add( typeof( BatWing ), 1 );
				Add( typeof( GraveDust ), 1 );

				Type[] types = Loot.RegularScrollTypes;

				for (int i = 0; i < types.Length; ++i)
                    Add(types[i], ((i / 8) + 2) * 2);
			}
		}
	}
}

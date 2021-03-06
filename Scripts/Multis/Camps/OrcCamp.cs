using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{ 
    public class OrcCamp : BaseCamp
    {
        [Constructable]
        public OrcCamp()
            : base(0x1F6D)
        {
        }

        public OrcCamp(Serial serial)
            : base(serial)
        {
        }

        public virtual Mobile Orcs
        {
            get
            {
                return new Orc();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override TimeSpan DecayDelay { get { return TimeSpan.FromMinutes(5.0); } }

        public override void AddComponents()
        {
            Visible = false;

            AddItem(new Static(0x10ee), 0, 0, 0);
            AddItem(new Static(0xfac), 0, 7, 0);
						
            switch ( Utility.Random(3) )
            {
                case 0:
                    {
                        AddItem(new Item(0xDE3), 0, 7, 0); // Campfire
                        AddItem(new Item(0x974), 0, 7, 1); // Cauldron
                        break;
                    }
                case 1:
                    {
                        AddItem(new Item(0x1E95), 0, 7, 1); // Rabbit on a spit
                        break;
                    }
                default:
                    {
                        AddItem(new Item(0x1E94), 0, 7, 1); // Chicken on a spit
                        break;
                    }
            }

            AddItem(new Item(0x428), -5, -4, 0); // Gruesome Standart West
            AddCampChests();
			
            for (int i = 0; i < 3; i ++)
            { 
                AddMobile(Orcs, Utility.RandomMinMax(-7, 7), Utility.RandomMinMax(-7, 7), 0);
            }
            AddMobile(new OrcCaptain(), Utility.RandomMinMax(-7, 7), Utility.RandomMinMax(-7, 7), 0);

        }

        // Don't refresh decay timer
        public override void OnExit(Mobile m)
        {
        }

        public override void AddItem(Item item, int xOffset, int yOffset, int zOffset)
        {
            if (item != null)
                item.Movable = false;
				
            base.AddItem(item, xOffset, yOffset, zOffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

        
        }
    }
}
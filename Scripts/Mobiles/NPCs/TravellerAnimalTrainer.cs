namespace Server.Mobiles
{
	public class TravellerAnimalTrainer : AnimalTrainer
	{
		[Constructable]
		public TravellerAnimalTrainer()
		{
			SetSkill(SkillName.Begging, 64.0, 100.0);
			
			if (Utility.RandomBool())
			{
				Title = "the traveller animal trainer";
			}
			else
			{
				Title = "the traveller animal herder";
			}
		}

		public TravellerAnimalTrainer(Serial serial)
			: base(serial)
		{ }

		public override VendorShoeType ShoeType { get { return Female ? VendorShoeType.ThighBoots : VendorShoeType.Boots; } }

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			Item item = FindItemOnLayer(Layer.Pants);

			if (item != null)
			{
				item.Hue = Utility.RandomBrightHue();
			}

			item = FindItemOnLayer(Layer.OuterLegs);

			if (item != null)
			{
				item.Hue = Utility.RandomBrightHue();
			}

			item = FindItemOnLayer(Layer.InnerLegs);

			if (item != null)
			{
				item.Hue = Utility.RandomBrightHue();
			}

			item = FindItemOnLayer(Layer.OuterTorso);

			if (item != null)
			{
				item.Hue = Utility.RandomBrightHue();
			}

			item = FindItemOnLayer(Layer.InnerTorso);

			if (item != null)
			{
				item.Hue = Utility.RandomBrightHue();
			}

			item = FindItemOnLayer(Layer.Shirt);

			if (item != null)
			{
				item.Hue = Utility.RandomBrightHue();
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
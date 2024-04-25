using System;
using System.Runtime.Serialization;

namespace StarScape.Source.Types
{
	[Serializable()]
	public readonly struct Attribute : ISerializable
	{
		public readonly string id = null;
		public readonly string name = null;
		public readonly string description = null;

		private Attribute(SerializationInfo info, StreamingContext context)
		{
			id = (string)info.GetValue("id", typeof(string));
			name = (string)info.GetValue("name", typeof(string));
			description = (string)info.GetValue("desc", typeof(string));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("id", id, id.GetType());
			info.AddValue("name", name, name.GetType());
			info.AddValue("desc", description, description.GetType());
		}
	}

}

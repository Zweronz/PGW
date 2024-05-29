using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SimpleJSON
{
	public class JSONArray : JSONNode, IEnumerable
	{
		private List<JSONNode> list_0 = new List<JSONNode>();

		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex >= 0 && aIndex < list_0.Count)
				{
					return list_0[aIndex];
				}
				return new JSONLazyCreator(this);
			}
			set
			{
				if (aIndex >= 0 && aIndex < list_0.Count)
				{
					list_0[aIndex] = value;
				}
				else
				{
					list_0.Add(value);
				}
			}
		}

		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				list_0.Add(value);
			}
		}

		public override int Int32_0
		{
			get
			{
				return list_0.Count;
			}
		}

		public override IEnumerable<JSONNode> Prop_0
		{
			get
			{
				List<JSONNode>.Enumerator enumerator = list_0.GetEnumerator();
				/*Error near IL_0039: Could not find block for branch target IL_0047*/;
				yield break;
			}
		}

		public override void Add(string string_0, JSONNode jsonnode_0)
		{
			list_0.Add(jsonnode_0);
		}

		public override JSONNode Remove(int int_0)
		{
			if (int_0 >= 0 && int_0 < list_0.Count)
			{
				JSONNode result = list_0[int_0];
				list_0.RemoveAt(int_0);
				return result;
			}
			return null;
		}

		public override JSONNode Remove(JSONNode jsonnode_0)
		{
			list_0.Remove(jsonnode_0);
			return jsonnode_0;
		}

		public IEnumerator GetEnumerator()
		{
			List<JSONNode>.Enumerator enumerator = list_0.GetEnumerator();
			/*Error near IL_0039: Could not find block for branch target IL_0047*/;
			yield break;
		}

		public override string ToString()
		{
			string text = "[ ";
			foreach (JSONNode item in list_0)
			{
				if (text.Length > 2)
				{
					text += ", ";
				}
				text += item.ToString();
			}
			return text + " ]";
		}

		public override string ToString(string string_0)
		{
			string text = "[ ";
			foreach (JSONNode item in list_0)
			{
				if (text.Length > 3)
				{
					text += ", ";
				}
				text = text + "\n" + string_0 + "   ";
				text += item.ToString(string_0 + "   ");
			}
			return text + "\n" + string_0 + "]";
		}

		public override void Serialize(BinaryWriter binaryWriter_0)
		{
			binaryWriter_0.Write((byte)1);
			binaryWriter_0.Write(list_0.Count);
			for (int i = 0; i < list_0.Count; i++)
			{
				list_0[i].Serialize(binaryWriter_0);
			}
		}
	}
}

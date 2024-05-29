namespace SimpleJSON
{
	internal class JSONLazyCreator : JSONNode
	{
		private JSONNode jsonnode_0;

		private string string_0;

		public override JSONNode this[int aIndex]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				JSONArray jSONArray = new JSONArray();
				jSONArray.Add(value);
				Set(jSONArray);
			}
		}

		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				JSONClass jSONClass = new JSONClass();
				jSONClass.Add(aKey, value);
				Set(jSONClass);
			}
		}

		public override int Int32_1
		{
			get
			{
				JSONData jsonnode_ = new JSONData(0);
				Set(jsonnode_);
				return 0;
			}
			set
			{
				JSONData jsonnode_ = new JSONData(value);
				Set(jsonnode_);
			}
		}

		public override float Single_0
		{
			get
			{
				JSONData jsonnode_ = new JSONData(0f);
				Set(jsonnode_);
				return 0f;
			}
			set
			{
				JSONData jsonnode_ = new JSONData(value);
				Set(jsonnode_);
			}
		}

		public override double Double_0
		{
			get
			{
				JSONData jsonnode_ = new JSONData(0.0);
				Set(jsonnode_);
				return 0.0;
			}
			set
			{
				JSONData jsonnode_ = new JSONData(value);
				Set(jsonnode_);
			}
		}

		public override bool Boolean_0
		{
			get
			{
				JSONData jsonnode_ = new JSONData(false);
				Set(jsonnode_);
				return false;
			}
			set
			{
				JSONData jsonnode_ = new JSONData(value);
				Set(jsonnode_);
			}
		}

		public override JSONArray JSONArray_0
		{
			get
			{
				JSONArray jSONArray = new JSONArray();
				Set(jSONArray);
				return jSONArray;
			}
		}

		public override JSONClass JSONClass_0
		{
			get
			{
				JSONClass jSONClass = new JSONClass();
				Set(jSONClass);
				return jSONClass;
			}
		}

		public JSONLazyCreator(JSONNode jsonnode_1)
		{
			jsonnode_0 = jsonnode_1;
			string_0 = null;
		}

		public JSONLazyCreator(JSONNode jsonnode_1, string string_1)
		{
			jsonnode_0 = jsonnode_1;
			string_0 = string_1;
		}

		private void Set(JSONNode jsonnode_1)
		{
			if (string_0 == null)
			{
				jsonnode_0.Add(jsonnode_1);
			}
			else
			{
				jsonnode_0.Add(string_0, jsonnode_1);
			}
			jsonnode_0 = null;
		}

		public override void Add(JSONNode jsonnode_1)
		{
			JSONArray jSONArray = new JSONArray();
			jSONArray.Add(jsonnode_1);
			Set(jSONArray);
		}

		public override void Add(string string_1, JSONNode jsonnode_1)
		{
			JSONClass jSONClass = new JSONClass();
			jSONClass.Add(string_1, jsonnode_1);
			Set(jSONClass);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return true;
			}
			return object.ReferenceEquals(this, obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Empty;
		}

		public override string ToString(string string_1)
		{
			return string.Empty;
		}

		public static bool operator ==(JSONLazyCreator jsonlazyCreator_0, object object_0)
		{
			if (object_0 == null)
			{
				return true;
			}
			return object.ReferenceEquals(jsonlazyCreator_0, object_0);
		}

		public static bool operator !=(JSONLazyCreator jsonlazyCreator_0, object object_0)
		{
			return !(jsonlazyCreator_0 == object_0);
		}
	}
}

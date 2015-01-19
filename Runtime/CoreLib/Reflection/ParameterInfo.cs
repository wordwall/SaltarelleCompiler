using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Reflection {
	[Serializable]
	public class ParameterInfo : MemberInfo {
        public Type ParameterType { [InlineCode("{this}")] get { return null; } }
	}
}
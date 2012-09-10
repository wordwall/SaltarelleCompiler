﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory;

namespace Saltarelle.Compiler {
	internal static class Messages {
		private static Dictionary<int, Tuple<MessageSeverity, string>> _allMessages = new Dictionary<int, Tuple<MessageSeverity, string>> {
			{ 7001, Tuple.Create(MessageSeverity.Error, "The type {0} has both [IgnoreNamespace] and [ScriptNamespace] specified. At most one of these attributes can be specified for a type.") },
			{ 7002, Tuple.Create(MessageSeverity.Error, "{0}: The argument for [ScriptNamespace] must be a valid JavaScript qualified identifier, or be blank.") },
			{ 7003, Tuple.Create(MessageSeverity.Error, "The type {0} cannot have a [ResourcesAttribute] because it is not static.") },
			{ 7004, Tuple.Create(MessageSeverity.Error, "The type {0} cannot have a [ResourcesAttribute] because it is generic.") },
			{ 7005, Tuple.Create(MessageSeverity.Error, "The type {0} cannot have a [ResourcesAttribute] because it contains members that are not const fields.") },
			{ 7006, Tuple.Create(MessageSeverity.Error, "{0}: The argument for [ScriptName], when applied to a type, must be a valid JavaScript identifier.") },
			{ 7007, Tuple.Create(MessageSeverity.Error, "[IgnoreNamespace] or [ScriptNamespace] cannot be specified for the nested type {0}.") },
			{ 7008, Tuple.Create(MessageSeverity.Error, "The non-serializable type {0} cannot inherit from the serializable type {1}.") },
			{ 7009, Tuple.Create(MessageSeverity.Error, "The serializable type {0} must inherit from another serializable type, System.Object or System.Record.") },
			{ 7010, Tuple.Create(MessageSeverity.Error, "The serializable type {0} cannot implement interfaces.") },
			{ 7011, Tuple.Create(MessageSeverity.Error, "The serializable type {0} cannot declare instance events.") },
			{ 7012, Tuple.Create(MessageSeverity.Error, "The type {0} must be static in order to be decorated with a [MixinAttribute]") },
			{ 7013, Tuple.Create(MessageSeverity.Error, "The type {0} can contain only methods order to be decorated with a [MixinAttribute]") },
			{ 7014, Tuple.Create(MessageSeverity.Error, "[MixinAttribute] cannot be applied to the generic type {0}.") },
			{ 7015, Tuple.Create(MessageSeverity.Error, "The type {0} must be static in order to be decorated with a [GlobalMethodsAttribute]") },
			{ 7016, Tuple.Create(MessageSeverity.Error, "The type {0} cannot have any fields, events or properties in order to be decorated with a [GlobalMethodsAttribute]") },
			{ 7017, Tuple.Create(MessageSeverity.Error, "[GlobalMethodsAttribute] cannot be applied to the generic type {0}.") },
			{ 7018, Tuple.Create(MessageSeverity.Error, "The type {0} cannot inherit from both {1} and {2} because both those types have a member with the script name {3}. You have to rename the member on one of the base types, or refactor your code.") },
			{ 7019, Tuple.Create(MessageSeverity.Error, "The method {0} cannot have the script name 'runTests' because its defining type has a TestFixtureAttribute.") },
			{ 7020, Tuple.Create(MessageSeverity.Error, "Method {0}: Methods decorated with a [TestAttribute] or [AsyncTestAttribute] must be public, non-generic, parameterless instance methods that return void.") },
			{ 7021, Tuple.Create(MessageSeverity.Error, "The method {0} cannot have both an [AsyncTestAttribute] and a [TestAttribute].") },
			{ 7022, Tuple.Create(MessageSeverity.Error, "The method {0} cannot have an [AsyncTestAttribute] or a [TestAttribute] because its declaring class does not have a [TestFixtureAttribute].") },
			{ 7023, Tuple.Create(MessageSeverity.Error, "The serializable type {0} cannot declare the virtual member {1}.") },
			{ 7024, Tuple.Create(MessageSeverity.Error, "The serializable type {0} cannot override the member {1}.") },
			{ 7025, Tuple.Create(MessageSeverity.Error, "The argument to the [MixinAttribute] for the type {0} must be a valid Javascript nested identifier.") },

			{ 7100, Tuple.Create(MessageSeverity.Error, "The member {0} has an [AlternateSignatureAttribute], but there is not exactly one other method with the same name that does not have that attribute.") },
			{ 7101, Tuple.Create(MessageSeverity.Error, "The name specified in the [ScriptName] attribute for member {0} must be a valid JavaScript identifier, or be blank.") },
			{ 7102, Tuple.Create(MessageSeverity.Error, "The constructor {0} cannot have an [ExpandParamsAttribute] because it does not have a parameter with the 'params' modifier.") },
			{ 7103, Tuple.Create(MessageSeverity.Error, "The inline code for the constructor {0} contained errors: {1}.") },
			{ 7104, Tuple.Create(MessageSeverity.Error, "The named specified in a [ScriptNameAttribute] for the indexer of type {0} cannot be empty.") },
			{ 7105, Tuple.Create(MessageSeverity.Error, "The named specified in a [ScriptNameAttribute] for the property {0} cannot be empty.") },
			{ 7106, Tuple.Create(MessageSeverity.Error, "Indexers cannot be decorated with [ScriptAliasAttribute].") },
			{ 7107, Tuple.Create(MessageSeverity.Error, "The property {0} cannot have a [ScriptAliasAttribute] because it is an instance member.") },
			{ 7108, Tuple.Create(MessageSeverity.Error, "The indexer cannot be decorated with [IntrinsicPropertyAttribute] because it is an interface member.") },
			{ 7109, Tuple.Create(MessageSeverity.Error, "The property {0} cannot have an [IntrinsicPropertyAttribute] because it is an interface member.") },
			{ 7110, Tuple.Create(MessageSeverity.Error, "The indexer be decorated with an [IntrinsicPropertyAttribute] because it overrides a base member.") },
			{ 7111, Tuple.Create(MessageSeverity.Error, "The property {0} cannot have an [IntrinsicPropertyAttribute] because it overrides a base member.") },
			{ 7112, Tuple.Create(MessageSeverity.Error, "The indexer cannot be decorated with an [IntrinsicPropertyAttribute] because it is overridable.") },
			{ 7113, Tuple.Create(MessageSeverity.Error, "The property {0} cannot have an [IntrinsicPropertyAttribute] because it is overridable.") },
			{ 7114, Tuple.Create(MessageSeverity.Error, "The indexer cannot be decorated with an [IntrinsicPropertyAttribute] because it implements an interface member.") },
			{ 7115, Tuple.Create(MessageSeverity.Error, "The property {0} cannot have an [IntrinsicPropertyAttribute] because it implements an interface member.") },
			{ 7116, Tuple.Create(MessageSeverity.Error, "The indexer must have exactly one parameter in order to have an [IntrinsicPropertyAttribute].") },
			{ 7117, Tuple.Create(MessageSeverity.Error, "The method {0} cannot have an [IntrinsicOperatorAttribute] because it is not an operator method.") },
			{ 7118, Tuple.Create(MessageSeverity.Error, "The [IntrinsicOperatorAttribute] cannot be applied to the operator {0} because it is a conversion operator.") },
			{ 7119, Tuple.Create(MessageSeverity.Error, "The method {0} cannot have a [ScriptSkipAttribute] because it is an interface method.") },
			{ 7120, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have a [ScriptSkipAttribute] because it overrides a base member.") },
			{ 7121, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have a [ScriptSkipAttribute] because it is overridable.") },
			{ 7122, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have a [ScriptSkipAttribute] because it implements an interface member.") },
			{ 7123, Tuple.Create(MessageSeverity.Error, "The static method {0} must have exactly one parameter in order to have a [ScriptSkipAttribute].") },
			{ 7124, Tuple.Create(MessageSeverity.Error, "The instance method {0} must have no parameters in order to have a [ScriptSkipAttribute].") },
			{ 7125, Tuple.Create(MessageSeverity.Error, "The method {0} must be static in order to have a [ScriptAliasAttribute].") },
			{ 7126, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have an [InlineCodeAttribute] because it is an interface method.") },
			{ 7127, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have an [InlineCodeAttribute] because it overrides a base member.") },
			{ 7128, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have an [InlineCodeAttribute] because it is overridable.") },
			{ 7129, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have a [InlineCodeAttribute] because it implements an interface member.") },
			{ 7130, Tuple.Create(MessageSeverity.Error, "The inline code for the method {0} contained errors: {1}.") },
			{ 7131, Tuple.Create(MessageSeverity.Error, "The method {0} cannot have an [InstanceMethodOnFirstArgumentAttribute] because it is not static.") },
			{ 7132, Tuple.Create(MessageSeverity.Error, "The [ScriptName], [PreserveName] and [PreserveCase] attributes cannot be specified on method the method {0} because it overrides a base member. Specify the attribute on the base member instead.") },
			{ 7133, Tuple.Create(MessageSeverity.Error, "The [IgnoreGenericArgumentsAttribute] attribute cannot be specified on the method {0} because it overrides a base member. Specify the attribute on the base member instead.") },
			{ 7134, Tuple.Create(MessageSeverity.Error, "The overriding member {0} cannot implement the interface method {1} because it has a different script name. Consider using explicit interface implementation.") },
			{ 7135, Tuple.Create(MessageSeverity.Error, "The [ScriptName], [PreserveName] and [PreserveCase] attributes cannot be specified on the method {0} because it implements an interface member. Specify the attribute on the interface member instead, or consider using explicit interface implementation.") },
			{ 7136, Tuple.Create(MessageSeverity.Error, "The member {0} cannot implement multiple interface methods with differing script names. Consider using explicit interface implementation.") },
			{ 7137, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have an [ExpandParamsAttribute] because it does not have a parameter with the 'params' modifier.") },
			{ 7138, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have an empty name specified in its [ScriptName] because it is an interface method.") },
			{ 7139, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have an empty name specified in its [ScriptName] because it is overridable.") },
			{ 7140, Tuple.Create(MessageSeverity.Error, "The member {0} cannot have an empty name specified in its [ScriptName] because it is static.") },
			{ 7141, Tuple.Create(MessageSeverity.Error, "The named specified in a [ScriptNameAttribute] for the event {0} cannot be empty.") },
			{ 7142, Tuple.Create(MessageSeverity.Error, "The named specified in a [ScriptNameAttribute] for the field {0} cannot be empty.") },
			{ 7143, Tuple.Create(MessageSeverity.Error, "The type {0} doesn't contain a matching property or field for the constructor parameter {1}.") },
			{ 7144, Tuple.Create(MessageSeverity.Error, "The parameter {0} has the type {1} but the matching member has type {2}. The types must be the same.") },
			{ 7145, Tuple.Create(MessageSeverity.Error, "The parameter {0} cannot be declared as ref or out.") },
			{ 7146, Tuple.Create(MessageSeverity.Error, "The constructor cannot have an [ObjectLiteralAttribute] because the type {0} is not a serializable type.") },
			{ 7147, Tuple.Create(MessageSeverity.Error, "The delegate type {0} cannot have a [BindThisToFirstParameterAttribute] because it does not have any parameters.") },
			{ 7148, Tuple.Create(MessageSeverity.Error, "The delegate type {0} cannot have an [ExpandParamsAttribute] because it does not have a parameter with the 'params' modifier.") },

			{ 7500, Tuple.Create(MessageSeverity.Error, "Cannot use the type {0} in the inheritance list for type {1} because it is marked as not usable from script.") },
			{ 7501, Tuple.Create(MessageSeverity.Error, "More than one unnamed constructor for the type {0}.") },
			{ 7502, Tuple.Create(MessageSeverity.Error, "The constructor {0} must be invoked in expanded form for its its param array.") },
			{ 7503, Tuple.Create(MessageSeverity.Error, "Chaining from a normal constructor to a static method constructor is not supported.") },
			{ 7504, Tuple.Create(MessageSeverity.Error, "Chaining from a normal constructor to a constructor implemented as inline code is not supported.") },
			{ 7505, Tuple.Create(MessageSeverity.Error, "This constructor cannot be used from script.") },
			{ 7506, Tuple.Create(MessageSeverity.Error, "Property {0}, declared as being a native indexer, is not an indexer with exactly one argument.") },
			{ 7507, Tuple.Create(MessageSeverity.Error, "Cannot use the property {0} from script.") },
			{ 7508, Tuple.Create(MessageSeverity.Error, "The field {0} is constant in script and cannot be assigned to.") },
			{ 7509, Tuple.Create(MessageSeverity.Error, "The field {0} is not usable from script.") },
			{ 7511, Tuple.Create(MessageSeverity.Error, "The event {0} is not usable from script.") },
			{ 7512, Tuple.Create(MessageSeverity.Error, "The property {0} is not usable from script.") },
			{ 7513, Tuple.Create(MessageSeverity.Error, "Only locals can be passed by reference.") },
			{ 7514, Tuple.Create(MessageSeverity.Error, "The method {0} must be invoked in expanded form for its its param array.") },
			{ 7515, Tuple.Create(MessageSeverity.Error, "Cannot use the type {0} in as a generic argument to the method {1} because it is marked as not usable from script.") },
			{ 7516, Tuple.Create(MessageSeverity.Error, "The method {0} cannot be used from script.") },
			{ 7517, Tuple.Create(MessageSeverity.Error, "Cannot use the the property {0} in an anonymous object initializer.") },
			{ 7518, Tuple.Create(MessageSeverity.Error, "Cannot use the field {0} in an anonymous object initializer.") },
			{ 7519, Tuple.Create(MessageSeverity.Error, "Cannot create an instance of the type {0} because it is marked as not usable from script.") },
			{ 7520, Tuple.Create(MessageSeverity.Error, "Cannot use the type {0} in as a type argument for the class {1} because it is marked as not usable from script.") },
			{ 7521, Tuple.Create(MessageSeverity.Error, "Cannot use the variable {0} because it is an expanded param array.") },
			{ 7522, Tuple.Create(MessageSeverity.Error, "Cannot use the type {0} in a typeof expression because it is marked as not usable from script.") },
			{ 7523, Tuple.Create(MessageSeverity.Error, "Cannot perform method group conversion on {0} because it is not a normal method.") },
			{ 7524, Tuple.Create(MessageSeverity.Error, "Cannot convert the method '{0}' to the delegate type '{1}' because the method and delegate type differ in whether they expand their param array.") },
			{ 7525, Tuple.Create(MessageSeverity.Error, "Error in inline code compilation: {0}.") },
			{ 7526, Tuple.Create(MessageSeverity.Error, "Dynamic invocations cannot use named arguments.") },
			{ 7527, Tuple.Create(MessageSeverity.Error, "The member {0} cannot be initialized in an initializer statement because it was also initialized by the constructor call.") },
			{ 7528, Tuple.Create(MessageSeverity.Error, "Dynamic indexing must have exactly one argument.") },
			{ 7529, Tuple.Create(MessageSeverity.Error, "Cannot compile this dynamic invocation because all the applicable methods do not have the same script name. If you want to call the method with this exact name, cast the invocation target to dynamic.") },
			{ 7530, Tuple.Create(MessageSeverity.Error, "Cannot compile this dynamic invocation because at least one of the applicable methods is not a normal method. If you want to call the method with this exact name, cast the invocation target to dynamic.") },
			{ 7531, Tuple.Create(MessageSeverity.Error, "Cannot compile this dynamic invocation because the applicable methods are compiled in different ways.") },
			{ 7532, Tuple.Create(MessageSeverity.Error, "Chaining from a normal constructor to a JSON constructor is not supported.") },
			{ 7533, Tuple.Create(MessageSeverity.Error, "Cannot convert the delegate type {0} to {1} because they differ in whether the Javascript 'this' is bound to the first parameter.") },
			{ 7534, Tuple.Create(MessageSeverity.Error, "Delegates of type {0} must be invoked in expanded form for its its param array.") },

			{ 7700, Tuple.Create(MessageSeverity.Error, "Boxing of 'char' is not allowed because this is likely to cause undesired behaviour. Insert a cast to 'int' or 'string' to tell the compiler about the desired behaviour.") },

			{ 7950, Tuple.Create(MessageSeverity.Error, "Error writing assembly: {0}.") },
			{ 7951, Tuple.Create(MessageSeverity.Error, "Error writing script: {0}.") },
			{ 7952, Tuple.Create(MessageSeverity.Error, "Error writing documentation file: {0}.") },

			{ 7996, Tuple.Create(MessageSeverity.Error, "Indirectly referenced assembly {0} must be referenced.") },
			{ 7997, Tuple.Create(MessageSeverity.Error, "Unable to resolve the assembly reference {0}.") },
			{ 7998, Tuple.Create(MessageSeverity.Error, "Use of unsupported feature {0}.") },
			{ 7999, Tuple.Create(MessageSeverity.Error, "INTERNAL ERROR: {0}. Please report this as an issue on https://github.com/erik-kallen/SaltarelleCompiler/") },
		};

		internal static Tuple<MessageSeverity, string> Get(int code) {
			Tuple<MessageSeverity, string> result;
			_allMessages.TryGetValue(code, out result);
			return result;
		}
	}
}

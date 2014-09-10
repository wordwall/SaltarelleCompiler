﻿using System;
using System.IO;
using System.Linq;
using CoreLib.Plugin;
using NUnit.Framework;
using Saltarelle.Compiler;
using Saltarelle.Compiler.JSModel;
using Saltarelle.Compiler.Tests;

namespace CoreLib.Tests.OOPEmulatorTests {
	[TestFixture]
	public class OverallStructureTests : OOPEmulatorTestBase {
		[Test]
		public void CodeBeforeFirstTypeIncludesAssemblyAndNamespaceInitialization() {
			var compilation = Compile(
@"namespace OuterNamespace {
	namespace InnerNamespace {
		public class SomeType {}
		public class SomeType2 {}
		public enum SomeEnum {}
	}
	namespace InnerNamespace2 {
		public class OtherType : InnerNamespace.SomeType2 {}
		public interface OtherInterface {}
	}
}");

			var actual = compilation.Item2.GetCodeBeforeFirstType(compilation.Item3, new AssemblyResource[0]).ToList();

			Assert.That(OutputFormatter.Format(actual, allowIntermediates: true).Replace("\r\n", "\n"), Is.EqualTo(
@"global.OuterNamespace = global.OuterNamespace || {};
global.OuterNamespace.InnerNamespace = global.OuterNamespace.InnerNamespace || {};
global.OuterNamespace.InnerNamespace2 = global.OuterNamespace.InnerNamespace2 || {};
{Script}.initAssembly($asm, 'Test');
".Replace("\r\n", "\n")));
		}

		[Test]
		public void CodeBeforeFirstTypeExportsNamespacesToTheExportsObjectIfTheAssemblyHasAModuleName() {
			var compilation = Compile(
@"[assembly: System.Runtime.CompilerServices.ModuleName(""m"")]
namespace OuterNamespace {
	namespace InnerNamespace {
		public class SomeType {}
		public class SomeType2 {}
		public enum SomeEnum {}
	}
	namespace InnerNamespace2 {
		public class OtherType : InnerNamespace.SomeType2 {}
		public interface OtherInterface {}
	}
}");

			var actual = compilation.Item2.GetCodeBeforeFirstType(compilation.Item3, new AssemblyResource[0]).ToList();

			Assert.That(OutputFormatter.Format(actual, allowIntermediates: true).Replace("\r\n", "\n"), Is.EqualTo(
@"exports.OuterNamespace = exports.OuterNamespace || {};
exports.OuterNamespace.InnerNamespace = exports.OuterNamespace.InnerNamespace || {};
exports.OuterNamespace.InnerNamespace2 = exports.OuterNamespace.InnerNamespace2 || {};
{Script}.initAssembly($asm, 'Test');
".Replace("\r\n", "\n")));
		}

		[Test]
		public void CodeBeforeFirstTypeDoesNotIncludeInitializationOfNamespaceForNonExportedTypes() {
			var compilation = Compile(
@"namespace OuterNamespace {
	namespace InnerNamespace {
		internal class SomeType {}
	}
}");

			var actual = compilation.Item2.GetCodeBeforeFirstType(compilation.Item3, new AssemblyResource[0]).ToList();

			Assert.That(OutputFormatter.Format(actual, allowIntermediates: true).Replace("\r\n", "\n"), Is.EqualTo(
@"{Script}.initAssembly($asm, 'Test');
".Replace("\r\n", "\n")));
		}

		[Test]
		public void CodeBeforeFirstTypeDoesNotIncludeInitializationOfNamespaceForGlobalOrMixinTypes() {
			var compilation = Compile(
@"namespace OuterNamespace {
	namespace InnerNamespace {
		[System.Runtime.CompilerServices.Mixin(""x"")] public static class GlobalType {}
		[System.Runtime.CompilerServices.GlobalMethods] public static class MixinType {}
	}
}");

			var actual = compilation.Item2.GetCodeBeforeFirstType(compilation.Item3, new AssemblyResource[0]).ToList();

			Assert.That(OutputFormatter.Format(actual, allowIntermediates: true).Replace("\r\n", "\n"), Is.EqualTo(
@"{Script}.initAssembly($asm, 'Test');
".Replace("\r\n", "\n")));
		}

		[Test]
		public void AssemblyAttributesAreAssignedInTheCodeAfterLastType() {
			var compilation = Compile(
@"[assembly: MyAttribute(42)]
public class MyAttribute : System.Attribute {
	public MyAttribute(int x) {}
	static MyAttribute() { int a = 0; }
}");

			var actual = compilation.Item2.GetCodeAfterLastType(compilation.Item3, new AssemblyResource[0]).ToList();

			Assert.That(actual.Count, Is.EqualTo(1));
			Assert.That(OutputFormatter.Format(actual, allowIntermediates: true).Replace("\r\n", "\n"), Is.EqualTo("$asm.attr = [new {MyAttribute}(42)];\n"));
		}

		[Test]
		public void BothPublicAndPrivateEmbeddedResourcesAreIncludedInTheInitAssemblyCallButThisExcludesPluginDllsAndLinkedResources() {
			var compilation = Compile("");
			var resources = new[] { new AssemblyResource("Resource.Name", true, () => new MemoryStream(new byte[] { 45, 6, 7, 4 })),
			                        new AssemblyResource("Some.Private.Resource", false, () => new MemoryStream(new byte[] { 5, 3, 7 })),
			                        new AssemblyResource("Namespace.Plugin.dll", true, () => new MemoryStream(new byte[] { 5, 3, 7 })),
			                        new AssemblyResource("Plugin.dll", true, () => new MemoryStream(new byte[] { 5, 3, 7 })) };
			
			var actual = compilation.Item2.GetCodeBeforeFirstType(compilation.Item3, resources).Select(s => OutputFormatter.Format(s, allowIntermediates: true)).Single(s => s.StartsWith("{Script}.initAssembly"));
			
			Assert.That(actual.Replace("\r\n", "\n"), Is.EqualTo("{Script}.initAssembly($asm, 'Test', { 'Resource.Name': 'LQYHBA==', 'Some.Private.Resource': 'BQMH' });\n"));
		}
	}
}

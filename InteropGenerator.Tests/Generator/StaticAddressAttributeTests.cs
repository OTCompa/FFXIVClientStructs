﻿using InteropGenerator.Tests.Helpers;
using Xunit;
using VerifyIG = InteropGenerator.Tests.Helpers.IncrementalGeneratorVerifier<InteropGenerator.Generator.InteropGenerator>;

namespace InteropGenerator.Tests.Generator;

public class StaticAddressAttributeTests {
    [Fact]
    public async Task GenerateStaticAddress() {
        const string code = """
                            [GenerateInterop]
                            public unsafe partial struct TestStruct
                            {
                                [StaticAddress("AA BB CC DD ?? ?? ?? ?? AA BB ?? DD", 4)]
                                public static partial TestStruct* Instance();
                            }
                            """;

        const string result = """
                              // <auto-generated/>
                              unsafe partial struct TestStruct
                              {
                                  public static class Addresses
                                  {
                                      public static readonly Address Instance = new Address("TestStruct.Instance", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD ?? ?? ?? ??", 4, new ulong[] {0x00000000DDCCBBAA, 0x00000000DD00BBAA}, new ulong[] {0x00000000FFFFFFFF, 0x00000000FF00FFFF}, 0);
                                  }
                                  public unsafe static class StaticAddressPointers
                                  {
                                      public static global::TestStruct* pInstance => (global::TestStruct*)TestStruct.Addresses.Instance.Value;
                                  }
                                  public static partial global::TestStruct* Instance()
                                  {
                                      if (StaticAddressPointers.pInstance is null)
                                      {
                                          InteropGenerator.Runtime.ThrowHelper.ThrowNullAddress("TestStruct.Instance", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD");
                                      }
                                      return StaticAddressPointers.pInstance;
                                  }
                              }
                              """;
        
        await VerifyIG.VerifyGeneratorAsync(
            code,
            ("TestStruct.InteropGenerator.g.cs", result),
            SourceGeneration.GetInitializerSource(string.Empty, "TestStruct", ["Instance"]));
    }
    
        [Fact]
    public async Task GenerateStaticAddressIsPointer() {
        const string code = """
                            [GenerateInterop]
                            public unsafe partial struct TestStruct
                            {
                                [StaticAddress("AA BB CC DD ?? ?? ?? ?? AA BB ?? DD", 4, true)]
                                public static partial TestStruct* Instance();
                            }
                            """;

        const string result = """
                              // <auto-generated/>
                              unsafe partial struct TestStruct
                              {
                                  public static class Addresses
                                  {
                                      public static readonly Address Instance = new Address("TestStruct.Instance", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD ?? ?? ?? ??", 4, new ulong[] {0x00000000DDCCBBAA, 0x00000000DD00BBAA}, new ulong[] {0x00000000FFFFFFFF, 0x00000000FF00FFFF}, 0);
                                  }
                                  public unsafe static class StaticAddressPointers
                                  {
                                      public static global::TestStruct** ppInstance => (global::TestStruct**)TestStruct.Addresses.Instance.Value;
                                  }
                                  public static partial global::TestStruct* Instance()
                                  {
                                      if (StaticAddressPointers.ppInstance is null)
                                      {
                                          InteropGenerator.Runtime.ThrowHelper.ThrowNullAddress("TestStruct.Instance", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD");
                                      }
                                      return *StaticAddressPointers.ppInstance;
                                  }
                              }
                              """;
        
        await VerifyIG.VerifyGeneratorAsync(
            code,
            ("TestStruct.InteropGenerator.g.cs", result),
            SourceGeneration.GetInitializerSource(string.Empty, "TestStruct", ["Instance"]));
    }
    
    [Fact]
    public async Task GenerateStaticAddress_CALLOffset() {
        const string code = """
                            [GenerateInterop]
                            public unsafe partial struct TestStruct
                            {
                                [StaticAddress("E8 BB CC DD ?? ?? ?? ?? AA BB ?? DD", 1)]
                                public static partial TestStruct* Instance();
                            }
                            """;

        const string result = """
                              // <auto-generated/>
                              unsafe partial struct TestStruct
                              {
                                  public static class Addresses
                                  {
                                      public static readonly Address Instance = new Address("TestStruct.Instance", "E8 BB CC DD ?? ?? ?? ?? AA BB ?? DD ?? ?? ?? ??", 1, new ulong[] {0x00000000DDCCBBE8, 0x00000000DD00BBAA}, new ulong[] {0x00000000FFFFFFFF, 0x00000000FF00FFFF}, 0);
                                  }
                                  public unsafe static class StaticAddressPointers
                                  {
                                      public static global::TestStruct* pInstance => (global::TestStruct*)TestStruct.Addresses.Instance.Value;
                                  }
                                  public static partial global::TestStruct* Instance()
                                  {
                                      if (StaticAddressPointers.pInstance is null)
                                      {
                                          InteropGenerator.Runtime.ThrowHelper.ThrowNullAddress("TestStruct.Instance", "E8 BB CC DD ?? ?? ?? ?? AA BB ?? DD");
                                      }
                                      return StaticAddressPointers.pInstance;
                                  }
                              }
                              """;
        
        await VerifyIG.VerifyGeneratorAsync(
            code,
            ("TestStruct.InteropGenerator.g.cs", result),
            SourceGeneration.GetInitializerSource(string.Empty, "TestStruct", ["Instance"]));
    }
    
    [Fact]
    public async Task GenerateStaticAddress_JMPOffset() {
        const string code = """
                            [GenerateInterop]
                            public unsafe partial struct TestStruct
                            {
                                [StaticAddress("E9 BB CC DD ?? ?? ?? ?? AA BB ?? DD", 1)]
                                public static partial TestStruct* Instance();
                            }
                            """;

        const string result = """
                              // <auto-generated/>
                              unsafe partial struct TestStruct
                              {
                                  public static class Addresses
                                  {
                                      public static readonly Address Instance = new Address("TestStruct.Instance", "E9 BB CC DD ?? ?? ?? ?? AA BB ?? DD ?? ?? ?? ??", 1, new ulong[] {0x00000000DDCCBBE9, 0x00000000DD00BBAA}, new ulong[] {0x00000000FFFFFFFF, 0x00000000FF00FFFF}, 0);
                                  }
                                  public unsafe static class StaticAddressPointers
                                  {
                                      public static global::TestStruct* pInstance => (global::TestStruct*)TestStruct.Addresses.Instance.Value;
                                  }
                                  public static partial global::TestStruct* Instance()
                                  {
                                      if (StaticAddressPointers.pInstance is null)
                                      {
                                          InteropGenerator.Runtime.ThrowHelper.ThrowNullAddress("TestStruct.Instance", "E9 BB CC DD ?? ?? ?? ?? AA BB ?? DD");
                                      }
                                      return StaticAddressPointers.pInstance;
                                  }
                              }
                              """;
        
        await VerifyIG.VerifyGeneratorAsync(
            code,
            ("TestStruct.InteropGenerator.g.cs", result),
            SourceGeneration.GetInitializerSource(string.Empty, "TestStruct", ["Instance"]));
    }
    
    [Fact]
    public async Task GenerateStaticAddressInNamespace() {
        const string code = """
                            namespace TestNamespace.InnerNamespace;
                            
                            [GenerateInterop]
                            public unsafe partial struct TestStruct
                            {
                                [StaticAddress("AA BB CC DD ?? ?? ?? ?? AA BB ?? DD", 4)]
                                public static partial TestStruct* Instance();
                            }
                            """;

        const string result = """
                              // <auto-generated/>
                              namespace TestNamespace.InnerNamespace;
                              
                              unsafe partial struct TestStruct
                              {
                                  public static class Addresses
                                  {
                                      public static readonly Address Instance = new Address("TestNamespace.InnerNamespace.TestStruct.Instance", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD ?? ?? ?? ??", 4, new ulong[] {0x00000000DDCCBBAA, 0x00000000DD00BBAA}, new ulong[] {0x00000000FFFFFFFF, 0x00000000FF00FFFF}, 0);
                                  }
                                  public unsafe static class StaticAddressPointers
                                  {
                                      public static global::TestNamespace.InnerNamespace.TestStruct* pInstance => (global::TestNamespace.InnerNamespace.TestStruct*)TestStruct.Addresses.Instance.Value;
                                  }
                                  public static partial global::TestNamespace.InnerNamespace.TestStruct* Instance()
                                  {
                                      if (StaticAddressPointers.pInstance is null)
                                      {
                                          InteropGenerator.Runtime.ThrowHelper.ThrowNullAddress("TestStruct.Instance", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD");
                                      }
                                      return StaticAddressPointers.pInstance;
                                  }
                              }
                              """;
        
        await VerifyIG.VerifyGeneratorAsync(
            code,
            ("TestNamespace.InnerNamespace.TestStruct.InteropGenerator.g.cs", result),
            SourceGeneration.GetInitializerSource(string.Empty, "TestNamespace.InnerNamespace.TestStruct", ["Instance"]));
    }
    
        [Fact]
    public async Task GenerateStaticAddressInnerStruct() {
        const string code = """
                            public partial struct TestStruct
                            {
                                [GenerateInterop]
                                public unsafe partial struct InnerStruct
                                {
                                    [StaticAddress("AA BB CC DD ?? ?? ?? ?? AA BB ?? DD", 4)]
                                    public static partial InnerStruct* Instance();
                                }
                            }
                            """;

        const string result = """
                              // <auto-generated/>
                              unsafe partial struct TestStruct
                              {
                                  unsafe partial struct InnerStruct
                                  {
                                      public static class Addresses
                                      {
                                          public static readonly Address Instance = new Address("TestStruct+InnerStruct.Instance", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD ?? ?? ?? ??", 4, new ulong[] {0x00000000DDCCBBAA, 0x00000000DD00BBAA}, new ulong[] {0x00000000FFFFFFFF, 0x00000000FF00FFFF}, 0);
                                      }
                                      public unsafe static class StaticAddressPointers
                                      {
                                          public static global::TestStruct.InnerStruct* pInstance => (global::TestStruct.InnerStruct*)InnerStruct.Addresses.Instance.Value;
                                      }
                                      public static partial global::TestStruct.InnerStruct* Instance()
                                      {
                                          if (StaticAddressPointers.pInstance is null)
                                          {
                                              InteropGenerator.Runtime.ThrowHelper.ThrowNullAddress("InnerStruct.Instance", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD");
                                          }
                                          return StaticAddressPointers.pInstance;
                                      }
                                  }
                              }
                              """;
        
        await VerifyIG.VerifyGeneratorAsync(
            code,
            ("TestStruct+InnerStruct.InteropGenerator.g.cs", result),
            SourceGeneration.GetInitializerSource(string.Empty, "TestStruct.InnerStruct", ["Instance"]));
    }
    
    [Fact]
    public async Task GenerateStaticAddressMultiple() {
        const string code = """
                            [GenerateInterop]
                            public unsafe partial struct TestStruct
                            {
                                [StaticAddress("AA BB CC DD ?? ?? ?? ?? AA BB ?? DD", 4)]
                                public static partial TestStruct* Instance();
                                [StaticAddress("AA BB CC DD ?? ?? ?? ?? AA BB ?? DD", 4)]
                                public static partial TestStruct* Instance2();
                            }
                            """;

        const string result = """
                              // <auto-generated/>
                              unsafe partial struct TestStruct
                              {
                                  public static class Addresses
                                  {
                                      public static readonly Address Instance = new Address("TestStruct.Instance", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD ?? ?? ?? ??", 4, new ulong[] {0x00000000DDCCBBAA, 0x00000000DD00BBAA}, new ulong[] {0x00000000FFFFFFFF, 0x00000000FF00FFFF}, 0);
                                      public static readonly Address Instance2 = new Address("TestStruct.Instance2", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD ?? ?? ?? ??", 4, new ulong[] {0x00000000DDCCBBAA, 0x00000000DD00BBAA}, new ulong[] {0x00000000FFFFFFFF, 0x00000000FF00FFFF}, 0);
                                  }
                                  public unsafe static class StaticAddressPointers
                                  {
                                      public static global::TestStruct* pInstance => (global::TestStruct*)TestStruct.Addresses.Instance.Value;
                                      public static global::TestStruct* pInstance2 => (global::TestStruct*)TestStruct.Addresses.Instance2.Value;
                                  }
                                  public static partial global::TestStruct* Instance()
                                  {
                                      if (StaticAddressPointers.pInstance is null)
                                      {
                                          InteropGenerator.Runtime.ThrowHelper.ThrowNullAddress("TestStruct.Instance", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD");
                                      }
                                      return StaticAddressPointers.pInstance;
                                  }
                                  public static partial global::TestStruct* Instance2()
                                  {
                                      if (StaticAddressPointers.pInstance2 is null)
                                      {
                                          InteropGenerator.Runtime.ThrowHelper.ThrowNullAddress("TestStruct.Instance2", "AA BB CC DD ?? ?? ?? ?? AA BB ?? DD");
                                      }
                                      return StaticAddressPointers.pInstance2;
                                  }
                              }
                              """;
        
        await VerifyIG.VerifyGeneratorAsync(
            code,
            ("TestStruct.InteropGenerator.g.cs", result),
            SourceGeneration.GetInitializerSource(string.Empty, "TestStruct", ["Instance", "Instance2"]));
    }
}

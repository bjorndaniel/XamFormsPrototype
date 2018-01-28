using SQLite;
using System.Collections.Generic;
using System.IO;
using XamFormsPrototype.Contracts;

namespace XamFormsPrototype.Tests.Helpers
{
    public class TestSource
    {
        public int IntProp { get; set; }
        public string StringProp { get; set; }
        public TestEnum EnumProp { get; set; }
        public bool BoolProp { get; set; }
    }

    [Table("TestTargets")]
    public class TestTarget
    {
        public int IntProp { get; set; }
        public string StringProp { get; set; }
        public TestEnum EnumProp { get; set; }
        public bool BoolProp { get; set; }
    }

    public enum TestEnum
    {
        Zero,
        One,
        Two
    }

    public class TestArtist
    {
        public string Name { get; set; }
        public List<TestAlbum> Albums { get; set; }
    }

    public class TestAlbum
    {
        public string Name { get; set; }
        public TestArtist Artist { get; set; }

    }

    public class TestArtistTarget
    {
        public string Name { get; set; }
        public List<TestAlbumTarget> Albums { get; set; }
    }

    public class TestAlbumTarget
    {
        public string Name { get; set; }
        public TestArtistTarget Artist { get; set; }

    }

    public class TestFileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), filename);
        }
    }
    public interface ITestClass
    {
        string GetName();
    }

    public interface ITestClass2
    {
        string GetName();
    }

    public class TestClass : ITestClass
    {
        public string GetName() =>
            "I am the implementation of ITestClass";
    }

    public class TestClass2 : ITestClass2
    {
        private string _name;

        public TestClass2(string name)
        {
            _name = name;
        }

        public string GetName() =>
            _name;

    }
}

﻿using SQLite;
using System;
using System.Reflection;
using XamFormsPrototype.Helpers;
using XamFormsPrototype.Model;
using XamFormsPrototype.Tests.Helpers;
using Xunit;

namespace XamFormsPrototype.Tests
{
    public class HelperTests : BaseTest
    {
        [Fact]
        public void Can_Map()
        {
            var source = Given_a_valid_source();
            var result = When(() => { return source.Map<TestSource, TestTarget>(); });
            Then_source_and_result_should_be_equal(source, result);
        }

        [Fact]
        public void Wont_Map_ChildClasses()
        {
            var album = Given_a_valid_testalbum();
            var result = When(() => { return album.Map<TestAlbum, TestAlbumTarget>(); });
            Then_child_classes_should_be_null(album, result);
        }

        [Fact]
        public void Can_Get_Classes_With_Attribute()
        {
            var result = When(() => {
                return Assembly.GetAssembly(typeof(Album)).GetClassesWithAttribute<TableAttribute>();
            });
            Then(() => Assert.Single(result));
        }

        [Fact]
        public void Can_Get_Properties_As_Formdata()
        {
            var target = Given_an_object_with_values();
            var result = When(() => target.ToFormDataString());
            Then(() => Assert.Equal("Id=1&Title=Test album&UserId=2", result));
        }

        [Fact]
        public void Can_Get_Properties_As_Formdata_With_Null_Values()
        {
            var target = Given_an_object_with_null_values();
            var result = When(() => target.ToFormDataString());
            Then(() => Assert.Equal("City=A city&Zipcode=123 45", result));
        }

        private Album Given_an_object_with_values() =>
            new Album
            {
                Id = 1,
                Title = "Test album",
                UserId = 2
            };
    
        private Address Given_an_object_with_null_values() =>
            new Address
            {
                City = "A city",
                Zipcode = "123 45"
            };


        private static void Then_source_and_result_should_be_equal(TestSource source, TestTarget result)
        {
            Assert.Equal(source.BoolProp, result.BoolProp);
            Assert.Equal(source.EnumProp, result.EnumProp);
            Assert.Equal(source.IntProp, result.IntProp);
            Assert.Equal(source.StringProp, result.StringProp);
        }

        private static TestSource Given_a_valid_source()
        {
            return new TestSource
            {
                BoolProp = true,
                EnumProp = TestEnum.Two,
                IntProp = 3,
                StringProp = "The string"
            };
        }

        private static void Then_child_classes_should_be_null(TestAlbum album, TestAlbumTarget result)
        {
            Assert.Null(result.Artist);
            Assert.Equal(album.Name, result.Name);
        }

        private static TestAlbum Given_a_valid_testalbum()
        {
            return new TestAlbum
            {
                Name = "The album",
                Artist = new TestArtist
                {
                    Name = "The Artist"
                }
            };
        }

    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XamFormsPrototype.Contracts;
using XamFormsPrototype.DependencyResolution;
using XamFormsPrototype.Model;
using XamFormsPrototype.Tests.Helpers;
using Xunit;

namespace XamFormsPrototype.Tests
{
    public class RepositoryTests : BaseTest
    {
        private IoC _container = new IoC();
        private IRepository _repository;

        [Fact]
        public void Can_Get_FilePath()
        {
            Given(() => {
                _container.Init(new TestRegistry());
                _repository = _container.Resolve<IRepository>();
            });

            var result = When(() => {
                return _container.Resolve<IFileHelper>().GetLocalFilePath("XamFormsPrototype.db3");
            });

            Then(() => {
                Assert.Equal(Path.Combine(Directory.GetCurrentDirectory(), "XamFormsPrototype.db3"), result);
            });
        }

        [Fact]
        public async Task Can_Create_Database()
        {
            var album = await Given_a_valid_Album();
            var result = await When(async () => { return await _repository.GetAll<Album>(); });
            Then_result_should_be_valid(result, album);
            CleanUp(new List<Album> { album });
        }

        [Fact]
        public async Task Can_Get_FirstOrDefault()
        {
            var album = await Given_a_valid_Album();
            var result = await When(async () => { return await _repository.FirstOrDefault<Album>(_ => _.Id == album.Id); });
            Then_should_return_an_Album(result);
            CleanUp(new List<Album> { album });
        }

        private void Then_should_return_an_Album(Album result)
        {
            Assert.NotNull(result);
            Assert.Equal("TestAlbum", result.Title);
        }

        private void Then_result_should_be_valid(List<Album> result, Album added)
        {
            Assert.Contains(result, _ => _.Id == added.Id);
            Assert.Equal(added.Title, result.First(_ => _.Id == added.Id).Title);
        }

        private async Task<Album> Given_a_valid_Album()
        {
            _container.Init(new TestRegistry());
            _repository = _container.Resolve<IRepository>();
            var album = await _repository.Add<Album> (new Album
            {
                Title = "TestAlbum"
            });
            return album;
        }

        private void CleanUp(List<Album> result)
        {
            result.ForEach(_ => _repository.Delete(_).ConfigureAwait(false));
        }
    }
}

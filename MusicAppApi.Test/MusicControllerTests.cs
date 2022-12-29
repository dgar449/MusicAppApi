using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicAppApi.ApiModels;
using MusicAppApi.AutoMapper;
using MusicAppApi.Controllers;
using MusicAppApi.Data.Models;
using MusicAppApi.Data.Models.Context;
using MusicAppApi.Data.ViewModels;

namespace MusicAppApi.Test
{
    public class MusicControllerTests
    {
        private readonly Mock<ISongRepo> songRepo;
        private readonly IMapper mapper;
        Fixture fixture = new();

        public MusicControllerTests()
        {
            songRepo= new Mock<ISongRepo>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MusicMappingProfile());
            });
            mapper = mockMapper.CreateMapper();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void GetAllGenre_Returns_Notnull()
        {
            //arrange
            var result = fixture.CreateMany<Genre>();
            songRepo.Setup(x=>x.GetAllGenre())
                .Returns(result);
            var songController = new MusicController(songRepo.Object,mapper);

            //act
            var actionResult = songController.GetAllGenre();

            //assert
            var songResult = actionResult as OkObjectResult;
            Assert.NotNull(songResult);

            var model = songResult.Value as IEnumerable<GenreAM>;
            Assert.NotNull(model);

            Assert.Equal(model.Count(), result.Count());

        }

        [Fact]
        public void GetAllSongs_Returns_Notnull()
        {
            //arrange
            var result = fixture.CreateMany<Song>();
            songRepo.Setup(x => x.GetAllSongs())
                .Returns(result);
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.GetAllSongs();

            //assert
            var songResult = actionResult as OkObjectResult;
            Assert.NotNull(songResult);

            var model = songResult.Value as IEnumerable<SongAM>;
            Assert.NotNull(model);

            Assert.Equal(model.Count(), result.Count());

        }

        [Fact]
        public void ArtistList_Returns_Notnull()
        {
            //arrange
            var result = fixture.CreateMany<Artist>();
            songRepo.Setup(x => x.GetArtists())
                .Returns(result);
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.ArtistList();

            //assert
            var songResult = actionResult as OkObjectResult;
            Assert.NotNull(songResult);

            var model = songResult.Value as IEnumerable<ArtistAM>;
            Assert.NotNull(model);

            Assert.Equal(model.Count(), result.Count());

        }

        [Fact]
        public void AlbumList_Returns_Notnull()
        {
            //arrange
            var result = fixture.CreateMany<Album>();
            songRepo.Setup(x => x.GetAlbum())
                .Returns(result);
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.AlbumList();

            //assert
            var songResult = actionResult as OkObjectResult;
            Assert.NotNull(songResult);

            var model = songResult.Value as IEnumerable<AlbumAM>;
            Assert.NotNull(model);

            Assert.Equal(model.Count(), result.Count());

        }

        [Fact]
        public void GetSongs_Returns_Notnull()
        {
            //arrange
            var result = fixture.Create<Song>();
            songRepo.Setup(x => x.GetSongs(result.SongID))
                .Returns(result);
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.GetSongs(result.SongID);

            //assert
            var songResult = actionResult as OkObjectResult;
            Assert.NotNull(songResult);

            var model = songResult.Value as SongAM;
            Assert.NotNull(model);

            Assert.Equal(model.SongID, result.SongID);

        }

        [Fact]
        public async void GetAllAlbums_Returns_Notnull()
        {
            //arrange
            var result = fixture.Create<AlbumAM>();
            var input = fixture.CreateMany<AlbumListVm>();
            var inputa = fixture.CreateMany<Album>();
            songRepo.Setup(x => x.AllAlbums(4))
                .ReturnsAsync(input);
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.AllAlbums(4);

            //assert
            var songResult = await actionResult as OkObjectResult;
            Assert.NotNull(songResult);

            var model = songResult.Value as AlbumAM;
            Assert.NotNull(model);

            Assert.Equal(result, model);

        }

        [Fact]
        public async Task GetSearch_Returns_Notnull()
        {
            //arrange
            var result = fixture.CreateMany<SearchSongVm>();
            songRepo.Setup(x => x.Search("k"))
                .ReturnsAsync(result);
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.Search("k");

            //assert
            var searchResult = await actionResult as OkObjectResult;
            Assert.NotNull(searchResult);

            var model = searchResult.Value as IEnumerable<SearchSongVm>;
            Assert.NotNull(model);

            Assert.Equal(result, model);

        }

        [Fact]
        public async void GetRankSongsTotal_Returns_Notnull()
        {
            //arrange
            var result = fixture.CreateMany<RankSongCountVm>();
            songRepo.Setup(x => x.RankSongsTotal())
                .ReturnsAsync(result);
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.RankSongsTotal();

            //assert
            var songResult = await actionResult as OkObjectResult;
            Assert.NotNull(songResult);

            var model = songResult.Value as IEnumerable<RankSongCountVm>;
            Assert.NotNull(model);

            Assert.Equal(result, model);

        }

        [Fact]
        public async void GetCareerAge_Returns_Notnull()
        {
            //arrange
            var result = fixture.CreateMany<MusicCareer>();
            songRepo.Setup(x => x.CareerAge())
                .ReturnsAsync(result);
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.CareerAge();

            //assert
            var careerResult = await actionResult as OkObjectResult;
            Assert.NotNull(careerResult);

            var model = careerResult.Value as IEnumerable<MusicCareer>;
            Assert.NotNull(model);

            Assert.Equal(result,model);

        }

        [Fact]
        public async void GetSongCreate_Returns_Notnull()
        {
            //arrange
            var result = fixture.Create<ServiceResult<Song>>();
            var inputAM = fixture.Create<SongAM>();
            songRepo.Setup(x => x.Create(It.IsAny<Song>()))
                .ReturnsAsync(result);
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            
            var actionResult = songController.CreateSong(inputAM);
            var expectedresult = result;

            //assert
            var songResult = await actionResult as OkObjectResult;

            Assert.NotNull(actionResult);

            var model = songResult.Value as ServiceResult<Song>;
            Assert.NotNull(model);

            Assert.Equal(result.IsSuccess,model.IsSuccess);
        }

        [Fact]
        public void GetAllGenre_Throws_Exception()
        {
            //arrange
            var result = fixture.CreateMany<Genre>();
            songRepo.Setup(x => x.GetAllGenre())
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.GetAllGenre();

            //assert
            var exceptionResult = actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }
        [Fact]
        public void GetAllSongs_Throws_Exception()
        {
            //arrange
            var result = fixture.CreateMany<Song>();
            songRepo.Setup(x => x.GetAllSongs())
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.GetAllSongs();

            //assert
            var exceptionResult = actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }

        [Fact]
        public void GetArtistList_Throws_Exception()
        {
            //arrange
            var result = fixture.Create<Artist>();
            songRepo.Setup(x => x.GetArtists())
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.ArtistList();

            //assert
            var exceptionResult = actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }

        [Fact]
        public void GetAlbumList_Throws_Exception()
        {
            //arrange
            var result = fixture.Create<Album>();
            songRepo.Setup(x => x.GetAlbum())
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.AlbumList();

            //assert
            var exceptionResult = actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }

        [Fact]
        public void GetSongs_Throws_Exception()
        {
            //arrange
            var result = fixture.Create<Song>();
            songRepo.Setup(x => x.GetSongs(24))
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.GetSongs(24);

            //assert
            var exceptionResult = actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }

        [Fact]
        public async void GetAllAlbums_Throws_Exception()
        {
            //arrange
            var result = fixture.CreateMany<AlbumListVm>();
            songRepo.Setup(x => x.AllAlbums(4))
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.AllAlbums(4);

            //assert
            var exceptionResult = await actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }
        [Fact]
        public async void GetSearch_Throws_Exception()
        {
            //arrange
            var result = fixture.CreateMany<SearchSongVm>();
            songRepo.Setup(x => x.Search("a"))
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.Search("a");

            //assert
            var exceptionResult = await actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }
        [Fact]
        public async void GetCareerAge_Throws_Exception()
        {
            //arrange
            var result = fixture.CreateMany<MusicCareer>();
            songRepo.Setup(x => x.CareerAge())
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.CareerAge();

            //assert
            var exceptionResult = await actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }
        [Fact]
        public async void GetCareerAverage_Throws_Exception()
        {
            //arrange
            var result = fixture.CreateMany<MusicCareer>();
            songRepo.Setup(x => x.CareerAverage())
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.CareerAverage();

            //assert
            var exceptionResult = await actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }
        [Fact]
        public async void GetCareerDuration_Throws_Exception()
        {
            //arrange
            var result = fixture.CreateMany<MusicCareer>();
            songRepo.Setup(x => x.CareerDuration())
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.CareerDuration();

            //assert
            var exceptionResult = await actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }
        [Fact]
        public async void GetGenrePopularity_Throws_Exception()
        {
            //arrange
            var result = fixture.CreateMany<GenrePopularityVm>();
            songRepo.Setup(x => x.GenrePopularity())
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.GenrePopularity();

            //assert
            var exceptionResult = await actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }
        [Fact]
        public async void GetGenreArtistPopularity_Throws_Exception()
        {
            //arrange
            var result = fixture.CreateMany<GenreArtistPopularityVm>();
            songRepo.Setup(x => x.GenreArtistPopularity())
                .Throws(new NullReferenceException());
            var songController = new MusicController(songRepo.Object, mapper);

            //act
            var actionResult = songController.GenreArtistPopularity();

            //assert
            var exceptionResult = await actionResult as ObjectResult;
            Assert.NotNull(exceptionResult);

            var model = exceptionResult.Value as ProblemDetails;
            Assert.NotNull(model);

            Assert.Equal("Object reference not set to an instance of an object.", model.Detail);

        }
    }
}

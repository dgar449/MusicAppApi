using Microsoft.AspNetCore.Mvc;
using MusicAppApi.Data.Models;
using AutoMapper;
using MusicAppApi.ApiModels;
using MusicAppApi.Data.ViewModels;
using MusicAppApi.Data.Models.Context;

namespace MusicAppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly ISongRepo _songRepo;
        private readonly IMapper _mapper;

        public MusicController(ISongRepo songRepo, IMapper mapper)
        {
            _songRepo = songRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllGenre()
        {
            try
            {
                var model = _songRepo.GetAllGenre();
                var response = _mapper.Map<IEnumerable<GenreAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
            
        }
        [HttpGet]
        public IActionResult GetAllSongs()
        {
            try
            {
                var model = _songRepo.GetAllSongs();
                var response = _mapper.Map<IEnumerable<SongAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        [HttpGet]
        public IActionResult ArtistList()
        {
            try
            {
                var model = _songRepo.GetArtists().ToList();
                var response = _mapper.Map<IEnumerable<ArtistAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        [HttpGet]
        public IActionResult AlbumList()
        {
            try
            {
                var model = _songRepo.GetAlbum().ToList();
                var response = _mapper.Map<IEnumerable<AlbumAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        [HttpGet]
        public IActionResult GetSongs(int id)
        {
            try
            {
                var model = _songRepo.GetSongs(id);
                var response = _mapper.Map<SongAM>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> AllAlbums(int sq)
        {
            try
            {
                var model = await _songRepo.AllAlbums(sq);
                var response = _mapper.Map<IEnumerable<AlbumAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Search(string sq)
        {
            try
            {
                var model = await _songRepo.Search(sq);
                var response = _mapper.Map<IEnumerable<SearchSongVm>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> RankSongsTotal()
        {
            try
            {
                var model = await _songRepo.RankSongsTotal();
                var response = _mapper.Map<IEnumerable<RankSongCountAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> CareerAge()
        {
            try
            {
                var model = await _songRepo.CareerAge();
                var response = _mapper.Map<IEnumerable<MusicCareerAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> CareerAverage()
        {
            try
            {
                var model = await _songRepo.CareerAverage();
                var response = _mapper.Map<IEnumerable<MusicCareerAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> CareerDuration()
        {
            try
            {
                var model = await _songRepo.CareerDuration();
                var response = _mapper.Map<IEnumerable<MusicCareerAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GenrePopularity()
        {
            try
            {
                var model = await _songRepo.GenrePopularity();
                var response = _mapper.Map<IEnumerable<GenrePopularityAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GenreArtistPopularity()
        {
            try
            {
                var model = await _songRepo.GenreArtistPopularity();
                var response = _mapper.Map<IEnumerable<GenreArtistPopularityAM>>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateSong(SongAM input)
        {
            try
            {
                var song = _mapper.Map<Song>(input);
                var result = await _songRepo.Update(song);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateSong(SongAM input)
        {
            try
            {
                var song = _mapper.Map<Song>(input);
                var result = await _songRepo.Create(song);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = _songRepo.Delete(id);
                var response = _mapper.Map<SongAM>(model);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}

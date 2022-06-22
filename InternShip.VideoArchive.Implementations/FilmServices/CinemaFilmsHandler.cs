using AutoMapper;
using InternShip.VideoArchive.Contracts.Abstractions;
using InternShip.VideoArchive.Contracts.Abstractions.FilmCatalogServices;
using InternShip.VideoArchive.Contracts.Abstractions.FilmServices;
using InternShip.VideoArchive.Contracts.Abstractions.Integrations;
using InternShip.VideoArchive.Contracts.Models.Cinema;

namespace InternShip.VideoArchive.Implementations.FilmServices
{
	/// <summary>
	/// Обработчик запросов по фильмам для кинотеатра
	/// </summary>
	public class CinemaFilmsHandler : ICinemaFilmsHandler
	{
		#region Конструкторы и DI

		private readonly IMapper _mapper;
		private readonly ISortFilters _sortFilters;
		private readonly IFilmsCatalogHandler _filmsCatalogHandler;
		private readonly IFakeInegrationService _fakeInegrationService;

		public CinemaFilmsHandler(
			ISortFilters sortFilters,
			IFilmsCatalogHandler filmsCatalogHandler,
			IMapper mapper,
			IFakeInegrationService fakeInegrationService)
		{
			_sortFilters = sortFilters;
			_filmsCatalogHandler = filmsCatalogHandler;
			_mapper = mapper;
			_fakeInegrationService = fakeInegrationService;
		}

		#endregion

		/// <summary>
		/// Предположим, что для кинотеатров берем только фильмы из нашего внутреннего каталога
		/// Получаем фильмы, удовлетворяющие нашим условиям
		/// </summary>
		public async Task<List<CinemaFilm>> GetCinemaFilms(CinemaFilmSortFlags flags)
		{
			var filters = new List<Func<CinemaFilm, bool>>();

			var films = _mapper.Map<List<CinemaFilm>>(await _filmsCatalogHandler.GetFilms());

			for (int i = 0; i < films.Count; i++)
			{
				films[i].AgeRestrictions = _fakeInegrationService.GetAgeRestrictions(films[i]);
			}

			films = films.Where(
				film => filters.All(
					filter => filter(film))).ToList();

			return films;
		}
	}
}
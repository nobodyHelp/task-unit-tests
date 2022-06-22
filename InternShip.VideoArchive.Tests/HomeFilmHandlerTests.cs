using AutoMapper;
using InternShip.VideoArchive.Contracts.Abstractions.Data;
using InternShip.VideoArchive.Contracts.Abstractions.Integrations;
using InternShip.VideoArchive.Contracts.Models.Home;
using InternShip.VideoArchive.Implementations.FilmServices;
using Moq;
using Xunit;

namespace InternShip.VideoArchive.Tests
{
	/// <summary>
	/// Тесты для <see cref="HomeFilmHandler">
	/// </summary>
	public class HomeFilmHandlerTests
	{
		private readonly Mock<IFakeDatabaseService> _fakeDatabaseService = new Mock<IFakeDatabaseService>();
		private readonly Mock<IFakeInegrationService> _fakeIntegrationService = new Mock<IFakeInegrationService>();
		private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

		private readonly HomeFilmHandler _homeFilmHandler;

		public HomeFilmHandlerTests()
		{
			_fakeDatabaseService
				.Setup(service => service
					.GetAllFilms())
				.ReturnsAsync(new List<HomeFilm>());

			_homeFilmHandler = new HomeFilmHandler(
				_fakeDatabaseService.Object,
				_fakeIntegrationService.Object,
				_mapper.Object);
		}

		/// <summary>
		/// Проверяем, что метод возвращает корректный результат
		/// </summary>
		[Fact]
		public void NoFilmsInDatabase_ReturnsCorrectResult()
		{
			Assert.NotNull(() => { _homeFilmHandler.IndicateFavouritesOfOurFilms(); });
		}

		/// <summary>
		/// Проверяем, что метод возвращает корректный результат
		/// </summary>
		[Fact]
		public async Task ChooseFilmToWatchWithChilds_ReturnsCorrectResult()
		{
			var allFilms = _homeFilmHandler.ChooseFilmToWatchWithChilds();
			
			Assert.NotNull(allFilms);
		}
	}
}
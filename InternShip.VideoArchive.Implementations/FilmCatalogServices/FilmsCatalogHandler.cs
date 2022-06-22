using InternShip.VideoArchive.Contracts.Abstractions.FilmCatalogServices;
using InternShip.VideoArchive.Contracts.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace InternShip.VideoArchive.Implementations.FilmCatalogServices
{
	/// <summary>
	/// Обработчик запроса на получение всех фильмов из каталога
	/// </summary>
	public class FilmsCatalogHandler : IFilmsCatalogHandler
	{
		private const string JsonFileName = "FilmCatalog.json";

		/// <summary>
		/// Метод получения массива фильмов из каталога
		/// </summary>
		public async Task<List<Film>> GetFilms()
		{
			var jsonFilePath = GetFilePackagePath();

			var result = JsonConvert.DeserializeObject<FilmCatalog>(await File.ReadAllTextAsync(jsonFilePath));

			return result.VideoOptions;
		}

		private string GetFilePackagePath()
		{
			string codeBase = Assembly.GetExecutingAssembly().Location;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Uri.UnescapeDataString(uri.Path);
			var directory = Path.GetDirectoryName(path);
			return Path.Combine(directory, JsonFileName);
		}
	}
}
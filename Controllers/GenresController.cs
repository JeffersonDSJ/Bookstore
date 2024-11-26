using BookStore.Models;

using BookStore.Services;

using BookStore.Data;

using Microsoft.AspNetCore.Mvc;

using BookStore.Models.ViewModels;

using System.Diagnostics;

namespace BookStore.Controllers

{

	public class GenresController : Controller

	{

		private readonly GenreService _service;

		public GenresController(GenreService service)

		{

			_service = service;

		}

		public async Task<IActionResult> Index()

		{

			return View(await _service.FindAllAsync());

		}

		public IActionResult Create()

		{

			return View();

		}

		[HttpPost]

		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(Genre genre)

		{

			if (!ModelState.IsValid)

			{

				return View();

			}

			await _service.InsertAsync(genre);

			return RedirectToAction(nameof(Index));

		}

		public async Task<IActionResult> Delete(int? id)

		{

			if (id is null)

			{

				return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });

			}

			var obj = await _service.FindByIdAsync(id.Value);

			if (obj is null)

			{

				return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });

			}

			return View(obj);

		}

		public async Task<IActionResult> Edit(int? id)

		{

			if (id is null)

			{

				return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });

			}

			var obj = await _service.FindByIdAsync(id.Value);

			if (obj is null)

			{

				return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });

			}

			return View(obj);

		}

		public ActionResult Error(string? message)

		{

			var viewModel = new ErrorViewModel

			{

				Message = message,

				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

			};

			return View(viewModel);

		}

	}



	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, Genre genre)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}

		if (id != genre.Id)
		{
			return RedirectToAction(nameof(Error), new { message = "Id's não condizentes" });
		}

		try
		{
			await _service.UpdateAsync(genre);
			return RedirectToAction(nameof(Index));
		}
		catch (ApplicationException ex)
		{
			return RedirectToAction(nameof(Error), new { message = ex.Message });
		}
	}

	public IActionResult Error(string message)
	{
		var viewModel = new ErrorViewModel
		{
			Message = message,
			RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
		};
		return View(viewModel);
	}

}


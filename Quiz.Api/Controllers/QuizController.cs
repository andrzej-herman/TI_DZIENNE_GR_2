using Microsoft.AspNetCore.Mvc;

namespace Quiz.Api.Controllers
{
	[ApiController]
	public class QuizController : ControllerBase
	{
		public QuizController()
		{

		}

		// pytanie wraz z odpowiedziami
		// url: https://localhost:7000/getquestion
		[HttpGet]
		[Route("getquestion")]
		public IActionResult GetQuestion([FromQuery] int category)
		{
			// QuestionDto
		}

		// sprawdzenie czy odpowiedü o danym id jest prawid≥owa
		// url: https://localhost:7000/checkanswer
		[HttpGet]
		[Route("checkanswer")]
		public IActionResult CheckAnswer([FromQuery] int answerId)
		{
			// bool
		}

	}
}

// ConnectionString do bazy u mnie na komputerze
// Server=.\HERMANLOCAL;Database=CqrsTp2;Integratred Security = True;

// Server=tcp:san-edu-pl.database.windows.net,1433;Initial Catalog=san;Persist Security Info=False;User ID=aherman;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

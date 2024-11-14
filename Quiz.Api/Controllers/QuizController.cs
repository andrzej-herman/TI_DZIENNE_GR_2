using Microsoft.AspNetCore.Mvc;
using Quiz.Api.Services;

namespace Quiz.Api.Controllers
{
	[ApiController]
	public class QuizController : ControllerBase
	{
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
		{
            _quizService = quizService;
        }

		// pytanie wraz z odpowiedziami
		// url: https://localhost:7000/getquestion
		[HttpGet]
		[Route("getquestion")]
		public IActionResult GetQuestion([FromQuery] int category)
		{
			var question = _quizService.GetQuestion(category);
			return Ok(question);
		}

		// sprawdzenie czy odpowiedü o danym id jest prawid≥owa
		// url: https://localhost:7000/checkanswer
		[HttpGet]
		[Route("checkanswer")]
		public IActionResult CheckAnswer([FromQuery] Guid answerId, [FromQuery] int category)
		{
            var result = _quizService.CheckAnswer(answerId, category);
            return Ok(result);
        }

	}
}

// ConnectionString do bazy u mnie na komputerze
// Server=.\HERMANLOCAL;Database=CqrsTp2;Integratred Security = True;

// Server=tcp:san-edu-pl.database.windows.net,1433;Initial Catalog=san;Persist Security Info=False;User ID=aherman;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

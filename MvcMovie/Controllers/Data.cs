using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class Data : Controller
    {
        public IActionResult Index( string number)
        {


            if (int.TryParse(number, out int result))
            {
                int answer = 0;

                for(int i=0; i <= result; i++)
                {
                    answer = answer +i ;
                }


                if(answer > 0) 
                {
                    return Ok(answer);
                }
                else
                {
                    return BadRequest("Out of Range");
                }
                



               
               

            }
            else
            {
                if (number == null)
                {
                    number = "Lack of Parameter";
                }
                else
                {
                    number = "Wrong Parameter";
                }

                return Ok(number);
            }

         
        }
    }
}

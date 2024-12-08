using AutomobileManagement.ViewModel;
using AutoMobileManagement.Model;
using AutoMobileManagement.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoMobileManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomobilesController : ControllerBase
    {
        private readonly IAutomobileRepository _repository;

        //DI - Dependency Injection
        public AutomobilesController(IAutomobileRepository repository)
        {
            _repository = repository;
        }

        #region 1 - Get all Orders - search all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutoPart>>> GetAllAutoParts()
        {
            var autoParts = await _repository.GetAllAutoParts();
            if (autoParts == null)
            {
                return NotFound("No AutoParts found");
            }
            return Ok(autoParts);
        }
        #endregion

        #region 2 - Get all from viewModel 
        [HttpGet("vm")]
        public async Task<ActionResult<IEnumerable<AutoMobileViewModel>>> GetAllAutoMobilesByViewModel()
        {
            var autoParts = await _repository.GetAutoMobileViewModel();
            if (autoParts == null)
            {
                return NotFound("No Records found");
            }
            return Ok(autoParts);
        }
        #endregion

        #region 3 - Get Autoparts - Search By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<AutoPart>> GetAutoPartById(int id)
        {
            var part = await _repository.GetAutoPartById(id);
            if (part == null)
            {
                return NotFound("No AutoParts found");
            }
            return Ok(part);
        }
        #endregion

        #region   4  - Insert an order -return order record
        public async Task<ActionResult<AutoPart>> InsertAutoPartReturnRecord(AutoPart autoPart)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee
                var newParts = await _repository.PostTblAutoPartReturnRecord(autoPart);
                if (newParts != null)
                {
                    return Ok(newParts);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region    5 - Insert an order -return Id
        [HttpPost("v1")]
        public async Task<ActionResult<int>> InsertAutoPartsReturnId(AutoPart autoPart)
        {
            if (ModelState.IsValid)
            {
                var newPartId = await _repository.PostTblAutoPartReturnId(autoPart);
                if (newPartId != null)
                {
                    return Ok(newPartId);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region    6  - Update an autopart with ID
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateAutoPartReturnRecord(int id, AutoPart autoParts)
        {
            if (ModelState.IsValid)
            {
                var updateAutoPartTbl = await _repository.PutTblAutoPart(id, autoParts);
                if (updateAutoPartTbl != null)
                {
                    return Ok(updateAutoPartTbl);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region  7  - Delete an Auto part order
        [HttpDelete("{id}")]
        public IActionResult DeleteAutoPart(int id)
        {
            try
            {
                var result = _repository.DeleteTblAutoPart(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "AutoPart could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion

        #region   8  - Get all CarModels
        [HttpGet("cm")]
        public async Task<ActionResult<IEnumerable<CarModel>>> GetAllCarModels()
        {
            var cms = await _repository.GetTblCarModels();
            if (cms == null)
            {
                return NotFound("No CarModels found");
            }
            return Ok(cms);
        }
        #endregion

        #region   9 - Get all Categories 
        [HttpGet("cat")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllTblCategories()
        {
            var cat = await _repository.GetAllCategories();
            if (cat == null)
            {
                return NotFound("No Categories found");
            }
            return Ok(cat);
        }
        #endregion

        #region   10 - Get all CompatibleCarModel
        [HttpGet("ccm")]
        public async Task<ActionResult<IEnumerable<CompatibleCarModel>>> GetAllTblCompatibleCarModels()
        {
            var ccm = await _repository.GetAllCompatibleCarModels();
            if (ccm == null)
            {
                return NotFound("No CompatibleCarModels found");
            }
            return Ok(ccm);
        }
        #endregion

        #region   11 - Get all ManufacturingCompany 
        [HttpGet("mc")]
        public async Task<ActionResult<IEnumerable<ManufacturingCompany>>> GetAllTblManufacturingCompanies()
        {
            var mc = await _repository.GetAllManufacturingCompanies();
            if (mc == null)
            {
                return NotFound("No ManufacturingCompanies found");
            }
            return Ok(mc);
        }
        #endregion

        
    }
}

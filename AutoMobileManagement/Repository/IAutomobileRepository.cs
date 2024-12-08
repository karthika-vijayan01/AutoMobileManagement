using AutomobileManagement.ViewModel;
using AutoMobileManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace AutoMobileManagement.Repository
{
    public interface IAutomobileRepository
    {
        #region   1  - Get all autoparts from DB - Search All
        //Get all employees from DB - Search All
        public Task<ActionResult<IEnumerable<AutoPart>>> GetAllAutoParts();
        #endregion

        #region  2  - Get all Automobileviewmodel 
        public Task<ActionResult<IEnumerable<AutoMobileViewModel>>> GetAutoMobileViewModel();
        #endregion

        #region 3 - Get an autopart based on Id
         public Task<ActionResult<AutoPart>> GetAutoPartById(int id);
        #endregion

        #region  4  - Insert an autopart -return autopart record
         public Task<ActionResult<AutoPart>> PostTblAutoPartReturnRecord(AutoPart autoPart);
        #endregion

        #region    5 - Insert an autopart - return Id
        public Task<ActionResult<int>> PostTblAutoPartReturnId(AutoPart orderTable);
        #endregion

        #region  6  - Update an autopart with ID 
         public Task<ActionResult<AutoPart>> PutTblAutoPart(int id, AutoPart autoPart);
        #endregion

        #region 7  - Delete an autopart
         public JsonResult DeleteTblAutoPart(int id); //return type > JsonResult -> true/false
        #endregion

        #region   8  - Get all CarModels
         public Task<ActionResult<IEnumerable<CarModel>>> GetTblCarModels();
        #endregion

        #region  9  - Get all Categories
         public Task<ActionResult<IEnumerable<Category>>> GetAllCategories();
        #endregion

        #region  10  - Get all CompatibleCarModel
        public Task<ActionResult<IEnumerable<CompatibleCarModel>>> GetAllCompatibleCarModels();
        #endregion

        #region  11  - Get all ManufacturingCompany
        public Task<ActionResult<IEnumerable<ManufacturingCompany>>> GetAllManufacturingCompanies();
       // Task PutTblAutoPart(int id, AutoPart autoParts);
        #endregion

    }
}

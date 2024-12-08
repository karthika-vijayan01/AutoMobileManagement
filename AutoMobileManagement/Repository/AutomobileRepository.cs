using AutomobileManagement.ViewModel;
using AutoMobileManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMobileManagement.Repository
{
    public class AutomobileRepository : IAutomobileRepository
    {
        private readonly CarPartsMngtContext _context;

        public AutomobileRepository(CarPartsMngtContext context)
        {
            _context = context;
        }

        #region   1  - Get all autoparts from DB - Search All
        public async Task<ActionResult<IEnumerable<AutoPart>>> GetAllAutoParts()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.AutoParts.Include(part => part.Category).Include(part => part.Company)
                        .ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<AutoPart>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region    2  - Get all Automobileviewmodel 
        public async Task<ActionResult<IEnumerable<AutoMobileViewModel>>> GetAutoMobileViewModel()
        {
            //LINQ
            try
            {
                if (_context != null)
                {
                    //LINQ
                    return await(from ap in _context.AutoParts
                                 join c in _context.Categories on ap.CategoryId equals c.CategoryId
                                 join mc in _context.ManufacturingCompanies on ap.CompanyId equals mc.CompanyId
                                 join ccm in _context.CompatibleCarModels on ap.PartId equals ccm.PartId
                                 join cm in _context.CarModels on ccm.CarModelId equals cm.CarModelId
                                 select new AutoMobileViewModel
                                 {
                                     Code = ap.Code,
                                     Name = ap.Name,
                                    //CategoryId = ap.CategoryId,
                                    // CompanyId = ap.CompanyId,
                                     Country = mc.Country,
                                     SalePrice = ap.SalePrice,
                                     Brand = cm.Brand,
                                     Model = cm.Model,
                                     Year = cm.Year
                                 }).ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<AutoMobileViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   3 - Get an autopart based on Id
        public async Task<ActionResult<AutoPart>> GetAutoPartById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var autopart = await _context.AutoParts.Include(part => part.Category).Include(part => part.Company)
                        .FirstOrDefaultAsync(ord => ord.PartId == id);
                    return autopart;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  4  - Insert an autopart -return autopart record 
        public async Task<ActionResult<AutoPart>> PostTblAutoPartReturnRecord(AutoPart autoPart)
        {
            try
            {
                if (autoPart == null)
                {
                    throw new ArgumentException(nameof(autoPart), "Order data is null");
                    //return null;
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                await _context.AutoParts.AddAsync(autoPart);
                await _context.SaveChangesAsync();
                var autoMobileParts = await _context.AutoParts.Include(part => part.Category).Include(part => part.Company)
                       .FirstOrDefaultAsync(ord => ord.PartId == autoPart.PartId);

                return autoMobileParts;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   5 - Insert an autopart - return Id
        public async Task<ActionResult<int>> PostTblAutoPartReturnId(AutoPart autoParts)
        {
            try
            {
                if (autoParts == null)
                {
                    throw new ArgumentException(nameof(autoParts), "AutoPart data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                await _context.AutoParts.AddAsync(autoParts);
                var changesRecord = await _context.SaveChangesAsync();
                if (changesRecord > 0)
                {
                    return autoParts.PartId;
                }
                else
                {
                    throw new Exception("Failed to save AutoPart record to the database");
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   6  - Update an autopart with ID 
        public async Task<ActionResult<AutoPart>> PutTblAutoPart(int id, AutoPart autoPart)
        {
            try
            {
                if (autoPart == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                var existingPart = await _context.AutoParts.FindAsync(id);
                if (existingPart == null)
                {
                    return null;
                }

                //Map values wit fields
                existingPart.Name = autoPart.Name;
                existingPart.CategoryId = autoPart.CategoryId;
                existingPart.PurchasePrice = autoPart.PurchasePrice;
                existingPart.SalePrice = autoPart.SalePrice;
                existingPart.CompanyId = autoPart.CompanyId;

                //save changes to the database
                await _context.SaveChangesAsync();

                var autoMobileParts = await _context.AutoParts.Include(part => part.Category).Include(part => part.Company)
                       .FirstOrDefaultAsync(ord => ord.PartId == autoPart.PartId);

                return autoMobileParts;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   7  - Delete an autopart
        public JsonResult DeleteTblAutoPart(int id)
        {
            try
            {
                if (id <= null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid PartId"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                //Ensure the context is not null
                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized"
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                //Find the employee by id
                var existingAutoPart = _context.AutoParts.Find(id);

                if (existingAutoPart == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "AutoPart not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //Remove the employee record from the DBContext
                _context.AutoParts.Remove(existingAutoPart);

                //save changes to the database
                _context.SaveChangesAsync();
                return new JsonResult(new
                {
                    success = true,
                    message = "AutoPart Deleted successfully"
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database context is not initialized"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion

        #region    8  - Get all CarModels
        public async Task<ActionResult<IEnumerable<CarModel>>> GetTblCarModels()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.CarModels.ToListAsync();
                }
                
                return new List<CarModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   9  - Get all Categories
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Categories.ToListAsync();
                }
                return new List<Category>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   10  - Get all CompatibleCarModel
        public async Task<ActionResult<IEnumerable<CompatibleCarModel>>> GetAllCompatibleCarModels()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.CompatibleCarModels.Include(model => model.CarModel).Include(model => model.Part).ToListAsync();
                }
                return new List<CompatibleCarModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   11  - Get all ManufacturingCompany
        public async Task<ActionResult<IEnumerable<ManufacturingCompany>>> GetAllManufacturingCompanies()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.ManufacturingCompanies.ToListAsync();
                }
                return new List<ManufacturingCompany>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}

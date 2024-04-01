using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FujitsuWebApp.Models;
using System.Drawing;
using System.Reflection.Metadata;
using FujitsuWebApp.Helper;
using NPOI.OpenXmlFormats;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace FujitsuWebApp.Controllers
{
    public class TbMSuppliersController : Controller
    {
        private readonly DbFujitsuContext _context;
        private readonly IExcelHelperService<TbMSupplier> _excelHelperService;

        public TbMSuppliersController(DbFujitsuContext context, IExcelHelperService<TbMSupplier> excelHelperService)
        {
            _context = context;
            _excelHelperService = excelHelperService;
        }

        // GET: TbMSuppliers
        public async Task<IActionResult> Index()
        {
            var data = _context.TbMCity.AsNoTracking().Select(x => new DropDownViewModel { Text = x.Province, Value = x.Province }).Distinct();
            ViewBag.SelectedListProvince = new SelectList(data, "Text", "Value");

            return View(await _context.TbMSuppliers.ToListAsync());
        }

        // GET: TbMSuppliers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMSupplier = await _context.TbMSuppliers
                .FirstOrDefaultAsync(m => m.SupplierCode == id);
            if (tbMSupplier == null)
            {
                return NotFound();
            }

            return View(tbMSupplier);
        }

        // GET: TbMSuppliers/Create
        public IActionResult Create()
        {
            var data = _context.TbMCity.AsNoTracking().Select(x => new DropDownViewModel { Text = x.Province, Value = x.Province }).Distinct();
            ViewBag.SelectedListProvince = new SelectList(data, "Text", "Value");
            return View();
        }
        [HttpGet]
        public IActionResult City(string Province)
        {
            var data = _context.TbMCity.AsNoTracking().Where(x => x.Province == Province);
            var distinctData = data.Select(x => new DropDownViewModel { Text = x.City, Value = x.City }).Distinct();
            var SelectedListCity = new SelectList(distinctData, "Text", "Value");
            return new JsonResult(SelectedListCity);
        }

        // POST: TbMSuppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierCode,SupplierName,Address,Province,City,Pic")] TbMSupplier tbMSupplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(tbMSupplier);
                    await _context.SaveChangesAsync();
                    return new JsonResult(tbMSupplier);
                }
                catch (DbUpdateException ex)
                {
                    var dataProvince = _context.TbMCity.AsNoTracking().Select(x => new DropDownViewModel { Text = x.Province, Value = x.Province }).Distinct();
                    var dataCity = _context.TbMCity.AsNoTracking().Where(y=>y.Province == tbMSupplier.Province).Select(x => new DropDownViewModel { Text = x.City, Value = x.City }).Distinct();
                    ViewBag.SelectedListProvince = new SelectList(dataProvince, "Text", "Value");
                    ViewBag.SelectedListCity = new SelectList(dataCity, "Text", "Value");

                    var ex2 = ex.InnerException;
                    if (ex2.ToString().Contains("duplicate"))
                        return BadRequest("Duplicate Supplier Code");
                }
            }
            return BadRequest("Model Not Valid");
        }

        // GET: TbMSuppliers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var tbMSupplier = await _context.TbMSuppliers.FindAsync(id);


            if (tbMSupplier == null)
            {
                return NotFound();
            }

            var dataProvince = _context.TbMCity.AsNoTracking().Select(x => new DropDownViewModel { Text = x.Province, Value = x.Province }).Distinct();
            var dataCity = _context.TbMCity.AsNoTracking().Select(x => new DropDownViewModel { Text = x.City, Value = x.City }).Distinct();
            ViewBag.SelectedListProvince = new SelectList(dataProvince, "Text", "Value");
            ViewBag.SelectedListCity = new SelectList(dataCity, "Text", "Value");

            return View(tbMSupplier);
        }

        // POST: TbMSuppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string supplierCode, [Bind("SupplierCode,SupplierName,Address,Province,City,Pic")] TbMSupplier tbMSupplier)
        {
            if (supplierCode != tbMSupplier.SupplierCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbMSupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbMSupplierExists(tbMSupplier.SupplierCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return new JsonResult(tbMSupplier);
            }
            return BadRequest("Model Not Valid");
        }

        // POST: TbMSuppliers/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var tbMSupplier = await _context.TbMSuppliers.FindAsync(id);
            if (tbMSupplier != null)
            {
                _context.TbMSuppliers.Remove(tbMSupplier);
            }

            await _context.SaveChangesAsync();
            return new JsonResult(true);
        }

        private bool TbMSupplierExists(string id)
        {
            return _context.TbMSuppliers.Any(e => e.SupplierCode == id);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var updatedData = new List<TbMSupplier>();
            var createdData = new List<TbMSupplier>();

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var fileContent = memoryStream.ToArray();
                var fileExtension = file.FileName.Split('.').Last();

                if ((fileExtension.Equals("xlsx", StringComparison.InvariantCultureIgnoreCase)
                    || fileExtension.Equals("xls", StringComparison.InvariantCultureIgnoreCase)) && fileContent.Length > 0)
                {
                    var data = _excelHelperService.ReadFile(fileContent, fileExtension, false);
                    
                    foreach (var item in data.ToList())
                    {
                        var dataExist = _context.TbMSuppliers.Where(x => x.SupplierCode.ToUpper() == item.SupplierCode.ToUpper()).Any();

                        if (dataExist)
                        {
                            updatedData.Add(item);
                        }
                        else
                        {
                            createdData.Add(item);
                        }
                    }
                }
            }

            _context.TbMSuppliers.UpdateRange(updatedData);
            await _context.TbMSuppliers.AddRangeAsync(createdData);
            await _context.SaveChangesAsync();

            return Ok("File uploaded successfully.");
        }

        public async Task<IActionResult> Download()
        {
            string excelName = $"TbMSupplier-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            var _excelGenerator = new SpreadsheetGenerator<TbMSupplier>(new SpreadsheetConfig()
            {
                FileName = excelName,
                SheetName = "TbMSupplier"
            });

            var excelData = await _context.TbMSuppliers.AsNoTracking().ToListAsync();
            var excelByte = _excelGenerator.Generate(excelData);

            return File(excelByte, _excelGenerator.Config.ContentType, _excelGenerator.Config.FileName);
        }

        public async Task<IActionResult> Search(SearchParam param)
        {
            var data = _context.TbMSuppliers.AsNoTracking();

            if (!string.IsNullOrEmpty(param.SupplierCode))
                data = data.Where(x => x.SupplierCode.ToUpper().StartsWith(param.SupplierCode.ToUpper()));

            if (!string.IsNullOrEmpty(param.Province))
                data = data.Where(x => x.Province == param.Province);

            if (!string.IsNullOrEmpty(param.City))
                data = data.Where(x => x.City == param.City);

            return new JsonResult(data);
        }
    }
}

using DigitalAssetManagementSystem.dao;
using DigitalAssetManagementSystem.exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DigitalAssetManagementSystem.Controllers
{
    [Authorize]
    public class AllocationsController : Controller
    {
        private readonly IAssetManagementService _svc;
        public AllocationsController(IAssetManagementService svc) => _svc = svc;

        public async Task<IActionResult> Index()
        {
            var list = await _svc.GetAllocationsAsync();
            return View(list);
        }

        public async Task<IActionResult> Allocate()
        {
            ViewBag.Assets = new SelectList(await _svc.GetAssetsAsync(), "AssetId", "Name");
            ViewBag.Employees = new SelectList(await _svc.GetEmployeesAsync(), "EmployeeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Allocate(int assetId, int employeeId, DateTime allocationDate)
        {
            try
            {
                await _svc.AllocateAssetAsync(assetId, employeeId, allocationDate);
                TempData["Msg"] = "Asset allocated.";
                return RedirectToAction(nameof(Index));
            }
            catch (AssetNotFoundException ex) { TempData["Err"] = ex.Message; }
            catch (AssetNotMaintainException ex) { TempData["Err"] = ex.Message; }
            catch (Exception ex) { TempData["Err"] = ex.Message; }

            ViewBag.Assets = new SelectList(await _svc.GetAssetsAsync(), "AssetId", "Name", assetId);
            ViewBag.Employees = new SelectList(await _svc.GetEmployeesAsync(), "EmployeeId", "Name", employeeId);
            return View();
        }

        public async Task<IActionResult> Deallocate()
        {
            ViewBag.Assets = new SelectList(await _svc.GetAssetsAsync(), "AssetId", "Name");
            ViewBag.Employees = new SelectList(await _svc.GetEmployeesAsync(), "EmployeeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deallocate(int assetId, int employeeId, DateTime returnDate)
        {
            try
            {
                await _svc.DeallocateAssetAsync(assetId, employeeId, returnDate);
                TempData["Msg"] = "Asset deallocated.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Err"] = ex.Message;
                ViewBag.Assets = new SelectList(await _svc.GetAssetsAsync(), "AssetId", "Name", assetId);
                ViewBag.Employees = new SelectList(await _svc.GetEmployeesAsync(), "EmployeeId", "Name", employeeId);
                return View();
            }
        }
    }
}
using DigitalAssetManagementSystem.dao;
using DigitalAssetManagementSystem.entity;
using DigitalAssetManagementSystem.exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DigitalAssetManagementSystem.Controllers
{
    [Authorize]
    public class AssetsController : Controller
    {
        private readonly IAssetManagementService _svc;

        public AssetsController(IAssetManagementService svc)
        {
            _svc = svc;
        }

        // GET: /Assets
        public async Task<IActionResult> Index()
        {
            var assets = await _svc.GetAssetsAsync();
            return View(assets);
        }

        // GET: /Assets/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var asset = await _svc.GetAssetByIdAsync(id);
            if (asset == null)
                return NotFound();

            // Maintenance history for Details page
            ViewBag.Maintenance = await _svc.GetMaintenanceAsync(id);

            return View(asset);
        }

        // GET: /Assets/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.OwnerId = new SelectList(
                await _svc.GetEmployeesAsync(),
                "EmployeeId",
                "Name"
            );

            return View();
        }

        // POST: /Assets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Asset asset)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.OwnerId = new SelectList(
                    await _svc.GetEmployeesAsync(),
                    "EmployeeId",
                    "Name",
                    asset.OwnerId
                );
                return View(asset);
            }

            await _svc.AddAssetAsync(asset);
            TempData["Msg"] = "Asset added successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Assets/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var asset = await _svc.GetAssetByIdAsync(id);
            if (asset == null)
                return NotFound();

            ViewBag.OwnerId = new SelectList(
                await _svc.GetEmployeesAsync(),
                "EmployeeId",
                "Name",
                asset.OwnerId
            );

            return View(asset);
        }

        // POST: /Assets/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Asset asset)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.OwnerId = new SelectList(
                    await _svc.GetEmployeesAsync(),
                    "EmployeeId",
                    "Name",
                    asset.OwnerId
                );
                return View(asset);
            }

            try
            {
                await _svc.UpdateAssetAsync(asset);
                TempData["Msg"] = "Asset updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (AssetNotFoundException ex)
            {
                TempData["Err"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Err"] = ex.Message;

                ViewBag.OwnerId = new SelectList(
                    await _svc.GetEmployeesAsync(),
                    "EmployeeId",
                    "Name",
                    asset.OwnerId
                );

                return View(asset);
            }
        }

        // GET: /Assets/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var asset = await _svc.GetAssetByIdAsync(id);
            if (asset == null)
                return NotFound();

            return View(asset);
        }

        // POST: /Assets/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int assetId)
        {
            try
            {
                await _svc.DeleteAssetAsync(assetId);
                TempData["Msg"] = "Asset deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (AssetNotFoundException ex)
            {
                TempData["Err"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Err"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
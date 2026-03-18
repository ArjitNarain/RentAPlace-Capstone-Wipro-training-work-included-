using DigitalAssetManagementSystem.dao;
using DigitalAssetManagementSystem.entity;
using DigitalAssetManagementSystem.exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalAssetManagementSystem.Controllers
{
    [Authorize]
    public class MaintenanceController : Controller
    {
        private readonly IAssetManagementService _svc;

        public MaintenanceController(IAssetManagementService svc)
        {
            _svc = svc;
        }

        // GET: /Maintenance/Create?assetId=1
        [HttpGet]
        public async Task<IActionResult> Create(int assetId)
        {
            var asset = await _svc.GetAssetByIdAsync(assetId);
            if (asset == null)
                return NotFound();

            // ✅ Pass asset directly to view (Strong typed)
            return View(asset);
        }

        // POST: /Maintenance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int assetId, DateTime maintenanceDate, string description, double cost)
        {
            try
            {
                await _svc.PerformMaintenanceAsync(assetId, maintenanceDate, description, cost);
                TempData["Msg"] = "Maintenance recorded successfully.";
                return RedirectToAction("Details", "Assets", new { id = assetId });
            }
            catch (AssetNotFoundException ex)
            {
                TempData["Err"] = ex.Message;
                return RedirectToAction("Index", "Assets");
            }
            catch (Exception ex)
            {
                TempData["Err"] = ex.Message;

                // reload asset page with proper data
                var asset = await _svc.GetAssetByIdAsync(assetId);
                if (asset == null) return RedirectToAction("Index", "Assets");

                return View(asset);
            }
        }
    }
}